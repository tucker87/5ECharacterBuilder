namespace _5ECharacterBuilder.CharacterRaces
{
    class Human : CharacterRace
    {
        public Human(ICharacter character) : base(character)
        {
            character.Attributes = new CharacterAttributes(character.Attributes, new RacialBonuses(1, 1, 1, 1, 1, 1));
        }

        public override string Race { get { return "Human"; } }
        public override int Speed { get { return 30; } }
        public override string Size { get { return "Medium";} }
    }
}