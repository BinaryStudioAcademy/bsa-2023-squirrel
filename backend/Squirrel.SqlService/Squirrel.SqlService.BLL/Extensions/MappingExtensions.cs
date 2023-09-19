using System.Reflection;

namespace Squirrel.SqlService.BLL.Extensions;

public static class MappingExtensions
{
    public static T MapToObject<T>(IList<string> rowNames, IList<string> rowValues) where T : new()
    {
        var obj = new T();
        var propertyNames = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name).ToList();

        foreach (var propertyName in propertyNames)
        {
            var property = typeof(T).GetProperty(propertyName);
            if (property == null || !rowNames.Contains(propertyName.ToLower())) continue;
            var value = rowValues[rowNames.IndexOf(propertyName.ToLower())];
            if (property.PropertyType == typeof(string))
                property.SetValue(obj, value);
            else if (property.PropertyType == typeof(int?))
                property.SetValue(obj, value.ParseNullableInt());
            else if (property.PropertyType == typeof(bool?))
                property.SetValue(obj, value.ParseNullableBool());
        }
        return obj;
    }

    public static Dictionary<string, string> MapToDataRow(IList<string> rowNames, IList<string> rowValues)
    {
        var rowDict = new Dictionary<string, string>();

        // Start from 3 to skip the first three columns (Schema, Name, TotalRows)
        for (var i = 3; i < rowNames.Count; i++)
        {
            var columnName = rowNames[i];
            var cellValue = rowValues[i];
            rowDict[columnName] = cellValue;
        }
        return rowDict;
    }
}