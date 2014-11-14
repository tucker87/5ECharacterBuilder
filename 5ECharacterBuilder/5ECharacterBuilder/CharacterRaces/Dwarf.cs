using System.Linq;

namespace _5ECharacterBuilder.CharacterRaces
{
    class Dwarf : CharacterRace
    {
        public Dwarf(ICharacter character) : base(character)
        {
            Attributes.Constitution.RacialBonus = 2;

            Languages.Chosen.Add(AvailableLanguages.Common);
            Languages.Chosen.Add(AvailableLanguages.Dwarvish);

            WeaponProficiencies.Add(AvailableWeapon.BattleAxe);
            WeaponProficiencies.Add(AvailableWeapon.HandAxe);
            WeaponProficiencies.Add(AvailableWeapon.ThrowingHammer);
            WeaponProficiencies.Add(AvailableWeapon.Warhammer);

            Tools.Available.Add(AvailableTool.SmithsTools);
            Tools.Available.Add(AvailableTool.BrewersSupplies);
            Tools.Available.Add(AvailableTool.MasonsTools);

            using (var db = new CharacterBuilderDB())
            {
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Darkvision"));
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Dwarven Resilience"));
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Dwarven Combat Training"));
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Stonecunning"));
            }

            Languages.Max += 2;
        }
        
        public override int Speed
        {
            get { return 25; }
        }
    }
}