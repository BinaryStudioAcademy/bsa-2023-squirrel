using Squirrel.ConsoleApp.Models.DTO;
using System.Reflection;

namespace Squirrel.ConsoleApp.BL.Extensions
{
    public static class MappingExtensions
    {
        public static Column MapToStructureRow(IList<string> columnNames, IList<string> rowValues)
        {
            var row = new Column();
            var columns = typeof(Column).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name).ToList();

            foreach (var propertyName in columns)
            {
                var property = typeof(Column).GetProperty(propertyName);
                if (property != null && columnNames.Contains(propertyName))
                {
                    var value = rowValues[columnNames.ToList().IndexOf(propertyName)];
                    if (property.PropertyType == typeof(string))
                        property.SetValue(row, value);
                    else if (property.PropertyType == typeof(int?))
                        property.SetValue(row, Parsing.ParseNullableInt(value));
                    else if (property.PropertyType == typeof(bool?))
                        property.SetValue(row, Parsing.ParseNullableBool(value));
                }
            }
            return row;
        }

        public static Constraint MapToChecksRow(IList<string> columnNames, IList<string> rowValues)
        {
            var row = new Constraint();
            var columns = typeof(Constraint).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name).ToList();

            foreach (var propertyName in columns)
            {
                var property = typeof(Constraint).GetProperty(propertyName);
                if (property != null && columnNames.Contains(propertyName))
                {
                    var value = rowValues[columnNames.ToList().IndexOf(propertyName)];
                    property.SetValue(row, value);
                }
            }
            return row;
        }

        public static Dictionary<string, string> MapToDataRow(IList<string> columnNames, IList<string> rowValues)
        {
            var rowDict = new Dictionary<string, string>();

            // Start from 3 to skip the first three columns (Schema, name, TotalRows)
            for (int i = 3; i < columnNames.Count; i++)
            {
                var columnName = columnNames[i];
                var cellValue = rowValues[i];
                rowDict[columnName] = cellValue;
            }
            return rowDict;
        }
    }
}
