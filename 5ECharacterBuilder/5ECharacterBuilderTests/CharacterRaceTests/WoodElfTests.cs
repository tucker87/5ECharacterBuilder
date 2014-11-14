using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests.CharacterRaceTests
{
    [TestClass]
    public class WoodElfTests
    {
        private static ICharacter _character;

        [TestInitialize]
        public static void Setup()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.WoodElf, AvailableClasses.Fighter, AvailableBackgrounds.Criminal);
        }

        [TestMethod]
        public void RaceNameIsSet()
        {
            Assert.AreEqual("Wood Elf", _character.Race);
        }

        [TestMethod]
        public void WoodElvesDexterityIsRaisedBy2()
        {
            Assert.AreEqual(12, _character.Attributes.Dexterity.Score);
        }

        [TestMethod]
        public void WoodElvesWisdomIsRaisedBy1()
        {
            Assert.AreEqual(11, _character.Attributes.Wisdom.Score);
        }
        [TestMethod]
        public void WoodElvesAreMedium()
        {
            Assert.AreEqual("Medium", _character.Size);
        }

        [TestMethod]
        public void WoodElvesHaveElvenCombatTraining()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Elf Weapon Training"));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.LongSword));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.ShortSword));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.ShortBow));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.LongBow));
        }
        
        [TestMethod]
        public void WoodElvesHaveDarkVision()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Darkvision"));
        }

        [TestMethod]
        public void WoodElvesHaveKeenSenses()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Keen Senses"));
            Assert.IsTrue(_character.Skills.Available.Contains(AvailableSkill.Perception));
            Assert.IsTrue(_character.Skills.Chosen.Contains(AvailableSkill.Perception));
        }

        [TestMethod]
        public void WoodElvesHaveFeyAncestry()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Fey Ancestry"));
        }

        [TestMethod]
        public void WoodElvesHaveTrance()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Trance"));
        }

        [TestMethod]
        public void WoodElvesHaveFleetOfFoot()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Fleet of Foot"));
            Assert.AreEqual(35, _character.Speed);
        }

        [TestMethod]
        public void WoodElvesHaveMaskOfTheWild()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Mask of the Wild"));
        }
    }
}