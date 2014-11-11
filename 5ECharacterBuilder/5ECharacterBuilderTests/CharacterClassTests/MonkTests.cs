using System.Collections.Generic;
using System.Linq;
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
        public void MonksCanChooseTwoSkillsFromTheirProficiencyList()
        {
            _monk.LearnSkill(AvailableSkill.Acrobatics);
            _monk.LearnSkill(AvailableSkill.Religion);
            Assert.IsTrue(_monk.ChosenSkills.Contains(AvailableSkill.Acrobatics));
            Assert.IsTrue(_monk.ChosenSkills.Contains(AvailableSkill.Religion));
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
            _monk.AvailableToolProficiencies.Add(AvailableTool.AlchemistsSupplies);
            Assert.IsTrue(_monk.AvailableToolProficiencies.Contains(AvailableTool.AlchemistsSupplies));
            _monk = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
            _monk.AvailableInstrumentProficiencies.Add(AvailableInstrument.Lute);
            Assert.AreEqual(_monk.AvailableInstrumentProficiencies.First(), AvailableInstrument.Lute);
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