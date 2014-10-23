using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace _5ECharacterBuilder
{
    public class Character
    {
        private readonly ICharacter _character;
        
        public Character(AvailableRaces characterRace, AvailableClasses characterClass)
        {
            var characterFactory = new CharacterFactory();
            _character = characterFactory.BuildACharacter(characterRace, characterClass);
        }

        public Character(AvailableRaces characterRace, AvailableClasses characterClass, CharacterAttributeScores scores)
        {
            var characterFactory = new CharacterFactory();
            _character = characterFactory.BuildACharacter(characterRace, characterClass);
            _character.SetAttributes(new CharacterAttributes(scores));
        }

        public Character(AvailableRaces characterRace, AvailableClasses characterClass, int level)
        {
            var characterFactory = new CharacterFactory();
            _character = characterFactory.BuildACharacter(characterRace, characterClass);
            for (var l = 1; l < level; l++)
                _character = characterFactory.AddClass(_character, characterClass);

        }

        public CharacterAttributes Attributes
        {
            get { return _character.Attributes; }
            set { _character.Attributes = value; }
        }

        public int MaxHp { get { return _character.MaxHp; } }
        public List<int> HitDice { get { return _character.HitDice; } }
        public string Name
        {
            get { return _character.Name; }
            set { _character.Name = value; }
        }

        public ReadOnlyCollection<AvailableSkill> SkillProficiencies
        {
            get { return _character.SkillProficiencies; }
        }

        public string Race{get { return _character.Race; } }
        public string Class {get { return _character.Class; } }
        public ReadOnlyCollection<AvailableArmor> EquippedArmors { get { return _character.EquippedArmors; } }
        public ReadOnlyCollection<AvailableArmor> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public ReadOnlyCollection<AvailableWeapon> WeaponProficiencies { get { return _character.WeaponProficiencies; } }
        public List<string> RuleIssues { get { return _character.RuleIssues; } }
        public ReadOnlyCollection<AvailableTool> ToolProficiencies { get { return _character.ToolProficiencies; } }
        public ReadOnlyCollection<AvailableInstrument> InstrumentProficiencies { get { return _character.InstrumentProficiencies; } }
        public ReadOnlyCollection<SavingThrow> SavingThrowProficiencies { get { return _character.SavingThrowProficiencies; } }
        public int Initiative { get { return _character.Initiative; } }
        public int Speed { get { return _character.Speed; } }
        public ReadOnlyCollection<AvailableSkill> ClassSkills { get { return _character.ClassSkills; } }
        public int ClassSkillCount { get { return _character.CLassSkillCount; } }
        public Currency Currency { get { return _character.Currency; } }
        public int ArmorClass { get { return _character.ArmorClass; } }
        public ReadOnlyCollection<AvailableLanguages> Languages { get { return _character.Languages; } }
        public bool HasShieldEquipped {get { return _character.HasSheild; } set { _character.HasSheild = value; }}

        public int CalculateMaxHp() { return _character.MaxHp; }
        public static int CalculateMaxHp(List<int> hitDice, int constitution)
        {
            return CharacterBase.CalculateMaxHp(hitDice, constitution);
        }

        public List<string> VerifyCharacter()
        {
            throw new System.NotImplementedException();
        }

        public void AddSkills(List<AvailableSkill> skillList)
        {
            _character.AddSkills(skillList);
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

        public void AddArmor(AvailableArmor armor)
        {
            AddArmors(new List<AvailableArmor> { armor });
        }

        public void AddArmors(List<AvailableArmor> armors)
        {
            _character.AddEquippedArmors(armors);
        }
    }

    public class Currency
    {
        public int Gold { get; private set; }
        public int Silver { get; private set; }
        public int Copper { get; private set; }
    }

    public interface ICharacter
    {
        CharacterAttributes Attributes { get; set; }
        List<int> HitDice { get; }
        int MaxHp { get; }
        string Name { get; set; }
        ReadOnlyCollection<AvailableSkill> SkillProficiencies { get; set; }
        ReadOnlyCollection<AvailableArmor> EquippedArmors { get; }
        ReadOnlyCollection<AvailableArmor> ArmorProficiencies { get; }
        ReadOnlyCollection<AvailableInstrument> InstrumentProficiencies { get; }
        ReadOnlyCollection<SavingThrow> SavingThrowProficiencies { get; }
        ReadOnlyCollection<AvailableTool> ToolProficiencies { get; }
        ReadOnlyCollection<AvailableWeapon> WeaponProficiencies { get; }
        List<string> RuleIssues { get; }
        ReadOnlyCollection<AvailableSkill> ClassSkills { get; }
        string Race { get; }
        string Class { get; }
        int Initiative { get; }
        int Speed { get; }
        int CLassSkillCount { get; }
        Currency Currency { get; }
        int ArmorClass { get; }
        ReadOnlyCollection<AvailableLanguages> Languages { get; }
        bool HasSheild { get; set; }
        List<string> VerifyCharacter();
        void AddSkills(List<AvailableSkill> skillList);
        void AddWeaponProfs(List<AvailableWeapon> weaponList);
        void AddSavingThrows(List<SavingThrow> savingThrows);
        void AddToolProfs(List<AvailableTool> tools);
        void AddInstrumentProfs(List<AvailableInstrument> instruments);
        void SetAttributes(CharacterAttributes characterAttributes);
        void AddLanguages(List<AvailableLanguages> languages);
        void AddEquippedArmors(List<AvailableArmor> armors);
        void AddArmorProf(List<AvailableArmor> armors);
    }

    internal class CharacterBase : ICharacter
    {
        public CharacterBase(CharacterAttributeScores attributeScores = null, string name = "")
        {
            attributeScores = attributeScores ?? new CharacterAttributeScores();
            ArmorProficiencies = new ReadOnlyCollection<AvailableArmor>(new List<AvailableArmor>());
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
            EquippedArmors = new ReadOnlyCollection<AvailableArmor>(new List<AvailableArmor>());
        }
        
        public CharacterAttributes Attributes { get; set; }
        public List<int> HitDice { get; private set; }
        public int MaxHp { get { return CalculateMaxHp(HitDice, Attributes.Constitution.Modifier); } }
        public string Name { get; set; }
        public ReadOnlyCollection<AvailableSkill> SkillProficiencies { get; set; }
        public int SkillProficiencyCount { get { return SkillProficiencies.Count; } }
        public ReadOnlyCollection<AvailableArmor> ArmorProficiencies { get; private set; }
        public ReadOnlyCollection<AvailableArmor> EquippedArmors { get; private set; }
        public ReadOnlyCollection<AvailableWeapon> WeaponProficiencies { get; private set; }
        public List<string> RuleIssues { get; private set; }
        public ReadOnlyCollection<AvailableTool> ToolProficiencies { get; private set; }
        public ReadOnlyCollection<AvailableInstrument> InstrumentProficiencies { get; private set; }
        public ReadOnlyCollection<SavingThrow> SavingThrowProficiencies { get; private set; }
        public ReadOnlyCollection<AvailableSkill> ClassSkills { get; private set; }
        public string Race { get; private set; }
        public string Class { get; private set; }
        public int Initiative { get { return Attributes.Dexterity.Modifier; } }
        public int Speed { get; private set; }
        public int CLassSkillCount { get; private set; }
        public Currency Currency { get; private set; }

        public int ArmorClass
        {
            get
            {
                if (EquippedArmors.Count == 0)
                    AddEquippedArmor(AvailableArmor.Cloth);
                return EquippedArmors.Sum(a => GetArmorClassBonus(a, Attributes.Dexterity.Modifier)) + ShieldBonus;
            }
        }

       
        public int ShieldBonus { get { return HasSheild ? 2 : 0; } }

        private static int GetArmorClassBonus(AvailableArmor armor, int dex)
        {
            var armory = new Armory();
            return armory.GetArmorClassBonus(armor, dex);
        }

        public ReadOnlyCollection<AvailableLanguages> Languages { get; private set; }
        public bool HasSheild { get; set; }

        public List<string> VerifyCharacter()
        {
            return RuleIssues;
        }

        public void AddSkills(List<AvailableSkill> skillList)
        {
            var currentSkills = SkillProficiencies.ToList();
            SkillProficiencies = new ReadOnlyCollection<AvailableSkill>(currentSkills);
        }

        public void AddWeaponProfs(List<AvailableWeapon> weaponList)
        {
            var currentWeapons = WeaponProficiencies.ToList();
            currentWeapons.AddRange(weaponList);
            WeaponProficiencies = new ReadOnlyCollection<AvailableWeapon>(currentWeapons);
        }

        public void AddSavingThrows(List<SavingThrow> savingThrows)
        {
            SavingThrowProficiencies = new ReadOnlyCollection<SavingThrow>(savingThrows);
        }

        public void AddToolProfs(List<AvailableTool> tools)
        {
            ToolProficiencies = new ReadOnlyCollection<AvailableTool>(tools);
        }

        public void AddInstrumentProfs(List<AvailableInstrument> instruments)
        {
            InstrumentProficiencies = new ReadOnlyCollection<AvailableInstrument>(instruments);
        }

        public static int CalculateMaxHp(List<int> hitDice, int constitutionMod)
        {
            return hitDice[0] + hitDice.GetRange(1, hitDice.Count - 1).Sum(hitDie => (hitDie / 2) + 1) + constitutionMod;
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
            var currentLanguages = new List<AvailableLanguages>(Languages);
            currentLanguages.AddRange(languages);
            Languages = new ReadOnlyCollection<AvailableLanguages>(currentLanguages);
        }

        private void AddEquippedArmor(AvailableArmor armor)
        {
            AddEquippedArmors(new List<AvailableArmor>{armor});
        }

        public void AddEquippedArmors(List<AvailableArmor> armors)
        {
            var currentArmor = new List<AvailableArmor>(EquippedArmors);
            currentArmor.AddRange(armors);
            EquippedArmors = new ReadOnlyCollection<AvailableArmor>(currentArmor);
        }

        public void AddArmorProf(List<AvailableArmor> armors)
        {
            var currentArmor = new List<AvailableArmor>(ArmorProficiencies);
            currentArmor.AddRange(armors);
            ArmorProficiencies = new ReadOnlyCollection<AvailableArmor>(currentArmor);
        }
    }
}

