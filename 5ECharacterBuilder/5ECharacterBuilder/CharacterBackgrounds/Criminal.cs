using _5EDatabase;

namespace _5ECharacterBuilder.CharacterBackgrounds
{
    internal sealed class Criminal : CharacterBackground
    {
        public Criminal(ICharacter character) : base(character)
        {
            Skills.Chosen.Add(Skill.Deception);
            Skills.Chosen.Add(Skill.Stealth);
            Skills.Max += 2;
            Tools.Available.Add(Tool.ThievesTools);
            Tools.Chosen.Add(Tool.ThievesTools);
        }

        public override string Background => "Criminal";
    }
}