namespace Squirrel.SqlService.BLL.Extensions;

public static class Parsing
{
    public static int? ParseNullableInt(this string value)
    {
        if (string.IsNullOrEmpty(value) || value.Equals("null"))
        {
            return null;
        }
        
        return int.Parse(value);
    }

    public static bool? ParseNullableBool(this string value)
    {
        if (string.IsNullOrEmpty(value) || value.Equals("null"))
        {
            return null;
        }
        
        return bool.Parse(value);
    }
}