namespace _5ECharacterBuilder.CharacterBackgrounds
{
    class Acolyte : CharacterBackground
    {
        public Acolyte(ICharacter character) : base(character)
        {
           TrainedSkills.AddRange(new[] {AvailableSkill.Insight, AvailableSkill.Religion});
           AvailableSkills.AddRange(new[] {AvailableSkill.Insight, AvailableSkill.Religion});
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