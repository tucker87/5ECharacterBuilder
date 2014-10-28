using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    class CharacterAttributeTests
    {
        private static Character _character;
        private static CharacterAttributeScores _characterAttributeScrores;

        [TestInitialize]
        public static void Setup()
        {
            _characterAttributeScrores = new CharacterAttributeScores();
            _character = new Character(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AttributesCanNotBeLessThanOne()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new CharacterAttributeScores(0, 0, 0, 0, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AttributesCanNotBeGreaterThanTwenty()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new CharacterAttributeScores(21, 21, 21, 21, 21, 21);
        }

        [TestMethod]
        public void CharactersAttributesScoresAreSet()
        {
            const int humanBonus = 1;
            Assert.AreEqual(_characterAttributeScrores.Strength + humanBonus, _character.Attributes.Strength.Score);
            Assert.AreEqual(_characterAttributeScrores.Constitution + humanBonus, _character.Attributes.Constitution.Score);
            Assert.AreEqual(_characterAttributeScrores.Dexterity + humanBonus, _character.Attributes.Dexterity.Score);
            Assert.AreEqual(_characterAttributeScrores.Intelligence + humanBonus, _character.Attributes.Intelligence.Score);
            Assert.AreEqual(_characterAttributeScrores.Wisdom + humanBonus, _character.Attributes.Wisdom.Score);
            Assert.AreEqual(_characterAttributeScrores.Charisma + humanBonus, _character.Attributes.Charisma.Score);
        }

        [TestMethod]
        public void AttributeModifierIsZeroWhenGiven10()
        {
            var attributes = new CharacterAttributes(_characterAttributeScrores);
            Assert.AreEqual(0, attributes.Strength.Modifier);
        }

        [TestMethod]
        public void AttributeModifierIsZeroWhenGiven11()
        {
            var attributesScore = new CharacterAttributeScores(11, 11, 11, 11, 11, 11);
            var attributes = new CharacterAttributes(attributesScore);
            Assert.AreEqual(0, attributes.Constitution.Modifier);
        }

        [TestMethod]
        public void AttributeModifierIsOneWhenGiven12()
        {
            var attributesScore = new CharacterAttributeScores(12, 12, 12, 12, 12, 12);
            var attributes = new CharacterAttributes(attributesScore);
            Assert.AreEqual(1, attributes.Dexterity.Modifier);
        }

        [TestMethod]
        public void AttributeModifierIsTwoWhenGiven14()
        {
            var attributesScore = new CharacterAttributeScores(14, 14, 14, 14, 14, 14);
            var attributes = new CharacterAttributes(attributesScore);
            Assert.AreEqual(2, attributes.Intelligence.Modifier);
        }

        [TestMethod]
        public void AttributeModifierIsNegtiveOneWhenGiven8()
        {
            var attributesScore = new CharacterAttributeScores(8, 8, 8, 8, 8, 8);
            var attributes = new CharacterAttributes(attributesScore);
            Assert.AreEqual(-1, attributes.Wisdom.Modifier);
        }
    }
}
