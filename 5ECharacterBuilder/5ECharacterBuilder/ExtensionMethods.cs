using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder
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
    }
}
