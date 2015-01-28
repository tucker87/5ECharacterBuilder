using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests.CharacterRaceTests
{
    [TestClass]
    public class DarkElfTests
    {
        private static ICharacter _character;

        [TestInitialize]
        public void Setup()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.Drow, AvailableClasses.Fighter, AvailableBackgrounds.Criminal);
        }

        [TestMethod]
        public void RaceNameIsSet()
        {
            Assert.AreEqual("Dark Elf", _character.Race);
        }

        [TestMethod]
        public void DarkElvesDexterityIsRaisedBy2()
        {
            Assert.AreEqual(12, _character.Abilities.Dexterity.Score);
        }

        [TestMethod]
        public void DarkElvesCharismaIsRaisedBy1()
        {
            Assert.AreEqual(11, _character.Abilities.Charisma.Score);
        }
        [TestMethod]
        public void DarkElvesAreMedium()
        {
            Assert.AreEqual("Medium", _character.Size);
        }

        [TestMethod]
        public void DarkElvesHaveDarkVision()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Darkvision"));
        }

        [TestMethod]
        public void DarkElvesHaveKeenSenses()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Keen Senses"));
            Assert.IsTrue(_character.Skills.Available.Contains(AvailableSkill.Perception));
            Assert.IsTrue(_character.Skills.Chosen.Contains(AvailableSkill.Perception));
        }

        [TestMethod]
        public void DarkElvesHaveFeyAncestry()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Fey Ancestry"));
        }

        [TestMethod]
        public void DarkElvesHaveTrance()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Trance"));
        }

        [TestMethod]
        public void DarkElvesHaveSuperiorDarkvision()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Superior Darkvision"));
        }

        [TestMethod]
        public void DarkElvesHaveSunlightSensitivity()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Sunlight Sensitivity"));
        }

        [TestMethod]
        public void DarkElvesHaveDrowMagic()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Drow Magic"));
        }

        [TestMethod]
        public void DarkElvesHaveElvenCombatTraining()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Drow Weapon Training"));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.Rapier));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.ShortSword));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.HandCrossbows));
        }
    }
}