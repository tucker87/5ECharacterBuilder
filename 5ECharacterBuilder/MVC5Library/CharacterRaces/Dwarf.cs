using MVC5Library;

namespace _5ECharacterBuilder.CharacterRaces
{
    class Dwarf : CharacterRace
    {
        public Dwarf(ICharacter character) : base(character)
        {
            Abilities.Constitution.RacialBonus = 2;

            Languages.Chosen.Add(AvailableLanguages.Common);
            Languages.Chosen.Add(AvailableLanguages.Dwarvish);

            WeaponProficiencies.Add(AvailableWeapon.BattleAxe);
            WeaponProficiencies.Add(AvailableWeapon.HandAxe);
            WeaponProficiencies.Add(AvailableWeapon.ThrowingHammer);
            WeaponProficiencies.Add(AvailableWeapon.Warhammer);

            Tools.Available.Add(AvailableTool.SmithsTools);
            Tools.Available.Add(AvailableTool.BrewersSupplies);
            Tools.Available.Add(AvailableTool.MasonsTools);

            AddRaceFeature("Darkvision");
            AddRaceFeature("Dwarven Resilience");
            AddRaceFeature("Dwarven Combat Training");
            AddRaceFeature("Stonecunning");
            
            Languages.Max += 2;
        }
        
        public override int Speed => 25;
    }
}