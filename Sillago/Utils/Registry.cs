using System.Reflection;

namespace Sillago.Utils
{
    using System;
    using System.Collections.Generic;

    public class Registry
    {
        public static IEnumerable<T> GetAllEntries<T, TClass>()
        {
            FieldInfo[] fields = typeof(TClass).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                bool isRelevantType = field.FieldType == typeof(T) || field.FieldType.IsSubclassOf(typeof(T));
                if (isRelevantType)
                    yield return (T) field.GetValue(null)!;
            }
        }

        public static T? GetEntry<T, TClass>(string name, bool ignoreCase = false)
        {
            FieldInfo[] fields = typeof(TClass).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                bool isRelevantType = field.FieldType == typeof(T) || field.FieldType.IsSubclassOf(typeof(T));
                bool nameMatches = field.Name.Equals(name, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
                if (isRelevantType && nameMatches)
                    return (T) field.GetValue(null)!;
            }
            return default;
        }
    }
}