﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MVC5Library;

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
        //public All<Feature> BackgroundFeatures { get; internal set; } 
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

        public Tools(Tools tools)
        {
            Available = tools.Available;
            Chosen = tools.Chosen;
            Expertise = tools.Expertise;
            Max = tools.Max;
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
            AllSkills = (AvailableSkill[]) Enum.GetValues(typeof(AvailableSkill));
            Available = new SortedSet<AvailableSkill>();
            Chosen = new SortedSet<AvailableSkill>();
            Expertise = new SortedSet<AvailableSkill>();
        }

        public Skills(Skills skills)
        {
            AllSkills = skills.AllSkills;
            Available = skills.Available;
            Chosen = skills.Chosen;
            Expertise = skills.Expertise;
            Max = skills.Max;
            MaxExpertise = skills.MaxExpertise;
        }

        public AvailableSkill[] AllSkills { get; private set; }
        public SortedSet<AvailableSkill> Available { get; internal set; }
        public SortedSet<AvailableSkill> Chosen { get; internal set; }
        public SortedSet<AvailableSkill> Expertise { get; internal set; } 
        public int Max { get; internal set; }
        public int MaxExpertise { get; internal set; }
    }

    public class ClassPath : IEnumerable
    {
        public ClassPath()
        {
            Available = new SortedSet<AvailablePaths>();
        }

        public ClassPath(ClassPath classPath)
        {
            Available = classPath.Available;
            Chosen = classPath.Chosen;
        }

        public SortedSet<AvailablePaths> Available { get; internal set; }
        public AvailablePaths? Chosen { get; internal set; }

        public void Add(AvailablePaths[] paths)
        {
            foreach (var path in paths)
                Available.Add(path);
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable) Available).GetEnumerator();
        }
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
        public SpellSlots SpellSlots { get; internal set; }
        public int SaveDc { get; internal set; }
        public int AttackMod { get; internal set; }

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

    public class HitDice : IEnumerable
    {
        public HitDice() { List = new List<int>(); }

        public List<int> List { get; set; } 
        
        public void Add(int hitDice) { List.Add(hitDice); }
        public int Count => List.Count();
        public int First() { return List.First(); }
        public int Last() { return List.Last(); }
        public void Remove(int index) { List.Remove(index); }
        public int this[int i] => List[i];
        public IEnumerator GetEnumerator() { return List.GetEnumerator(); }

        public override string ToString()
        {
            var dice = new Dictionary<int, int>();
            foreach (var hitDie in List)
                if (dice.ContainsKey(hitDie))
                    dice[hitDie]++;
                else
                    dice.Add(hitDie, 1);

            return dice.Aggregate("", (current, die) => current + string.Format("{0}d{1} ", die.Value, die.Key).Trim());
        }
    }

    public class ClassTraits
    {
        public int RagesPerDay { get; internal set; }
        public int KiPoints { get; internal set; }
        public int RageDamage { get; internal set; }
    }
}
