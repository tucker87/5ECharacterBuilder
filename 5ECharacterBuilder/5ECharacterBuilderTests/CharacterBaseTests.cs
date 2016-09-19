using System.Linq;
using NUnit.Framework;
using _5ECharacterBuilder;
using _5EDatabase;

namespace _5ECharacterBuilderTests
{
    [TestFixture]
    public class CharacterBaseTests
    {
        private static ICharacter _character;

        [SetUp]
        public void SetUp()
        {
            _character = CharacterFactory.BuildACharacter(Race.Human, Class.Monk, Background.Criminal);
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
            var characterWithHitDice = CharacterFactory.BuildACharacter(Race.Human, Class.Monk, Background.Criminal);
            Assert.AreEqual(8, characterWithHitDice.MaxHp);
        }

        [Test]
        public void CharactersMaxHpIsBasedOnMaxOfFirstPlusAverageOfRemainingHitDice()
        {
            var characterWithMultipleHitDice = CharacterFactory.BuildACharacter(Race.Human, Class.Monk, Background.Criminal, 2);
            Assert.AreEqual(13, characterWithMultipleHitDice.MaxHp);
        }

        [Test]
        public void CharactersMaxHpIsBoostedByConstitutionModifier()
        {
            var highCon = new CharacterAbilityScores(constitution: 14);
            var characterWithHitDiceAndCon = CharacterFactory.BuildACharacter(Race.Human, Class.Monk, Background.Criminal);
            characterWithHitDiceAndCon.SetAttributes(new CharacterAbilities(highCon));
            Assert.AreEqual(10, characterWithHitDiceAndCon.MaxHp);
        }
        
        [Test]
        public void CharactersHitDiceIsOutputProperly()
        {
            Assert.AreEqual("1d8", "" +_character.HitDice);
            CharacterFactory.LevelUp(ref _character, Class.Monk);
            Assert.AreEqual(2, _character.HitDice.Count);
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
            TestingUtility.LevelTo(ref _character, 5, Class.Monk);
            Assert.AreEqual(3, _character.ProficiencyBonus);
        }

        [Test]
        public void Level9CharactersHaveAProficiencyBonusOf4()
        {
            for (var i = 1; i < 9; i++)
                CharacterFactory.LevelUp(ref _character, Class.Monk);
            Assert.AreEqual(4, _character.ProficiencyBonus);
        }

        [Test]
        public void Level13CharactersHaveAProficiencyBonusOf5()
        {
            TestingUtility.LevelTo(ref _character, 13, Class.Monk);
            Assert.AreEqual(5, _character.ProficiencyBonus);
        }

        [Test]
        public void Level17CharactersHaveAProficiencyBonusOf6()
        {
            TestingUtility.LevelTo(ref _character, 17, Class.Monk);
            Assert.AreEqual(6, _character.ProficiencyBonus);
        }

        [Test]
        public void Level4CharactersHave2AbilityScoreImprovements()
        {
            Assert.AreEqual(0, _character.Abilities.ImprovementPoints);
            TestingUtility.LevelTo(ref _character, 4, Class.Monk);
            Assert.AreEqual(2, _character.Abilities.ImprovementPoints);
        }

        [Test]
        public void Level8CharactersHave4AbilityScoreImprovements()
        {
            Assert.AreEqual(0, _character.Abilities.ImprovementPoints);
            TestingUtility.LevelTo(ref _character, 8, Class.Monk);
            Assert.AreEqual(4, _character.Abilities.ImprovementPoints);
        }

        [Test]
        public void Level12CharactersHave6AbilityScoreImprovements()
        {
            Assert.AreEqual(0, _character.Abilities.ImprovementPoints);
            TestingUtility.LevelTo(ref _character, 12, Class.Monk);
            Assert.AreEqual(6, _character.Abilities.ImprovementPoints);
        }

        [Test]
        public void Level16CharactersHave8AbilityScoreImprovements()
        {
            Assert.AreEqual(0, _character.Abilities.ImprovementPoints);
            TestingUtility.LevelTo(ref _character, 16, Class.Monk);
            Assert.AreEqual(8, _character.Abilities.ImprovementPoints);
        }

        [Test]
        public void Level19CharactersHave10AbilityScoreImprovements()
        {
            Assert.AreEqual(0, _character.Abilities.ImprovementPoints);
            TestingUtility.LevelTo(ref _character, 19, Class.Monk);
            Assert.AreEqual(10, _character.Abilities.ImprovementPoints);
        }

        [Test]
        public void CharactersCanSpendTheirAbilityScoreImprovements()
        {
            TestingUtility.LevelTo(ref _character, 4, Class.Monk);
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
            Assert.AreEqual(0, _character.SkillBonus(Skill.Acrobatics));
            _character.ChooseSkill(Skill.Acrobatics);
            Assert.AreEqual(2, _character.SkillBonus(Skill.Acrobatics));
            TestingUtility.LevelTo(ref _character, 5, Class.Monk);
            Assert.AreEqual(3, _character.SkillBonus(Skill.Acrobatics));
            _character.Abilities.Dexterity.Score = 12;
            Assert.AreEqual(4, _character.SkillBonus(Skill.Acrobatics));
        }

        //TODO Add Level Down Feature
        //[Test]
        //public void CharactersCanLevelDown()
        //{
        //    Assert.AreEqual(1, _character.Level);
        //    CharacterFactory.LevelUp(ref _character, Class.Monk);
        //    Assert.AreEqual(2, _character.Level);
        //    _character.LevelDown();
        //    Assert.AreEqual(1, _character.Level);
        //    Assert.IsFalse(_character.AllFeatures.Any(af => af.Name == "Ki"));
        //}
    }
}