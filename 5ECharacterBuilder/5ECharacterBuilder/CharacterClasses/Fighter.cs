using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace _5ECharacterBuilder.CharacterClasses
{
    sealed class Fighter : CharacterClass
    {
        public Fighter(ICharacter character, List<AvailableSkill> skillList = null) : base(character)
        {
            var armory = new Armory();

            HitDice.Add(10);
            AddArmorProf(new List<AvailableArmor>(armory.AllArmor) {AvailableArmor.Shield});
            var fighterWeapons = new List<AvailableWeapon>(armory.SimpleWeapons);
            fighterWeapons.AddRange(armory.MartialWeapons);
            AddWeaponProfs(fighterWeapons);
            AddSavingThrows(new List<SavingThrow>{SavingThrow.Strength, SavingThrow.Constitution});
            if (skillList != null)
                AddSkills(skillList);
        }

        public override string Class { get { return "Fighter"; } }
        public override string Name { get; set; }
        public override int CLassSkillCount { get { return 2; } }
        public override ReadOnlyCollection<AvailableSkill> ClassSkills
        {
            get
            {
                return new ReadOnlyCollection<AvailableSkill>(new[]
                    {
                        AvailableSkill.Acrobat,
                        AvailableSkill.AnimalHandling,
                        AvailableSkill.Athletics,
                        AvailableSkill.History,
                        AvailableSkill.Insight,
                        AvailableSkill.Intimidation,
                        AvailableSkill.Perception,
                        AvailableSkill.Survival
                    });
            }
        }
    }
}
