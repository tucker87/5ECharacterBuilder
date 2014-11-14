using System.Linq;

namespace _5ECharacterBuilder.CharacterRaces
{
    class WoodElf : Elf
    {
        public WoodElf(ICharacter character) : base(character)
        {
            Attributes.Wisdom.RacialBonus = 1;

            using (var db = new CharacterBuilderDB())
            {
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Elf Weapon Training"));
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Mask of the Wild"));
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Fleet of Foot"));
            }

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