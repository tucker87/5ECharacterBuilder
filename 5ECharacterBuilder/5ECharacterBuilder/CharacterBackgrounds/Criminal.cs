namespace _5ECharacterBuilder.CharacterBackgrounds
{
    sealed class Criminal : CharacterBackground
    {
        public Criminal(ICharacter character) : base(character)
        {
            Skills.Available.Add(_5ECharacterBuilder.AvailableSkill.Deception);
            Skills.Available.Add(_5ECharacterBuilder.AvailableSkill.Stealth);

            Skills.Chosen.Add(_5ECharacterBuilder.AvailableSkill.Deception);
            Skills.Chosen.Add(_5ECharacterBuilder.AvailableSkill.Stealth);

            Skills.Max += 2;

            Tools.Available.Add(AvailableTool.ThievesTools);
            Tools.Chosen.Add(AvailableTool.ThievesTools);
        }

        public override string Background
        {
            get { return "Criminal"; }
        }
    }
}