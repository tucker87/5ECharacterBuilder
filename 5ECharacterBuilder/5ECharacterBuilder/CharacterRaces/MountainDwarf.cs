using System.Linq;
using _5EDatabase;

namespace _5ECharacterBuilder.CharacterRaces
{
    class MountainDwarf : Dwarf
    {
        public MountainDwarf(ICharacter character) : base(character)
        {
            Abilities.Strength.RacialBonus = 2;

            foreach (var armor in Armory.LightArmor.Concat(Armory.MediumArmor))
                ArmorProficiencies.Add(armor);

            AddRaceFeature("Dwarven Armor Training");
        }
        
        public override string Race => "Mountain Dwarf";
    }
}