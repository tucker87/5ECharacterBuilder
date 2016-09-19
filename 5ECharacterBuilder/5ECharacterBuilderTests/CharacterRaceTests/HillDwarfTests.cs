using System.Linq;
using NUnit.Framework;
using _5ECharacterBuilder;
using _5EDatabase;

namespace _5ECharacterBuilderTests.CharacterRaceTests
{
    [TestFixture]
    public class HillDwarfTests
    {
        private static ICharacter _character;

        [SetUp]
        public void SetUp()
        {
            _character = CharacterFactory.BuildACharacter(Race.HillDwarf, Class.Fighter, Background.Criminal);
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
        public void RaceNameIsSet()
        {
            Assert.AreEqual("Hill Dwarf", _character.Race);
        }

        [Test]
        public void HillDwarvesConstitutionIsRaisedBy2()
        {
            Assert.AreEqual(12, _character.Abilities.Constitution.Score);
        }

        [Test]
        public void HillDwarvesWisdomIsRaisedBy1()
        {
            Assert.AreEqual(11, _character.Abilities.Wisdom.Score);
        }

        [Test]
        public void HillDwarvesHitPointsAreRaisedBy1PerLevel()
        {
            Assert.AreEqual(12, _character.MaxHp);
            CharacterFactory.LevelUp(ref _character, Class.Fighter);
            Assert.AreEqual(19, _character.MaxHp);
        }

        [Test]
        public void HillDwarvesAreMedium()
        {
            Assert.AreEqual("Medium", _character.Size);
        }

        [Test]
        public void HillDwavesHaveDwarvenCombatTraining()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Dwarven Combat Training"));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(WeaponType.BattleAxe));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(WeaponType.HandAxe));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(WeaponType.ThrowingHammer));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(WeaponType.Warhammer));
        }

        [Test]
        public void HillDwarvesCanBeProfienctDwarvenTools()
        {
            Assert.IsTrue(_character.Tools.Available.Contains(Tool.SmithsTools));
            Assert.IsTrue(_character.Tools.Available.Contains(Tool.BrewersSupplies));
            Assert.IsTrue(_character.Tools.Available.Contains(Tool.MasonsTools));
        }

        [Test]
        public void HillDwavesHaveDarkVision()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Darkvision"));
        }

        [Test]
        public void HillDwavesHaveDwarvenResilience()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Dwarven Resilience"));
        }

        [Test]
        public void HillDwavesHaveStoneCunning()
        {
            Assert.IsTrue(_character.RaceFeatures.Any(feature => feature.Name == "Stonecunning"));
        }
    }
}