using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    public class CharacterBaseTests
    {
        private static Character _character;

        [TestInitialize]
        public static void Setup()
        {
            _character = new Character(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
        }
        [TestMethod]
        public void CharactersCanHaveNames()
        {
            _character.Name = "John";
            Assert.AreEqual("John", _character.Name);
        }

        [TestMethod]
        public void CharactersMaxHpIsBasedOnMaxOfFirstHitDice()
        {
            var characterWithHitDice = new Character(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
            Assert.AreEqual(8, characterWithHitDice.MaxHp);
        }

        [TestMethod]
        public void CharactersMaxHpIsBasedOnMaxOfFirstPlusAverageOfRemainingHitDice()
        {
            var characterWithMultipleHitDice = new Character(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal, 2);
            Assert.AreEqual(13, characterWithMultipleHitDice.MaxHp);
        }

        [TestMethod]
        public void CharactersMaxHpIsBoostedByConstitutionModifier()
        {
            var highCon = new CharacterAttributeScores(constitution: 14);
            var characterWithHitDiceAndCon = new Character(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal, highCon);
            Assert.AreEqual(10, characterWithHitDiceAndCon.MaxHp);
        }

        [TestMethod]
        public void MyTestMethod()
        {
            
        }
    }
}
