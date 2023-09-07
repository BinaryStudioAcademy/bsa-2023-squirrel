using Squirrel.ConsoleApp.Models.DTO;
using System.Reflection;

namespace Squirrel.ConsoleApp.BL.Extensions
{
    public static class MappingExtensions
    {
        public static StructureRow MapToStructureRow(IList<string> columnNames, IList<string> rowValues)
        {
            var row = new StructureRow();
            var columns = typeof(StructureRow).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name).ToList();

            foreach (var propertyName in columns)
            {
                var property = typeof(StructureRow).GetProperty(propertyName);
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

        public static CheckRow MapToChecksRow(IList<string> columnNames, IList<string> rowValues)
        {
            var row = new CheckRow();
            var columns = typeof(CheckRow).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name).ToList();

            foreach (var propertyName in columns)
            {
                var property = typeof(CheckRow).GetProperty(propertyName);
                if (property != null && columnNames.Contains(propertyName))
                {
                    var value = rowValues[columnNames.ToList().IndexOf(propertyName)];
                    property.SetValue(row, value);
                }
            }
            return row;
        }
    }
}
