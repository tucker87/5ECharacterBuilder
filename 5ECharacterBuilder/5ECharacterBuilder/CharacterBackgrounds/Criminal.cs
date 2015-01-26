namespace _5ECharacterBuilder.CharacterBackgrounds
{
    sealed class Criminal : CharacterBackground
    {
        public Criminal(ICharacter character) : base(character)
        {
            Skills.Chosen.Add(AvailableSkill.Deception);
            Skills.Chosen.Add(AvailableSkill.Stealth);
            Skills.Max += 2;
            Tools.Available.Add(AvailableTool.ThievesTools);
            Tools.Chosen.Add(AvailableTool.ThievesTools);
        }

        public override string Background => "Criminal";
    }
}