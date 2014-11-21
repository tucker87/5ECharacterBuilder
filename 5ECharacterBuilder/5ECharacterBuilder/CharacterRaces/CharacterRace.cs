using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterRaces
{
    class CharacterRace : ICharacter
    {
        private readonly ICharacter _character;
        protected CharacterRace(ICharacter character) { _character = character; }

        public int ArmorClass { get { return _character.ArmorClass; } }
        public CharacterAbilities Abilities { get { return _character.Abilities; } }
        public string Background { get { return _character.Background; } }
        public List<string> Classes { get { return _character.Classes; } }
        public ClassPath ClassPath { get { return _character.ClassPath; } } 
        public string ClassesString { get { return _character.ClassesString; } }
        public Currency Currency { get { return _character.Currency; } }
        public Armor EquippedArmor { get { return _character.EquippedArmor; } }
        public bool HasShield { get { return _character.HasShield; } }
        public HitDice HitDice { get { return _character.HitDice; } }
        public int Initiative { get { return _character.Initiative; } }
        public Proficiencies<AvailableInstrument> Instruments { get { return _character.Instruments; } }
        public int Level { get { return _character.Level; } }
        public virtual int MaxHp { get { return _character.MaxHp; } }
        public string Name { get { return _character.Name; } }
        public int ProficiencyBonus { get { return _character.ProficiencyBonus; } }
        public virtual string Race { get { return _character.Race; } }
        public Languages Languages { get { return _character.Languages; } }
        public SortedSet<SavingThrow> SavingThrows { get { return _character.SavingThrows; } }
        public Skills Skills { get { return _character.Skills; } } 
        public virtual int Speed { get { return 30; } }
        public Tools Tools { get { return _character.Tools; } } 
        public SortedSet<AvailableWeapon> WeaponProficiencies { get { return _character.WeaponProficiencies; } }
        public SortedSet<AvailableArmor> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public CharacterFeatures Features { get { return _character.Features; } }
        public int KiPoints { get { return _character.KiPoints; } }
        public int MartialArts { get { return _character.MartialArts; } }
        public int AttacksPerTurn { get { return _character.AttacksPerTurn; } }
        public int SneakAttackDice { get { return _character.SneakAttackDice; } }
        public SortedSet<SpellcastingClass> SpellcastingClasses { get { return _character.SpellcastingClasses; } }

        public virtual string Size { get { return "Medium"; } }

        public void EquipArmor(AvailableArmor armor) { _character.EquipArmor(armor); }
        public void SetAttributes(CharacterAbilities characterAbilities) { _character.SetAttributes(characterAbilities); }
        public void ToggleShield()  { _character.ToggleShield(); }
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
        public void LevelUp(AvailableClasses cclass) { _character.LevelUp(cclass); }
        internal void AddRaceFeature(string feature) { Features.RaceFeatures.Add(feature, CharacterData.RaceFeatures[feature]); }
    }
}