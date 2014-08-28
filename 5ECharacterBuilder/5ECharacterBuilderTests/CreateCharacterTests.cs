using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    public class CreateCharacterTests
    {
        private static Character _character;
        private static CharacterAttributeScrores _characterAttributeScroreList;

        [TestInitialize]
        public static void Setup()
        {
            _characterAttributeScroreList = new CharacterAttributeScrores(1,2,3,4,5,6);
            _character = new Character(name:"John", hitDice: new List<int> { 8 }, characterAttributeScores: _characterAttributeScroreList);
        }
        [TestMethod]
        public void CharactersCanHaveNames()
        {
            Assert.AreEqual("John", _character.Name);
            _character.Name = "Jane";
            Assert.AreEqual("Jane", _character.Name);
        }

        [TestMethod]
        public void CharactersCanHaveOneHitDice()
        {
            _character = new Character(characterAttributeScores: _characterAttributeScroreList, hitDice: new List<int> { 8 });
            Assert.AreEqual(8, _character.HitDice[0]);
        }

        [TestMethod]
        public void CharactersCanHaveMultipleHitDice()
        {
            _character = new Character(characterAttributeScores: _characterAttributeScroreList, hitDice: new List<int> { 8, 4 });
            Assert.AreEqual(4, _character.HitDice[1]);
        }

        [TestMethod]
        public void CharactersMaxHpIsBasedOnAverageOfSingleHitDice()
        {
            _character = new Character(characterAttributeScores: _characterAttributeScroreList, hitDice: new List<int> { 8 });
            Assert.AreEqual(5, _character.MaxHp);
            _character = new Character(characterAttributeScores: _characterAttributeScroreList, hitDice: new List<int> { 6 });
            Assert.AreEqual(4, _character.MaxHp);
        }

        [TestMethod]
        public void CharactersMaxHpIsBasedOnAverageOfMultipleHitDice()
        {
            _character = new Character(characterAttributeScores: _characterAttributeScroreList, hitDice: new List<int> { 8, 6 });
            Assert.AreEqual(9, _character.MaxHp);
            _character = new Character(characterAttributeScores: _characterAttributeScroreList, hitDice: new List<int> { 6, 12 });
            Assert.AreEqual(11, _character.MaxHp);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AttributesCanNotBeLessThanOne()
        {
// ReSharper disable once ObjectCreationAsStatement
            new CharacterAttribute(0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AttributesCanNotBeGreaterThanTwenty()
        {
// ReSharper disable once ObjectCreationAsStatement
            new CharacterAttribute(21);
        }

        [TestMethod]
        public void CharactersAttributesScoresAreSet()
        {
            Assert.AreEqual(_characterAttributeScroreList.Strength, _character.CharacterAttributes.Strength);
            Assert.AreEqual(_characterAttributeScroreList.Constitution, _character.CharacterAttributes.Constitution);
            Assert.AreEqual(_characterAttributeScroreList.Dexterity, _character.CharacterAttributes.Dexterity);
            Assert.AreEqual(_characterAttributeScroreList.Intelligence, _character.CharacterAttributes.Intelligence);
            Assert.AreEqual(_characterAttributeScroreList.Wisdom, _character.CharacterAttributes.Wisdom);
            Assert.AreEqual(_characterAttributeScroreList.Charisma, _character.CharacterAttributes.Charisma);
        }

        [TestMethod]
        public void AttributeModifierIsZeroWhenGiven10()
        {
            var attributes = new CharacterAttribute(10);
            Assert.AreEqual(0, attributes.Modifier);
        }

        [TestMethod]
        public void AttributeModifierIsZeroWhenGiven11()
        {
            var attributes = new CharacterAttribute(11);
            Assert.AreEqual(0, attributes.Modifier);
        }

        [TestMethod]
        public void AttributeModifierIsOneWhenGiven12()
        {
            var attributes = new CharacterAttribute(12);
            Assert.AreEqual(1, attributes.Modifier);
        }

        [TestMethod]
        public void AttributeModifierIsTwoWhenGiven14()
        {
            var attributes = new CharacterAttribute(14);
            Assert.AreEqual(2, attributes.Modifier);
        }

        [TestMethod]
        public void AttributeModifierIsNegtiveOneWhenGiven8()
        {
            var attributes = new CharacterAttribute(8);
            Assert.AreEqual(-1, attributes.Modifier);
        }
    }
}
