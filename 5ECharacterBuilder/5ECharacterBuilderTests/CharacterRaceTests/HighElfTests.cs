using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests.CharacterRaceTests
{
    [TestClass]
    public class HighElfTests
    {
        private static ICharacter _character;

        [TestInitialize]
        public static void Setup()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.HighElf, AvailableClasses.Fighter, AvailableBackgrounds.Criminal);
        }

        [TestMethod]
        public void RaceNameIsSet()
        {
            Assert.AreEqual("High Elf", _character.Race);
        }

        [TestMethod]
        public void HighElvesDexterityIsRaisedBy2()
        {
            Assert.AreEqual(12, _character.Abilities.Dexterity.Score);
        }

        [TestMethod]
        public void HighElvesIntelligenceIsRaisedBy1()
        {
            Assert.AreEqual(11, _character.Abilities.Intelligence.Score);
        }
        [TestMethod]
        public void HighElvesAreMedium()
        {
            Assert.AreEqual("Medium", _character.Size);
        }

        [TestMethod]
        public void HighElvesHaveElvenCombatTraining()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Elf Weapon Training"));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.LongSword));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.ShortSword));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.ShortBow));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.LongBow));
        }
        
        [TestMethod]
        public void HighElvesHaveDarkVision()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Darkvision"));
        }

        [TestMethod]
        public void HighElvesHaveKeenSenses()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Keen Senses"));
            Assert.IsTrue(_character.Skills.Available.Contains(AvailableSkill.Perception));
            Assert.IsTrue(_character.Skills.Chosen.Contains(AvailableSkill.Perception));
        }

        [TestMethod]
        public void HighElvesHaveFeyAncestry()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Fey Ancestry"));
        }

        [TestMethod]
        public void HighElvesHaveTrance()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Trance"));
        }

        [TestMethod]
        public void HighElvesHaveCantrip()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Cantrip"));
        }

        [TestMethod]
        public void HighElvesHaveExtraLanguage()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Extra Language"));
            Assert.AreEqual(3, _character.Languages.Max);
        }
    }
}