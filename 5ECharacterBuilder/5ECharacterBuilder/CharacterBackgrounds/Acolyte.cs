namespace _5ECharacterBuilder.CharacterBackgrounds
{
    class Acolyte : CharacterBackground
    {
        public Acolyte(ICharacter character) : base(character)
        {
            Skills.Chosen.Add(AvailableSkill.Insight);
            Skills.Chosen.Add(AvailableSkill.Religion);

            Skills.Max += 2;

            Languages.Max += 2;
        }

        public override string Background
        {
            get { return "Acolyte"; }
        }
    }
}