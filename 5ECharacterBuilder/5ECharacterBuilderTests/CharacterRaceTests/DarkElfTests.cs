using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests.CharacterRaceTests
{
    [TestClass]
    public class DElfTests
    {
        private static ICharacter _character;

        [TestInitialize]
        public static void Setup()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.DarkElf, AvailableClasses.Fighter, AvailableBackgrounds.Criminal);
        }

        [TestMethod]
        public void RaceNameIsSet()
        {
            Assert.AreEqual("Dark Elf", _character.Race);
        }

        [TestMethod]
        public void DarkElvesDexterityIsRaisedBy2()
        {
            Assert.AreEqual(12, _character.Attributes.Dexterity.Score);
        }

        [TestMethod]
        public void DarkElveCharismaIsRaisedBy1()
        {
            Assert.AreEqual(11, _character.Attributes.Charisma.Score);
        }
        [TestMethod]
        public void DarkElvesAreMedium()
        {
            Assert.AreEqual("Medium", _character.Size);
        }

        [TestMethod]
        public void DarkElvesHaveDarkVision()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Darkvision"));
        }

        [TestMethod]
        public void DarkElvesHaveKeenSenses()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Keen Senses"));
            Assert.IsTrue(_character.AvailableSkills.Contains(AvailableSkill.Perception));
            Assert.IsTrue(_character.ChosenSkills.Contains(AvailableSkill.Perception));
        }

        [TestMethod]
        public void DarkElvesHaveFeyAncestry()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Fey Ancestry"));
        }

        [TestMethod]
        public void DarkElvesHaveTrance()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Trance"));
        }

        [TestMethod]
        public void DarkElvesHaveSuperiorDarkvision()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Superior Darkvision"));
        }

        [TestMethod]
        public void DarkElvesHaveSunlightSensitivity()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Sunlight Sensitivity"));
        }

        [TestMethod]
        public void DarkElvesHaveDrowMagic()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Drow Magic"));
        }

        [TestMethod]
        public void DarkElvesHaveElvenCombatTraining()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Drow Weapon Training"));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.Rapier));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.ShortSword));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.HandCrossbows));
        }
    }
}