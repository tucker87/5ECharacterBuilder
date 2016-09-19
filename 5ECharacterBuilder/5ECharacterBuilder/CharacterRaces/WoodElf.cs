using _5EDatabase;

namespace _5ECharacterBuilder.CharacterRaces
{
    internal class WoodElf : Elf
    {
        public WoodElf(ICharacter character) : base(character)
        {
            Abilities.Wisdom.RacialBonus = 1;

            AddRaceFeature("Elf Weapon Training");
            AddRaceFeature("Mask of the Wild");
            AddRaceFeature("Fleet of Foot");

            WeaponProficiencies.Add(WeaponType.LongSword);
            WeaponProficiencies.Add(WeaponType.ShortSword);
            WeaponProficiencies.Add(WeaponType.ShortBow);
            WeaponProficiencies.Add(WeaponType.LongBow);
        }

        public override string Race => "Wood Elf";

        public override int Speed => 35;
    }
}