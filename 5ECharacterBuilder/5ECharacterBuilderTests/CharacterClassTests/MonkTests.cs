using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests.CharacterClassTests
{
    [TestClass]
    public class MonkClassTests
    {
        private static ICharacter _monk;

        [TestInitialize]
        public static void Setup()
        {
            _monk =CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
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
            _monk.TrainedSkills.AddRange(skillList);
            Assert.IsTrue(_monk.RuleIssues.Contains("Monks can only choose 2 skills from their list."));
        }

        [TestMethod]
        public void MonksCanChooseTwoKillsFromTheirProficiencyList()
        {
            var skillList = new List<AvailableSkill>{AvailableSkill.Acrobatics, AvailableSkill.Religion};
            _monk.TrainedSkills.AddRange(skillList);
            Assert.IsTrue(_monk.TrainedSkills.Contains(AvailableSkill.Acrobatics));
            Assert.IsTrue(_monk.TrainedSkills.Contains(AvailableSkill.Religion));
        }

        [TestMethod]
        public void MonkCanNotChooseASkillThatIsNotOnTheirProficiencyList()
        {
            var skillList = new List<AvailableSkill>{ AvailableSkill.Acrobatics, AvailableSkill.Arcana };
            _monk.TrainedSkills.AddRange(skillList);
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
            foreach (var weapon in Armory.SimpleWeapons)
            {
                Assert.IsTrue(_monk.WeaponProficiencies.Contains(weapon));
            }
        }
        [TestMethod]
        public void MonksAreProficientWithOneToolOrMusicalInstrument()
        {
            _monk.ToolProficiencies.Add(AvailableTool.AlchemistsSupplies);
            Assert.IsTrue(_monk.ToolProficiencies.Contains(AvailableTool.AlchemistsSupplies));
            _monk = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
            _monk.InstrumentProficiencies.Add(AvailableInstrument.Lute);
            Assert.AreEqual(_monk.InstrumentProficiencies[0], AvailableInstrument.Lute);
        }

        [TestMethod]
        public void MonksCanPickAnArtisanToolOrAnInstrument()
        {
            _monk.InstrumentProficiencies.Add(AvailableInstrument.Lute);
            Assert.IsFalse(_monk.RuleIssues.Contains("Instrument"));
            
        }

        [TestMethod]
        public void MonksCannotPickBothAToolAndInstrument()
        {
            _monk.ToolProficiencies.Add(AvailableTool.AlchemistsSupplies);
            _monk.InstrumentProficiencies.Add(AvailableInstrument.Lute);
            Assert.AreEqual("Monks can only choose an Instrument or a Tool", _monk.RuleIssues[0]);
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