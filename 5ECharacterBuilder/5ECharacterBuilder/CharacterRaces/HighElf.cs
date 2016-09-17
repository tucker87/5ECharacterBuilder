using _5EDatabase;

namespace _5ECharacterBuilder.CharacterRaces
{
    class HighElf : Elf
    {
        public HighElf(ICharacter character) : base(character)
        {
            Abilities.Intelligence.RacialBonus = 1;

            AddRaceFeature("Elf Weapon Training");
            AddRaceFeature("Extra Language");

            WeaponProficiencies.Add(AvailableWeapon.LongSword);
            WeaponProficiencies.Add(AvailableWeapon.ShortSword);
            WeaponProficiencies.Add(AvailableWeapon.ShortBow);
            WeaponProficiencies.Add(AvailableWeapon.LongBow);

            Features.RaceFeatures.Add("Cantrip", "You know one cantrip of your choice from the wizard spell list. Intelligence is your spellcasting ability for it.");
            
            Languages.Max += 1;
        }

        public override string Race => "High Elf";
    }
}