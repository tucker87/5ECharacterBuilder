using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder
{
    public class CharacterFeatures
    {
        public CharacterFeatures()
        {
            RaceFeatures = new List<Feature>();
            ClassFeatures = new List<Feature>();
        }
        public List<Feature> AllFeatures { get { return RaceFeatures.Concat(ClassFeatures).Distinct().ToList(); } } 
        public List<Feature> RaceFeatures { get; internal set; } 
        public List<Feature> ClassFeatures { get; internal set; } 
        //public List<Feature> BackgroundFeatures { get; internal set; } 
    }

    public class Feature
    {
        public Feature(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
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
