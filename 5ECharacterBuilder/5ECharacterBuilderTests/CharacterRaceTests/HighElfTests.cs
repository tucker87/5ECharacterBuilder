﻿using System.Linq;
using NUnit.Framework;
using _5ECharacterBuilder;
using _5EDatabase;

namespace _5ECharacterBuilderTests.CharacterRaceTests
{
    [TestFixture]
    public class HighElfTests
    {
        private static ICharacter _character;

        [SetUp]
        public void SetUp()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.HighElf, AvailableClasses.Fighter, AvailableBackgrounds.Criminal);
        }

        [Test]
        public void RaceNameIsSet()
        {
            Assert.AreEqual("High Elf", _character.Race);
        }

        [Test]
        public void HighElvesDexterityIsRaisedBy2()
        {
            Assert.AreEqual(12, _character.Abilities.Dexterity.Score);
        }

        [Test]
        public void HighElvesIntelligenceIsRaisedBy1()
        {
            Assert.AreEqual(11, _character.Abilities.Intelligence.Score);
        }
        [Test]
        public void HighElvesAreMedium()
        {
            Assert.AreEqual("Medium", _character.Size);
        }

        [Test]
        public void HighElvesHaveElvenCombatTraining()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Elf Weapon Training"));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.LongSword));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.ShortSword));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.ShortBow));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.LongBow));
        }
        
        [Test]
        public void HighElvesHaveDarkVision()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Darkvision"));
        }

        [Test]
        public void HighElvesHaveKeenSenses()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Keen Senses"));
            Assert.IsTrue(_character.Skills.Available.Contains(AvailableSkill.Perception));
            Assert.IsTrue(_character.Skills.Chosen.Contains(AvailableSkill.Perception));
        }

        [Test]
        public void HighElvesHaveFeyAncestry()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Fey Ancestry"));
        }

        [Test]
        public void HighElvesHaveTrance()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Trance"));
        }

        [Test]
        public void HighElvesHaveCantrip()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Cantrip"));
        }

        [Test]
        public void HighElvesHaveExtraLanguage()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Extra Language"));
            Assert.AreEqual(3, _character.Languages.Max);
        }
    }
}