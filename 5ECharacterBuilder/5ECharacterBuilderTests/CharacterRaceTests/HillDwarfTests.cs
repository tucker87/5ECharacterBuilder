using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests.CharacterRaceTests
{
    [TestClass]
    public class HillDwarfTests
    {
        private static ICharacter _character;

        [TestInitialize]
        public static void Setup()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.HillDwarf, AvailableClasses.Fighter, AvailableBackgrounds.Criminal);
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
        public void RaceNameIsSet()
        {
            Assert.AreEqual("Hill Dwarf", _character.Race);
        }

        [TestMethod]
        public void HillDwarvesConstitutionIsRaisedBy2()
        {
            Assert.AreEqual(12, _character.Attributes.Constitution.Score);
        }

        [TestMethod]
        public void HillDwarvesWisdomIsRaisedBy1()
        {
            Assert.AreEqual(11, _character.Attributes.Wisdom.Score);
        }

        [TestMethod]
        public void HillDwarvesHitPointsAreRaisedBy1PerLevel()
        {
            Assert.AreEqual(12, _character.MaxHp);
            CharacterFactory.LevelUp(_character, AvailableClasses.Fighter);
            Assert.AreEqual(19, _character.MaxHp);
        }

        [TestMethod]
        public void HillDwarvesAreMedium()
        {
            Assert.AreEqual("Medium", _character.Size);
        }

        [TestMethod]
        public void HillDwavesHaveDwarvenCombatTraining()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Dwarven Combat Training"));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.BattleAxe));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.HandAxe));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.ThrowingHammer));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.Warhammer));
        }

        [TestMethod]
        public void HillDwarvesCanBeProfienctDwarvenTools()
        {
            Assert.IsTrue(_character.Tools.Available.Contains(AvailableTool.SmithsTools));
            Assert.IsTrue(_character.Tools.Available.Contains(AvailableTool.BrewersSupplies));
            Assert.IsTrue(_character.Tools.Available.Contains(AvailableTool.MasonsTools));
        }

        [TestMethod]
        public void HillDwavesHaveDarkVision()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Darkvision"));
        }

        [TestMethod]
        public void HillDwavesHaveDwarvenResilience()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Dwarven Resilience"));
        }

        [TestMethod]
        public void HillDwavesHaveStoneCunning()
        {
            Assert.IsTrue(_character.Features.RaceFeatures.Any(feature => feature.Name == "Stonecunning"));
        }
    }
}