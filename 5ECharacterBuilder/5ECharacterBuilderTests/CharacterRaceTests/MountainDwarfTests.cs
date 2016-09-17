using System.Linq;
using NUnit.Framework;
using _5ECharacterBuilder;
using _5EDatabase;

namespace _5ECharacterBuilderTests.CharacterRaceTests
{
    [TestFixture]
    public class MountainDwarfTests
    {
        private static ICharacter _character;

        [SetUp]
        public void SetUp()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.MountainDwarf, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
        }

        [Test]
        public void RaceNameIsSet()
        {
            Assert.AreEqual("Mountain Dwarf", _character.Race);
        }

        [Test]
        public void MountainDwarvesSizeIsMedium()
        {
            Assert.AreEqual("Medium", _character.Size);
        }

        [Test]
        public void MountainDwarvesSpeedIs25()
        {
            Assert.AreEqual(25, _character.Speed);
        }

        [Test]
        public void MountainDwarvesConstitutionIsRaisedBy2()
        {
            Assert.AreEqual(12, _character.Abilities.Constitution.Score);
        }

        [Test]
        public void MountainDwarvesStrengthIsRaisedBy2()
        {
            Assert.AreEqual(12, _character.Abilities.Strength.Score);
        }

        [Test]
        public void MountainDwarvesAreMedium()
        {
            Assert.AreEqual("Medium", _character.Size);
        }

        [Test]
        public void MountainDwavesHaveDwarvenCombatTraining()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Dwarven Combat Training"));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.BattleAxe));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.HandAxe));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.ThrowingHammer));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.Warhammer));
        }

        [Test]
        public void MountainDwarvesCanBeProfienctDwarvenTools()
        {
            Assert.IsTrue(_character.Tools.Available.Contains(AvailableTool.SmithsTools));
            Assert.IsTrue(_character.Tools.Available.Contains(AvailableTool.BrewersSupplies));
            Assert.IsTrue(_character.Tools.Available.Contains(AvailableTool.MasonsTools));
        }

        [Test]
        public void MountainDwavesHaveDarkVision()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Darkvision"));
        }

        [Test]
        public void MountainDwavesHaveDwarvenResilience()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Dwarven Resilience"));
        }

        [Test]
        public void MountainDwavesHaveStoneCunning()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Stonecunning"));
        }

        [Test]
        public void MountainDwarvesHaveDwarvenArmorTraining()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Key == "Dwarven Armor Training"));
            Assert.IsTrue(_character.ArmorProficiencies.Contains(AvailableArmor.Leather));
            Assert.IsTrue(_character.ArmorProficiencies.Contains(AvailableArmor.Hide));
        }
    }
}