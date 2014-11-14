using System.Linq;

namespace _5ECharacterBuilder.CharacterRaces
{
    class Dwarf : CharacterRace
    {
        public Dwarf(ICharacter character) : base(character)
        {
            Attributes.Constitution.RacialBonus = 2;

            ChosenLanguages.Add(AvailableLanguages.Common);
            ChosenLanguages.Add(AvailableLanguages.Dwarvish);

            WeaponProficiencies.Add(AvailableWeapon.BattleAxe);
            WeaponProficiencies.Add(AvailableWeapon.HandAxe);
            WeaponProficiencies.Add(AvailableWeapon.ThrowingHammer);
            WeaponProficiencies.Add(AvailableWeapon.WarHammer);

            AvailableToolProficiencies.Add(AvailableTool.SmithsTools);
            AvailableToolProficiencies.Add(AvailableTool.BrewersSupplies);
            AvailableToolProficiencies.Add(AvailableTool.MasonsTools);

            using (var db = new CharacterBuilderDB())
            {
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Darkvision"));
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Dwarven Resilience"));
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Dwarven Combat Training"));
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Stonecunning"));
            }
        }

        public override int LanguageCount
        {
            get { return 2; }
        }
        
        public override int Speed
        {
            get { return 25; }
        }
    }
}