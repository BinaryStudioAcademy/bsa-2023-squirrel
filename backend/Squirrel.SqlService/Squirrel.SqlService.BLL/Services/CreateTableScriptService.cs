using Squirrel.Shared.DTO.Table;
using Squirrel.SqlService.BLL.Interfaces;
using System.Text;

namespace Squirrel.SqlService.BLL.Services;

public class CreateTableScriptService: ICreateTableScriptService
{
    public string GenerateCreateTableScript(string tableSchema, string tableName, TableStructureDto tableStructure, List<string> fkConstraintScripts)
    {
        List<string> primaryKeyColumns = new List<string>();
        StringBuilder createScript = new StringBuilder($"CREATE TABLE [{tableSchema}].[{tableName}] (");
        createScript.AppendLine();

        foreach (var column in tableStructure.Columns)
        {
            createScript.Append($"{column.ColumnName} ");
            AppendTableColumnDataType(createScript, column);
            AddPrimaryKeyColumn(column, primaryKeyColumns);
            AppendTableColumnIdentity(createScript, column);
            AppendTableColumnNull(createScript, column);
            createScript.AppendLine(",");
            AddTableColumnFKScript(fkConstraintScripts, column, tableSchema, tableName);
        }
        createScript.AppendLine(");");
        AppendPrimaryKey(createScript, primaryKeyColumns, tableSchema, tableName);
        
        return createScript.ToString();
    }

    public string ConcatForeignKeysConstraintScripts(StringBuilder createScript, List<string> fkConstraintScripts)
    {
        foreach (var fkConstraint in fkConstraintScripts)
        {
            createScript.AppendLine(fkConstraint);
        }

        return createScript.ToString();
    }

    private void AppendTableColumnDataType(StringBuilder createScript, TableColumnInfo column)
    {
        string[] variableTypes = { "binary", "varbinary", "char", "nchar", "varchar", "nvarchar" };
        if (variableTypes.Contains(column.DataType))
        {
            if (column.MaxLength < 1)
            {
                createScript.Append($"{column.DataType} (MAX) ");
            }
            else
            {
                createScript.Append($"{column.DataType} ({column.MaxLength}) ");
            }
        }
        else
        {
            createScript.Append($"{column.DataType} ");
        }
    }
    private void AddPrimaryKeyColumn(TableColumnInfo column, List<string> primaryKeyColumns)
    {
        if (column.IsPrimaryKey ?? false)
        {
            primaryKeyColumns.Add(column.ColumnName);
        }
    }

    private void AppendTableColumnIdentity(StringBuilder createScript, TableColumnInfo column)
    {
        if (column.IsIdentity ?? false)
        {
            createScript.Append("IDENTITY (1, 1) ");
        }
    }

    private void AppendTableColumnNull(StringBuilder createScript, TableColumnInfo column)
    {
        if (column.IsAllowNulls ?? false)
        {
            createScript.Append("NULL");
        }
        else
        {
            createScript.Append("NOT NULL");
        }
    }

    private void AddTableColumnFKScript(List<string> fkConstraintScripts, TableColumnInfo column, string tableSchema, string tableName)
    {
        if (column.IsForeignKey ?? false)
        {
            fkConstraintScripts.Add($"ALTER TABLE [{tableSchema}].[{tableName}] ADD FOREIGN KEY ({column.ColumnName})" +
                $" REFERENCES [{column.RelatedTableSchema}].[{column.RelatedTable}] ({column.RelatedTableColumn});");
        }
    }

    private void AppendPrimaryKey(StringBuilder createScript, List<string> primaryKeyColumns, string tableSchema, string tableName)
    {
        if (primaryKeyColumns.Any())
        {
            if (primaryKeyColumns.Count > 1)
            {
                createScript.Append(GetCompositePrimaryKey(primaryKeyColumns, tableSchema, tableName));
            }
            else
            {
                createScript.Append($"ALTER TABLE [{tableSchema}].[{tableName}] ADD PRIMARY KEY ({primaryKeyColumns.First()})");
            }
        }
    }

    private StringBuilder GetCompositePrimaryKey(List<string> primaryKeyColumns, string tableSchema, string tableName)
    {
        StringBuilder compositePk = new StringBuilder($"ALTER TABLE [{tableSchema}].[{tableName}] ADD PRIMARY KEY ");
        compositePk.AppendLine("(");
        foreach (var columnName in primaryKeyColumns)
        {
            if (primaryKeyColumns.Last() == columnName)
            {
                compositePk.AppendLine($"{columnName}");
            }
            else
            {
                compositePk.AppendLine($"{columnName},");
            }
        }
        compositePk.Length--;
        compositePk.Append(");");
        return compositePk;
    }
}