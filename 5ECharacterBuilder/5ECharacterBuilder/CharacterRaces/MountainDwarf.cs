using System.Linq;

namespace _5ECharacterBuilder.CharacterRaces
{
    class MountainDwarf : Dwarf
    {
        public MountainDwarf(ICharacter character) : base(character)
        {
            Attributes.Strength.RacialBonus = 2;

            foreach (var armor in Armory.LightArmor.Concat(Armory.MediumArmor))
                ArmorProficiencies.Add(armor);

            Features.RaceFeatures.Add(CharacterData.FeatureData.Single(f => f.Name == "Dwarven Armor Training"));
        }

        public override string Race
        {
            get { return "Mountain Dwarf"; }
        }
    }
}