namespace _5ECharacterBuilder.CharacterRaces
{
    class Human : CharacterRace
    {
        public Human(ICharacter character) : base(character)
        {
            foreach (var attribute in Attributes)
                attribute.RacialBonus = 1;

            Languages.Chosen.Add(AvailableLanguages.Common);
            Languages.Max += 2;
        }

        public override string Race
        {
            get { return "Human"; }
        }
    }
}