using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests.CharacterClassTests
{
    [TestClass]
    public class FighterTests
    {
        private Character _fighter;
        [TestInitialize]
        public void Setup()
        {
            _fighter = new Character(AvailableRaces.Human, AvailableClasses.Fighter);
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
            Assert.IsTrue(_fighter.SavingThrowProficiencies.Contains(SavingThrow.Strength));
            Assert.IsTrue(_fighter.SavingThrowProficiencies.Contains(SavingThrow.Constitution));
        }

        [TestMethod]
        public void FighterClassSkills()
        {
            Assert.IsTrue(_fighter.ClassSkills.Contains(AvailableSkill.Acrobat));
            Assert.IsTrue(_fighter.ClassSkills.Contains(AvailableSkill.AnimalHandling));
            Assert.IsTrue(_fighter.ClassSkills.Contains(AvailableSkill.Athletics));
            Assert.IsTrue(_fighter.ClassSkills.Contains(AvailableSkill.History));
            Assert.IsTrue(_fighter.ClassSkills.Contains(AvailableSkill.Insight));
            Assert.IsTrue(_fighter.ClassSkills.Contains(AvailableSkill.Intimidation));
            Assert.IsTrue(_fighter.ClassSkills.Contains(AvailableSkill.Perception));
            Assert.IsTrue(_fighter.ClassSkills.Contains(AvailableSkill.Survival));
        }
    }
}
