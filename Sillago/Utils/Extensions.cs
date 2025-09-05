namespace Sillago.Utils;

using System.Reflection;

public static class Extensions
{
    public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        where TKey : IComparable<TKey>
        => source
            .OrderByDescending(selector)
            .First();

    // Weighted sum
    public static float WeightedSum<T>(this IEnumerable<T> items, Func<T, float> weightSelector,
        Func<T, float> valueSelector)
    {
        float totalWeight = 0;
        float weightedSum = 0;

        foreach (T item in items)
        {
            float weight = weightSelector(item);
            float value = valueSelector(item);
            totalWeight += weight;
            weightedSum += weight * value;
        }

        return totalWeight > 0 ? weightedSum / totalWeight : 0;
    }

    public static int WeightedColorwiseSum<T>(
        this IEnumerable<T> items,
        Func<T, float> weightSelector,
        Func<T, int> colorSelector)
    {
        float totalWeight = 0;
        float r = 0, g = 0, b = 0;

        foreach (T item in items)
        {
            float weight = weightSelector(item);
            (int red, int green, int blue) = Extensions.UnpackColor(colorSelector(item));

            r += red   * weight;
            g += green * weight;
            b += blue  * weight;
            totalWeight += weight;
        }

        if (totalWeight == 0)
            return 0;

        return Extensions.PackColor(
            (int)(r / totalWeight),
            (int)(g / totalWeight),
            (int)(b / totalWeight)
        );
    }

    public static (int R, int G, int B) UnpackColor(int color)
    {
        int r = (color >> 16) & 0xFF;
        int g = (color >> 8)  & 0xFF;
        int b = color         & 0xFF;
        return (r, g, b);
    }

    public static int PackColor(int r, int g, int b)
        => (Math.Clamp(r, 0, 255)   << 16)
           | (Math.Clamp(g, 0, 255) << 8)
           | Math.Clamp(b, 0, 255);

    public static string FriendlyName(this Enum enumValue)
    {
        Type type = enumValue.GetType();
        string name = Enum.GetName(type, enumValue);
        if (name == null)
            return string.Empty;

        FieldInfo field = type.GetField(name);
        if (field == null)
            return name;

        FriendlyAttribute attribute = field
            .GetCustomAttributes(typeof(FriendlyAttribute), false)
            .FirstOrDefault() as FriendlyAttribute;
        return attribute?.Name ?? name;
    }

    public static bool Is<TEnum>(this TEnum value, TEnum flag)
        where TEnum : Enum
    {
        if (value.GetType() != flag.GetType())
            throw new ArgumentException("Enum types do not match.");

        return (Convert.ToUInt64(value) & Convert.ToUInt64(flag)) == Convert.ToUInt64(flag);
    }

    public static IEnumerable<TEnum> ExpandFlags<TEnum>(this TEnum value)
        where TEnum : Enum
    {
        Type type = value.GetType();
        IEnumerable<TEnum> values = Enum.GetValues(type).Cast<TEnum>();
        return values.Where(flag => value.Is(flag));
    }
    public static string Identifier(string name)
    {
        // Replace invalid characters with underscores
        return new string(name.Select(c => char.IsLetterOrDigit(c) ? char.ToLower(c) : '_').ToArray());
    }
}

public class FriendlyAttribute : Attribute
{
    public FriendlyAttribute(string name)
    {
        this.Name = name;
    }

    public string Name { get; }
}