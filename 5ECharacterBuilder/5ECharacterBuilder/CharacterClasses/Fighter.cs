using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterClasses
{
    sealed class Fighter : CharacterClass
    {
        public Fighter(Character character, List<AvailableSkill> skillList = null) : base(character)
        {
            HitDice.Add(10);
            ArmorProficiencies.AddRange(new List<AvailableArmor>(Armory.AllArmor) {AvailableArmor.Shield});
            var fighterWeapons = new List<AvailableWeapon>(Armory.SimpleWeapons);
            fighterWeapons.AddRange(Armory.MartialWeapons);
            WeaponProficiencies.AddRange(fighterWeapons);
            SavingThrowProficiencies.AddRange(new List<SavingThrow>{SavingThrow.Strength, SavingThrow.Constitution});

            Class += "Fighter";
            ClassSkillCount = 2;
            
            ClassSkills.AddRange(new List<AvailableSkill>
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
