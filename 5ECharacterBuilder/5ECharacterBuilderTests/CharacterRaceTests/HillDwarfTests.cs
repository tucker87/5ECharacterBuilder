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
        public void HillDwarvesConstitutionIsRaisedBy2()
        {
            Assert.AreEqual(12, _character.Attributes.Constitution.Score);
        }

        [TestMethod]
        public void HillDwarvesAreMedium()
        {
            Assert.AreEqual("Medium", _character.Size);
        }

        [TestMethod]
        public void HillDwarvesAreProficientWithBattleAxeHandAxeThrowingAxeAndWarHammer()
        {
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.BattleAxe));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.HandAxe));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.ThrowingHammer));
            Assert.IsTrue(_character.WeaponProficiencies.Contains(AvailableWeapon.WarHammer));
        }

        [TestMethod]
        public void HillDwarvesAreProficientWith()
        {
            
        }
    }
}