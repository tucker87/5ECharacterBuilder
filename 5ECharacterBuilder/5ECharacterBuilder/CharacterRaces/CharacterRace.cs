using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace _5ECharacterBuilder.CharacterRaces
{
    class CharacterRace : ICharacter
    {
        private readonly ICharacter _character;
        protected CharacterRace(ICharacter character) { _character = character; }

        public virtual CharacterAttributes Attributes { get { return _character.Attributes; } set{ _character.Attributes = value; } }
        public virtual string Background { get { return _character.Background; } }
        public virtual ReadOnlyCollection<AvailableSkill> BackgroundSkills { get { return _character.BackgroundSkills; } }
        public virtual List<int> HitDice { get { return _character.HitDice; } }
        public virtual int MaxHp { get { return _character.MaxHp; } }
        public virtual string Name { get { return _character.Name; } set { _character.Name = value; } }
        public virtual List<string> RuleIssues { get { return _character.RuleIssues; } }
        public virtual string Race { get { return _character.Race; } }
        public virtual string Class { get { return _character.Class; } }
        public virtual int Initiative { get { return _character.Initiative; } }
        public virtual int Speed { get { return _character.Speed; } }
        public virtual int ClassSkillCount { get { return _character.ClassSkillCount; } }
        public virtual Currency Currency { get { return _character.Currency; } }
        public virtual int ArmorClass { get { return _character.ArmorClass; } }
        public virtual ReadOnlyCollection<AvailableLanguages> Languages { get { return _character.Languages; } }
        public virtual bool HasSheild { get { return _character.HasSheild; } set { _character.HasSheild = value; } }

        public virtual Armor EquippedArmor { get { return _character.EquippedArmor; } }
        public virtual ReadOnlyCollection<AvailableSkill> ClassSkills { get { return _character.ClassSkills; } }
        public virtual ReadOnlyCollection<AvailableSkill> SkillProficiencies { get { return _character.SkillProficiencies; } set { _character.SkillProficiencies = value; } }
        public virtual ReadOnlyCollection<AvailableArmor> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public virtual ReadOnlyCollection<AvailableWeapon> WeaponProficiencies { get { return _character.WeaponProficiencies; } }

        public virtual ReadOnlyCollection<AvailableTool> ToolProficiencies { get { return _character.ToolProficiencies; } set { _character.ToolProficiencies = value;} }
        public virtual ReadOnlyCollection<AvailableInstrument> InstrumentProficiencies { get { return _character.InstrumentProficiencies; } }
        public virtual ReadOnlyCollection<SavingThrow> SavingThrowProficiencies { get { return _character.SavingThrowProficiencies; } }
        public virtual string Size { get { return _character.Size; } }
        
        public void AddClassSkills(List<AvailableSkill> skillList) {  _character.AddClassSkills(skillList); }

        public void AddWeaponProfs(List<AvailableWeapon> weaponList)  {  _character.AddWeaponProfs(weaponList); }

        public void EquipArmor(AvailableArmor armor) { _character.EquipArmor(armor); }

        public void AddSavingThrows(List<SavingThrow> savingThrows) {  _character.AddSavingThrows(savingThrows); }

        public void AddToolProfs(List<AvailableTool> tools)  {  _character.AddToolProfs(tools); }

        public void AddInstrumentProfs(List<AvailableInstrument> instruments)  {  _character.AddInstrumentProfs(instruments); }

        public void SetAttributes(CharacterAttributes characterAttributes) { _character.SetAttributes(characterAttributes); }

        public void AddLanguages(List<AvailableLanguages> languages) { _character.AddLanguages(languages); }

        public void AddBackgroundSkills(List<AvailableSkill> skillList) { _character.AddBackgroundSkills(skillList); }

        public void AddArmorProf(List<AvailableArmor> armors) { _character.AddArmorProf(armors); }
    }
}