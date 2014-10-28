using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace _5ECharacterBuilder.CharacterClasses
{
    class CharacterClass : ICharacter
    {
        private readonly ICharacter _character;
        protected CharacterClass(ICharacter character) { _character = character; }

        public virtual CharacterAttributes Attributes { get { return _character.Attributes; } set { _character.Attributes = value; } }
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

        public virtual ReadOnlyCollection<AvailableArmor> EquippedArmors { get { return _character.EquippedArmors; } }
        public virtual ReadOnlyCollection<AvailableSkill> ClassSkills { get { return _character.ClassSkills; } }
        public virtual ReadOnlyCollection<AvailableSkill> SkillProficiencies { get { return _character.SkillProficiencies; } set { _character.SkillProficiencies = value; } }
        public virtual ReadOnlyCollection<AvailableArmor> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public virtual ReadOnlyCollection<AvailableWeapon> WeaponProficiencies { get { return _character.WeaponProficiencies; } }

        public virtual ReadOnlyCollection<AvailableTool> ToolProficiencies { get { return _character.ToolProficiencies; } set { _character.ToolProficiencies = value; } }
        public virtual ReadOnlyCollection<AvailableInstrument> InstrumentProficiencies { get { return _character.InstrumentProficiencies; } }
        public virtual ReadOnlyCollection<SavingThrow> SavingThrowProficiencies { get { return _character.SavingThrowProficiencies; } }
        

        public virtual string Size { get { return _character.Size; } }

        public virtual List<string> VerifyCharacter()
        {
            var ruleIssues =_character.VerifyCharacter();

            if (ClassSkills.ToList().Count > ClassSkillCount)
                ruleIssues.Add(Class + " can only choose " + ClassSkillCount + " skills from their list.");

            return ruleIssues;
        }

        public virtual void AddClassSkills(List<AvailableSkill> skillList)
        {
            _character.AddClassSkills(skillList);

            var currentSkills = SkillProficiencies.ToList();
            var classSkills = ClassSkills.ToList();

            foreach (var skill in skillList)
            {
                currentSkills.Add(skill);
                if (!classSkills.Contains(skill))
                    RuleIssues.Add(skill + " is not a skill available to this class.");
            }

            SkillProficiencies = new ReadOnlyCollection<AvailableSkill>(currentSkills);

            VerifyCharacter();
        }

        public virtual void AddWeaponProfs(List<AvailableWeapon> weaponList) { _character.AddWeaponProfs(weaponList); }

        public virtual void AddSavingThrows(List<SavingThrow> savingThrows) { _character.AddSavingThrows(savingThrows); }

        public virtual void AddToolProfs(List<AvailableTool> tools) { _character.AddToolProfs(tools); }

        public virtual void AddInstrumentProfs(List<AvailableInstrument> instruments) { _character.AddInstrumentProfs(instruments); }

        public virtual void SetAttributes(CharacterAttributes characterAttributes) { _character.SetAttributes(characterAttributes); }

        public virtual void AddLanguages(List<AvailableLanguages> languages) { _character.AddLanguages(languages); }

        public void AddBackgroundSkills(List<AvailableSkill> skillList)  { _character.AddBackgroundSkills(skillList); }

        public virtual void AddEquippedArmors(List<AvailableArmor> armors) { _character.AddEquippedArmors(armors); }

        public virtual void AddArmorProf(List<AvailableArmor> armors) { _character.AddArmorProf(armors); }
    }
}