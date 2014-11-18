namespace _5ECharacterBuilder.CharacterBackgrounds
{
    class Acolyte : CharacterBackground
    {
        public Acolyte(ICharacter character) : base(character)
        {
           Skills.Available.Add(_5ECharacterBuilder.AvailableSkill.Insight);
           Skills.Available.Add(_5ECharacterBuilder.AvailableSkill.Religion);
           Skills.Chosen.Add(_5ECharacterBuilder.AvailableSkill.Insight);
           Skills.Chosen.Add(_5ECharacterBuilder.AvailableSkill.Religion);

            Languages.Max += 2;
        }

        public override string Background
        {
            get { return "Acolyte"; }
        }
    }
}