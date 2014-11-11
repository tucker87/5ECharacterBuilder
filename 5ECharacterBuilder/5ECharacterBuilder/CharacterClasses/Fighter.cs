using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder.CharacterClasses
{
    class Fighter : CharacterClass
    {
        public Fighter(ICharacter character) : base(character)
        {
            HitDice.Add(10);
            foreach (var armor in Armory.AllArmor)
                ArmorProficiencies.Add(armor);
            
            ArmorProficiencies.Add(AvailableArmor.Shield);
            foreach (var weapon in Armory.SimpleWeapons.Concat(Armory.MartialWeapons))
                WeaponProficiencies.Add(weapon);
            
            SavingThrowProficiencies.Add(SavingThrow.Strength);
            SavingThrowProficiencies.Add( SavingThrow.Constitution);

            Classes.Add("Fighter");

            AvailableSkills.Add(AvailableSkill.Acrobatics);
            AvailableSkills.Add(AvailableSkill.AnimalHandling);
            AvailableSkills.Add(AvailableSkill.Athletics);
            AvailableSkills.Add(AvailableSkill.History);
            AvailableSkills.Add(AvailableSkill.Insight);
            AvailableSkills.Add(AvailableSkill.Intimidation);
            AvailableSkills.Add(AvailableSkill.Perception);
            AvailableSkills.Add(AvailableSkill.Survival);
        }

        public override int SkillCount
        {
            get
            {
                return 2;
            }
        }
    }
}
