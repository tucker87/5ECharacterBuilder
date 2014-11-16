using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    public class CharacterBaseTests
    {
        private static ICharacter _character;

        [TestInitialize]
        public static void Setup()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
        }
        [TestMethod]
        public void CharactersCanHaveNames()
        {
            _character.SetName("John");
            Assert.AreEqual("John", _character.Name);
        }

        [TestMethod]
        public void CharactersMaxHpIsBasedOnMaxOfFirstHitDice()
        {
            var characterWithHitDice = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
            Assert.AreEqual(8, characterWithHitDice.MaxHp);
        }

        [TestMethod]
        public void CharactersMaxHpIsBasedOnMaxOfFirstPlusAverageOfRemainingHitDice()
        {
            var characterWithMultipleHitDice = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal, 2);
            Assert.AreEqual(13, characterWithMultipleHitDice.MaxHp);
        }

        [TestMethod]
        public void CharactersMaxHpIsBoostedByConstitutionModifier()
        {
            var highCon = new CharacterAttributeScores(constitution: 14);
            var characterWithHitDiceAndCon = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
            characterWithHitDiceAndCon.SetAttributes(new CharacterAttributes(highCon));
            Assert.AreEqual(10, characterWithHitDiceAndCon.MaxHp);
        }

        [TestMethod]
        public void Level1CharactersHaveAProficiencyBonusOf2()
        {
            Assert.AreEqual(2, _character.ProficiencyBonus);
        }

        [TestMethod]
        public void Level5CharactersHaveAProficiencyBonusOf3()
        {
            for (var i = 1; i < 5; i++)
                CharacterFactory.LevelUp(_character, AvailableClasses.Monk);
            Assert.AreEqual(3, _character.ProficiencyBonus);
        }

        [TestMethod]
        public void Level9CharactersHaveAProficiencyBonusOf4()
        {
            for (var i = 1; i < 9; i++)
                CharacterFactory.LevelUp(_character, AvailableClasses.Monk);
            Assert.AreEqual(4, _character.ProficiencyBonus);
        }

        [TestMethod]
        public void Level13CharactersHaveAProficiencyBonusOf5()
        {
            for (var i = 1; i < 13; i++)
                CharacterFactory.LevelUp(_character, AvailableClasses.Monk);
            Assert.AreEqual(5, _character.ProficiencyBonus);
        }

        [TestMethod]
        public void Level17CharactersHaveAProficiencyBonusOf6()
        {
            for (var i = 1; i < 17; i++)
                CharacterFactory.LevelUp(_character, AvailableClasses.Monk);
            Assert.AreEqual(6, _character.ProficiencyBonus);
        }
    }
}