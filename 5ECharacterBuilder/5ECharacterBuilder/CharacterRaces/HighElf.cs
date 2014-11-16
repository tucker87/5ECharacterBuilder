using System.Linq;

namespace _5ECharacterBuilder.CharacterRaces
{
    class HighElf : Elf
    {
        public HighElf(ICharacter character) : base(character)
        {
            Attributes.Intelligence.RacialBonus = 1;

            Features.RaceFeatures.Add(CharacterData.FeatureData.Single(f => f.Name == "Elf Weapon Training"));
            Features.RaceFeatures.Add(CharacterData.FeatureData.Single(f => f.Name == "Extra Language"));

            WeaponProficiencies.Add(AvailableWeapon.LongSword);
            WeaponProficiencies.Add(AvailableWeapon.ShortSword);
            WeaponProficiencies.Add(AvailableWeapon.ShortBow);
            WeaponProficiencies.Add(AvailableWeapon.LongBow);

            Features.RaceFeatures.Add(new Feature("Cantrip", "You know one cantrip of your choice from the wizard spell list. Intelligence is your spellcasting ability for it."));
            
            Languages.Max += 1;
        }

        public override string Race
        {
            get { return "High Elf"; }
        }
    }
}