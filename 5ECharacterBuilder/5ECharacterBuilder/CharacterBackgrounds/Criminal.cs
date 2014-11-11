using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterBackgrounds
{
    sealed class Criminal : CharacterBackground
    {
        public Criminal(ICharacter character) : base(character)
        {
            AvailableSkills.Add(AvailableSkill.Deception);
            AvailableSkills.Add(AvailableSkill.Stealth);

            TrainedSkills.Add(AvailableSkill.Deception);
            TrainedSkills.Add(AvailableSkill.Stealth);

            AvailableToolProficiencies.Add(AvailableTool.ThievesTools);
            ChosenToolProficiencies.Add(AvailableTool.ThievesTools);
        }

        public override string Background
        {
            get { return "Criminal"; }
        }
    }
}