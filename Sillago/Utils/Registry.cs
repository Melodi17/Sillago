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
            else if (typeof(IEnumerable<T>).IsAssignableFrom(field.FieldType))
            {
                // if the field is a collection, yield each item in the collection
                foreach (T item in (IEnumerable<T>) field.GetValue(null))
                    yield return item;
            }
            else if (field.FieldType.IsArray && field.FieldType.GetElementType() == typeof(T))
                yield return (T) field.GetValue(null);
        }
    }

    public static T GetEntry<T, TClass>(string name)
    {
        // get all static fields of type T in TClass
        FieldInfo[] fields = typeof(TClass).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(T) && field.Name == name)
                return (T) field.GetValue(null);
        }
        return default;
    }
}