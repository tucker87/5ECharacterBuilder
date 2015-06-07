namespace _5ECharacterBuilder.CharacterRaces
{
    class Human : CharacterRace
    {
        public Human(ICharacter character) : base(character)
        {
            foreach (var attribute in Abilities)
                attribute.Value.RacialBonus = 1;

            Languages.Chosen.Add(AvailableLanguages.Common);
            Languages.Max += 2;

            AddRaceFeature("Ability Score Increase");
        }

        public override string Race => "Human";
    }
}