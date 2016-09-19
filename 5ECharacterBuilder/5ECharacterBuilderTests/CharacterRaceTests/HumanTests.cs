using System.Linq;
using NUnit.Framework;
using _5ECharacterBuilder;
using _5EDatabase;

namespace _5ECharacterBuilderTests.CharacterRaceTests
{
    [TestFixture]
    public class HumanTests
    {
        private static ICharacter _character;

        [SetUp]
        public void SetUp()
        {
            _character = CharacterFactory.BuildACharacter(Race.Human, Class.Fighter,
                Background.Criminal);
        }

        [Test]
        public void MountainDwarvesSizeIsMedium()
        {
            Assert.AreEqual("Medium", _character.Size);
        }

        [Test]
        public void MountainDwarvesSpeedIs25()
        {
            Assert.AreEqual(30, _character.Speed);
        }

        [Test]
        public void RaceNameIsSet()
        {
            Assert.AreEqual("Human", _character.Race);
        }

        [Test]
        public void HumansGetPlusOneToAllAttributeScores()
        {
            Assert.IsTrue(_character.AllFeatures.Any(af => af.Name == "Ability Score Increase"));
            Assert.AreEqual(11, _character.Abilities.Strength.Score);
            Assert.AreEqual(11, _character.Abilities.Constitution.Score);
            Assert.AreEqual(11, _character.Abilities.Dexterity.Score);
            Assert.AreEqual(11, _character.Abilities.Intelligence.Score);
            Assert.AreEqual(11, _character.Abilities.Wisdom.Score);
            Assert.AreEqual(11, _character.Abilities.Charisma.Score);
        }

        [Test]
        public void HumansSpeakCommon()
        {
            Assert.IsTrue(_character.Languages.Chosen.Contains(Language.Common));
        }

        [Test]
        public void HumansCanSpeakOneOtherLanguage()
        {
            _character.LearnLanguage(Language.Draconic);
            Assert.IsTrue(_character.Languages.Chosen.Contains(Language.Draconic));
        }
    }
}