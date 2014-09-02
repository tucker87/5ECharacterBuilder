using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    public class CharacterRaceTests
    {
        private static CharacterBase _characterBase;
        private static CharacterAttributeScores _characterAttributeScrores;

        [TestInitialize]
        public static void Setup()
        {
            _characterAttributeScrores = new CharacterAttributeScores(10,10,10,10,10,10);
            _characterBase = new CharacterBase(name: "John", attributeScores: _characterAttributeScrores);
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