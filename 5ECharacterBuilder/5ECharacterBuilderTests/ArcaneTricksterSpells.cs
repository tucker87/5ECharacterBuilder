using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    public class ArcaneTricksterSpells
    {
        private ICharacter _character;

        [TestInitialize]
        public void Setup()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Rogue,
                AvailableBackgrounds.Criminal);

        }

        [TestMethod]
        public void OneReturnsZero()
        {
            Assert.AreEqual(0, GetArcaneTricksterMaxSpells(1));
        }

        [TestMethod]
        public void TwoReturnsZero()
        {
            Assert.AreEqual(0, GetArcaneTricksterMaxSpells(2));
        }

        [TestMethod]
        public void ThreeReturnsThree()
        {
            Assert.AreEqual(3, GetArcaneTricksterMaxSpells(3));
        }

        [TestMethod]
        public void FourReturnsFour()
        {
            Assert.AreEqual(4, GetArcaneTricksterMaxSpells(4));
        }

        [TestMethod]
        public void FiveReturnsFour()
        {
            Assert.AreEqual(4, GetArcaneTricksterMaxSpells(5));
        }

        [TestMethod]
        public void SixReturnsFour()
        {
            Assert.AreEqual(4, GetArcaneTricksterMaxSpells(6));
        }

        [TestMethod]
        public void SevenReturnsFive()
        {
            Assert.AreEqual(5, GetArcaneTricksterMaxSpells(7));
        }

        private static int GetArcaneTricksterMaxSpells(int i)
        {
            if ((i - 1)%4 == 0)
                return i - 1;

            if (i%4 == 0)
                return i;

            if (i%3 == 0)
                return i;

            return 0;
        }
    }
}