﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests.CharacterClassTests
{
    [TestClass]
    public class FighterTests
    {
        private ICharacter _fighter;
        [TestInitialize]
        public void Setup()
        {
            _fighter = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Fighter, AvailableBackgrounds.Criminal);
        }

        [TestMethod]
        public void FightersHitDiceIs10()
        {
            Assert.AreEqual(10, _fighter.HitDice.First());
        }

        [TestMethod]
        public void FightersAreProficientWithAllArmorsAndShields()
        {
            Assert.IsTrue(_fighter.ArmorProficiencies.Contains(AvailableArmor.Shield));
            Assert.IsTrue(_fighter.ArmorProficiencies.Contains(AvailableArmor.Plate));
            Assert.IsTrue(_fighter.ArmorProficiencies.Contains(AvailableArmor.Leather));
            Assert.IsTrue(_fighter.ArmorProficiencies.Contains(AvailableArmor.Cloth));
        }

        [TestMethod]
        public void FightersAreProficientWithAllSimpleAndMartialWeapons()
        {
            Assert.IsTrue(_fighter.WeaponProficiencies.Contains(AvailableWeapon.Club));
            Assert.IsTrue(_fighter.WeaponProficiencies.Contains(AvailableWeapon.Dagger));
            Assert.IsTrue(_fighter.WeaponProficiencies.Contains(AvailableWeapon.GreatSword));
            Assert.IsTrue(_fighter.WeaponProficiencies.Contains(AvailableWeapon.ShortSword));
        }

        [TestMethod]
        public void FightersSavingThrowsAreStrengthAndConstitution()
        {
            Assert.IsTrue(_fighter.SavingThrows.Contains(SavingThrow.Strength));
            Assert.IsTrue(_fighter.SavingThrows.Contains(SavingThrow.Constitution));
        }

        [TestMethod]
        public void FighterClassSkills()
        {
            Assert.IsTrue(_fighter.Skills.Available.Contains(AvailableSkills.Acrobatics));
            Assert.IsTrue(_fighter.Skills.Available.Contains(AvailableSkills.AnimalHandling));
            Assert.IsTrue(_fighter.Skills.Available.Contains(AvailableSkills.Athletics));
            Assert.IsTrue(_fighter.Skills.Available.Contains(AvailableSkills.History));
            Assert.IsTrue(_fighter.Skills.Available.Contains(AvailableSkills.Insight));
            Assert.IsTrue(_fighter.Skills.Available.Contains(AvailableSkills.Intimidation));
            Assert.IsTrue(_fighter.Skills.Available.Contains(AvailableSkills.Perception));
            Assert.IsTrue(_fighter.Skills.Available.Contains(AvailableSkills.Survival));
        }
    }
}
