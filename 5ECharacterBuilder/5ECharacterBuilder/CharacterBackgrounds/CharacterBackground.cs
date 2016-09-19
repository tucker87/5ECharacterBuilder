using System.Collections.Generic;
using System.Linq;
using _5EDatabase;

namespace _5ECharacterBuilder.CharacterBackgrounds
{
    internal class CharacterBackground : ICharacter
    {
        private readonly ICharacter _character;
        protected CharacterBackground(ICharacter character) { _character = character; }

        public int ArmorClass => _character.ArmorClass;
        public CharacterAbilities Abilities => _character.Abilities;
        public virtual string Background => _character.Background;
        public IEnumerable<Class> Classes => _character.Classes;
        public List<Path> AvailablePaths => _character.AvailablePaths;
        public List<Path> ChosenPaths => _character.ChosenPaths;
        public string ClassesString => _character.ClassesString;
        public Currency Currency => _character.Currency;
        public Armor EquippedArmor => _character.EquippedArmor;
        public bool HasShield => _character.HasShield;
        public HitDice HitDice => _character.HitDice;
        public int Initiative => _character.Initiative;
        public Proficiencies<Instrument> Instruments => _character.Instruments;
        public int Level => _character.Level;
        public int MaxHp => _character.MaxHp;
        public string Name => _character.Name;
        public int ProficiencyBonus => CalculateProficiencyBonus(Level);
        public int CalculateProficiencyBonus(int level) => _character.CalculateProficiencyBonus(level);
        public string Race => _character.Race;
        public Languages Languages => _character.Languages;
        public IEnumerable<SavingThrow> SavingThrows => _character.SavingThrows;
        public virtual Skills Skills => _character.Skills;
        public int Speed => _character.Speed;
        public Tools Tools => _character.Tools;
        public SortedSet<WeaponType> WeaponProficiencies => _character.WeaponProficiencies;
        public List<Feature> AllFeatures =>
            new List<Feature>()
                .Union(RaceFeatures)
                .Union(ClassFeatures)
                .Union(ClassPathFeatures)
                .ToList();

        public List<ClassFeature> ClassFeatures => _character.ClassFeatures;
        public List<ClassPathFeature> ClassPathFeatures => _character.ClassPathFeatures;
        public List<RaceFeature> RaceFeatures => _character.RaceFeatures;

        public SortedSet<ArmorType> ArmorProficiencies => _character.ArmorProficiencies;
        public string Size => _character.Size;
        
        public int MartialArts => _character.MartialArts;
        public int AttacksPerTurn => _character.AttacksPerTurn;
        public int SneakAttackDice => _character.SneakAttackDice;
        public SortedSet<SpellcastingClass> SpellcastingClasses => _character.SpellcastingClasses;
        public ClassTraits ClassTraits => _character.ClassTraits;

        public void EquipArmor(ArmorType armor) { _character.EquipArmor(armor); }
        public void SetAttributes(CharacterAbilities characterAbilities) { _character.SetAttributes(characterAbilities); }
        public void ToggleShield() { _character.ToggleShield(); }
        public void SetName(string name) { _character.SetName(name); }
        public void ChooseSkill(Skill chosenSkill) { _character.ChooseSkill(chosenSkill); }
        public void LearnTool(Tool chosenTool) { _character.LearnTool(chosenTool); }
        public void LearnInstrument(Instrument chosenInstrument) { _character.LearnInstrument(chosenInstrument); }
        public void LearnLanguage(Language chosenLanguage) { _character.LearnLanguage(chosenLanguage); }
        public void ChosePath(Path chosenPath) { _character.ChosePath(chosenPath); }
        public void ImproveAbility(string ability) { _character.ImproveAbility(ability); }
        public void ChooseExpertise(Skill skill) { _character.ChooseExpertise(skill); }
        public void ChooseExpertise(Tool tool) { _character.ChooseExpertise(tool); }
        public int GetClassLevel(Class className) { return _character.GetClassLevel(className); }
        public int SkillBonus(Skill skill, int? profBonus = null) { return _character.SkillBonus(skill, profBonus ?? ProficiencyBonus); }
        //public void LevelUp(Class cclass) { _character.LevelUp( cclass); }
        public void LevelDown() { _character.LevelDown(); }
        public int CalculateMaxHp(HitDice hitDice) => _character.CalculateMaxHp(hitDice);
    }
}
