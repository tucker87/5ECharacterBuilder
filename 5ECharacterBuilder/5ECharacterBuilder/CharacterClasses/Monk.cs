using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace _5ECharacterBuilder.CharacterClasses
{
    sealed class Monk : CharacterClass
    {
        public Monk(ICharacter character, List<AvailableSkill> skillList = null) : base(character)
        {
            HitDice.Add(8);
            AddWeaponProfs(new List<AvailableWeapon>{AvailableWeapon.ShortSword});
            AddSimpleWeaponProficiencies();
            AddSavingThrows(new List<SavingThrow> {SavingThrow.Strength, SavingThrow.Dexterity});
            if(skillList != null)
                AddSkills(skillList);
        }

        private void AddSimpleWeaponProficiencies()
        {
            var armory = new Armory();
            AddWeaponProfs(armory.SimpleWeapons);
        }

        public override void AddSkills(List<AvailableSkill> skillList)
        {
            var currentSkills = SkillProficiencies.ToList();
            var classSkills = ClassSkills.ToList();

            foreach (var skill in skillList)
            {
                currentSkills.Add(skill);
                if (!classSkills.Contains(skill))
                    RuleIssues.Add(skill + " is not a skill available to this class.");
            }

            SkillProficiencies = new ReadOnlyCollection<AvailableSkill>(currentSkills);
            
            if (skillList.Count > CLassSkillCount)
                RuleIssues.Add("Monks can only choose two skills from their list.");
        }

        public override void AddToolProfs(List<AvailableTool> tools)
        {
            base.AddToolProfs(tools);
            CheckMonkToolIntrumentProf();
        }

        public override void AddInstrumentProfs(List<AvailableInstrument> instrument)
        {
            base.AddInstrumentProfs(instrument);
            CheckMonkToolIntrumentProf();
        }

        private void CheckMonkToolIntrumentProf()
        {
            if (ToolProficiencies.Count > 0 && InstrumentProficiencies.Count > 0)
                RuleIssues.Add("Monks can only choose an instrument or a Tool");
        }

        public override int SkillProficiencyCount { get { return base.SkillProficiencyCount + 2; } }
        public override string Class { get { return "Monk"; } }
        public override int CLassSkillCount { get { return 2; } }
        public override int ArmorClass {  get { return 10 + Attributes.Dexterity.Modifier + Attributes.Wisdom.Modifier; } }
        
        public override ReadOnlyCollection<AvailableSkill> ClassSkills
        {
            get { return new ReadOnlyCollection<AvailableSkill>(new[]
                    {
                        AvailableSkill.Acrobat, 
                        AvailableSkill.Athletics, 
                        AvailableSkill.History,
                        AvailableSkill.Insight, 
                        AvailableSkill.Religion, 
                        AvailableSkill.Stealth
                    });
            }
        }
    }
}