using _5EDatabase;

namespace _5ECharacterBuilder.CharacterRaces
{
    internal class HighElf : Elf
    {
        public HighElf(ICharacter character) : base(character)
        {
            Abilities.Intelligence.RacialBonus = 1;

            AddRaceFeature("Elf Weapon Training");
            AddRaceFeature("Extra Language");

            WeaponProficiencies.Add(WeaponType.LongSword);
            WeaponProficiencies.Add(WeaponType.ShortSword);
            WeaponProficiencies.Add(WeaponType.ShortBow);
            WeaponProficiencies.Add(WeaponType.LongBow);

            RaceFeatures.Add(new RaceFeature("Cantrip", "You know one cantrip of your choice from the wizard spell list. Intelligence is your spellcasting ability for it."));
            
            Languages.Max += 1;
        }

        public override string Race => "High Elf";
    }
}