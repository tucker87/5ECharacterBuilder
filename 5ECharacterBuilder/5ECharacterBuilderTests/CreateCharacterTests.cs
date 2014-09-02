using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    class CreateCharacterTests
    {
        [TestMethod]
        public void FullCharactersMustHaveARaceAndClass()
        {
            var fullCharacter = new CompletedCharacter(new Human(new CharacterBase(new CharacterAttributeScores(10, 10, 10, 10, 10, 10))));
        }
    }

    internal class CompletedCharacter : ICharacter
    {
        public CompletedCharacter(ICharacter character)
        {
            
        }

        public CharacterAttributes Attributes { get; private set; }
        public List<int> HitDice { get; private set; }
        public int MaxHp { get; private set; }
        public string Name { get; private set; }
    }
}
