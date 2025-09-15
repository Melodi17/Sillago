namespace Sillago.Utils;

public static class StringExtensions
{
    public static string TitleCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
    }
}