using System;
using MVC5Library;

namespace _5ECharacterBuilder
{
    public class RequirementsExpection : Exception { }

    public class TooManySkillsException : Exception
    {
        public TooManySkillsException(AvailableSkill chosenSkill) : base("" + chosenSkill) { }
    }

    public class SkillNotAvailableException : Exception
    {
        public SkillNotAvailableException(AvailableSkill chosenSkill) : base("" + chosenSkill) { }
    }
}
