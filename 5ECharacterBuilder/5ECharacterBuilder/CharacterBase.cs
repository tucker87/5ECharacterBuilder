using System;
using System.Collections.Generic;
using System.Linq;
using _5ECharacterBuilder.CharacterClasses;

namespace _5ECharacterBuilder
{
    public abstract class Character
    {
        public abstract int ArmorClass { get; }
        public abstract List<AvailableArmor> ArmorProficiencies { get; internal set; }
        public abstract CharacterAttributes Attributes { get; set; }
        public abstract string Background { get; internal set; }
        public abstract List<AvailableLanguages> BackgroundLanguages { get; internal set; }
        public abstract int BackgroundLanguageCount { get; internal set; }
        public abstract List<AvailableSkill> BackgroundSkills { get; internal set; }
        internal abstract string Class { get; set; }
        public abstract List<string> Classes { get; } 
        public abstract int ClassSkillCount { get; internal set; }
        public abstract Currency Currency { get; internal set; }
        public abstract List<AvailableSkill> ClassSkills { get; internal set; }
        public abstract Armor EquippedArmor { get; internal set; }
        public abstract bool HasShield { get; set; }
        public abstract List<int> HitDice { get; internal set; }
        public abstract int Initiative { get; }
        public abstract List<AvailableInstrument> InstrumentProficiencies { get; internal set; }
        public abstract List<AvailableLanguages> Languages { get; internal set; }
        public abstract int MaxHp { get; }
        public abstract string Name { get; set; }
        public abstract string Race { get; internal set; }
        public abstract List<AvailableLanguages> RaceLanguages { get; internal set; }
        public abstract int RaceLanguageCount { get; internal set; }
        public abstract List<string> RuleIssues { get; }
        public abstract List<SavingThrow> SavingThrowProficiencies { get; internal set; }
        public abstract string Size { get; internal set; }
        public abstract List<AvailableSkill> Skills { get; internal set; }
        public abstract List<AvailableSkill> TrainedSkills { get; internal set; }
        public abstract int Speed { get; internal set; }
        public abstract List<AvailableTool> ToolProficiencies { get; internal set; }
        public abstract List<AvailableWeapon> WeaponProficiencies { get; internal set; }

        public abstract void EquipArmor(AvailableArmor armor);
    }

    internal sealed class CharacterBase : Character
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
        }

        public override int ArmorClass { get {  return GetArmorClassBonus(EquippedArmor, Attributes.Dexterity.Modifier) + ShieldBonus; } }
        public override List<AvailableArmor> ArmorProficiencies { get; internal set; }
        public override string Background { get; internal set; }
        internal override string Class { get; set; }
        public override List<string> Classes { get { return new List<string>(); } }
        public override Currency Currency { get; internal set; }
        public override int MaxHp { get { return CalculateMaxHp(HitDice, Attributes.Constitution.Modifier); } }
        public override List<int> HitDice { get; internal set; }
        public override List<AvailableInstrument> InstrumentProficiencies { get; internal set; }
        public override int Initiative { get { return Attributes.Dexterity.Modifier; } }
        public override List<AvailableLanguages> Languages { get; internal set; }
        public override string Name { get; set; }
        public override string Race { get; internal set; }
        public override List<AvailableLanguages> RaceLanguages { get; internal set; }
        public override int RaceLanguageCount { get; internal set; }
        private int ShieldBonus { get { return HasShield ? 2 : 0; } }
        public override CharacterAttributes Attributes { get; set; }
        public override List<AvailableLanguages> BackgroundLanguages { get; internal set; }
        public override int BackgroundLanguageCount { get; internal set; }
        public override List<AvailableSkill> BackgroundSkills { get; internal set; }
        public override int ClassSkillCount { get; internal set; }
        public override List<AvailableSkill> ClassSkills { get; internal set; }
        public override Armor EquippedArmor { get; internal set; }
        public override bool HasShield { get; set; }

        public override List<string> RuleIssues
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

        public override List<SavingThrow> SavingThrowProficiencies { get; internal set; }
        public override string Size { get; internal set; }
        public override List<AvailableSkill> Skills { get; internal set; }
        public override List<AvailableSkill> TrainedSkills { get; internal set; }
        public override int Speed { get; internal set; }
        public override List<AvailableTool> ToolProficiencies { get; internal set; }
        public override List<AvailableWeapon> WeaponProficiencies { get; internal set; }

        public override void EquipArmor(AvailableArmor armor) { EquippedArmor = Armory.GetArmor(armor); }
        
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

