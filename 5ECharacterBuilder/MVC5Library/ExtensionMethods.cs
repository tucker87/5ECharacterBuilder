using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MVC5Library
{
    static class ExtensionMethods
    {
        public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> dictionaryToAdd)
        {
            foreach (var entry in dictionaryToAdd)
                if (!dictionary.ContainsKey(entry.Key))
                    dictionary.Add(entry.Key, entry.Value);
        }

        public static Dictionary<TKey, TValue> UnionDictionary<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> dictionaryToUnion)
        {
            return dictionary.Union(dictionaryToUnion).ToDictionary(p => p.Key, p => p.Value);
        }

        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null)
                return null;

            var field = type.GetField(name);

            if (field == null)
                return null;

            var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attr != null ? attr.Description : name;
        }
    }
}
