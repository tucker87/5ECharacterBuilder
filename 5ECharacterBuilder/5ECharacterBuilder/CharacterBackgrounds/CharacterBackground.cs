using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterBackgrounds
{
    class CharacterBackground : ICharacter
    {
        private readonly ICharacter _character;
        protected CharacterBackground(ICharacter character) { _character = character; }

        public int ArmorClass { get { return _character.ArmorClass; } }
        public CharacterAttributes Attributes { get { return _character.Attributes; } }
        public virtual string Background { get { return _character.Background; } }
        public List<string> Classes { get { return _character.Classes; } }
        public string ClassesString { get { return _character.ClassesString; } }
        public Currency Currency { get { return _character.Currency; } }
        public Armor EquippedArmor { get { return _character.EquippedArmor; } }
        public bool HasShield { get { return _character.HasShield; } }
        public List<int> HitDice { get { return _character.HitDice; } }
        public int Initiative { get { return _character.Initiative; } }
        public List<AvailableInstrument> ChosenInstrumentProficiencies { get { return _character.ChosenInstrumentProficiencies; } }
        public int MaxHp { get { return _character.MaxHp; } }
        public string Name { get { return _character.Name; } }
        public string Race { get { return _character.Race; } }
        public List<AvailableLanguages> ChosenLanguages { get { return _character.ChosenLanguages; } }
        public List<string> RuleIssues { get { return CharacterFactory.GetRuleIssues(this); } }
        public int Speed { get { return _character.Speed; } }
        public List<AvailableSkill> TrainedSkills { get { return _character.TrainedSkills; } }
        public List<AvailableArmor> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public List<AvailableTool> ChosenToolProficiencies { get { return _character.ChosenToolProficiencies; } }
        public List<AvailableWeapon> WeaponProficiencies { get { return _character.WeaponProficiencies; } }
        public int ClassSkillCount { get { return _character.ClassSkillCount; } }
        public List<AvailableTool> AvailableToolProficiencies { get { return _character.AvailableToolProficiencies; } }
        public List<AvailableInstrument> AvailableInstrumentProficiencies { get { return _character.AvailableInstrumentProficiencies; } }
        public List<SavingThrow> SavingThrowProficiencies { get { return _character.SavingThrowProficiencies; } }
        public string Size { get { return _character.Size; } }
        public List<AvailableSkill> AvailableSkills { get { return _character.AvailableSkills; } }
        public virtual int LanguageCount { get { return _character.LanguageCount; } }

        public void EquipArmor(AvailableArmor armor) { _character.EquipArmor(armor); }
        public void SetAttributes(CharacterAttributes characterAttributes) { _character.SetAttributes(characterAttributes); }
        public void ToggleShield() { _character.ToggleShield(); }
        public void SetName(string name) { _character.SetName(name); }
        public void LearnSkill(AvailableSkill chosenSkill) { _character.LearnSkill(chosenSkill); }
        public void LearnTool(AvailableTool chosenTool) { _character.LearnTool(chosenTool); }
    }
}
