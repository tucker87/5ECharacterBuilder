using System.Collections.Generic;
using _5EDatabase;

namespace _5ECharacterBuilder.CharacterBackgrounds
{
    class CharacterBackground : ICharacter
    {
        private readonly ICharacter _character;
        protected CharacterBackground(ICharacter character) { _character = character; }

        public int ArmorClass => _character.ArmorClass;
        public CharacterAbilities Abilities => _character.Abilities;
        public virtual string Background => _character.Background;
        public List<string> Classes => _character.Classes;
        public ClassPath ClassPath => _character.ClassPath;
        public string ClassesString => _character.ClassesString;
        public Currency Currency => _character.Currency;
        public Armor EquippedArmor => _character.EquippedArmor;
        public bool HasShield => _character.HasShield;
        public HitDice HitDice => _character.HitDice;
        public int Initiative => _character.Initiative;
        public Proficiencies<AvailableInstrument> Instruments => _character.Instruments;
        public int Level => _character.Level;
        public int MaxHp => _character.MaxHp;
        public string Name => _character.Name;
        public int ProficiencyBonus => _character.ProficiencyBonus;
        public string Race => _character.Race;
        public Languages Languages => _character.Languages;
        public SortedSet<SavingThrow> SavingThrows => _character.SavingThrows;
        public virtual Skills Skills => _character.Skills;
        public int Speed => _character.Speed;
        public Tools Tools => _character.Tools;
        public SortedSet<AvailableWeapon> WeaponProficiencies => _character.WeaponProficiencies;
        public SortedSet<AvailableArmor> ArmorProficiencies => _character.ArmorProficiencies;
        public string Size => _character.Size;
        public CharacterFeatures Features => _character.Features;
        public int MartialArts => _character.MartialArts;
        public int AttacksPerTurn => _character.AttacksPerTurn;
        public int SneakAttackDice => _character.SneakAttackDice;
        public SortedSet<SpellcastingClass> SpellcastingClasses => _character.SpellcastingClasses;
        public ClassTraits ClassTraits => _character.ClassTraits;

        public void EquipArmor(AvailableArmor armor) { _character.EquipArmor(armor); }
        public void SetAttributes(CharacterAbilities characterAbilities) { _character.SetAttributes(characterAbilities); }
        public void ToggleShield() { _character.ToggleShield(); }
        public void SetName(string name) { _character.SetName(name); }
        public void ChooseSkill(AvailableSkill chosenSkill) { _character.ChooseSkill(chosenSkill); }
        public void LearnTool(AvailableTool chosenTool) { _character.LearnTool(chosenTool); }
        public void LearnInstrument(AvailableInstrument chosenInstrument) { _character.LearnInstrument(chosenInstrument); }
        public void LearnLanguage(AvailableLanguages chosenLanguage) { _character.LearnLanguage(chosenLanguage); }
        public void ChosePath(AvailablePaths chosenPath) { _character.ChosePath(chosenPath); }
        public void ImproveAbility(string ability) { _character.ImproveAbility(ability); }
        public void ChooseExpertise(AvailableSkill skill) { _character.ChooseExpertise(skill); }
        public void ChooseExpertise(AvailableTool tool) { _character.ChooseExpertise(tool); }
        public int ClassLevel(string className) { return _character.ClassLevel(className); }
        public int SkillBonus(AvailableSkill skill) { return _character.SkillBonus(skill); }
        public void LevelUp(AvailableClasses cclass) { _character.LevelUp( cclass); }
        public void LevelDown() { _character.LevelDown(); }
    }
}
