using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;
using _5ECharacterBuilder.CharacterClasses;

namespace _5ECharacterBuilderTests.CharacterClassTests
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
            _monk = new Monk(_characterBase, artisanTool:AvailableTools.AlchemistsSupplies);
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
        [ExpectedException(typeof(Exception), "Monks can only choose two skills from their list.")]
        public void MonksCannotChooseMoreThanTwoSkills()
        {
            var skillList = new List<AvailableSkills>{AvailableSkills.Acrobat, AvailableSkills.Religion, AvailableSkills.Stealth };
            var monk = new Monk(new CharacterBase(), skillList, AvailableTools.AlchemistsSupplies);
            Assert.AreEqual(2, monk.SkillProficiencies.Count);
        }

        [TestMethod]
        public void MonksCanChooseTwoKillsFromTheirProficiencyList()
        {
            var skillList = new List<AvailableSkills>{AvailableSkills.Acrobat, AvailableSkills.Religion};
            var monk = new Monk(_characterBase, skillList);
            Assert.AreEqual(monk.SkillProficiencies[0], AvailableSkills.Acrobat);
        }

        [TestMethod]
        [ExpectedException (typeof(Exception), "Arcana is not a skill available to this class.")]
        public void MonkCanNotChooseASkillThatIsNotOnTheirProficiencyList()
        {
            var skillList = new List<AvailableSkills>{ AvailableSkills.Acrobat, AvailableSkills.Arcana };
            // ReSharper disable once ObjectCreationAsStatement
            new Monk(_characterBase, skillList, AvailableTools.AlchemistsSupplies);
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
            var armory = new Armory();
            foreach (var weapon in armory.SimpleWeapons)
            {
                Assert.IsTrue(_monk.WeaponProficiencies.Contains(weapon));
            }
        }
        [TestMethod]
        public void MonksAreProficientWithOneToolOrMusicalInstrument()
        {
            var monk = new Monk(new CharacterBase(), artisanTool: AvailableTools.AlchemistsSupplies);
            Assert.AreEqual(monk.ToolProficiencies[0], AvailableTools.AlchemistsSupplies);
            monk = new Monk(new CharacterBase(), instrument: AvailableInstruments.Lute);
            Assert.AreEqual(monk.InstrumentProficiencies[0], AvailableInstruments.Lute);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Monks must select one tool or instrument")]
        public void MonksMustPickAnArtisanToolOrAnInstrument()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new Monk(new CharacterBase());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Monks can only select one tool or instrument")]
        public void MonksCannotPickBothAToolAndInstrument()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new Monk(new CharacterBase(), instrument: AvailableInstruments.Lute, artisanTool: AvailableTools.AlchemistsSupplies);
        }

        [TestMethod]
        public void MonksAreProficientInStrengthSavingThrows()
        {
            var monk = new Monk(_characterBase);
            Assert.IsTrue(monk.SavingThrowProficiencies.Contains(SavingThrows.Strength));
        }

        [TestMethod]
        public void MonkAreProficientInDexteritySavingThrows()
        {
            var monk = new Monk(_characterBase);
            Assert.IsTrue(monk.SavingThrowProficiencies.Contains(SavingThrows.Dexterity));
        }
    }
}