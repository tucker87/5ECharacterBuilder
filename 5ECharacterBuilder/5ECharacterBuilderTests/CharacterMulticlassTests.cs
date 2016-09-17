using NUnit.Framework;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestFixture]
    class CharacterMulticlassTests
    {
        private static ICharacter _character;

        [SetUp]
        public static void SetUp()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
        }

        [Test]
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
