using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests.CharacterRaceTests
{
    [TestClass]
    public class MountainDwarfTests
    {
        private static ICharacter _character;

        [TestInitialize]
        public static void Setup()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.MountainDwarf, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
        }

        [TestMethod]
        public void RaceNameIsSet()
        {
            Assert.AreEqual("Mountain Dwarf", _character.Race);
        }

        [TestMethod]
        public void MountainDwarvesSizeIsMedium()
        {
            Assert.AreEqual("Medium", _character.Size);
        }

        [TestMethod]
        public void MountainDwarvesSpeedIs25()
        {
            Assert.AreEqual(25, _character.Speed);
        }

        [TestMethod]
        public void MountainDwarvesConstitutionIsRaisedBy2()
        {
            Assert.AreEqual(12, _character.Attributes.Constitution.Score);
        }

        [TestMethod]
        public void MountainDwarvesStrengthIsRaisedBy2()
        {
            Assert.AreEqual(12, _character.Attributes.Strength.Score);
        }

        [TestMethod]
        public void MountainDwarvesAreMedium()
        {
            Assert.AreEqual("Medium", _character.Size);
        }

        [TestMethod]
        public void MountainDwavesHaveDwarvenCombatTraining()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Dwarven Combat Training"));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.BattleAxe));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.HandAxe));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.ThrowingHammer));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.Warhammer));
        }

        [TestMethod]
        public void MountainDwarvesCanBeProfienctDwarvenTools()
        {
            Assert.IsTrue(_character.Tools.Available.Contains(AvailableTool.SmithsTools));
            Assert.IsTrue(_character.Tools.Available.Contains(AvailableTool.BrewersSupplies));
            Assert.IsTrue(_character.Tools.Available.Contains(AvailableTool.MasonsTools));
        }

        [TestMethod]
        public void MountainDwavesHaveDarkVision()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Darkvision"));
        }

        [TestMethod]
        public void MountainDwavesHaveDwarvenResilience()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Dwarven Resilience"));
        }

        [TestMethod]
        public void MountainDwavesHaveStoneCunning()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Stonecunning"));
        }

        [TestMethod]
        public void MountainDwarvesHaveDwarvenArmorTraining()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Dwarven Armor Training"));
            Assert.IsTrue(_character.ArmorProficiencies.Contains(AvailableArmor.Leather));
            Assert.IsTrue(_character.ArmorProficiencies.Contains(AvailableArmor.Hide));
        }
    }
}