namespace Sillago.Utils;

public static class EnumerableExtensions
{
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
}