namespace _5ECharacterBuilder.CharacterBackgrounds
{
    class Acolyte : CharacterBackground
    {
        public Acolyte(ICharacter character) : base(character)
        {
           AvailableSkills.Add(AvailableSkill.Insight);
           AvailableSkills.Add(AvailableSkill.Religion);
           ChosenSkills.Add(AvailableSkill.Insight);
           ChosenSkills.Add(AvailableSkill.Religion);
        }

        public override string Background
        {
            get { return "Acolyte"; }
        }

        public override int LanguageCount
        {
            get { return 2; }
        }
    }
}