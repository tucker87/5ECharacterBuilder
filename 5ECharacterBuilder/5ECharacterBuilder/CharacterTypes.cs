using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder
{
    public class CharacterFeatures
    {
        public CharacterFeatures()
        {
            RaceFeatures = new Dictionary<string, string>();
            ClassFeatures = new Dictionary<string, string>();
            ClassPathFeatures = new Dictionary<string, string>();
        }
        public Dictionary<string, string> AllFeatures { get { return RaceFeatures.Concat(ClassFeatures).Distinct().Concat(ClassPathFeatures).ToList().ToDictionary(k => k.Key, v => v.Value); } } 
        public Dictionary<string, string> RaceFeatures { get; internal set; } 
        public Dictionary<string, string> ClassFeatures { get; internal set; } 
        public Dictionary<string, string> ClassPathFeatures { get; internal set; } 
        //public List<Feature> BackgroundFeatures { get; internal set; } 
    }

    public class Proficiencies<T>
    {
        public Proficiencies()
        {
            Available = new SortedSet<T>();
            Chosen = new SortedSet<T>();
        }
        public  SortedSet<T> Available { get; internal set; }
        public  SortedSet<T> Chosen { get; internal set; }
        public int Max { get; internal set; }
    }

    public class ClassPath
    {
        public ClassPath()
        {
            Available = new SortedSet<AvailablePaths>();
        }

        public SortedSet<AvailablePaths> Available { get; internal set; }
        public AvailablePaths? Chosen { get; internal set; }
    }

    public class Languages
    {
        public Languages()
        {
            Chosen = new SortedSet<AvailableLanguages>();
        }
        public SortedSet<AvailableLanguages> Chosen { get; internal set; }
        public int Max { get; internal set; }
    }
}
