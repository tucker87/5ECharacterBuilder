using System;
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

    public class Tools
    {
        public Tools()
        {
            Available = new SortedSet<AvailableTool>();
            Chosen = new SortedSet<AvailableTool>();
            Expertise = new SortedSet<AvailableTool>();
        }
        public SortedSet<AvailableTool> Available { get; internal set; }
        public SortedSet<AvailableTool> Chosen { get; internal set; }
        public SortedSet<AvailableTool> Expertise { get; internal set; }
        public int Max { get; internal set; }
    }

    public class Skills
    {
        public Skills()
        {
            Available = new SortedSet<AvailableSkill>();
            Chosen = new SortedSet<AvailableSkill>();
            Expertise = new SortedSet<AvailableSkill>();
        }
        public SortedSet<AvailableSkill> Available { get; internal set; }
        public SortedSet<AvailableSkill> Chosen { get; internal set; }
        public SortedSet<AvailableSkill> Expertise { get; internal set; } 
        public int Max { get; internal set; }
        public int MaxExpertise { get; internal set; }
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

    public class SpellcastingClass : IComparable
    {
        public SpellcastingClass(string name)
        {
            Name = name;

            SpellSlots = new SpellSlots();
        }

        public string Name { get; internal set; }
        public int MaxCantrips { get; internal set; }
        public int MaxSpells { get; internal set; }
        public SpellSlots SpellSlots { get; private set; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            var otherSpellcastingClass = obj as SpellcastingClass;
            if (otherSpellcastingClass != null)
                return String.Compare(Name, otherSpellcastingClass.Name, StringComparison.Ordinal);

            throw new ArgumentException("Object is not a Temperature");
        }
    }

    public class SpellSlots
    {
        public int First { get; internal set; }
        public int Second { get; internal set; }
        public int Third { get; internal set; }
        public int Fourth { get; internal set; }
    }
}
