using System;
using NUnit.Framework;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestFixture]
    class CharacterAttributeTests
    {
        private static ICharacter _character;
        private static CharacterAbilityScores _characterAbilityScrores;

        [SetUp]
        public static void SetUp()
        {
            _characterAbilityScrores = new CharacterAbilityScores();
            _character = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
        }
        [Test]
        public void AttributesCanNotBeLessThanOne()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<Exception>(() => new CharacterAbilityScores(0, 0, 0, 0, 0, 0));
        }

        [Test]
        public void AttributesCanNotBeGreaterThanTwenty()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<Exception>(() => new CharacterAbilityScores(21, 21, 21, 21, 21, 21));
        }

        [Test]
        public void CharactersAttributesScoresAreSet()
        {
            const int humanBonus = 1;
            Assert.AreEqual(_characterAbilityScrores.Strength + humanBonus, _character.Abilities.Strength.Score);
            Assert.AreEqual(_characterAbilityScrores.Constitution + humanBonus, _character.Abilities.Constitution.Score);
            Assert.AreEqual(_characterAbilityScrores.Dexterity + humanBonus, _character.Abilities.Dexterity.Score);
            Assert.AreEqual(_characterAbilityScrores.Intelligence + humanBonus, _character.Abilities.Intelligence.Score);
            Assert.AreEqual(_characterAbilityScrores.Wisdom + humanBonus, _character.Abilities.Wisdom.Score);
            Assert.AreEqual(_characterAbilityScrores.Charisma + humanBonus, _character.Abilities.Charisma.Score);
        }

        [Test]
        public void AttributeModifierIsZeroWhenGiven10()
        {
            var attributes = new CharacterAbilities(_characterAbilityScrores);
            Assert.AreEqual(0, attributes.Strength.Modifier);
        }

        [Test]
        public void AttributeModifierIsZeroWhenGiven11()
        {
            var attributesScore = new CharacterAbilityScores(11, 11, 11, 11, 11, 11);
            var attributes = new CharacterAbilities(attributesScore);
            Assert.AreEqual(0, attributes.Constitution.Modifier);
        }

        [Test]
        public void AttributeModifierIsOneWhenGiven12()
        {
            var attributesScore = new CharacterAbilityScores(12, 12, 12, 12, 12, 12);
            var attributes = new CharacterAbilities(attributesScore);
            Assert.AreEqual(1, attributes.Dexterity.Modifier);
        }

        [Test]
        public void AttributeModifierIsTwoWhenGiven14()
        {
            var attributesScore = new CharacterAbilityScores(14, 14, 14, 14, 14, 14);
            var attributes = new CharacterAbilities(attributesScore);
            Assert.AreEqual(2, attributes.Intelligence.Modifier);
        }

        [Test]
        public void AttributeModifierIsNegtiveOneWhenGiven8()
        {
            var attributesScore = new CharacterAbilityScores(8, 8, 8, 8, 8, 8);
            var attributes = new CharacterAbilities(attributesScore);
            Assert.AreEqual(-1, attributes.Wisdom.Modifier);
        }
    }
}
