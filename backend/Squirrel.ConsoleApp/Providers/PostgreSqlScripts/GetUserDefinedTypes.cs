namespace Squirrel.ConsoleApp.Providers.PostgreSqlScripts
{
    internal static class GetUserDefinedTypes
    {
        public static string GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionScript =>
            @"
            select
			    t.typnamespace::regnamespace::text as schema,
				 t.typname as name,
				d.data_type as base_type,
				d.character_maximum_length as max_length,
				d.numeric_precision as numeric_precision,
				d.numeric_scale as numeric_scale,
				t.typnotnull as allow_null,
				d.domain_default as default,
				dc.constraint_name as ""ConstraintName"",
				cc.check_clause as ""ConstraintDefinition""
			
			from
				pg_type as t
			    
			left join information_schema.domains as d
					  on t.typnamespace::regnamespace::text = d.domain_schema
					  and t.typname = d.domain_name
					  
			left join information_schema.domain_constraints as dc
					  on dc.constraint_schema = d.domain_schema
					  and dc.domain_name = d.domain_name
			
			left join information_schema.check_constraints as cc
					  on d.domain_schema = cc.constraint_schema
					  and dc.constraint_name = cc.constraint_name
					  
			where
			    d.domain_schema NOT IN ('pg_catalog', 'information_schema')  -- or choose specific table_scheme
				
			order by schema, name
            ";

        public static string GetUserDefinedTableTypesStructureScript =>
            @"
            with types as (
            select n.nspname,
        			t.oid::regtype::text as obj_name,
                    case
                        when t.typrelid != 0 then cast ( 'tuple' as pg_catalog.text )
                        when t.typlen < 0 then cast ( 'var' as pg_catalog.text )
                        	else cast ( t.typlen as pg_catalog.text )
                        	end as obj_type
                    
                from pg_catalog.pg_type t
                join pg_catalog.pg_namespace n 
        			 on n.oid = t.typnamespace
        	
                where ( t.typrelid = 0
                        OR ( select c.relkind = 'c'
                                from pg_catalog.pg_class c
                                where c.oid = t.typrelid ) )
                    and not exists (
                            select 1
                                from pg_catalog.pg_type el
                                where el.oid = t.typelem
                                and el.typarray = t.oid )
                    and n.nspname not in ('pg_catalog', 'information_schema')  -- or choose specific table_scheme
            ),
        
            cols as (
            select n.nspname::text as schema_name,
                    pg_catalog.format_type ( t.oid, NULL ) as obj_name,
        			t.typname as name,
                    a.attname::text as column_name,
                    pg_catalog.format_type ( a.atttypid, a.atttypmod ) as data_type,
                    a.attnotnull as is_required,
                    a.attnum as ordinal_position,
                    pg_catalog.col_description ( a.attrelid, a.attnum ) as description
        	
                from pg_catalog.pg_attribute a
                join pg_catalog.pg_type t
                    on a.attrelid = t.typrelid
        	
                join pg_catalog.pg_namespace n
                    on ( n.oid = t.typnamespace )
        	
                join types
                    on ( types.nspname = n.nspname
                        and types.obj_name = pg_catalog.format_type ( t.oid, NULL ) )
        	
                where a.attnum > 0
                    and not a.attisdropped
            )
        
            select cols.schema_name as schema,
        		cols.name as name,
                cols.column_name as column_name,
                cols.ordinal_position as ordinal_position,
        		attrs.data_type as base_type,
        		attrs.attribute_udt_schema,
        		attrs.attribute_udt_name,
        		attrs.character_maximum_length as max_length,
        		attrs.numeric_precision as numeric_precision, 
        		attrs.numeric_scale as numeric_scale,
                case when attrs.is_nullable = 'YES' then 'True' else 'False' end as allow_null
        		
            from cols 
        	left join information_schema.attributes as attrs
        		      on attrs.udt_schema = cols.schema_name
        			  and attrs.udt_name = cols.name
        			  and attrs.attribute_name = cols.column_name
        			  
            order by cols.schema_name,
                     cols.obj_name,
                     cols.ordinal_position;
            ";
    }
}