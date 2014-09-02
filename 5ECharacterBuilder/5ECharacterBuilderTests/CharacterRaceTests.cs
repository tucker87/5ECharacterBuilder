using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    public class CharacterRaceTests
    {
        private static CharacterBase _characterBase;

        [TestInitialize]
        public static void Setup()
        {
            _characterBase = new CharacterBase(name: "John");
        }

        [TestMethod]
        public void HumansGetPlusOneToAllAttributeScores()
        {
            var human = new Human(_characterBase);
            Assert.AreEqual(11, human.Attributes.Strength.Score);
            Assert.AreEqual(11, human.Attributes.Constitution.Score);
            Assert.AreEqual(11, human.Attributes.Dexterity.Score);
            Assert.AreEqual(11, human.Attributes.Intelligence.Score);
            Assert.AreEqual(11, human.Attributes.Wisdom.Score);
            Assert.AreEqual(11, human.Attributes.Charisma.Score);
        }
    }
}