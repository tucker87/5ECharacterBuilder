﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests.CharacterClassTests
{
    [TestClass]
    public class MonkClassTests
    {
        private static Character _monk;

        [TestInitialize]
        public static void Setup()
        {
            _monk = new Character(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
        }
        
        [TestMethod]
        public void MonksHave1D8HitDiceAtLevelOne()
        {
            Assert.AreEqual(8, _monk.HitDice[0]);
        }

        [TestMethod]
        public void MonksCannotChooseMoreThanTwoSkills()
        {
            var skillList = new List<AvailableSkill>{AvailableSkill.Acrobatics, AvailableSkill.Religion, AvailableSkill.Stealth };
            _monk.AddSkills(skillList);
            Assert.AreEqual(1, _monk.RuleIssues.Count);
        }

        [TestMethod]
        public void MonksCanChooseTwoKillsFromTheirProficiencyList()
        {
            var skillList = new List<AvailableSkill>{AvailableSkill.Acrobatics, AvailableSkill.Religion};
            _monk.AddSkills(skillList);
            Assert.IsTrue(_monk.SkillProficiencies.Contains(AvailableSkill.Acrobatics));
            Assert.IsTrue(_monk.SkillProficiencies.Contains(AvailableSkill.Religion));
        }

        [TestMethod]
        public void MonkCanNotChooseASkillThatIsNotOnTheirProficiencyList()
        {
            var skillList = new List<AvailableSkill>{ AvailableSkill.Acrobatics, AvailableSkill.Arcana };
            _monk.AddSkills(skillList);
            Assert.IsTrue(_monk.RuleIssues.Contains("Arcana is not a skill available to this class."));
        }

        [TestMethod]
        public void MonksAreProfientWithNoArmor()
        {
            Assert.AreEqual(0, _monk.ArmorProficiencies.Count);
        }

        [TestMethod]
        public void MonksAreProficientWithShortSwords()
        {
            Assert.IsTrue(_monk.WeaponProficiencies.Contains(AvailableWeapon.ShortSword));
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
            _monk.AddToolProfs(new List<AvailableTool>{AvailableTool.AlchemistsSupplies});
            Assert.IsTrue(_monk.ToolProficiencies.Contains(AvailableTool.AlchemistsSupplies));
            _monk = new Character(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
            _monk.AddInstrumentProfs(new List<AvailableInstrument> {AvailableInstrument.Lute});
            Assert.AreEqual(_monk.InstrumentProficiencies[0], AvailableInstrument.Lute);
        }

        [TestMethod]
        public void MonksCanPickAnArtisanToolOrAnInstrument()
        {
            _monk.AddInstrumentProfs(new List<AvailableInstrument>{AvailableInstrument.Lute});
            Assert.IsFalse(_monk.RuleIssues.Contains("Instrument"));
            
        }

        [TestMethod]
        public void MonksCannotPickBothAToolAndInstrument()
        {
            _monk.AddToolProfs(new List<AvailableTool>{AvailableTool.AlchemistsSupplies});
            _monk.AddInstrumentProfs(new List<AvailableInstrument>{AvailableInstrument.Lute});
            Assert.AreEqual("Monks can only choose an instrument or a Tool", _monk.RuleIssues[0]);
        }

        [TestMethod]
        public void MonksAreProficientInStrengthSavingThrows()
        {
            Assert.IsTrue(_monk.SavingThrowProficiencies.Contains(SavingThrow.Strength));
        }

        [TestMethod]
        public void MonkAreProficientInDexteritySavingThrows()
        {
            Assert.IsTrue(_monk.SavingThrowProficiencies.Contains(SavingThrow.Dexterity));
        }
    }
}