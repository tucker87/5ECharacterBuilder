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
            Assert.AreEqual(11, _character.Attributes.Strength.Score);
            Assert.AreEqual(11, _character.Attributes.Constitution.Score);
            Assert.AreEqual(11, _character.Attributes.Dexterity.Score);
            Assert.AreEqual(11, _character.Attributes.Intelligence.Score);
            Assert.AreEqual(11, _character.Attributes.Wisdom.Score);
            Assert.AreEqual(11, _character.Attributes.Charisma.Score);
        }

        [TestMethod]
        public void HumansSpeakCommon()
        {
            Assert.IsTrue(_character.ChosenLanguages.Contains(AvailableLanguages.Common));
        }

        [TestMethod]
        public void HumansCanSpeakOneOtherLanguage()
        {
            _character.ChosenLanguages.Add(AvailableLanguages.Draconic);
            Assert.IsTrue(_character.ChosenLanguages.Contains(AvailableLanguages.Draconic));
        }
    }
}