using System;
using System.Collections.Generic;
using System.Linq;
using _5EDatabase;

namespace _5ECharacterBuilder
{
    public interface ICharacter
    {
        int ArmorClass { get; }
        SortedSet<ArmorType> ArmorProficiencies { get; }
        CharacterAbilities Abilities { get; }
        string Background { get; }
        IEnumerable<Class> Classes { get; }
        List<Path> AvailablePaths { get; }
        List<Path> ChosenPaths { get; }
        string ClassesString { get; }
        Currency Currency { get; }
        Armor EquippedArmor { get; }
        bool HasShield { get; }
        HitDice HitDice { get; }
        int Initiative { get; }
        Proficiencies<Instrument> Instruments { get; }
        int Level { get; }
        int MaxHp { get; }
        string Name { get; }
        string Race { get; }
        Languages Languages { get; }
        IEnumerable<SavingThrow> SavingThrows { get; }
        string Size { get; }
        Skills Skills { get; }
        int Speed { get; }
        Tools Tools { get; }
        SortedSet<WeaponType> WeaponProficiencies { get; }
        List<Feature> AllFeatures { get; }
        List<ClassFeature> ClassFeatures { get; }
        List<ClassPathFeature> ClassPathFeatures { get; }
        List<RaceFeature> RaceFeatures { get; }
        int ProficiencyBonus { get; }

        int MartialArts { get; }
        int AttacksPerTurn { get; }
        int SneakAttackDice { get; }
        SortedSet<SpellcastingClass> SpellcastingClasses { get; }
        ClassTraits ClassTraits { get; }
        void EquipArmor(ArmorType armor);
        void SetAttributes(CharacterAbilities characterAbilities);
        void ToggleShield();
        void SetName(string name);
        void ChooseSkill(Skill chosenSkill);
        void LearnTool(Tool chosenTool);
        void LearnInstrument(Instrument chosenInstrument);
        void LearnLanguage(Language chosenLanguage);
        void ChosePath(Path chosenPath);
        void ImproveAbility(string ability);
        void ChooseExpertise(Skill skill);
        void ChooseExpertise(Tool tool);
        int GetClassLevel(Class className);
        int SkillBonus(Skill skill, int? profBonus = null);
        //void LevelUp(Class cclass);
        void LevelDown();
        int CalculateMaxHp(HitDice hitDice);
        int CalculateProficiencyBonus(int level);

    }

    internal class CharacterBase : ICharacter
    {
        public CharacterBase(CharacterAbilityScores abilityScores = null, string name = "")
        {
            Name = name;
            abilityScores = abilityScores ?? new CharacterAbilityScores();
            EquipArmor(ArmorType.Cloth);
            AttacksPerTurn = 1;
            Abilities = new CharacterAbilities(abilityScores);
        }

        private int ShieldBonus => HasShield ? 2 : 0;
        public int ArmorClass => GetArmorClassBonus(EquippedArmor, Abilities.Dexterity.Modifier) + ShieldBonus;
        public SortedSet<ArmorType> ArmorProficiencies { get; } = new SortedSet<ArmorType>(new List<ArmorType>());
        public CharacterAbilities Abilities { get; internal set; }
        public string Background { get; internal set; }
        public IEnumerable<Class> Classes { get; } = new List<Class>();
        public List<Path> AvailablePaths { get; } = new List<Path>();
        public List<Path> ChosenPaths { get; } = new List<Path>();
        public string ClassesString { get; internal set; }
        public Currency Currency { get; } = new Currency();
        public Armor EquippedArmor { get; internal set; }
        public bool HasShield { get; internal set; }
        public virtual HitDice HitDice { get; } = new HitDice();
        public int Initiative { get; internal set; }
        public Proficiencies<Instrument> Instruments { get; } = new Proficiencies<Instrument>();
        public int Level { get; }

        public virtual int MaxHp { get; }

        public string Name { get; private set; }

        public int ProficiencyBonus => CalculateProficiencyBonus(Level);
        public int CalculateProficiencyBonus(int level) => level >= 17 ? 6 : level >= 13 ? 5 : level >= 9 ? 4 : level >= 5 ? 3 : 2;

        public string Race { get; internal set; }
        public Languages Languages { get; } = new Languages();
        public IEnumerable<SavingThrow> SavingThrows { get; } = new List<SavingThrow>();
        public string Size { get; internal set; }
        public Skills Skills { get; } = new Skills();
        public int Speed { get; internal set; }
        public Tools Tools { get; } = new Tools();
        public SortedSet<WeaponType> WeaponProficiencies { get; } = new SortedSet<WeaponType>(new List<WeaponType>());

        public List<Feature> AllFeatures => 
            new List<Feature>()
                .Union(RaceFeatures)
                .Union(ClassFeatures)
                .Union(ClassPathFeatures)
                .ToList();

        public List<RaceFeature> RaceFeatures { get; } = new List<RaceFeature>();
        public List<ClassFeature> ClassFeatures { get; } = new List<ClassFeature>();
        public List<ClassPathFeature> ClassPathFeatures { get; } = new List<ClassPathFeature>();

        public int MartialArts { get; internal set; }
        public int AttacksPerTurn { get; }
        public int SneakAttackDice { get; internal set; }
        public SortedSet<SpellcastingClass> SpellcastingClasses { get; } = new SortedSet<SpellcastingClass>();
        public ClassTraits ClassTraits { get; } = new ClassTraits();

        public void EquipArmor(ArmorType armor)
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

        public void ChooseSkill(Skill chosenSkill)
        {
            throw new NotImplementedException();
        }

        public void LearnTool(Tool chosenTool)
        {
            Tools.Chosen.Add(chosenTool);
        }

        public void LearnInstrument(Instrument chosenInstrument)
        {
            Instruments.Chosen.Add(chosenInstrument);
        }

        public void LearnLanguage(Language chosenLanguage)
        {
            Languages.Chosen.Add(chosenLanguage);
        }

        public void ChosePath(Path chosenPath)
        {
            throw new NotImplementedException();
        }

        public void ImproveAbility(string ability)
        {
            throw new NotImplementedException();
        }

        public void ChooseExpertise(Skill skill)
        {
            throw new NotImplementedException();
        }

        public void ChooseExpertise(Tool tool)
        {
            throw new NotImplementedException();
        }

        public int GetClassLevel(Class className)
        {
            return 0;
        }

        public int SkillBonus(Skill skill, int? profBonus)
        {
            var abilityMod =
                ((CharacterAbility)
                    Abilities.GetType()
                        .GetProperty(CharacterData.SkillMods.First(s => s.Key == skill).Value.ToString())
                        .GetValue(Abilities)).Modifier;

            profBonus = Skills.Expertise.Contains(skill)
                ? profBonus * 2
                : Skills.Chosen.Contains(skill) ? profBonus : 0;

            return abilityMod + (profBonus ?? 0);
        }

        //public void LevelUp(Class cclass)
        //{
        //    CharacterFactory.LevelUp(ref this, cclass);
        //}

        public void LevelDown()
        {
            throw new NotImplementedException();
        }

        private static int GetArmorClassBonus(Armor armor, int dex)
        {
            if (armor.MaxDexBonus == null)
                return armor.BaseArmor + dex;

            if (dex > armor.MaxDexBonus)
                dex = (int) armor.MaxDexBonus;

            return armor.BaseArmor + dex;
        }

        public int CalculateMaxHp(HitDice hitDice)
        {
            var halfDie = hitDice.List.GetRange(1, hitDice.List.Count - 1).Sum(hitDie => (hitDie / 2) + 1);
            return hitDice[0] + halfDie + Abilities.Constitution.Modifier;
        }
    }
}