using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterClasses
{
    public class Fighter : CharacterClass
    {
        public Fighter(ICharacter character, List<AvailableSkills> skillList = null, AvailableTools? artisanTool = null, AvailableInstruments? instrument = null) : base(character)
        {
        }
    }
}
