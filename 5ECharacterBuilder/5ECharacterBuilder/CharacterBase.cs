using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace _5ECharacterBuilder
{
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
        bool HasShield { get; set; }
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
        ReadOnlyCollection<AvailableSkill> Skills { get; }
        ReadOnlyCollection<AvailableSkill> TrainedSkills { get; set; }
        int Speed { get; }
        ReadOnlyCollection<AvailableTool> ToolProficiencies { get; set; }
        ReadOnlyCollection<AvailableWeapon> WeaponProficiencies { get; }
        void PickSkill(AvailableSkill skill);

        void AddArmorProf(List<AvailableArmor> armors);
        void AddBackgroundSkills(List<AvailableSkill> skillList);
        void EquipArmor(AvailableArmor armor);
        void AddSavingThrows(List<SavingThrow> savingThrows);
        void AddInstrumentProfs(List<AvailableInstrument> instruments);
        void AddLanguages(List<AvailableLanguages> languages);
        void PickSkills(List<AvailableSkill> skillList);
        void AddToolProfs(List<AvailableTool> tools);
        void AddWeaponProfs(List<AvailableWeapon> weaponList);
        void SetAttributes(CharacterAttributes characterAttributes);
        void AddClassSkills(List<AvailableSkill> classSkills);
    }

    internal class CharacterBase : ICharacter
    {
        public CharacterBase(CharacterAttributeScores attributeScores = null, string name = "")
        {
            EquipArmor(AvailableArmor.Cloth);
            attributeScores = attributeScores ?? new CharacterAttributeScores();
            ArmorProficiencies = new ReadOnlyCollection<AvailableArmor>(new List<AvailableArmor>());
            //BackgroundSkills = new ReadOnlyCollection<AvailableSkill>(new List<AvailableSkill>());
            TrainedSkills = new ReadOnlyCollection<AvailableSkill>(new List<AvailableSkill>());
            ToolProficiencies = new ReadOnlyCollection<AvailableTool>(new List<AvailableTool>());
            InstrumentProficiencies = new ReadOnlyCollection<AvailableInstrument>(new List<AvailableInstrument>());
            WeaponProficiencies = new ReadOnlyCollection<AvailableWeapon>(new List<AvailableWeapon>());
            SavingThrowProficiencies = new ReadOnlyCollection<SavingThrow>(new List<SavingThrow>());
            //ClassSkills = new ReadOnlyCollection<AvailableSkill>(new List<AvailableSkill>());
            Name = name;
            Attributes = new CharacterAttributes(attributeScores);
            HitDice = new List<int>(new int[0]);
            Currency = new Currency();
            Languages = new ReadOnlyCollection<AvailableLanguages>(new List<AvailableLanguages>());
            //Skills = new ReadOnlyCollection<AvailableSkill>(new AvailableSkill[] {});
        }

        public CharacterAttributes Attributes { get; set; }
        public string Background { get; private set; }
        public ReadOnlyCollection<AvailableSkill> BackgroundSkills { get; private set; }
        public List<int> HitDice { get; private set; }
        public string Class { get; private set; }
        public int ClassSkillCount { get; private set; }
        public ReadOnlyCollection<AvailableSkill> ClassSkills { get; private set; }
        public Currency Currency { get; private set; }
        public Armor EquippedArmor { get; set; }
        public int MaxHp { get { return CalculateMaxHp(HitDice, Attributes.Constitution.Modifier); } }
        public string Name { get; set; }
        public ReadOnlyCollection<AvailableSkill> TrainedSkills { get; set; }
        public ReadOnlyCollection<AvailableArmor> ArmorProficiencies { get; private set; }
        public ReadOnlyCollection<AvailableWeapon> WeaponProficiencies { get; private set; }
        public ReadOnlyCollection<AvailableTool> ToolProficiencies { get; set; }
        public ReadOnlyCollection<AvailableInstrument> InstrumentProficiencies { get; private set; }
        public ReadOnlyCollection<SavingThrow> SavingThrowProficiencies { get; private set; }
        public int ShieldBonus { get { return HasShield ? 2 : 0; } }
        public ReadOnlyCollection<AvailableSkill> Skills { get; private set; } 
        public string Race { get; private set; }
        public int Initiative { get { return Attributes.Dexterity.Modifier; } }
        public int TrainedSkillsCount { get { return TrainedSkills.Count; } }
        public int Speed { get; private set; }
        public ReadOnlyCollection<AvailableLanguages> Languages { get; private set; }
        public bool HasShield { get; set; }
        public string Size { get; private set; }

        public int ArmorClass
        {
            get
            {
                return GetArmorClassBonus(EquippedArmor, Attributes.Dexterity.Modifier) + ShieldBonus;
            }
        }

        public List<string> RuleIssues
        {
            get
            {
                var currentIssues = new List<string>();

                if (EquippedArmor.Name != AvailableArmor.Cloth.ToString())
                    if (!ArmorProficiencies.Contains((AvailableArmor)Enum.Parse(typeof(AvailableArmor), EquippedArmor.Name)))
                        currentIssues.Add(String.Format("Character is not proficient with {0} Armor. Penalties will apply.", EquippedArmor.Name));

                if (HasShield)
                    if (!ArmorProficiencies.Contains(AvailableArmor.Shield))
                        currentIssues.Add("Character is not proficient with Shields. Penalties will apply.");

                return currentIssues;
            }
        }

        public void PickSkill(AvailableSkill skill)
        {
            PickSkills(new List<AvailableSkill>{skill});
        }

        public void PickSkills(List<AvailableSkill> skillList)
        {
            var currentSkills = TrainedSkills.ToList();
            currentSkills.AddRange(skillList);
            TrainedSkills = new ReadOnlyCollection<AvailableSkill>(currentSkills);
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

        public void AddClassSkills(List<AvailableSkill> classSkills)
        {
            var currentClassSkills = ClassSkills.ToList();
            currentClassSkills.AddRange(classSkills);
            ClassSkills = new ReadOnlyCollection<AvailableSkill>(currentClassSkills);
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

