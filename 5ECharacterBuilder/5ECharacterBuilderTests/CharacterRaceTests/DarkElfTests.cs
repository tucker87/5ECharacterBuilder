using System.Linq;
using NUnit.Framework;
using _5ECharacterBuilder;
using _5EDatabase;

namespace _5ECharacterBuilderTests.CharacterRaceTests
{
    [TestFixture]
    public class DarkElfTests
    {
        private static ICharacter _character;

        [SetUp]
        public void SetUp()
        {
            _character = CharacterFactory.BuildACharacter(Race.Drow, Class.Fighter, Background.Criminal);
        }

        [Test]
        public void RaceNameIsSet()
        {
            Assert.AreEqual("Dark Elf", _character.Race);
        }

        [Test]
        public void DarkElvesDexterityIsRaisedBy2()
        {
            Assert.AreEqual(12, _character.Abilities.Dexterity.Score);
        }

        [Test]
        public void DarkElvesCharismaIsRaisedBy1()
        {
            Assert.AreEqual(11, _character.Abilities.Charisma.Score);
        }
        [Test]
        public void DarkElvesAreMedium()
        {
            Assert.AreEqual("Medium", _character.Size);
        }

        [Test]
        public void DarkElvesHaveDarkVision()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Darkvision"));
        }

        [Test]
        public void DarkElvesHaveKeenSenses()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Keen Senses"));
            Assert.IsTrue(_character.Skills.Available.Contains(Skill.Perception));
            Assert.IsTrue(_character.Skills.Chosen.Contains(Skill.Perception));
        }

        [Test]
        public void DarkElvesHaveFeyAncestry()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Fey Ancestry"));
        }

        [Test]
        public void DarkElvesHaveTrance()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Trance"));
        }

        [Test]
        public void DarkElvesHaveSuperiorDarkvision()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Superior Darkvision"));
        }

        [Test]
        public void DarkElvesHaveSunlightSensitivity()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Sunlight Sensitivity"));
        }

        [Test]
        public void DarkElvesHaveDrowMagic()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Drow Magic"));
        }

        [Test]
        public void DarkElvesHaveElvenCombatTraining()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Drow Weapon Training"));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(WeaponType.Rapier));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(WeaponType.ShortSword));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(WeaponType.HandCrossbows));
        }
    }
}