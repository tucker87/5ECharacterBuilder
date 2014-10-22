using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace _5ECharacterBuilder
{
    class CharacterRace : ICharacter
    {
        private readonly ICharacter _character;
        protected CharacterRace(ICharacter character) { _character = character; }

        public virtual CharacterAttributes Attributes { get { return _character.Attributes; } set{ _character.Attributes = value; } }
        public virtual List<int> HitDice { get { return _character.HitDice; } }
        public virtual int MaxHp { get { return _character.MaxHp; } }
        public virtual string Name { get { return _character.Name; } set { _character.Name = value; } }
        public virtual ReadOnlyCollection<AvailableSkill> SkillProficiencies { get { return _character.SkillProficiencies; } set { _character.SkillProficiencies = value; } }
        public virtual int SkillProficiencyCount { get { return _character.SkillProficiencyCount; } }
        public ReadOnlyCollection<AvailableArmor> EquippedArmors { get { return _character.EquippedArmors; } }
        public virtual ReadOnlyCollection<AvailableArmor> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public virtual ReadOnlyCollection<AvailableWeapon> WeaponProficiencies { get { return _character.WeaponProficiencies; } }
        public virtual List<string> RuleIssues { get { return _character.RuleIssues; } }
        public virtual ReadOnlyCollection<AvailableSkill> ClassSkills { get { return _character.ClassSkills; } }
        public virtual string Race { get { return _character.Race; } }
        public virtual string Class { get { return _character.Class; } }
        public virtual int Initiative { get { return _character.Initiative; } }
        public virtual int Speed { get { return _character.Speed; } }
        public int CLassSkillCount { get { return _character.CLassSkillCount; } }
        public Currency Currency { get { return _character.Currency; } }
        public int ArmorClass { get { return _character.ArmorClass; } }
        public ReadOnlyCollection<AvailableLanguages> Languages { get { return _character.Languages; } }
        public bool HasSheild { get { return _character.HasSheild; } set { _character.HasSheild = value; } }

        public virtual ReadOnlyCollection<AvailableTool> ToolProficiencies { get { return _character.ToolProficiencies; } }
        public virtual ReadOnlyCollection<AvailableInstrument> InstrumentProficiencies { get { return _character.InstrumentProficiencies; } }
        public virtual ReadOnlyCollection<SavingThrow> SavingThrowProficiencies { get { return _character.SavingThrowProficiencies; } }

        public List<string> VerifyCharacter()  {  return _character.VerifyCharacter(); }

        public void AddSkills(List<AvailableSkill> skillList) {  _character.AddSkills(skillList); }

        public void AddWeaponProfs(List<AvailableWeapon> weaponList)  {  _character.AddWeaponProfs(weaponList); }

        public void AddSavingThrows(List<SavingThrow> savingThrows) {  _character.AddSavingThrows(savingThrows); }

        public void AddToolProfs(List<AvailableTool> tools)  {  _character.AddToolProfs(tools); }

        public void AddInstrumentProfs(List<AvailableInstrument> instruments)  {  _character.AddInstrumentProfs(instruments); }

        public void SetAttributes(CharacterAttributes characterAttributes) { _character.SetAttributes(characterAttributes); }

        public void AddLanguages(List<AvailableLanguages> languages) { _character.AddLanguages(languages); }

        public void AddEquippedArmors(List<AvailableArmor> armors) { _character.AddEquippedArmors(armors); }

        public void AddArmorProf(List<AvailableArmor> armors) { _character.AddArmorProf(armors); }
    }

    class Human : CharacterRace
    {
        public Human(ICharacter character) : base(character)
        {
            character.Attributes = new CharacterAttributes(character.Attributes, new RacialBonuses(1, 1, 1, 1, 1, 1));
        }

        public override string Race { get { return "Human"; } }
        public override int Speed { get { return 30; } }
    }
}