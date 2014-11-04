namespace _5ECharacterBuilder.CharacterBackgrounds
{
    sealed class Acolyte : CharacterBackground
    {
        public Acolyte(Character character) : base(character)
        {
            BackgroundSkills.AddRange(new[] { AvailableSkill.Insight, AvailableSkill.Religion });
        }
        public override string Background { get {return "Acolyte"; } }
        public override int BackgroundLanguageCount {  get { return 2; } }
    }
}