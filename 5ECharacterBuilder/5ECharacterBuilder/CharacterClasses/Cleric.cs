using System.Collections.Generic;
using System.Linq;
using _5EDatabase;

namespace _5ECharacterBuilder.CharacterClasses
{
    internal sealed class Cleric : CharacterClass
    {
        public Cleric(ICharacter character) :base(character)
        {
            Class = Class.Cleric;

            ArmorProficiencies.UnionWith(Armory.LightArmor);
            ArmorProficiencies.UnionWith(Armory.MediumArmor);
            ArmorProficiencies.Add(ArmorType.Shield);

            WeaponProficiencies.UnionWith(Armory.SimpleWeapons);

            Skills.Available.UnionWith(new[]
                {
                    Skill.History,
                    Skill.Insight,
                    Skill.Medicine,
                    Skill.Persuasion,
                    Skill.Religion
                });

            Skills.Max += 2;
        }

        public override IEnumerable<SavingThrow> SavingThrows
            => base.SavingThrows.Union(SavingThrow.Wisdom).Union(SavingThrow.Charisma);

        public override IEnumerable<Class> Classes => base.Classes.Concat(Class.Cleric);

        internal override int AddedHitDice => 8;

        internal override List<ClassFeature> ClassFeatureData => new List<ClassFeature>
        {
            new ClassFeature("Spellcasting")
        };

        internal override List<ClassPathFeature> ClassPathFeatureData => new List<ClassPathFeature>();
    }
}
