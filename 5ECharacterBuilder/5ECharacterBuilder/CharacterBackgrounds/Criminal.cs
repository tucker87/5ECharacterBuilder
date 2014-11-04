using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterBackgrounds
{
    sealed class Criminal : CharacterBackground
    {
        public Criminal(Character character) : base(character)
        {
            BackgroundSkills.AddRange(new List<AvailableSkill>{AvailableSkill.Deception, AvailableSkill.Stealth});
            ToolProficiencies.Add(AvailableTool.ThievesTools);
        }

        public override string Background
        {
            get { return "Criminal"; }
        }
    }
}