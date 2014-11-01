using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    public class CharacterRaceTests
    {
        private static ICharacter _character;

        [TestInitialize]
        public static void Setup()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Fighter, AvailableBackgrounds.Criminal);
        }

        [TestMethod]
        public void HumansGetPlusOneToAllAttributeScores()
        {
            Assert.AreEqual(11, _character.Attributes.Strength.Score);
            Assert.AreEqual(11, _character.Attributes.Constitution.Score);
            Assert.AreEqual(11, _character.Attributes.Dexterity.Score);
            Assert.AreEqual(11, _character.Attributes.Intelligence.Score);
            Assert.AreEqual(11, _character.Attributes.Wisdom.Score);
            Assert.AreEqual(11, _character.Attributes.Charisma.Score);
        }
    }
}