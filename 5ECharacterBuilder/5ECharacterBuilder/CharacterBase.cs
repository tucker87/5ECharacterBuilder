using System;
using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder
{
    public interface ICharacter
    {
        int ArmorClass { get; }
        List<AvailableArmor> ArmorProficiencies { get; }
        CharacterAttributes Attributes { get; }
        string Background { get; }
        List<string> Classes { get; }
        string ClassesString { get; }
        Currency Currency { get; }
        Armor EquippedArmor { get; }
        bool HasShield { get; }
        List<int> HitDice { get; }
        int Initiative { get; }
        List<AvailableInstrument> AvailableInstrumentProficiencies { get; }
        List<AvailableInstrument> ChosenInstrumentProficiencies { get; }
        int MaxHp { get; }
        string Name { get; }
        string Race { get; }
        List<AvailableLanguages> ChosenLanguages { get; }
        List<string> RuleIssues { get; }
        List<SavingThrow> SavingThrowProficiencies { get; }
        string Size { get; }
        List<AvailableSkill> AvailableSkills { get; }
        List<AvailableSkill> TrainedSkills { get; }
        int Speed { get; }
        List<AvailableTool> AvailableToolProficiencies { get; }
        List<AvailableTool> ChosenToolProficiencies { get; }
        List<AvailableWeapon> WeaponProficiencies { get; }
        int ClassSkillCount { get; }
        int LanguageCount { get; }

        void EquipArmor(AvailableArmor armor);
        void SetAttributes(CharacterAttributes characterAttributes);
        void ToggleShield();
        void SetName(string name);
        void LearnSkill(AvailableSkill chosenSkill);
        void LearnTool(AvailableTool chosenTool);
    }

    class CharacterBase : ICharacter
    {
        public CharacterBase(CharacterAttributeScores attributeScores = null, string name = "")
        {
            AvailableSkills = new List<AvailableSkill>();
            ChosenLanguages = new List<AvailableLanguages>();
            EquipArmor(AvailableArmor.Cloth);
            attributeScores = attributeScores ?? new CharacterAttributeScores();
            Attributes = new CharacterAttributes(attributeScores);
            ArmorProficiencies = new List<AvailableArmor>(new List<AvailableArmor>());
            TrainedSkills = new List<AvailableSkill>(new List<AvailableSkill>());
            AvailableToolProficiencies = new List<AvailableTool>(new List<AvailableTool>());
            AvailableInstrumentProficiencies = new List<AvailableInstrument>(new List<AvailableInstrument>());
            WeaponProficiencies = new List<AvailableWeapon>(new List<AvailableWeapon>());
            SavingThrowProficiencies = new List<SavingThrow>(new List<SavingThrow>());
            Name = name;
            HitDice = new List<int>(new int[0]);
            Currency = new Currency();
            _classes = new List<string>();
        }

        private int ShieldBonus { get { return HasShield ? 2 : 0; } }

        public int ArmorClass { get {  return GetArmorClassBonus(EquippedArmor, Attributes.Dexterity.Modifier) + ShieldBonus; } }
        public List<AvailableArmor> ArmorProficiencies { get; internal set; }
        public string Background { get; internal set; }
        private readonly List<string> _classes;
        public List<string> Classes { get { return _classes; } }
        public string ClassesString { get { return null; } }
        public Currency Currency { get; internal set; }
        public int MaxHp { get { return CalculateMaxHp(HitDice, Attributes.Constitution.Modifier); } }
        public List<int> HitDice { get; internal set; }
        public List<AvailableInstrument> AvailableInstrumentProficiencies { get; internal set; }
        public List<AvailableInstrument> ChosenInstrumentProficiencies { get; private set; }
        public int Initiative { get { return Attributes.Dexterity.Modifier; } }
        public List<AvailableLanguages> ChosenLanguages { get; internal set; }
        public string Name { get; set; }
        public string Race { get; internal set; }
        public CharacterAttributes Attributes { get; set; }
        public int ClassSkillCount { get; private set; }
        public int LanguageCount { get; internal set; }
        public int SkillCount { get; internal set; }
        public Armor EquippedArmor { get; internal set; }
        public bool HasShield { get; set; }
        public List<string> RuleIssues { get { return CharacterFactory.GetRuleIssues(this); } }
        public List<SavingThrow> SavingThrowProficiencies { get; internal set; }
        public string Size { get; internal set; }
        public List<AvailableSkill> AvailableSkills { get; internal set; }
        public List<AvailableSkill> TrainedSkills { get; internal set; }
        public int Speed { get; internal set; }
        public List<AvailableTool> AvailableToolProficiencies { get; internal set; }
        public List<AvailableTool> ChosenToolProficiencies { get; private set; }
        public List<AvailableWeapon> WeaponProficiencies { get; internal set; }

        public void EquipArmor(AvailableArmor armor) { EquippedArmor = Armory.GetArmor(armor); }
        
        public void SetAttributes(CharacterAttributes characterAttributes)
        {
            var racialBonuses = new RacialBonuses(
                Attributes.Strength.RacialBonus,
                Attributes.Dexterity.RacialBonus,
                Attributes.Constitution.RacialBonus,
                Attributes.Intelligence.RacialBonus,
                Attributes.Wisdom.RacialBonus,
                Attributes.Charisma.RacialBonus);
            Attributes = new CharacterAttributes(characterAttributes, racialBonuses);
        }

        public void ToggleShield()
        {
            HasShield = !HasShield;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void LearnSkill(AvailableSkill chosenSkill)
        {
            TrainedSkills.Add(chosenSkill);
        }

        public void LearnTool(AvailableTool chosenTool)
        {
            ChosenToolProficiencies.Add(chosenTool);
        }

        private static int GetArmorClassBonus(Armor armor, int dex)
        {
            if (armor.MaxDexterityBonus == -1)
                return armor.BaseArmor + dex;

            if (dex > armor.MaxDexterityBonus)
                dex = armor.MaxDexterityBonus;

            return armor.BaseArmor + dex;
        }

        public static int CalculateMaxHp(List<int> hitDice, int constitutionMod)
        {
            return hitDice[0] + hitDice.GetRange(1, hitDice.Count - 1).Sum(hitDie => (hitDie / 2) + 1) + constitutionMod;
        }
    }
}

