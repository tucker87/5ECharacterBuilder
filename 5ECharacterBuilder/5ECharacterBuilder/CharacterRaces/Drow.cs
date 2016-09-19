using _5EDatabase;

namespace _5ECharacterBuilder.CharacterRaces
{
    internal class Drow : Elf
    {
        public Drow(ICharacter character) : base(character)
        {
            Abilities.Charisma.RacialBonus = 1;

            AddRaceFeature("Superior Darkvision");
            AddRaceFeature("Sunlight Sensitivity");
            AddRaceFeature("Drow Magic");
            AddRaceFeature("Drow Weapon Training");

            WeaponProficiencies.Add(WeaponType.Rapier);
            WeaponProficiencies.Add(WeaponType.ShortSword);
            WeaponProficiencies.Add(WeaponType.HandCrossbows);
        }

        public override string Race => "Dark Elf";

        public override int Speed => 35;
    }
}