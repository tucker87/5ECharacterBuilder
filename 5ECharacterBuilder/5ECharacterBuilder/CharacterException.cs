using System;
using _5EDatabase;

namespace _5ECharacterBuilder
{
    public class RequirementsExpection : Exception { }

    public class TooManySkillsException : Exception
    {
        public TooManySkillsException(Skill chosenSkill) : base("" + chosenSkill) { }
    }

    public class SkillNotAvailableException : Exception
    {
        public SkillNotAvailableException(Skill chosenSkill) : base("" + chosenSkill) { }
    }
}
