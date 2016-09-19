using System.Linq;
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
            _fighter = CharacterFactory.BuildACharacter(Race.Human, Class.Fighter, Background.Criminal);
        }

        [Test]
        public void FightersHitDiceIs10()
        {
            Assert.AreEqual(10, _fighter.HitDice.First());
        }

        [Test]
        public void FightersAreProficientWithAllArmorsAndShields()
        {
            Assert.IsTrue(_fighter.ArmorProficiencies.Contains(ArmorType.Shield));
            Assert.IsTrue(_fighter.ArmorProficiencies.Contains(ArmorType.Plate));
            Assert.IsTrue(_fighter.ArmorProficiencies.Contains(ArmorType.Leather));
            Assert.IsTrue(_fighter.ArmorProficiencies.Contains(ArmorType.Cloth));
        }

        [Test]
        public void FightersAreProficientWithAllSimpleAndMartialWeapons()
        {
            Assert.IsTrue(_fighter.WeaponProficiencies.Contains(WeaponType.Club));
            Assert.IsTrue(_fighter.WeaponProficiencies.Contains(WeaponType.Dagger));
            Assert.IsTrue(_fighter.WeaponProficiencies.Contains(WeaponType.GreatSword));
            Assert.IsTrue(_fighter.WeaponProficiencies.Contains(WeaponType.ShortSword));
        }

        [Test]
        public void FightersSavingThrowsAreStrengthAndConstitution()
        {
            Assert.IsTrue(_fighter.SavingThrows.ToList().Contains(SavingThrow.Strength));
            Assert.IsTrue(_fighter.SavingThrows.ToList().Contains(SavingThrow.Constitution));
        }

        [Test]
        public void FighterClassSkills()
        {
            Assert.IsTrue(_fighter.Skills.Available.Contains(Skill.Acrobatics));
            Assert.IsTrue(_fighter.Skills.Available.Contains(Skill.AnimalHandling));
            Assert.IsTrue(_fighter.Skills.Available.Contains(Skill.Athletics));
            Assert.IsTrue(_fighter.Skills.Available.Contains(Skill.History));
            Assert.IsTrue(_fighter.Skills.Available.Contains(Skill.Insight));
            Assert.IsTrue(_fighter.Skills.Available.Contains(Skill.Intimidation));
            Assert.IsTrue(_fighter.Skills.Available.Contains(Skill.Perception));
            Assert.IsTrue(_fighter.Skills.Available.Contains(Skill.Survival));
        }
    }
}
