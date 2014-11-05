using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    class CharacterMulticlassTests
    {
        private static ICharacter _character;

        [TestInitialize]
        public static void Setup()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
        }

        [TestMethod]
        public void CanRetrieveCharacterClassString()
        {
            Assert.AreEqual("Monk 1", _character.ClassesString);
            CharacterFactory.LevelUp(_character, AvailableClasses.Monk);
            Assert.AreEqual("Monk 2", _character.ClassesString);
            CharacterFactory.LevelUp(_character, AvailableClasses.Fighter);
            Assert.AreEqual("Monk 2 Fighter 1", _character.ClassesString);
            CharacterFactory.LevelUp(_character, AvailableClasses.Fighter);
            Assert.AreEqual("Monk 2 Fighter 2", _character.ClassesString);
        }
    }
}
