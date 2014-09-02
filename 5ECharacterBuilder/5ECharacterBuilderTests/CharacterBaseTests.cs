using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    public class CharacterBaseTests
    {
        private static CharacterBase _characterBase;
        private static CharacterAttributeScores _characterAttributeScrores;

        [TestInitialize]
        public static void Setup()
        {
            _characterAttributeScrores = new CharacterAttributeScores(10,10,10,10,10,10);
            _characterBase = new CharacterBase(name:"John", attributeScores: _characterAttributeScrores);
        }
        [TestMethod]
        public void CharactersCanHaveNames()
        {
            Assert.AreEqual("John", _characterBase.Name);
            _characterBase.Name = "Jane";
            Assert.AreEqual("Jane", _characterBase.Name);
        }

        //[TestMethod]
        //public void CharactersCanHaveOneHitDice()
        //{
        //    _characterBase = new CharacterBase(attributeScores: _characterAttributeScrores, hitDice: new List<int> { 8 });
        //    Assert.AreEqual(8, _characterBase.HitDice[0]);
        //}

        //[TestMethod]
        //public void CharactersCanHaveMultipleHitDice()
        //{
        //    _characterBase = new CharacterBase(attributeScores: _characterAttributeScrores, hitDice: new List<int> { 8, 4 });
        //    Assert.AreEqual(4, _characterBase.HitDice[1]);
        //}

        //[TestMethod]
        //public void CharactersMaxHpIsBasedOnAverageOfSingleHitDice()
        //{
        //    _characterBase = new CharacterBase(attributeScores: _characterAttributeScrores, hitDice: new List<int> { 8 });
        //    Assert.AreEqual(5, _characterBase.MaxHp);
        //    _characterBase = new CharacterBase(attributeScores: _characterAttributeScrores, hitDice: new List<int> { 6 });
        //    Assert.AreEqual(4, _characterBase.MaxHp);
        //}

        //[TestMethod]
        //public void CharactersMaxHpIsBasedOnAverageOfMultipleHitDice()
        //{
        //    _characterBase = new CharacterBase(attributeScores: _characterAttributeScrores, hitDice: new List<int> { 8, 6 });
        //    Assert.AreEqual(9, _characterBase.MaxHp);
        //    _characterBase = new CharacterBase(attributeScores: _characterAttributeScrores, hitDice: new List<int> { 6, 12 });
        //    Assert.AreEqual(11, _characterBase.MaxHp);
        //}

        //[TestMethod]
        //public void CharactersMaxHpIsBoostedByConstitutionModifier()
        //{

        //    var highCon = new CharacterAttributeScores(10, 14, 10, 10, 10, 10);
        //    _characterBase = new CharacterBase(attributeScores: highCon, hitDice: new List<int> { 8 });
        //    Assert.AreEqual(7, _characterBase.MaxHp);
        //}
    }
}
