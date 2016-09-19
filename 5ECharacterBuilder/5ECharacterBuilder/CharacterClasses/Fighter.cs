using System.Collections.Generic;
using System.Linq;
using _5EDatabase;

namespace _5ECharacterBuilder.CharacterClasses
{
    internal sealed class Fighter : CharacterClass
    {
        public Fighter(ICharacter character) : base(character)
        {
            Class = Class.Fighter;

            foreach (var armor in Armory.AllArmor)
                ArmorProficiencies.Add(armor);
            
            ArmorProficiencies.Add(ArmorType.Shield);
            foreach (var weapon in Armory.SimpleWeapons.Concat(Armory.MartialWeapons))
                WeaponProficiencies.Add(weapon);
            
            Skills.Available.Add(Skill.Acrobatics);
            Skills.Available.Add(Skill.AnimalHandling);
            Skills.Available.Add(Skill.Athletics);
            Skills.Available.Add(Skill.History);
            Skills.Available.Add(Skill.Insight);
            Skills.Available.Add(Skill.Intimidation);
            Skills.Available.Add(Skill.Perception);
            Skills.Available.Add(Skill.Survival);

            Skills.Max = 2;
        }

        public override IEnumerable<Class> Classes => base.Classes.Concat(Class.Fighter);

        public override IEnumerable<SavingThrow> SavingThrows
            => base.SavingThrows.Union(SavingThrow.Strength).Union(SavingThrow.Constitution);

        internal override int AddedHitDice => 10;
        internal override List<ClassFeature> ClassFeatureData => new List<ClassFeature>();
        internal override List<ClassPathFeature> ClassPathFeatureData => new List<ClassPathFeature>();
    }
}