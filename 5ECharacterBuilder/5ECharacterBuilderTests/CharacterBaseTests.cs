using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    public class CharacterBaseTests
    {
        private static CharacterBase _characterBase;

        [TestInitialize]
        public static void Setup()
        {
            _characterBase = new CharacterBase(name:"John", attributeScores: new CharacterAttributeScores());
        }
        [TestMethod]
        public void CharactersCanHaveNames()
        {
            Assert.AreEqual("John", _characterBase.Name);
        }

        [TestMethod]
        public void CharactersMaxHpIsBasedOnMaxOfFirstHitDice()
        {
            var characterWithHitDice = new Monk(_characterBase, artisanTool:AvailableTools.AlchemistsSupplies);
            Assert.AreEqual(8, characterWithHitDice.MaxHp);
        }

        [TestMethod]
        public void CharactersMaxHpIsBasedOnMaxOfFirstPlusAverageOfRemainingHitDice()
        {
            var characterWithMultipleHitDice = new Monk(new Monk(_characterBase, artisanTool: AvailableTools.AlchemistsSupplies));
            Assert.AreEqual(13, characterWithMultipleHitDice.MaxHp);
        }

        [TestMethod]
        public void CharactersMaxHpIsBoostedByConstitutionModifier()
        {
            var highCon = new CharacterAttributeScores(constitution: 14);
            var characterWithHitDiceAndCon = new Monk(new CharacterBase(highCon),instrument:AvailableInstruments.Lute);
            Assert.AreEqual(10, characterWithHitDiceAndCon.MaxHp);
        }
    }
}
