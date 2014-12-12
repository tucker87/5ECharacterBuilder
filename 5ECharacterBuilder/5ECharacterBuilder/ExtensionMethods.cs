using System.Collections.Generic;

namespace _5ECharacterBuilder
{
    static class ExtensionMethods
    {
        public static void Add(this Dictionary<string, string> dictionary, Dictionary<string, string> dictionaryToAdd)
        {
            foreach (var entry in dictionaryToAdd)
                if (!dictionary.ContainsKey(entry.Key))
                    dictionary.Add(entry.Key, entry.Value);
        }
    }
}
