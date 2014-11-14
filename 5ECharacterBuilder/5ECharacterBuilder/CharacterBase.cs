using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace _5ECharacterBuilder
{
    public interface ICharacter
    {
        int ArmorClass { get; }
        SortedSet<AvailableArmor> ArmorProficiencies { get; }
        CharacterAttributes Attributes { get; }
        string Background { get; }
        List<string> Classes { get; }
        string ClassesString { get; }
        Currency Currency { get; }
        Armor EquippedArmor { get; }
        bool HasShield { get; }
        List<int> HitDice { get; }
        int Initiative { get; }
        SortedSet<AvailableInstrument> AvailableInstrumentProficiencies { get; }
        SortedSet<AvailableInstrument> ChosenInstrumentProficiencies { get; }
        int MaxHp { get; }
        string Name { get; }
        string Race { get; }
        SortedSet<AvailableLanguages> ChosenLanguages { get; }
        SortedSet<SavingThrow> SavingThrowProficiencies { get; }
        string Size { get; }
        SortedSet<AvailableSkill> AvailableSkills { get; }
        SortedSet<AvailableSkill> ChosenSkills { get; }
        int Speed { get; }
        SortedSet<AvailableTool> AvailableToolProficiencies { get; }
        SortedSet<AvailableTool> ChosenToolProficiencies { get; }
        SortedSet<AvailableWeapon> WeaponProficiencies { get; }
        int ClassSkillCount { get; }
        int LanguageCount { get; }
        int ToolProficiencyCount { get; }
        CharacterFeatures Features { get; }

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
            AvailableSkills = new SortedSet<AvailableSkill>();
            ChosenLanguages = new SortedSet<AvailableLanguages>();
            EquipArmor(AvailableArmor.Cloth);
            attributeScores = attributeScores ?? new CharacterAttributeScores();
            Attributes = new CharacterAttributes(attributeScores);
            ArmorProficiencies = new SortedSet<AvailableArmor>(new List<AvailableArmor>());
            ChosenSkills = new SortedSet<AvailableSkill>(new List<AvailableSkill>());
            AvailableToolProficiencies = new SortedSet<AvailableTool>(new List<AvailableTool>());
            AvailableInstrumentProficiencies = new SortedSet<AvailableInstrument>(new List<AvailableInstrument>());
            WeaponProficiencies = new SortedSet<AvailableWeapon>(new List<AvailableWeapon>());
            SavingThrowProficiencies = new SortedSet<SavingThrow>(new List<SavingThrow>());
            ChosenInstrumentProficiencies = new SortedSet<AvailableInstrument>();
            ChosenToolProficiencies = new SortedSet<AvailableTool>();
            Features = new CharacterFeatures();
            Name = name;
            HitDice = new List<int>(new int[0]);
            Currency = new Currency();
            _classes = new List<string>();
        }

        private int ShieldBonus { get { return HasShield ? 2 : 0; } }

        public int ArmorClass { get {  return GetArmorClassBonus(EquippedArmor, Attributes.Dexterity.Modifier) + ShieldBonus; } }
        public SortedSet<AvailableArmor> ArmorProficiencies { get; internal set; }
        public string Background { get; internal set; }
        private readonly List<string> _classes;
        public List<string> Classes { get { return _classes; } }
        public string ClassesString { get { return null; } }
        public Currency Currency { get; internal set; }
        public int MaxHp { get { return CalculateMaxHp(HitDice, Attributes.Constitution.Modifier); } }
        public List<int> HitDice { get; internal set; }
        public SortedSet<AvailableInstrument> AvailableInstrumentProficiencies { get; internal set; }
        public SortedSet<AvailableInstrument> ChosenInstrumentProficiencies { get; private set; }
        public int Initiative { get { return Attributes.Dexterity.Modifier; } }
        public SortedSet<AvailableLanguages> ChosenLanguages { get; internal set; }
        public string Name { get; set; }
        public string Race { get; internal set; }
        public CharacterAttributes Attributes { get; set; }
        public int ClassSkillCount { get; internal set; }
        public int LanguageCount { get; internal set; }
        public int ToolProficiencyCount { get; internal set; }
        public CharacterFeatures Features { get; internal set; }
        public int SkillCount { get; internal set; }
        public Armor EquippedArmor { get; internal set; }
        public bool HasShield { get; set; }
        public SortedSet<SavingThrow> SavingThrowProficiencies { get; internal set; }
        public string Size { get; internal set; }
        public SortedSet<AvailableSkill> AvailableSkills { get; internal set; }
        public SortedSet<AvailableSkill> ChosenSkills { get; internal set; }
        public int Speed { get; internal set; }
        public SortedSet<AvailableTool> AvailableToolProficiencies { get; internal set; }
        public SortedSet<AvailableTool> ChosenToolProficiencies { get; private set; }
        public SortedSet<AvailableWeapon> WeaponProficiencies { get; internal set; }

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
            ChosenSkills.Add(chosenSkill);
        }

        public void LearnTool(AvailableTool chosenTool)
        {
            ChosenToolProficiencies.Add(chosenTool);
        }

        private static int GetArmorClassBonus(Armor armor, int dex)
        {
            if (armor.MaxDexBonus == -1)
                return armor.BaseArmor + dex;

            if (dex > armor.MaxDexBonus)
                dex = armor.MaxDexBonus;

            return armor.BaseArmor + dex;
        }

        public static int CalculateMaxHp(List<int> hitDice, int constitutionMod)
        {
            return hitDice[0] + hitDice.GetRange(1, hitDice.Count - 1).Sum(hitDie => (hitDie / 2) + 1) + constitutionMod;
        }
    }
}

