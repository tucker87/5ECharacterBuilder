using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace _5ECharacterBuilder.CharacterClasses
{
    abstract class CharacterClass : ICharacter
    {
        private readonly ICharacter _character;
        protected CharacterClass(ICharacter character) { _character = character; }

        public virtual int ArmorClass { get { return _character.ArmorClass; } }
        public virtual CharacterAttributes Attributes { get { return _character.Attributes; } set { _character.Attributes = value; } }
        public virtual string Background { get { return _character.Background; } }
        public virtual ReadOnlyCollection<AvailableSkill> BackgroundSkills { get { return _character.BackgroundSkills; } }

        public virtual List<int> HitDice { get { return _character.HitDice; } }
        public virtual int MaxHp { get { return _character.MaxHp; } }
        public virtual string Name { get { return _character.Name; } set { _character.Name = value; } }
        public virtual string Race { get { return _character.Race; } }
        public abstract string Class { get; }
        public virtual int Initiative { get { return _character.Initiative; } }
        public virtual int Speed { get { return _character.Speed; } }
        public abstract ReadOnlyCollection<AvailableSkill> ClassSkills { get; }
        public virtual int ClassSkillCount { get { return _character.ClassSkillCount; } }
        public virtual Currency Currency { get { return _character.Currency; } }
        public virtual ReadOnlyCollection<AvailableLanguages> Languages { get { return _character.Languages; } }
        public virtual bool HasShield { get { return _character.HasShield; } set { _character.HasShield = value; } }
        public virtual Armor EquippedArmor { get { return _character.EquippedArmor; } }

        public ReadOnlyCollection<AvailableSkill> Skills { get { return _character.Skills; } }

        public virtual ReadOnlyCollection<AvailableSkill> TrainedSkills { get { return _character.TrainedSkills; } set { _character.TrainedSkills = value; } }
        public virtual ReadOnlyCollection<AvailableArmor> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public virtual ReadOnlyCollection<AvailableWeapon> WeaponProficiencies { get { return _character.WeaponProficiencies; } }

        public virtual ReadOnlyCollection<AvailableTool> ToolProficiencies { get { return _character.ToolProficiencies; } set { _character.ToolProficiencies = value; } }
        public virtual ReadOnlyCollection<AvailableInstrument> InstrumentProficiencies { get { return _character.InstrumentProficiencies; } }
        public virtual ReadOnlyCollection<SavingThrow> SavingThrowProficiencies { get { return _character.SavingThrowProficiencies; } }


        public virtual string Size { get { return _character.Size; } }

        public virtual List<string> RuleIssues
        {
            get
            {
                    var ruleIssues = _character.RuleIssues;

                    if (TrainedSkills.ToList().Count > ClassSkillCount)
                        ruleIssues.Add(String.Format("{0}s can only choose {1} skills from their list.", Class,
                            ClassSkillCount));
                

                    var classSkills = ClassSkills.ToList();

                ruleIssues.AddRange(from skill in TrainedSkills where !classSkills.Contains(skill) select skill + " is not a skill available to this class.");

                return ruleIssues;
            }
        }

        public virtual void PickSkills(List<AvailableSkill> skillList)
        {
            _character.PickSkills(skillList);

            var currentSkills = TrainedSkills.ToList();

            currentSkills.AddRange(skillList);

            TrainedSkills = new ReadOnlyCollection<AvailableSkill>(currentSkills);
        }

        public virtual void AddWeaponProfs(List<AvailableWeapon> weaponList) { _character.AddWeaponProfs(weaponList); }

        public virtual void AddSavingThrows(List<SavingThrow> savingThrows) { _character.AddSavingThrows(savingThrows); }

        public virtual void AddToolProfs(List<AvailableTool> tools) { _character.AddToolProfs(tools); }

        public virtual void AddInstrumentProfs(List<AvailableInstrument> instruments) { _character.AddInstrumentProfs(instruments); }

        public virtual void SetAttributes(CharacterAttributes characterAttributes) { _character.SetAttributes(characterAttributes); }
        public void AddClassSkills(List<AvailableSkill> classSkills) { _character.AddClassSkills(classSkills); }
        public virtual void AddLanguages(List<AvailableLanguages> languages) { _character.AddLanguages(languages); }

        public void AddBackgroundSkills(List<AvailableSkill> skillList)  { _character.AddBackgroundSkills(skillList); }

        public virtual void EquipArmor(AvailableArmor armor) { _character.EquipArmor(armor); }

        public void PickSkill(AvailableSkill skill) { _character.PickSkill(skill); }

        public virtual void AddArmorProf(List<AvailableArmor> armors) { _character.AddArmorProf(armors); }
    }
}