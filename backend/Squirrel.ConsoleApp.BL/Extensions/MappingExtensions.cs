using Squirrel.ConsoleApp.Models.DTO;
using System.Reflection;

namespace Squirrel.ConsoleApp.BL.Extensions
{
    public static class MappingExtensions
    {
        public static Row MapToRow(IList<string> columnNames, IList<string> rowValues)
        {
            var row = new Row();
            foreach (var propertyName in GetColumnNames())
            {
                var property = typeof(Row).GetProperty(propertyName);
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

        public static List<string> GetColumnNames()
        {
            var columnPropertyNames = typeof(Row).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name).ToList();

            return columnPropertyNames;
        }

    }
}
