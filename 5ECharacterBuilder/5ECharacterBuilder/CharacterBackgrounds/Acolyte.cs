namespace _5ECharacterBuilder.CharacterBackgrounds
{
    class Acolyte : CharacterBackground
    {
        public Acolyte(ICharacter character) : base(character)
        {
           Skills.Available.Add(_5ECharacterBuilder.AvailableSkills.Insight);
           Skills.Available.Add(_5ECharacterBuilder.AvailableSkills.Religion);
           Skills.Chosen.Add(_5ECharacterBuilder.AvailableSkills.Insight);
           Skills.Chosen.Add(_5ECharacterBuilder.AvailableSkills.Religion);

            Languages.Max += 2;
        }

        public override string Background
        {
            get { return "Acolyte"; }
        }
    }
}