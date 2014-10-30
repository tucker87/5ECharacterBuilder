using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace _5ECharacterBuilder.CharacterClasses
{
    sealed class Fighter : CharacterClass
    {
        public Fighter(ICharacter character, List<AvailableSkill> skillList = null) : base(character)
        {
            HitDice.Add(10);
            AddArmorProf(new List<AvailableArmor>(Armory.AllArmor) {AvailableArmor.Shield});
            var fighterWeapons = new List<AvailableWeapon>(Armory.SimpleWeapons);
            fighterWeapons.AddRange(Armory.MartialWeapons);
            AddWeaponProfs(fighterWeapons);
            AddSavingThrows(new List<SavingThrow>{SavingThrow.Strength, SavingThrow.Constitution});
            if (skillList != null)
                PickSkills(skillList);
        }

        public override string Class { get { return "Fighter"; } }
        public override string Name { get; set; }
        public override int ClassSkillCount { get { return 2; } }
        public override ReadOnlyCollection<AvailableSkill> ClassSkills
        {
            get
            {
                return new ReadOnlyCollection<AvailableSkill>(new[]
                    {
                        AvailableSkill.Acrobatics,
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
