using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace _5ECharacterBuilder
{
    public class Character
    {
        private readonly ICharacter _character;
        
        public Character(AvailableRaces characterRace, AvailableClasses characterClass, AvailableBackgrounds characterBackground)
        {
            var characterFactory = new CharacterFactory();
            _character = characterFactory.BuildACharacter(characterRace, characterClass, characterBackground);
        }

        public Character(AvailableRaces characterRace, AvailableClasses characterClass, AvailableBackgrounds characterBackground, CharacterAttributeScores scores)
        {
            var characterFactory = new CharacterFactory();
            _character = characterFactory.BuildACharacter(characterRace, characterClass, characterBackground);
            _character.SetAttributes(new CharacterAttributes(scores));
        }

        public Character(AvailableRaces characterRace, AvailableClasses characterClass, AvailableBackgrounds characterBackground, int level)
        {
            var characterFactory = new CharacterFactory();
            _character = characterFactory.BuildACharacter(characterRace, characterClass, characterBackground);
            for (var l = 1; l < level; l++)
                _character = characterFactory.AddClass(_character, characterClass);

        }

        public int ArmorClass { get { return _character.ArmorClass; } }
        public ReadOnlyCollection<AvailableArmor> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public CharacterAttributes Attributes { get { return _character.Attributes; } set { _character.Attributes = value; } }
        public string Background { get { return _character.Background; } }
        public string Class {get { return _character.Class; } }
        public int ClassSkillCount { get { return _character.ClassSkillCount; } }
        public Currency Currency { get { return _character.Currency; } }
        public Armor EquippedArmor { get { return _character.EquippedArmor; } }
        public bool HasShieldEquipped {get { return _character.HasSheild; } set { _character.HasSheild = value; }}
        public List<int> HitDice { get { return _character.HitDice; } }
        public int Initiative { get { return _character.Initiative; } }
        public ReadOnlyCollection<AvailableInstrument> InstrumentProficiencies { get { return _character.InstrumentProficiencies; } }
        public ReadOnlyCollection<AvailableLanguages> Languages { get { return _character.Languages; } }
        public int MaxHp { get { return _character.MaxHp; } }
        public string Name { get { return _character.Name; } set { _character.Name = value; } }
        public string Race{get { return _character.Race; } }
        public ReadOnlyCollection<SavingThrow> SavingThrowProficiencies { get { return _character.SavingThrowProficiencies; } }
        public string Size { get { return _character.Size; } }
        public ReadOnlyCollection<AvailableSkill> SkillProficiencies { get { return _character.SkillProficiencies; } }
        public ReadOnlyCollection<AvailableSkill> Skills { get { return new ReadOnlyCollection<AvailableSkill>(_character.ClassSkills.Union(_character.BackgroundSkills.ToList()).ToList()); } }
        public int Speed { get { return _character.Speed; } }
        public ReadOnlyCollection<AvailableTool> ToolProficiencies { get { return _character.ToolProficiencies; } }
        public ReadOnlyCollection<AvailableWeapon> WeaponProficiencies { get { return _character.WeaponProficiencies; } }

        public void AddSkills(List<AvailableSkill> skillList)
        {
            _character.AddClassSkills(skillList);
        }

        public void AddInstrumentProfs(List<AvailableInstrument> instrument)
        {
            _character.AddInstrumentProfs(instrument);
        }

        public void AddToolProfs(List<AvailableTool> tool)
        {
            _character.AddToolProfs(tool);
        }

        public void AddLanguages(List<AvailableLanguages> languages)
        {
            _character.AddLanguages(languages);
        }
        
        public void EquipArmor(AvailableArmor armor)
        {
            _character.EquipArmor(armor);
        }

        public List<string> RuleIssues
        {
            get
            {
                var currentIssues = _character.RuleIssues;

                if (!ArmorProficiencies.Contains((AvailableArmor)Enum.Parse(typeof(AvailableArmor), EquippedArmor.Name)))
                    currentIssues.Add(String.Format("Character is not proficient with {0} Armor. Penalties will apply.", EquippedArmor.Name));

                if(HasShieldEquipped)
                    if (!ArmorProficiencies.Contains(AvailableArmor.Shield))
                        currentIssues.Add("Character is not proficient with Shields. Penalties will apply.");
                    
                return currentIssues;
            }
        }
    }

    public interface ICharacter
    {
        int ArmorClass { get; }
        ReadOnlyCollection<AvailableArmor> ArmorProficiencies { get; }
        CharacterAttributes Attributes { get; set; }
        string Background { get; }
        ReadOnlyCollection<AvailableSkill> BackgroundSkills { get; }
        string Class { get; }
        int ClassSkillCount { get; }
        Currency Currency { get; }
        ReadOnlyCollection<AvailableSkill> ClassSkills { get; }
        Armor EquippedArmor { get; }
        bool HasSheild { get; set; }
        List<int> HitDice { get; }
        int Initiative { get; }
        ReadOnlyCollection<AvailableInstrument> InstrumentProficiencies { get; }
        ReadOnlyCollection<AvailableLanguages> Languages { get; }
        int MaxHp { get; }
        string Name { get; set; }
        string Race { get; }
        List<string> RuleIssues { get; }
        ReadOnlyCollection<SavingThrow> SavingThrowProficiencies { get; }
        string Size { get; }
        ReadOnlyCollection<AvailableSkill> SkillProficiencies { get; set; }
        int Speed { get; }
        ReadOnlyCollection<AvailableTool> ToolProficiencies { get; set; }
        ReadOnlyCollection<AvailableWeapon> WeaponProficiencies { get; }

        void AddArmorProf(List<AvailableArmor> armors);
        void AddBackgroundSkills(List<AvailableSkill> skillList);
        void EquipArmor(AvailableArmor armor);
        void AddSavingThrows(List<SavingThrow> savingThrows);
        void AddInstrumentProfs(List<AvailableInstrument> instruments);
        void AddLanguages(List<AvailableLanguages> languages);
        void AddClassSkills(List<AvailableSkill> skillList);
        void AddToolProfs(List<AvailableTool> tools);
        void AddWeaponProfs(List<AvailableWeapon> weaponList);
        void SetAttributes(CharacterAttributes characterAttributes);
    }

    internal class CharacterBase : ICharacter
    {
        public CharacterBase(CharacterAttributeScores attributeScores = null, string name = "")
        {
            EquipArmor(AvailableArmor.Cloth);
            attributeScores = attributeScores ?? new CharacterAttributeScores();
            ArmorProficiencies = new ReadOnlyCollection<AvailableArmor>(new List<AvailableArmor>());
            BackgroundSkills = new ReadOnlyCollection<AvailableSkill>(new List<AvailableSkill>());
            SkillProficiencies = new ReadOnlyCollection<AvailableSkill>(new List<AvailableSkill>());
            ToolProficiencies = new ReadOnlyCollection<AvailableTool>(new List<AvailableTool>());
            InstrumentProficiencies = new ReadOnlyCollection<AvailableInstrument>(new List<AvailableInstrument>());
            WeaponProficiencies = new ReadOnlyCollection<AvailableWeapon>(new List<AvailableWeapon>());
            SavingThrowProficiencies = new ReadOnlyCollection<SavingThrow>(new List<SavingThrow>());
            ClassSkills = new ReadOnlyCollection<AvailableSkill>(new List<AvailableSkill>());
            Name = name;
            Attributes = new CharacterAttributes(attributeScores);
            HitDice = new List<int>(new int[0]);
            RuleIssues = new List<string>();
            Currency = new Currency();
            Languages = new ReadOnlyCollection<AvailableLanguages>(new List<AvailableLanguages>());
        }

        public int SkillProficiencyCount { get { return SkillProficiencies.Count; } }
        public int ShieldBonus { get { return HasSheild ? 2 : 0; } }

        public CharacterAttributes Attributes { get; set; }
        public string Background { get; private set; }
        public ReadOnlyCollection<AvailableSkill> BackgroundSkills { get; private set; }
        public List<int> HitDice { get; private set; }
        public string Class { get; private set; }
        public Armor EquippedArmor { get; private set; }
        public int MaxHp { get { return CalculateMaxHp(HitDice, Attributes.Constitution.Modifier); } }
        public string Name { get; set; }
        public ReadOnlyCollection<AvailableSkill> SkillProficiencies { get; set; }
        public ReadOnlyCollection<AvailableArmor> ArmorProficiencies { get; private set; }
        public ReadOnlyCollection<AvailableWeapon> WeaponProficiencies { get; private set; }
        public List<string> RuleIssues { get; private set; }
        public ReadOnlyCollection<AvailableTool> ToolProficiencies { get; set; }
        public ReadOnlyCollection<AvailableInstrument> InstrumentProficiencies { get; private set; }
        public ReadOnlyCollection<SavingThrow> SavingThrowProficiencies { get; private set; }
        public ReadOnlyCollection<AvailableSkill> ClassSkills { get; private set; }
        public string Race { get; private set; }
        public int Initiative { get { return Attributes.Dexterity.Modifier; } }
        public int Speed { get; private set; }
        public int ClassSkillCount { get; private set; }
        public Currency Currency { get; private set; }
        public ReadOnlyCollection<AvailableLanguages> Languages { get; private set; }
        public bool HasSheild { get; set; }
        public string Size { get; private set; }

        public int ArmorClass
        {
            get
            {
                return GetArmorClassBonus(EquippedArmor, Attributes.Dexterity.Modifier) + ShieldBonus;
            }
        }

        public void AddClassSkills(List<AvailableSkill> skillList)
        {
            var currentSkills = SkillProficiencies.ToList();
            currentSkills.AddRange(skillList);
            SkillProficiencies = new ReadOnlyCollection<AvailableSkill>(currentSkills);
        }

        public void AddWeaponProfs(List<AvailableWeapon> weaponList)
        {
            var currentWeapons = WeaponProficiencies.ToList();
            currentWeapons.AddRange(weaponList);
            WeaponProficiencies = new ReadOnlyCollection<AvailableWeapon>(currentWeapons);
        }

        public void EquipArmor(AvailableArmor armor) { EquippedArmor = Armory.GetArmor(armor); }

        public void AddSavingThrows(List<SavingThrow> savingThrows)
        {
            var currentSavingThrows = SavingThrowProficiencies.ToList();
            currentSavingThrows.AddRange(savingThrows);
            SavingThrowProficiencies = new ReadOnlyCollection<SavingThrow>(currentSavingThrows);
        }

        public void AddToolProfs(List<AvailableTool> tools)
        {
            var currentTools = ToolProficiencies.ToList();
            currentTools.AddRange(tools);
            ToolProficiencies = new ReadOnlyCollection<AvailableTool>(currentTools);
        }

        public void AddInstrumentProfs(List<AvailableInstrument> instruments)
        {
            var currentInstruments = InstrumentProficiencies.ToList();
            currentInstruments.AddRange(instruments);
            InstrumentProficiencies = new ReadOnlyCollection<AvailableInstrument>(currentInstruments);
        }

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

        public void AddLanguages(List<AvailableLanguages> languages)
        {
            var currentLanguages = Languages.ToList();
            currentLanguages.AddRange(languages);
            Languages = new ReadOnlyCollection<AvailableLanguages>(currentLanguages);
        }

        public void AddBackgroundSkills(List<AvailableSkill> skillList)
        {
            var currentBackgroundSkills = BackgroundSkills.ToList();
            currentBackgroundSkills.AddRange(skillList);
            BackgroundSkills = new ReadOnlyCollection<AvailableSkill>(currentBackgroundSkills);
        }

        public void EquipArmor(Armor armor)
        {
            EquippedArmor = armor;
        }

        public void AddArmorProf(List<AvailableArmor> armors)
        {
            var currentArmor = ArmorProficiencies.ToList();
            currentArmor.AddRange(armors);
            ArmorProficiencies = new ReadOnlyCollection<AvailableArmor>(currentArmor);
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

