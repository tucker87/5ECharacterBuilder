using System;
using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder
{
    public interface ICharacter
    {
        int ArmorClass { get; }
        SortedSet<AvailableArmor> ArmorProficiencies { get; }
        CharacterAbilities Abilities { get; }
        string Background { get; }
        List<string> Classes { get; }
        ClassPath ClassPath { get; }
        string ClassesString { get; }
        Currency Currency { get; }
        Armor EquippedArmor { get; }
        bool HasShield { get; }
        HitDice HitDice { get; }
        int Initiative { get; }
        Proficiencies<AvailableInstrument> Instruments { get; }
        int Level { get; }
        int MaxHp { get; }
        string Name { get; }
        int ProficiencyBonus { get; }
        string Race { get; }
        Languages Languages { get; }
        SortedSet<SavingThrow> SavingThrows { get; }
        string Size { get; }
        Skills Skills { get; }
        int Speed { get; }
        Tools Tools { get; }
        SortedSet<AvailableWeapon> WeaponProficiencies { get; }
        CharacterFeatures Features { get; }
        int MartialArts { get; }
        int AttacksPerTurn { get; }
        int SneakAttackDice { get; }
        SortedSet<SpellcastingClass> SpellcastingClasses { get; }
        ClassTraits ClassTraits { get; }
        void EquipArmor(AvailableArmor armor);
        void SetAttributes(CharacterAbilities characterAbilities);
        void ToggleShield();
        void SetName(string name);
        void ChooseSkill(AvailableSkill chosenSkill);
        void LearnTool(AvailableTool chosenTool);
        void LearnInstrument(AvailableInstrument chosenInstrument);
        void LearnLanguage(AvailableLanguages chosenLanguage);
        void ChosePath(AvailablePaths chosenPath);
        void ImproveAbility(string ability);
        void ChooseExpertise(AvailableSkill skill);
        void ChooseExpertise(AvailableTool tool);
        int ClassLevel(string className);
        int SkillBonus(AvailableSkill skill);
        void LevelUp(AvailableClasses cclass);
        void LevelDown();
    }

    internal class CharacterBase : ICharacter
    {
        public CharacterBase(CharacterAbilityScores abilityScores = null, string name = "")
        {
            Name = name;
            abilityScores = abilityScores ?? new CharacterAbilityScores();
            EquipArmor(AvailableArmor.Cloth);
            AttacksPerTurn = 1;
            Abilities = new CharacterAbilities(abilityScores);
        }

        private int ShieldBonus => HasShield ? 2 : 0;
        public int ArmorClass => GetArmorClassBonus(EquippedArmor, Abilities.Dexterity.Modifier) + ShieldBonus;
        public SortedSet<AvailableArmor> ArmorProficiencies { get; } = new SortedSet<AvailableArmor>(new List<AvailableArmor>());
        public CharacterAbilities Abilities { get; internal set; }
        public string Background { get; internal set; }
        public List<string> Classes { get; } = new List<string>();
        public ClassPath ClassPath { get; } = new ClassPath();
        public string ClassesString { get; internal set; }
        public Currency Currency { get; } = new Currency();
        public Armor EquippedArmor { get; internal set; }
        public bool HasShield { get; internal set; }
        public HitDice HitDice { get; } = new HitDice();
        public int Initiative { get; internal set; }
        public Proficiencies<AvailableInstrument> Instruments { get; } = new Proficiencies<AvailableInstrument>();
        public int Level => Classes.Count;

        public int MaxHp => CalculateMaxHp();

        public string Name { get; private set; }

        public int ProficiencyBonus => Level >= 17 ? 6 : Level >= 13 ? 5 : Level >= 9 ? 4 : Level >= 5 ? 3 : 2;

        public string Race { get; internal set; }
        public Languages Languages { get; } = new Languages();
        public SortedSet<SavingThrow> SavingThrows { get; } = new SortedSet<SavingThrow>(new List<SavingThrow>());
        public string Size { get; internal set; }
        public Skills Skills { get; } = new Skills();
        public int Speed { get; internal set; }
        public Tools Tools { get; } = new Tools();
        public SortedSet<AvailableWeapon> WeaponProficiencies { get; } = new SortedSet<AvailableWeapon>(new List<AvailableWeapon>());
        public CharacterFeatures Features { get; } = new CharacterFeatures();
        public int MartialArts { get; internal set; }
        public int AttacksPerTurn { get; }
        public int SneakAttackDice { get; internal set; }
        public SortedSet<SpellcastingClass> SpellcastingClasses { get; } = new SortedSet<SpellcastingClass>();
        public ClassTraits ClassTraits { get; } = new ClassTraits();

        public void EquipArmor(AvailableArmor armor)
        {
            EquippedArmor = Armory.GetArmor(armor);
        }

        public void SetAttributes(CharacterAbilities characterAbilities)
        {
            var racialBonuses = new RacialBonuses(
                Abilities.Strength.RacialBonus,
                Abilities.Dexterity.RacialBonus,
                Abilities.Constitution.RacialBonus,
                Abilities.Intelligence.RacialBonus,
                Abilities.Wisdom.RacialBonus,
                Abilities.Charisma.RacialBonus);
            Abilities = new CharacterAbilities(characterAbilities, racialBonuses);
        }

        public void ToggleShield()
        {
            HasShield = !HasShield;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void ChooseSkill(AvailableSkill chosenSkill)
        {
            throw new NotImplementedException();
        }

        public void LearnTool(AvailableTool chosenTool)
        {
            Tools.Chosen.Add(chosenTool);
        }

        public void LearnInstrument(AvailableInstrument chosenInstrument)
        {
            Instruments.Chosen.Add(chosenInstrument);
        }

        public void LearnLanguage(AvailableLanguages chosenLanguage)
        {
            Languages.Chosen.Add(chosenLanguage);
        }

        public void ChosePath(AvailablePaths chosenPath)
        {
            throw new NotImplementedException();
        }

        public void ImproveAbility(string ability)
        {
            throw new NotImplementedException();
        }

        public void ChooseExpertise(AvailableSkill skill)
        {
            throw new NotImplementedException();
        }

        public void ChooseExpertise(AvailableTool tool)
        {
            throw new NotImplementedException();
        }

        public int ClassLevel(string className)
        {
            return Classes.Count(x => x == className);
        }

        public int SkillBonus(AvailableSkill skill)
        {
            var ability =
                (CharacterAbility)
                    Abilities.GetType()
                        .GetProperty(CharacterData.SkillMods.First(s => s.Key == skill).Value.ToString())
                        .GetValue(Abilities);
            var abilityMod = ability.Modifier;
            var profBonus = Skills.Expertise.Contains(skill)
                ? ProficiencyBonus * 2
                : Skills.Chosen.Contains(skill) ? ProficiencyBonus : 0;
            return abilityMod + profBonus;
        }

        public void LevelUp(AvailableClasses cclass)
        {
            CharacterFactory.LevelUp(this, cclass);
        }

        public void LevelDown()
        {
            throw new NotImplementedException();
        }

        private static int GetArmorClassBonus(Armor armor, int dex)
        {
            if (armor.MaxDexBonus == -1)
                return armor.BaseArmor + dex;

            if (dex > armor.MaxDexBonus)
                dex = armor.MaxDexBonus;

            return armor.BaseArmor + dex;
        }

        private int CalculateMaxHp()
        {
            var halfDie = HitDice.List.GetRange(1, HitDice.List.Count - 1).Sum(hitDie => (hitDie / 2) + 1);
            return HitDice[0] + halfDie + Abilities.Constitution.Modifier;
        }
    }
}