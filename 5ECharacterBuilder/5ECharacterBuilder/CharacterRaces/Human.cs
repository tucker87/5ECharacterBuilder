using _5EDatabase;

namespace _5ECharacterBuilder.CharacterRaces
{
    internal class Human : CharacterRace
    {
        public Human(ICharacter character) : base(character)
        {
            foreach (var attribute in Abilities)
                attribute.RacialBonus = 1;

            Languages.Chosen.Add(Language.Common);
            Languages.Max += 2;

            AddRaceFeature("Ability Score Increase");
        }

        public override string Race => "Human";
    }
}