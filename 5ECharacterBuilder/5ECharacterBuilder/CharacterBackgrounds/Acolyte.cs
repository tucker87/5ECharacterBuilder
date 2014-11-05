namespace _5ECharacterBuilder.CharacterBackgrounds
{
    internal sealed class Acolyte : CharacterBackground
    {
        public Acolyte(ICharacter character) : base(character)
        {
            BackgroundSkills.AddRange(new[] {AvailableSkill.Insight, AvailableSkill.Religion});
        }

        public override string Background
        {
            get { return "Acolyte"; }
        }

        public override int BackgroundLanguageCount
        {
            get { return 2; }
        }
    }
}