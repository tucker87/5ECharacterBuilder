using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests.CharacterRaceTests
{
    [TestClass]
    public class HumanTests
    {
        private static ICharacter _character;

        [TestInitialize]
        public static void Setup()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Fighter,
                AvailableBackgrounds.Criminal);
        }

        [TestMethod]
        public void MountainDwarvesSizeIsMedium()
        {
            Assert.AreEqual("Medium", _character.Size);
        }

        [TestMethod]
        public void MountainDwarvesSpeedIs25()
        {
            Assert.AreEqual(30, _character.Speed);
        }

        [TestMethod]
        public void RaceNameIsSet()
        {
            Assert.AreEqual("Human", _character.Race);
        }

        [TestMethod]
        public void HumansGetPlusOneToAllAttributeScores()
        {
            Assert.IsTrue(_character.Features.AllFeatures.ContainsKey("Ability Score Increase"));
            Assert.AreEqual(11, _character.Abilities.Strength.Score);
            Assert.AreEqual(11, _character.Abilities.Constitution.Score);
            Assert.AreEqual(11, _character.Abilities.Dexterity.Score);
            Assert.AreEqual(11, _character.Abilities.Intelligence.Score);
            Assert.AreEqual(11, _character.Abilities.Wisdom.Score);
            Assert.AreEqual(11, _character.Abilities.Charisma.Score);
        }

        [TestMethod]
        public void HumansSpeakCommon()
        {
            Assert.IsTrue(_character.Languages.Chosen.Contains(AvailableLanguages.Common));
        }

        [TestMethod]
        public void HumansCanSpeakOneOtherLanguage()
        {
            _character.LearnLanguage(AvailableLanguages.Draconic);
            Assert.IsTrue(_character.Languages.Chosen.Contains(AvailableLanguages.Draconic));
        }
    }
}