using System;

namespace _5ECharacterBuilder
{
    public class RequirementsExpection : Exception
    {
        public RequirementsExpection()
        {
        }

        public RequirementsExpection(string message)
            : base(message)
        {
        }

        public RequirementsExpection(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
