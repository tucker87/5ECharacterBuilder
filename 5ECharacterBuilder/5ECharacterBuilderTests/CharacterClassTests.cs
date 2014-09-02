using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    public class MonkClassTests
    {
        private static CharacterBase _characterBase;
        private static Monk _monk;

        [TestInitialize]
        public static void Setup()
        {
            _characterBase = new CharacterBase(name: "John");
            _monk = new Monk(_characterBase);
        }
        
        [TestMethod]
        public void MonksHave1D8HitDiceAtLevelOne()
        {

            Assert.AreEqual(8, _monk.HitDice[0]);
        }

        [TestMethod]
        public void MonksGetTwoSkillProficiencies()
        {
            Assert.AreEqual(2, _monk.SkillProficiencyCount);
        }

        [TestMethod]
        public void MonksCanChooseTwoKillsFromTheirProficiencyList()
        {
            var skills = new List<string> {"Acrobat", "Religion"};
            var skillList = new SkillList(skills);
            var monk = new Monk(_characterBase, skillList);
            Assert.AreEqual(monk.SkillProficiencies[0], "Acrobat");
        }

        [TestMethod]
        [ExpectedException (typeof(Exception))]
        public void MonkCanNotChooseASkillThatIsNotOnTheirProficiencyList()
        {
            var skills = new List<string> { "Acrobat", "Arcana" };
            var skillList = new SkillList(skills);
            // ReSharper disable once ObjectCreationAsStatement
            new Monk(_characterBase, skillList);
        }

        [TestMethod]
        public void MonksAreProfientWithNoArmor()
        {
            Assert.AreEqual(0, _monk.ArmorProficiencies.Count);
        }

        [TestMethod]
        public void MonksAreProficientWithShortSwords()
        {
            Assert.IsTrue(_monk.WeaponProficiencies.Contains(AvailableWeapons.ShortSword));
        }

        [TestMethod]
        public void MonksAreProficientWithSimpleWeapons()
        {
            Assert.IsTrue(_monk.WeaponProficiencies.Contains(AvailableWeapons.Club));
            Assert.IsTrue(_monk.WeaponProficiencies.Contains(AvailableWeapons.Dagger));
        }
    }
}