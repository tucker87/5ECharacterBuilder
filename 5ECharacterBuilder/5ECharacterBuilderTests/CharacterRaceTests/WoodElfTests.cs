using System.Linq;
using NUnit.Framework;
using _5ECharacterBuilder;
using _5EDatabase;

namespace _5ECharacterBuilderTests.CharacterRaceTests
{
    [TestFixture]
    public class WoodElfTests
    {
        private static ICharacter _character;

        [SetUp]
        public void SetUp()
        {
            _character = CharacterFactory.BuildACharacter(Race.WoodElf, Class.Fighter, Background.Criminal);
        }

        [Test]
        public void RaceNameIsSet()
        {
            Assert.AreEqual("Wood Elf", _character.Race);
        }

        [Test]
        public void WoodElvesDexterityIsRaisedBy2()
        {
            Assert.AreEqual(12, _character.Abilities.Dexterity.Score);
        }

        [Test]
        public void WoodElvesWisdomIsRaisedBy1()
        {
            Assert.AreEqual(11, _character.Abilities.Wisdom.Score);
        }
        [Test]
        public void WoodElvesAreMedium()
        {
            Assert.AreEqual("Medium", _character.Size);
        }

        [Test]
        public void WoodElvesHaveElvenCombatTraining()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Elf Weapon Training"));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(WeaponType.LongSword));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(WeaponType.ShortSword));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(WeaponType.ShortBow));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(WeaponType.LongBow));
        }
        
        [Test]
        public void WoodElvesHaveDarkVision()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Darkvision"));
        }

        [Test]
        public void WoodElvesHaveKeenSenses()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Keen Senses"));
            Assert.IsTrue(_character.Skills.Available.Contains(Skill.Perception));
            Assert.IsTrue(_character.Skills.Chosen.Contains(Skill.Perception));
        }

        [Test]
        public void WoodElvesHaveFeyAncestry()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Fey Ancestry"));
        }

        [Test]
        public void WoodElvesHaveTrance()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Trance"));
        }

        [Test]
        public void WoodElvesHaveFleetOfFoot()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Fleet of Foot"));
            Assert.AreEqual(35, _character.Speed);
        }

        [Test]
        public void WoodElvesHaveMaskOfTheWild()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Mask of the Wild"));
        }
    }
}