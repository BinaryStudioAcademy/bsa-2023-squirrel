namespace Squirrel.ConsoleApp.BL.SqlScripts.Postgres;

internal static class GetTables
{
    public static string GetTablesNamesScript =>
        @"SELECT schemaname AS ""schema"", tablename AS ""name"" FROM pg_catalog.pg_tables WHERE schemaname NOT IN ('pg_catalog', 'information_schema')";

    public static string GetTableDataQueryScript(string schema, string name, int rowsCount) =>
        $"SELECT '{schema}' AS schema, '{name}' AS name, (SELECT COUNT(*) FROM \"{schema}\".\"{name}\") AS TotalRows, t.* FROM \"{schema}\".\"{name}\" t LIMIT {rowsCount}";

    public static string GetTableStructureScript(string schema, string name) =>
        @$"
            with column_description_table as (
				select
					cols.table_schema,
					cols.table_name,
					cols.column_name,
					pg_catalog.col_description(c.oid, cols.ordinal_position::int) as col_description
				from pg_catalog.pg_class c, information_schema.columns cols
				where
					 cols.table_name = c.relname
					and c.relkind = 'r'
			),
			
			key_constraint_table as (
				select kcu.table_schema,
				   kcu.table_name,
				   kcu.column_name,
				   array_agg(tco.constraint_type) as constraints_type,
				   array_agg(distinct kcu.constraint_name) as constraints_name
				from information_schema.table_constraints tco
				left join information_schema.key_column_usage kcu 
						  on kcu.constraint_name = tco.constraint_name
						  and kcu.constraint_schema = tco.constraint_schema
						  and kcu.constraint_name = tco.constraint_name
				where tco.constraint_type = 'FOREIGN KEY' 
					  or tco.constraint_type = 'PRIMARY KEY' 
				group by kcu.table_schema,
					 	 kcu.table_name,
					 	 kcu.column_name
				order by kcu.table_schema,
						 kcu.table_name
			)
			
			select distinct
				col.table_schema as Schema,
			    col.table_name as Name,
			    col.column_name as ColumnName,
				col.ordinal_position as ColumnOrder,
				
				case when col.domain_name is not NULL then col.domain_name
					 when col.data_type = 'USER-DEFINED' then col.udt_name 
					 else col.data_type end as DataType,
				
				case when col.data_type = 'USER-DEFINED' or col.domain_name is not NULL then 'True' else 'False' end as IsUserDefined,		 
				
			    col.character_maximum_length as MaxLength,
				-- MaxLength (do we need it?)
				col.numeric_precision as Precision,
				col.numeric_scale as Scale,
			    case when col.is_nullable = 'YES' then 'True' else 'False' end as AllowNulls,
				case when col.is_identity = 'YES' then 'True' else 'False' end as Identity,
				case when 'PRIMARY KEY' = any(kct.constraints_type) then 'True' else 'False' end as PrimaryKey,
				case when 'FOREIGN KEY' = any(kct.constraints_type) then 'True' else 'False' end as ForeignKey,
				ccu.column_name as RelatedTableColumn,
				pk_tc.table_name as RelatedTable,
				pk_tc.table_schema as RelatedTableSchema,
			    col.column_default as Default,
				cdt.col_description as Description
				
			from information_schema.columns as col
			
			join information_schema.tables as t
				 on col.table_schema = t.table_schema
				 and col.table_name = t.table_name
				 and t.table_type = 'BASE TABLE'
			
			left join column_description_table as cdt
				 on col.table_schema = cdt.table_schema
				 and col.table_name = cdt.table_name
				 and col.column_name = cdt.column_name
			
			left join key_constraint_table as kct
				 on col.table_schema = kct.table_schema
				 and col.table_name = kct.table_name
				 and col.column_name = kct.column_name
				 
			left join information_schema.referential_constraints as refc
				 on refc.constraint_schema = kct.table_schema
				 and refc.constraint_name = any(kct.constraints_name)
				 
			left join information_schema.table_constraints as pk_tc
				 on refc.unique_constraint_schema = pk_tc.table_schema
				 and refc.unique_constraint_name = pk_tc.constraint_name
				 
			left join information_schema.constraint_column_usage as ccu
				 on refc.unique_constraint_schema = ccu.table_schema
				 and pk_tc.table_name = ccu.table_name
				 and refc.unique_constraint_name = ccu.constraint_name
			
			where col.table_schema not in ('information_schema', 'pg_catalog') 
				  AND col.table_schema = '{schema}' AND col.table_name = '{name}'
			
			order by col.table_schema, col.table_name, col.ordinal_position;
            ";

    public static string GetTableChecksAndUniqueConstraintsScript(string schema, string name) =>
        @$"
            select 
			    tc.table_schema as Schema,
				   tc.table_name as Name,
				   tc.constraint_name as ConstraintName,
				   string_agg(col.column_name, ', ') as Columns,
				   case when pgc.contype = 'c' then 'CHECK'
				   		when pgc.contype = 'u' then 'UNIQUE'
				   end as constraint_type,
				   cc.check_clause as CheckClause
			
			from information_schema.table_constraints as tc
			
			left join pg_namespace as nsp on nsp.nspname = tc.table_schema
			
			left join pg_constraint as pgc on pgc.conname = tc.constraint_name
										   and pgc.connamespace = nsp.oid
										   and (pgc.contype = 'c' or pgc.contype = 'u')
			
			join information_schema.columns as col
				 on col.table_schema = tc.table_schema
				 and col.table_name = tc.table_name
				 and col.ordinal_position = ANY(pgc.conkey)
			
			left join information_schema.check_constraints as cc
				 on tc.table_schema = cc.constraint_schema
				 and tc.constraint_name = cc.constraint_name
			
			where tc.constraint_schema not in ('information_schema', 'pg_catalog') AND tc.constraint_schema = '{schema}' AND tc.table_name = '{name}'
			
			group by tc.table_schema,
					 tc.table_name,
					 tc.constraint_name,
					 pgc.contype,
					 cc.check_clause
			order by tc.table_schema,
			tc.table_name
            ";
}