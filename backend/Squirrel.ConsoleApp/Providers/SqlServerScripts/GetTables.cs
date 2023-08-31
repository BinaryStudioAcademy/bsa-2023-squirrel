﻿namespace Squirrel.ConsoleApp.Providers.SqlServerScripts
{
    internal static class GetTables
    {
        public static string GetTablesNamesScript =>
            @"SELECT TABLE_SCHEMA + '.' + TABLE_NAME AS FULL_TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';";

        public static string GetTableDataQueryScript(int rowsCount, string schema, string table) =>
            @$"SELECT TOP {rowsCount} * FROM [{schema}].[{table}]";

        public static string GetTablesStructureScript =>
            @"
             SELECT	OBJECT_SCHEMA_NAME(syso.id) [TableSchema],
            		syso.name [Table],
            		sysc.name [Column],
            		sysc.colorder [ColumnOrder],   
            		syst.name [DataType],
            		syscmnts.text [Default],
            		sysc.prec [Precision],   
            		sysc.scale [Scale],   
            		CASE WHEN sysc.isnullable = 1 THEN 'True' ELSE 'False' END [AllowNulls],   
            		CASE WHEN sysc.[status] = 128 THEN 'True' ELSE 'False' END [Identity],
            		CASE WHEN sysc.colstat = 1 THEN 'True' ELSE 'False' END [PrimaryKey],  
            		CASE WHEN fkc.parent_object_id IS NULL THEN 'False' ELSE 'True' END [ForeignKey],   
            		CASE WHEN fkc.parent_object_id IS NULL THEN NULL ELSE obj.name END [RelatedTable],
            		COL_NAME(fkc.parent_object_id, fkc.parent_column_id) [RelatedTableColumn],
            		OBJECT_SCHEMA_NAME(fkc.referenced_object_id) [RelatedTableSchema],
            		CASE WHEN ep.value is NULL THEN NULL ELSE CAST(ep.value as NVARCHAR(500)) END [Description]

            FROM	[sys].[sysobjects] AS syso  
            		JOIN [sys].[syscolumns] AS sysc on syso.id = sysc.id  
            		LEFT JOIN [sys].[syscomments] AS syscmnts on sysc.cdefault = syscmnts.id
            		LEFT JOIN [sys].[systypes] AS syst ON sysc.xtype = syst.xtype AND syst.name != 'sysname'
            		LEFT JOIN [sys].[foreign_key_columns] AS fkc on syso.id = fkc.parent_object_id AND sysc.colid = fkc.parent_column_id      
            		LEFT JOIN [sys].[objects] AS obj ON fkc.referenced_object_id = obj.[object_id]  
            		LEFT JOIN [sys].[extended_properties] AS ep ON syso.id = ep.major_id AND sysc.colid = ep.minor_id AND ep.name = 'MS_Description' 

            WHERE	syso.type = 'U' AND syso.name != 'sysdiagrams'

            ORDER BY	[Table], [ColumnOrder], [Column]; 
            ";

        public static string GetDbTablesCheckAndUniqueConstraintsScript =>
            @"
            SELECT	TC.TABLE_SCHEMA [TableSchema],	
		            TC.TABLE_NAME [Table],
		            TC.Constraint_Name [ConstraintName],
		            STRING_AGG(CC.Column_Name, ', ') [Columns],
		            CASE WHEN TC.CONSTRAINT_TYPE = 'CHECK' THEN C.CHECK_CLAUSE ELSE NULL END [CheckClause]

            FROM	INFORMATION_SCHEMA.TABLE_CONSTRAINTS as TC
		            INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE as CC ON TC.CONSTRAINT_NAME = CC.CONSTRAINT_NAME
		            LEFT JOIN INFORMATION_SCHEMA.CHECK_CONSTRAINTS as C on TC.CONSTRAINT_NAME = C.CONSTRAINT_NAME

            WHERE	TC.CONSTRAINT_TYPE NOT LIKE '%KEY' AND TC.TABLE_NAME != 'sysdiagrams'

            GROUP BY	TC.TABLE_SCHEMA, TC.TABLE_NAME, TC.CONSTRAINT_NAME, TC.CONSTRAINT_TYPE, C.CHECK_CLAUSE

            ORDER BY	TC.TABLE_NAME
            ";
    }
}