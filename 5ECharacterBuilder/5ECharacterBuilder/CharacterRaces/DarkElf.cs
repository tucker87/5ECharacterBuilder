using System.Linq;

namespace _5ECharacterBuilder.CharacterRaces
{
    class DarkElf : Elf
    {
        public DarkElf(ICharacter character) : base(character)
        {
            Attributes.Charisma.RacialBonus = 1;

            Features.RaceFeatures.Add(CharacterData.FeatureData.Single(f => f.Name == "Superior Darkvision"));
            Features.RaceFeatures.Add(CharacterData.FeatureData.Single(f => f.Name == "Sunlight Sensitivity"));
            Features.RaceFeatures.Add(CharacterData.FeatureData.Single(f => f.Name == "Drow Magic"));
            Features.RaceFeatures.Add(CharacterData.FeatureData.Single(f => f.Name == "Drow Weapon Training"));
            

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