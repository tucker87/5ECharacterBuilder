using System;
using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder
{
    public interface ICharacter
    {
        CharacterAttributes Attributes { get; }
        List<int> HitDice { get; }
        int MaxHp { get; }
        string Name { get; }
        List<string> SkillProficiencies { get; }
        int SkillProficiencyCount { get; }
        List<string> ArmorProficiencies { get; }
        List<AvailableWeapons> WeaponProficiencies { get; }
        List<AvailableTools> ToolProficiencies { get; }
        List<AvailableInstruments> InstrumentProficiencies { get; }
        List<SavingThrows> SavingThrowProficiencies { get; }
    }

    public class CharacterBase : ICharacter
    {
        public CharacterBase(CharacterAttributeScores attributeScores = null, string name = "")
        {
            attributeScores = attributeScores ?? new CharacterAttributeScores();
            ArmorProficiencies = new List<string>();
            SkillProficiencies = new List<string>();
            ToolProficiencies = new List<AvailableTools>();
            InstrumentProficiencies = new List<AvailableInstruments>();
            WeaponProficiencies = new List<AvailableWeapons>();
            Name = name;
            Attributes = new CharacterAttributes(attributeScores);
            HitDice = new List<int>(new int[0]);
        }
        
        public static int CalculateMaxHp(List<int> hitDice, int constitutionMod)
        {
            return hitDice[0] + hitDice.GetRange(1, hitDice.Count-1).Sum(hitDie => (hitDie / 2) + 1) + constitutionMod;
        }

        public CharacterAttributes Attributes { get; private set; }
        public List<int> HitDice { get; private set; }
        public int MaxHp { get { return CalculateMaxHp(HitDice, Attributes.Constitution.Modifier); } }
        public string Name { get; private set; }
        public List<string> SkillProficiencies { get; private set; }
        public int SkillProficiencyCount { get; private set; }
        public List<string> ArmorProficiencies { get; private set; }
        public List<AvailableWeapons> WeaponProficiencies { get; private set; }
        public List<AvailableTools> ToolProficiencies { get; private set; }
        public List<AvailableInstruments> InstrumentProficiencies { get; private set; }
        public List<SavingThrows> SavingThrowProficiencies { get; private set; }
    }

    public enum AvailableRaces { Human }
    public enum AvailableClasses { Fighter }
    public enum AvailableSkills { Acrobat, Arcana, Athletics, AnimalHandling, Deception, History, Insight, Medicine, Perception, Religion, Stealth }
    public enum AvailableTools { AlchemistsSupplies }
    public enum AvailableInstruments { Lute }
    public enum SavingThrows { Strength, Constitution, Dexterity, Intelligence, Wisdom, Charisma }

    public class SkillList
    {
        public SkillList(IEnumerable<string> skills)
        {
            Skills = new List<string>();
            foreach (var skill in skills)
            {
                if (Enum.IsDefined(typeof(AvailableSkills), skill))
                {
                    Skills.Add(skill);
                }
                else
                {
                    throw new Exception(skill + " is not a valid skill.");
                }
            }
        }

        public List<string> Skills { get; private set; }
    }
}

