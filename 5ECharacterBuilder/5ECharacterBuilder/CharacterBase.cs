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
        List<AvailableLanguages> BackgroundLanguages { get; }
        int BackgroundLanguageCount { get; }
        List<AvailableSkill> BackgroundSkills { get;}
        List<string> Classes { get; }
        string ClassesString { get; }
        int ClassSkillCount { get; }
        Currency Currency { get; }
        List<AvailableSkill> ClassSkills { get; }
        Armor EquippedArmor { get; }
        bool HasShield { get; }
        List<int> HitDice { get; }
        int Initiative { get; }
        List<AvailableInstrument> InstrumentProficiencies { get; }
        List<AvailableLanguages> Languages { get; }
        int MaxHp { get; }
        string Name { get; }
        string Race { get; }
        List<AvailableLanguages> RaceLanguages { get; }
        int RaceLanguageCount { get; }
        List<string> RuleIssues { get; }
        List<SavingThrow> SavingThrowProficiencies { get; }
        string Size { get; }
        List<AvailableSkill> Skills { get; }
        List<AvailableSkill> TrainedSkills { get; }
        int Speed { get; }
        List<AvailableTool> ToolProficiencies { get; }
        List<AvailableWeapon> WeaponProficiencies { get; }

        void EquipArmor(AvailableArmor armor);
        void SetAttributes(CharacterAttributes characterAttributes);
        void ToggleShield();
        void SetName(string name);
    }

    class CharacterBase : ICharacter
    {
        public CharacterBase(CharacterAttributeScores attributeScores = null, string name = "")
        {
            EquipArmor(AvailableArmor.Cloth);
            attributeScores = attributeScores ?? new CharacterAttributeScores();
            Attributes = new CharacterAttributes(attributeScores);
            ArmorProficiencies = new List<AvailableArmor>(new List<AvailableArmor>());
            BackgroundLanguages = new List<AvailableLanguages>();
            BackgroundSkills = new List<AvailableSkill>(new List<AvailableSkill>());
            TrainedSkills = new List<AvailableSkill>(new List<AvailableSkill>());
            ToolProficiencies = new List<AvailableTool>(new List<AvailableTool>());
            InstrumentProficiencies = new List<AvailableInstrument>(new List<AvailableInstrument>());
            WeaponProficiencies = new List<AvailableWeapon>(new List<AvailableWeapon>());
            SavingThrowProficiencies = new List<SavingThrow>(new List<SavingThrow>());
            ClassSkills = new List<AvailableSkill>(new List<AvailableSkill>());
            Name = name;
            HitDice = new List<int>(new int[0]);
            Currency = new Currency();
            RaceLanguages = new List<AvailableLanguages>(new List<AvailableLanguages>());
            _classes = new List<string>();
        }

        private int ShieldBonus { get { return HasShield ? 2 : 0; } }

        public int ArmorClass { get {  return GetArmorClassBonus(EquippedArmor, Attributes.Dexterity.Modifier) + ShieldBonus; } }
        public List<AvailableArmor> ArmorProficiencies { get; internal set; }
        public string Background { get; internal set; }
        private readonly List<string> _classes;
        public List<string> Classes { get { return _classes; } }
        public string ClassesString { get { return ""; } }
        public Currency Currency { get; internal set; }
        public int MaxHp { get { return CalculateMaxHp(HitDice, Attributes.Constitution.Modifier); } }
        public List<int> HitDice { get; internal set; }
        public List<AvailableInstrument> InstrumentProficiencies { get; internal set; }
        public int Initiative { get { return Attributes.Dexterity.Modifier; } }
        public List<AvailableLanguages> Languages { get; internal set; }
        public string Name { get; set; }
        public string Race { get; internal set; }
        public List<AvailableLanguages> RaceLanguages { get; internal set; }
        public int RaceLanguageCount { get; internal set; }
        public CharacterAttributes Attributes { get; set; }
        public List<AvailableLanguages> BackgroundLanguages { get; internal set; }
        public int BackgroundLanguageCount { get; internal set; }
        public List<AvailableSkill> BackgroundSkills { get; internal set; }
        public int ClassSkillCount { get; internal set; }
        public List<AvailableSkill> ClassSkills { get; internal set; }
        public Armor EquippedArmor { get; internal set; }
        public bool HasShield { get; set; }

        public List<string> RuleIssues
        {
            get
            {
                var currentIssues = new List<string>();

                if (EquippedArmor.Name != AvailableArmor.Cloth.ToString())
                    if (!ArmorProficiencies.Contains((AvailableArmor)Enum.Parse(typeof(AvailableArmor), EquippedArmor.Name)))
                        currentIssues.Add(String.Format("Character is not proficient with {0} Armor. Penalties will apply.", EquippedArmor.Name));

                if (HasShield && !ArmorProficiencies.Contains(AvailableArmor.Shield))
                        currentIssues.Add("Character is not proficient with Shields. Penalties will apply.");

                return currentIssues;
            }
        }

        public List<SavingThrow> SavingThrowProficiencies { get; internal set; }
        public string Size { get; internal set; }
        public List<AvailableSkill> Skills { get; internal set; }
        public List<AvailableSkill> TrainedSkills { get; internal set; }
        public int Speed { get; internal set; }
        public List<AvailableTool> ToolProficiencies { get; internal set; }
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

