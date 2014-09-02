using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    public class CharacterClassTests
    {
        private static CharacterBase _characterBase;
        private static CharacterAttributeScores _characterAttributeScrores;

        [TestInitialize]
        public static void Setup()
        {
            _characterAttributeScrores = new CharacterAttributeScores(10, 10, 10, 10, 10, 10);
            _characterBase = new CharacterBase(name: "John", attributeScores: _characterAttributeScrores);
        }

        [TestMethod]
        public void MonksHave1D8HitDiceAtLevelOne()
        {
            var monk = new Monk(_characterBase);
            Assert.AreEqual(8, monk.HitDice[0]);
        }


    }
}