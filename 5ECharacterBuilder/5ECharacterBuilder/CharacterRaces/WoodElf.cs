namespace _5ECharacterBuilder.CharacterRaces
{
    class WoodElf : Elf
    {
        public WoodElf(ICharacter character) : base(character)
        {
            Abilities.Wisdom.RacialBonus = 1;

            AddRaceFeature("Elf Weapon Training");
            AddRaceFeature("Mask of the Wild");
            AddRaceFeature("Fleet of Foot");

            WeaponProficiencies.Add(AvailableWeapon.LongSword);
            WeaponProficiencies.Add(AvailableWeapon.ShortSword);
            WeaponProficiencies.Add(AvailableWeapon.ShortBow);
            WeaponProficiencies.Add(AvailableWeapon.LongBow);
        }

        public override string Race => "Wood Elf";

        public override int Speed => 35;
    }
}