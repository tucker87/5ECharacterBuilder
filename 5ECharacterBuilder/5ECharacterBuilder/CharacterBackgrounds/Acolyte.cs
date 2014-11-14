namespace _5ECharacterBuilder.CharacterBackgrounds
{
    class Acolyte : CharacterBackground
    {
        public Acolyte(ICharacter character) : base(character)
        {
           Skills.Available.Add(AvailableSkill.Insight);
           Skills.Available.Add(AvailableSkill.Religion);
           Skills.Chosen.Add(AvailableSkill.Insight);
           Skills.Chosen.Add(AvailableSkill.Religion);

            Languages.Max += 2;
        }

        public override string Background
        {
            get { return "Acolyte"; }
        }
    }
}