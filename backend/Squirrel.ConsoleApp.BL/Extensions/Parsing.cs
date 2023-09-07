namespace Squirrel.ConsoleApp.BL.Extensions
{
    public static class Parsing
    {
        public static int? ParseNullableInt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            return int.Parse(value);
        }

        public static bool? ParseNullableBool(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            return bool.Parse(value);
        }
    }
}
