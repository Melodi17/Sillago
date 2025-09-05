using System.Reflection;

namespace Sillago.Utils;

public class Registry
{
    public static IEnumerable<T> GetAllEntries<T, TClass>()
    {
        // get all static fields of type T in TClass
        FieldInfo[] fields = typeof(TClass).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(T) || field.FieldType.IsSubclassOf(typeof(T)))
            {
                // yield return the value of the field
                yield return (T) field.GetValue(null);
            }
        }
    }

    public static T? GetEntry<T, TClass>(string name, bool ignoreCase = false)
    {
        // get all static fields of type T in TClass
        FieldInfo[] fields = typeof(TClass).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(T) 
                && field.Name.Equals(name, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                return (T) field.GetValue(null);
        }
        return default;
    }
}