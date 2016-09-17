using NUnit.Framework;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestFixture]
    public class CharacterBaseTests
    {
        private static ICharacter _character;

        [SetUp]
        public void SetUp()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
        }
        [Test]
        public void CharactersCanHaveNames()
        {
            _character.SetName("John");
            Assert.AreEqual("John", _character.Name);
        }

        [Test]
        public void CharactersMaxHpIsBasedOnMaxOfFirstHitDice()
        {
            var characterWithHitDice = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
            Assert.AreEqual(8, characterWithHitDice.MaxHp);
        }

        [Test]
        public void CharactersMaxHpIsBasedOnMaxOfFirstPlusAverageOfRemainingHitDice()
        {
            var characterWithMultipleHitDice = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal, 2);
            Assert.AreEqual(13, characterWithMultipleHitDice.MaxHp);
        }

        [Test]
        public void CharactersMaxHpIsBoostedByConstitutionModifier()
        {
            var highCon = new CharacterAbilityScores(constitution: 14);
            var characterWithHitDiceAndCon = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
            characterWithHitDiceAndCon.SetAttributes(new CharacterAbilities(highCon));
            Assert.AreEqual(10, characterWithHitDiceAndCon.MaxHp);
        }

        [Test]
        public void CharactersHitDiceIsOutputProperly()
        {
            Assert.AreEqual("1d8", "" +_character.HitDice);
            CharacterFactory.LevelUp(_character, AvailableClasses.Monk);
            Assert.AreEqual("2d8", "" + _character.HitDice);
        }

        [Test]
        public void Level1CharactersHaveAProficiencyBonusOf2()
        {
            Assert.AreEqual(2, _character.ProficiencyBonus);
        }

        [Test]
        public void Level5CharactersHaveAProficiencyBonusOf3()
        {
            TestingUtility.LevelTo(_character, 5, AvailableClasses.Monk);
            Assert.AreEqual(3, _character.ProficiencyBonus);
        }

        [Test]
        public void Level9CharactersHaveAProficiencyBonusOf4()
        {
            for (var i = 1; i < 9; i++)
                CharacterFactory.LevelUp(_character, AvailableClasses.Monk);
            Assert.AreEqual(4, _character.ProficiencyBonus);
        }

        [Test]
        public void Level13CharactersHaveAProficiencyBonusOf5()
        {
            TestingUtility.LevelTo(_character, 13, AvailableClasses.Monk);
            Assert.AreEqual(5, _character.ProficiencyBonus);
        }

        [Test]
        public void Level17CharactersHaveAProficiencyBonusOf6()
        {
            TestingUtility.LevelTo(_character, 17, AvailableClasses.Monk);
            Assert.AreEqual(6, _character.ProficiencyBonus);
        }

        [Test]
        public void Level4CharactersHave2AbilityScoreImprovements()
        {
            Assert.AreEqual(0, _character.Abilities.ImprovementPoints);
            TestingUtility.LevelTo(_character, 4, AvailableClasses.Monk);
            Assert.AreEqual(2, _character.Abilities.ImprovementPoints);
        }

        [Test]
        public void Level8CharactersHave4AbilityScoreImprovements()
        {
            Assert.AreEqual(0, _character.Abilities.ImprovementPoints);
            TestingUtility.LevelTo(_character, 8, AvailableClasses.Monk);
            Assert.AreEqual(4, _character.Abilities.ImprovementPoints);
        }

        [Test]
        public void Level12CharactersHave6AbilityScoreImprovements()
        {
            Assert.AreEqual(0, _character.Abilities.ImprovementPoints);
            TestingUtility.LevelTo(_character, 12, AvailableClasses.Monk);
            Assert.AreEqual(6, _character.Abilities.ImprovementPoints);
        }

        [Test]
        public void Level16CharactersHave8AbilityScoreImprovements()
        {
            Assert.AreEqual(0, _character.Abilities.ImprovementPoints);
            TestingUtility.LevelTo(_character, 16, AvailableClasses.Monk);
            Assert.AreEqual(8, _character.Abilities.ImprovementPoints);
        }

        [Test]
        public void Level19CharactersHave10AbilityScoreImprovements()
        {
            Assert.AreEqual(0, _character.Abilities.ImprovementPoints);
            TestingUtility.LevelTo(_character, 19, AvailableClasses.Monk);
            Assert.AreEqual(10, _character.Abilities.ImprovementPoints);
        }

        [Test]
        public void CharactersCanSpendTheirAbilityScoreImprovements()
        {
            TestingUtility.LevelTo(_character, 4, AvailableClasses.Monk);
            Assert.AreEqual(11, _character.Abilities.Strength.Score);
            _character.ImproveAbility("Strength");
            Assert.AreEqual(12, _character.Abilities.Strength.Score);
        }

        [Test]
        public void CharactersCantSpendAbilityScoreImprovementsTheyDontHave()
        {
            _character.ImproveAbility("Strength");
            Assert.AreEqual(11, _character.Abilities.Strength.Score);
        }

        [Test]
        public void AcrobaticsSkillBonusIsCalculatedBasedOnProfAndDex()
        {
            Assert.AreEqual(0, _character.SkillBonus(AvailableSkill.Acrobatics));
            _character.ChooseSkill(AvailableSkill.Acrobatics);
            Assert.AreEqual(2, _character.SkillBonus(AvailableSkill.Acrobatics));
            TestingUtility.LevelTo(_character, 5, AvailableClasses.Monk);
            Assert.AreEqual(3, _character.SkillBonus(AvailableSkill.Acrobatics));
            _character.Abilities.Dexterity.Score = 12;
            Assert.AreEqual(4, _character.SkillBonus(AvailableSkill.Acrobatics));
        }

        [Test]
        public void CharactersCanLevelDown()
        {
            Assert.AreEqual(1, _character.Level);
            _character.LevelUp(AvailableClasses.Monk);
            Assert.AreEqual(2, _character.Level);
            _character.LevelDown();
            Assert.AreEqual(1, _character.Level);
            Assert.IsFalse(_character.Features.AllFeatures.ContainsKey("Ki"));
        }
    }
}