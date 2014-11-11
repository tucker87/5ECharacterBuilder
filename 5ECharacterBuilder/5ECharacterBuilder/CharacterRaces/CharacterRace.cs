using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterRaces
{
    class CharacterRace : ICharacter
    {
        private readonly ICharacter _character;
        protected CharacterRace(ICharacter character) { _character = character; }

        public int ArmorClass { get { return _character.ArmorClass; } }
        public CharacterAttributes Attributes { get { return _character.Attributes; } }
        public string Background { get { return _character.Background; } }
        public List<string> Classes { get { return _character.Classes; } }
        public string ClassesString { get { return _character.ClassesString; } }
        public Currency Currency { get { return _character.Currency; } }
        public Armor EquippedArmor { get { return _character.EquippedArmor; } }
        public bool HasShield { get { return _character.HasShield; } }
        public List<int> HitDice { get { return _character.HitDice; } }
        public int Initiative { get { return _character.Initiative; } }
        public SortedSet<AvailableInstrument> ChosenInstrumentProficiencies { get { return _character.ChosenInstrumentProficiencies; } }
        public SortedSet<AvailableLanguages> ChosenLanguages { get { return _character.ChosenLanguages; } }
        public virtual int MaxHp { get { return _character.MaxHp; } }
        public string Name { get { return _character.Name; } }
        public virtual string Race { get { return _character.Race; } }
        public virtual int Speed { get { return _character.Speed; } }
        public SortedSet<AvailableSkill> TrainedSkills { get { return _character.TrainedSkills; } }
        public SortedSet<AvailableArmor> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public SortedSet<AvailableTool> ChosenToolProficiencies { get { return _character.ChosenToolProficiencies; } }
        public SortedSet<AvailableWeapon> WeaponProficiencies { get { return _character.WeaponProficiencies; } }
        public int ClassSkillCount { get { return _character.ClassSkillCount; } }
        public virtual int LanguageCount { get { return _character.LanguageCount; } }
        public int ToolProficiencyCount { get { return _character.ToolProficiencyCount; } }
        public CharacterFeatures Features { get { return _character.Features; } }

        public SortedSet<AvailableTool> AvailableToolProficiencies { get { return _character.AvailableToolProficiencies; } }
        public SortedSet<AvailableInstrument> AvailableInstrumentProficiencies { get { return _character.AvailableInstrumentProficiencies; } }
        public SortedSet<SavingThrow> SavingThrowProficiencies { get { return _character.SavingThrowProficiencies; } }
        public virtual string Size { get { return _character.Size; } }
        public SortedSet<AvailableSkill> AvailableSkills { get { return _character.AvailableSkills; } }

        public void EquipArmor(AvailableArmor armor) { _character.EquipArmor(armor); }
        public void SetAttributes(CharacterAttributes characterAttributes) { _character.SetAttributes(characterAttributes); }
        public void ToggleShield()  { _character.ToggleShield(); }
        public void SetName(string name) { _character.SetName(name); }
        public void LearnSkill(AvailableSkill chosenSkill) { _character.LearnSkill(chosenSkill); }
        public void LearnTool(AvailableTool chosenTool) { _character.LearnTool(chosenTool); }
    }
}