using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace _5ECharacterBuilder.CharacterClasses
{
    sealed class Monk : CharacterClass
    {
        public Monk(ICharacter character, List<AvailableSkill> skillList = null) : base(character)
        {
            HitDice.Add(8);

            var armory = new Armory();
            AddWeaponProfs(armory.SimpleWeapons);
            AddWeaponProfs(new List<AvailableWeapon>{AvailableWeapon.ShortSword});

            AddSavingThrows(new List<SavingThrow> {SavingThrow.Strength, SavingThrow.Dexterity});
            if(skillList != null)
                AddClassSkills(skillList);
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

        public override string Class { get { return "Monk"; } }
        public override int ClassSkillCount { get { return 2; } }
        public override int ArmorClass {  get { return 10 + Attributes.Dexterity.Modifier + Attributes.Wisdom.Modifier; } }
        
        public override ReadOnlyCollection<AvailableSkill> ClassSkills
        {
            get { return new ReadOnlyCollection<AvailableSkill>(new[]
                    {
                        AvailableSkill.Acrobatics, 
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