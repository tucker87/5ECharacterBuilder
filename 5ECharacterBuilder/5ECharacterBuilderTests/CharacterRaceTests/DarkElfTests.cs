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
            _character = CharacterFactory.BuildACharacter(AvailableRaces.Drow, AvailableClasses.Fighter, AvailableBackgrounds.Criminal);
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
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Darkvision"));
        }

        [Test]
        public void DarkElvesHaveKeenSenses()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Keen Senses"));
            Assert.IsTrue(_character.Skills.Available.Contains(AvailableSkill.Perception));
            Assert.IsTrue(_character.Skills.Chosen.Contains(AvailableSkill.Perception));
        }

        [Test]
        public void DarkElvesHaveFeyAncestry()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Fey Ancestry"));
        }

        [Test]
        public void DarkElvesHaveTrance()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Trance"));
        }

        [Test]
        public void DarkElvesHaveSuperiorDarkvision()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Superior Darkvision"));
        }

        [Test]
        public void DarkElvesHaveSunlightSensitivity()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Sunlight Sensitivity"));
        }

        [Test]
        public void DarkElvesHaveDrowMagic()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Drow Magic"));
        }

        [Test]
        public void DarkElvesHaveElvenCombatTraining()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Drow Weapon Training"));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.Rapier));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.ShortSword));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.HandCrossbows));
        }
    }
}