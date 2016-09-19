using System.Linq;
using NUnit.Framework;
using _5ECharacterBuilder;
using _5EDatabase;

namespace _5ECharacterBuilderTests
{
    [TestFixture]
    class CharacterMulticlassTests
    {
        private static ICharacter _character;

        [SetUp]
        public static void SetUp()
        {
            _character = CharacterFactory.BuildACharacter(Race.Human, Class.Monk, Background.Criminal);
        }

        [Test]
        public void CanRetrieveCharacterClassString()
        {
            Assert.AreEqual("Monk 1", _character.ClassesString);
            CharacterFactory.LevelUp(ref _character, Class.Monk);

            var test = _character.Classes.Union(new[] {Class.Monk}).Union(new[] {Class.Monk});

            Assert.AreEqual("Monk 2", _character.ClassesString);
            CharacterFactory.LevelUp(ref _character, Class.Fighter);
            Assert.AreEqual("Monk 2 Fighter 1", _character.ClassesString);
            CharacterFactory.LevelUp(ref _character, Class.Fighter);
            Assert.AreEqual("Monk 2 Fighter 2", _character.ClassesString);
        }
    }
}
