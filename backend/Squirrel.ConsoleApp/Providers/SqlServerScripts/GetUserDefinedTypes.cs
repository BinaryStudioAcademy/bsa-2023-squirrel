namespace Squirrel.ConsoleApp.Providers.SqlServerScripts
{
    internal static class GetUserDefinedTypes
    {
        public static string GetUserDefinedTypesWithDefaultsAndRulesAndDefinitionScript =>
            @"
            SELECT	userDefinedTypes.name [UserTypeName],
		            TYPE_NAME(userDefinedTypes.system_type_id) [BaseTypeName],
		            SCHEMA_NAME(userDefinedTypes.schema_id) [UserTypeSchema],
		            userDefinedTypes.max_length [MaxLength],
		            userDefinedTypes.precision [Precision],
		            userDefinedTypes.scale [Scale],
		            CASE WHEN userDefinedTypes.is_nullable = 1 THEN 'True' ELSE 'False' END [AllowNulls],
		            CASE WHEN userDefinedTypes.is_table_type = 1 THEN 'True' ELSE 'False' END [IsTable],
		            CASE WHEN userDefinedTypeProperties.IsTableType = 1 
		            	THEN N'CREATE TYPE ' + QUOTENAME(sch.name) + '.' + QUOTENAME(userDefinedTypes.name) + ' AS TABLE (' 
		            	+ tAllColumns.column_definition  + N'); '   
		            	ELSE   
                         + N'CREATE TYPE ' + QUOTENAME(sch.name) + '.' + QUOTENAME(userDefinedTypes.name)   
                         + N' FROM '   
                         + tBaseTypeComputation.baseTypeName   
                         + CASE WHEN userDefinedTypeProperties.is_nullable = 0 THEN N' NOT NULL' ELSE N'' END   
                         + N'; ' END [UserTypeDefinition], 
		            OBJECT_NAME(userDefinedTypes.default_object_id) [DefaultValueName],
		            OBJECT_DEFINITION(userDefinedTypes.default_object_id) [DefaultValueDefinition],
		            OBJECT_NAME(userDefinedTypes.rule_object_id) [RuleName],
		            OBJECT_DEFINITION(userDefinedTypes.rule_object_id) [RuleDefinition]

	        FROM sys.types AS userDefinedTypes
	                INNER JOIN sys.schemas AS sch ON sch.schema_id = userDefinedTypes.schema_id
	                LEFT JOIN sys.table_types AS userDefinedTableTypes ON userDefinedTableTypes.user_type_id = userDefinedTypes.user_type_id
	                LEFT JOIN sys.types AS systemType ON systemType.system_type_id = userDefinedTypes.system_type_id AND systemType.is_user_defined = 0
	                OUTER APPLY   
                    (SELECT
	                userDefinedTypes.is_nullable,
	                userDefinedTypes.precision AS NUMERIC_PRECISION,
	                userDefinedTypes.scale AS NUMERIC_SCALE,
	                userDefinedTypes.max_length AS CHARACTER_MAXIMUM_LENGTH,
	                CASE WHEN userDefinedTableTypes.user_type_id IS NULL THEN 0 ELSE 1 END AS IsTableType,
	                CONVERT(smallint, CASE -- datetime/smalldatetime  
	                		WHEN userDefinedTypes.system_type_id IN (40, 41, 42, 43, 58, 61)
	                		THEN ODBCSCALE(userDefinedTypes.system_type_id, userDefinedTypes.scale) END) AS DATETIME_PRECISION   
	                		) AS userDefinedTypeProperties     
	                
	                OUTER APPLY   
                    (SELECT
	                systemType.name + CASE  WHEN
	                				  systemType.name IN ('char', 'varchar', 'nchar', 'nvarchar', 'binary', 'varbinary') 
	                				  THEN N'(' + CASE WHEN 
	                							  userDefinedTypeProperties.CHARACTER_MAXIMUM_LENGTH = -1
	                							  THEN 'MAX' ELSE CONVERT(varchar(4),userDefinedTypeProperties.CHARACTER_MAXIMUM_LENGTH)
	                							  END + N')'
	                						WHEN systemType.name IN ('decimal', 'numeric')  
	                						THEN N'(' + CONVERT(varchar(4), userDefinedTypeProperties.NUMERIC_PRECISION) + N', ' 
	                						+ CONVERT(varchar(4), userDefinedTypeProperties.NUMERIC_SCALE) + N')'  
	                						WHEN systemType.name IN ('time', 'datetime2', 'datetimeoffset')   
	                						THEN N'(' + CAST(userDefinedTypeProperties.DATETIME_PRECISION AS national character varying(36)) + N')'   
	                						ELSE N''
	                				  END AS baseTypeName    
                    ) AS tBaseTypeComputation        
 
	        OUTER APPLY (SELECT   
			        (SELECT   
                    -- ,clmns.is_nullable   
                    -- ,tComputedProperties.ORDINAL_POSITION  
                    -- ,tComputedProperties.COLUMN_DEFAULT  
                    CASE WHEN tComputedProperties.ORDINAL_POSITION = 1 THEN N' ' ELSE N',' END   
                    + QUOTENAME(clmns.name) + N' ' + tComputedProperties.DATA_TYPE +   
                    CASE WHEN tComputedProperties.DATA_TYPE IN ('char', 'varchar', 'nchar', 'nvarchar', 'binary', 'varbinary')   
                    THEN N'(' + CASE WHEN tComputedProperties.CHARACTER_MAXIMUM_LENGTH = -1 
			        			THEN 'MAX'  
                                ELSE CONVERT(varchar(4),tComputedProperties.CHARACTER_MAXIMUM_LENGTH)  
                                END + N')'   
                    WHEN tComputedProperties.DATA_TYPE IN ('decimal', 'numeric')  
                    THEN N'(' + CONVERT(varchar(4), tComputedProperties.NUMERIC_PRECISION) + N', ' 
			         + CONVERT(varchar(4), tComputedProperties.NUMERIC_SCALE) + N')'  
                    WHEN tComputedProperties.DATA_TYPE IN ('time', 'datetime2', 'datetimeoffset')   
                    THEN N'(' + CAST(tComputedProperties.DATETIME_PRECISION AS national character varying(36)) + N')'   
                    ELSE N''   
			        END   
                    + CASE WHEN tComputedProperties.is_nullable = 0 THEN N'  NOT NULL' ELSE N' NULL' END   
                    + NCHAR(13) + NCHAR(10) AS [text()]  
                    FROM sys.columns AS clmns   
                    INNER JOIN sys.types AS t ON t.system_type_id = clmns.system_type_id   
                    LEFT JOIN sys.types ut ON ut.user_type_id = clmns.user_type_id   
      
                    OUTER APPLY
			        (SELECT   
                     33 As bb,   
                     COLUMNPROPERTY(clmns.object_id, clmns.name, 'ordinal')  AS ORDINAL_POSITION,   
                     COLUMNPROPERTY(clmns.object_id, clmns.name, 'charmaxlen') AS CHARACTER_MAXIMUM_LENGTH,  
                     COLUMNPROPERTY(clmns.object_id, clmns.name, 'octetmaxlen') AS CHARACTER_OCTET_LENGTH,   
                     CONVERT(nvarchar(4000), OBJECT_DEFINITION(clmns.default_object_id)) AS COLUMN_DEFAULT,
                     clmns.is_nullable,   
                     t.name AS DATA_TYPE, 
                     CONVERT(tinyint, CASE -- int/decimal/numeric/real/float/money    
                                      WHEN clmns.system_type_id IN (48, 52, 56, 59, 60, 62, 106, 108, 122, 127)
			        				  THEN clmns.precision    
                                      END) AS NUMERIC_PRECISION,   
                     CONVERT(int, CASE -- datetime/smalldatetime    
                                  WHEN clmns.system_type_id IN (40, 41, 42, 43, 58, 61) THEN NULL    
                                  ELSE ODBCSCALE(clmns.system_type_id, clmns.scale)   
                                  END) AS NUMERIC_SCALE,  
                     CONVERT(smallint, CASE -- datetime/smalldatetime    
                                       WHEN clmns.system_type_id IN (40, 41, 42, 43, 58, 61)
			        				   THEN ODBCSCALE(clmns.system_type_id, clmns.scale)   
                                       END) AS DATETIME_PRECISION   
                     ) AS tComputedProperties

             WHERE clmns.object_id = userDefinedTableTypes.type_table_object_id AND tComputedProperties.DATA_TYPE<>'sysname'

             ORDER BY tComputedProperties.ORDINAL_POSITION FOR XML PATH(''), TYPE).value('.', 'nvarchar(MAX)') AS column_definition) AS tAllColumns 
                        WHERE userDefinedTypes.is_user_defined = 1
            ";

        public static string GetUserDefinedTableTypesStructureScript =>
            @"
            SELECT	systt.name [UserTableTypeName],
		            sysc.name [Column],
		            sysc.colorder [ColumnOrder],
		            syst.name [DataType],
		            syscmnts.text [Default],
		            sysc.prec [Precision],   
		            sysc.scale [Scale],
		            CASE WHEN sysc.isnullable = 1 THEN 'True' ELSE 'False' END [AllowNulls],
		            CASE WHEN sysc.[status] = 128 THEN 'True' ELSE 'False' END [Identity],
		            CASE WHEN sysc.colstat = 1 THEN 'True' ELSE 'False' END [PrimaryKey]
            FROM [sys].[syscolumns] as sysc
                    JOIN sys.table_types as systt ON systt.type_table_object_id = sysc.id
                    LEFT JOIN [sys].[syscomments] AS syscmnts on sysc.cdefault = syscmnts.id
                    LEFT JOIN [sys].[systypes] AS syst ON sysc.xtype = syst.xtype AND syst.name != 'sysname'
            ";
    }
}