using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterBackgrounds
{
    class Criminal : CharacterBackground
    {
        public Criminal(ICharacter character) : base(character)
        {
            var currentSkills = new List<AvailableSkill> {AvailableSkill.Deception, AvailableSkill.Stealth};
            character.AddBackgroundSkills(currentSkills);

            var currentTools = new List<AvailableTool> {AvailableTool.ThievesTools};
            character.AddToolProfs(currentTools);

        }

        public override string Background {get { return "Criminal"; }}
            //Rogue should choose one gaming set to be proficient with.
            //var armory = new Armory();
            //var gamingSets = armory.GetGamingSets();
            //currentTools.AddRange(gamingSets);
    }
}