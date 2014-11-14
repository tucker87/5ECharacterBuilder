namespace _5ECharacterBuilder.CharacterRaces
{
    class Human : CharacterRace
    {
        public Human(ICharacter character) : base(character)
        {
            foreach (var attribute in Attributes)
                attribute.RacialBonus = 1;

            ChosenLanguages.Add(AvailableLanguages.Common);
        }

        public override string Race
        {
            get { return "Human"; }
        }

        public override int LanguageCount
        {
            get { return 2; }
        }
    }
}