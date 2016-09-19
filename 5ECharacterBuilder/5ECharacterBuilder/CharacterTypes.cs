using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _5EDatabase;

namespace _5ECharacterBuilder
{
    public class Proficiencies<T>
    {
        public Proficiencies()
        {
            Available = new SortedSet<T>();
            Chosen = new SortedSet<T>();
        }
        public SortedSet<T> Available { get; internal set; }
        public SortedSet<T> Chosen { get; internal set; }
        public int Max { get; internal set; }
    }

    public class Tools
    {
        public Tools()
        {
            Available = new SortedSet<Tool>();
            Chosen = new SortedSet<Tool>();
            Expertise = new SortedSet<Tool>();
        }

        public Tools(Tools tools)
        {
            Available = tools.Available;
            Chosen = tools.Chosen;
            Expertise = tools.Expertise;
            Max = tools.Max;
        }
        public SortedSet<Tool> Available { get; internal set; }
        public SortedSet<Tool> Chosen { get; internal set; }
        public SortedSet<Tool> Expertise { get; internal set; }
        public int Max { get; internal set; }
    }

    public class Skills
    {
        public Skills()
        {
            AllSkills = (Skill[]) Enum.GetValues(typeof(Skill));
            Available = new SortedSet<Skill>();
            Chosen = new SortedSet<Skill>();
            Expertise = new SortedSet<Skill>();
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

        public Skill[] AllSkills { get; private set; }
        public SortedSet<Skill> Available { get; internal set; }
        public SortedSet<Skill> Chosen { get; internal set; }
        public SortedSet<Skill> Expertise { get; internal set; } 
        public int Max { get; internal set; }
        public int MaxExpertise { get; internal set; }
    }
    
    public class Languages
    {
        public Languages()
        {
            Chosen = new SortedSet<Language>();
        }
        public SortedSet<Language> Chosen { get; internal set; }
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
                return string.Compare(Name, otherSpellcastingClass.Name, StringComparison.Ordinal);

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
        public List<int> List { get; set; } = new List<int>();

        public void Add(int hitDice) { List.Add(hitDice); }

        public HitDice Union(int hitDie)
        {
            List.Add(hitDie);
            return this;
        }

        public HitDice Union(List<int> hitDice)
        {
            List.AddRange(hitDice);
            return this;
        }

        public int Count => List.Count;
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

            return dice.Aggregate("", (current, die) => current + $"{die.Value}d{die.Key} ".Trim());
        }
    }

    public class ClassTraits
    {
        public int RagesPerDay { get; internal set; }
        public int KiPoints { get; internal set; }
        public int RageDamage { get; internal set; }
    }
}
