using _5EDatabase;

namespace _5ECharacterBuilder.CharacterRaces
{
    internal class Dwarf : CharacterRace
    {
        public Dwarf(ICharacter character) : base(character)
        {
            Abilities.Constitution.RacialBonus = 2;

            Languages.Chosen.Add(Language.Common);
            Languages.Chosen.Add(Language.Dwarvish);

            WeaponProficiencies.Add(WeaponType.BattleAxe);
            WeaponProficiencies.Add(WeaponType.HandAxe);
            WeaponProficiencies.Add(WeaponType.ThrowingHammer);
            WeaponProficiencies.Add(WeaponType.Warhammer);

            Tools.Available.Add(Tool.SmithsTools);
            Tools.Available.Add(Tool.BrewersSupplies);
            Tools.Available.Add(Tool.MasonsTools);

            AddRaceFeature("Darkvision");
            AddRaceFeature("Dwarven Resilience");
            AddRaceFeature("Dwarven Combat Training");
            AddRaceFeature("Stonecunning");
            
            Languages.Max += 2;
        }
        
        public override int Speed => 25;
    }
}