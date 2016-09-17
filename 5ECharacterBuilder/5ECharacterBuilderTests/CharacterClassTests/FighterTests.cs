using NUnit.Framework;
using _5ECharacterBuilder;
using _5EDatabase;

namespace _5ECharacterBuilderTests.CharacterClassTests
{
    [TestFixture]
    public class FighterTests
    {
        private ICharacter _fighter;
        [SetUp]
        public void SetUp()
        {
            _fighter = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Fighter, AvailableBackgrounds.Criminal);
        }

        [Test]
        public void FightersHitDiceIs10()
        {
            Assert.AreEqual(10, _fighter.HitDice.First());
        }

        [Test]
        public void FightersAreProficientWithAllArmorsAndShields()
        {
            Assert.IsTrue(_fighter.ArmorProficiencies.Contains(AvailableArmor.Shield));
            Assert.IsTrue(_fighter.ArmorProficiencies.Contains(AvailableArmor.Plate));
            Assert.IsTrue(_fighter.ArmorProficiencies.Contains(AvailableArmor.Leather));
            Assert.IsTrue(_fighter.ArmorProficiencies.Contains(AvailableArmor.Cloth));
        }

        [Test]
        public void FightersAreProficientWithAllSimpleAndMartialWeapons()
        {
            Assert.IsTrue(_fighter.WeaponProficiencies.Contains(AvailableWeapon.Club));
            Assert.IsTrue(_fighter.WeaponProficiencies.Contains(AvailableWeapon.Dagger));
            Assert.IsTrue(_fighter.WeaponProficiencies.Contains(AvailableWeapon.GreatSword));
            Assert.IsTrue(_fighter.WeaponProficiencies.Contains(AvailableWeapon.ShortSword));
        }

        [Test]
        public void FightersSavingThrowsAreStrengthAndConstitution()
        {
            Assert.IsTrue(_fighter.SavingThrows.Contains(SavingThrow.Strength));
            Assert.IsTrue(_fighter.SavingThrows.Contains(SavingThrow.Constitution));
        }

        [Test]
        public void FighterClassSkills()
        {
            Assert.IsTrue(_fighter.Skills.Available.Contains(AvailableSkill.Acrobatics));
            Assert.IsTrue(_fighter.Skills.Available.Contains(AvailableSkill.AnimalHandling));
            Assert.IsTrue(_fighter.Skills.Available.Contains(AvailableSkill.Athletics));
            Assert.IsTrue(_fighter.Skills.Available.Contains(AvailableSkill.History));
            Assert.IsTrue(_fighter.Skills.Available.Contains(AvailableSkill.Insight));
            Assert.IsTrue(_fighter.Skills.Available.Contains(AvailableSkill.Intimidation));
            Assert.IsTrue(_fighter.Skills.Available.Contains(AvailableSkill.Perception));
            Assert.IsTrue(_fighter.Skills.Available.Contains(AvailableSkill.Survival));
        }
    }
}
