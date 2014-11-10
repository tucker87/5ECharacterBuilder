using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterBackgrounds
{
    sealed class Criminal : CharacterBackground
    {
        public Criminal(ICharacter character) : base(character)
        {
            AvailableSkills.AddRange(new List<AvailableSkill>{AvailableSkill.Deception, AvailableSkill.Stealth});
            TrainedSkills.AddRange(new List<AvailableSkill>{AvailableSkill.Deception, AvailableSkill.Stealth});
            AvailableToolProficiencies.Add(AvailableTool.ThievesTools);
        }

        public override string Background
        {
            get { return "Criminal"; }
        }
    }
}