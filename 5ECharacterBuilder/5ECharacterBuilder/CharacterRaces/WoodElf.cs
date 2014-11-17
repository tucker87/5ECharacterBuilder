namespace _5ECharacterBuilder.CharacterRaces
{
    class WoodElf : Elf
    {
        public WoodElf(ICharacter character) : base(character)
        {
            Attributes.Wisdom.RacialBonus = 1;

            AddRaceFeature("Elf Weapon Training");
            AddRaceFeature("Mask of the Wild");
            AddRaceFeature("Fleet of Foot");

            WeaponProficiencies.Add(AvailableWeapon.LongSword);
            WeaponProficiencies.Add(AvailableWeapon.ShortSword);
            WeaponProficiencies.Add(AvailableWeapon.ShortBow);
            WeaponProficiencies.Add(AvailableWeapon.LongBow);
        }

        public override string Race
        {
            get { return "Wood Elf"; }
        }

        public override int Speed
        {
            get { return 35; }
        }
    }
}