namespace _5ECharacterBuilder.CharacterRaces
{
    class Drow : Elf
    {
        public Drow(ICharacter character) : base(character)
        {
            Abilities.Charisma.RacialBonus = 1;

            AddRaceFeature("Superior Darkvision");
            AddRaceFeature("Sunlight Sensitivity");
            AddRaceFeature("Drow Magic");
            AddRaceFeature("Drow Weapon Training");

            WeaponProficiencies.Add(AvailableWeapon.Rapier);
            WeaponProficiencies.Add(AvailableWeapon.ShortSword);
            WeaponProficiencies.Add(AvailableWeapon.HandCrossbows);
        }

        public override string Race
        {
            get { return "Dark Elf"; }
        }

        public override int Speed
        {
            get { return 35; }
        }
    }
}