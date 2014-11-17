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
            var highCon = new CharacterAbilityScores(constitution: 14);
            var characterWithHitDiceAndCon = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
            characterWithHitDiceAndCon.SetAttributes(new CharacterAbilities(highCon));
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
            TestingUtility.LevelTo(_character, 5, AvailableClasses.Monk);
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
            TestingUtility.LevelTo(_character, 13, AvailableClasses.Monk);
            Assert.AreEqual(5, _character.ProficiencyBonus);
        }

        [TestMethod]
        public void Level17CharactersHaveAProficiencyBonusOf6()
        {
            TestingUtility.LevelTo(_character, 17, AvailableClasses.Monk);
            Assert.AreEqual(6, _character.ProficiencyBonus);
        }

        [TestMethod]
        public void Level4CharactersHave2AbilityScoreImprovements()
        {
            Assert.AreEqual(0, _character.Abilities.ImprovementPoints);
            TestingUtility.LevelTo(_character, 4, AvailableClasses.Monk);
            Assert.AreEqual(2, _character.Abilities.ImprovementPoints);
        }

        [TestMethod]
        public void Level8CharactersHave4AbilityScoreImprovements()
        {
            Assert.AreEqual(0, _character.Abilities.ImprovementPoints);
            TestingUtility.LevelTo(_character, 8, AvailableClasses.Monk);
            Assert.AreEqual(4, _character.Abilities.ImprovementPoints);
        }

        [TestMethod]
        public void Level12CharactersHave6AbilityScoreImprovements()
        {
            Assert.AreEqual(0, _character.Abilities.ImprovementPoints);
            TestingUtility.LevelTo(_character, 12, AvailableClasses.Monk);
            Assert.AreEqual(6, _character.Abilities.ImprovementPoints);
        }

        [TestMethod]
        public void Level16CharactersHave8AbilityScoreImprovements()
        {
            Assert.AreEqual(0, _character.Abilities.ImprovementPoints);
            TestingUtility.LevelTo(_character, 16, AvailableClasses.Monk);
            Assert.AreEqual(8, _character.Abilities.ImprovementPoints);
        }

        [TestMethod]
        public void Level19CharactersHave10AbilityScoreImprovements()
        {
            Assert.AreEqual(0, _character.Abilities.ImprovementPoints);
            TestingUtility.LevelTo(_character, 19, AvailableClasses.Monk);
            Assert.AreEqual(10, _character.Abilities.ImprovementPoints);
        }

        [TestMethod]
        public void CharactersCanSpendTheirAbilityScoreImprovements()
        {
            TestingUtility.LevelTo(_character, 4, AvailableClasses.Monk);
            Assert.AreEqual(11, _character.Abilities.Strength.Score);
            _character.ImproveAbility("Strength");
            Assert.AreEqual(12, _character.Abilities.Strength.Score);
        }

        [TestMethod]
        public void CharactersCantSpendAbilityScoreImprovementsTheyDontHave()
        {
            _character.ImproveAbility("Strength");
            Assert.AreEqual(11, _character.Abilities.Strength.Score);
        }
    }
}