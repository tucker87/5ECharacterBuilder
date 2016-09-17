using NUnit.Framework;
using _5ECharacterBuilder;
using _5EDatabase;

namespace _5ECharacterBuilderTests.MulticlassTests
{
    [TestFixture]
    class MonkToRogueTests
    {
        private ICharacter _multiclass;
        [SetUp]
        public void TestSetUp()
        {
            _multiclass = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Acolyte);
            _multiclass.Abilities.Dexterity.Score = 13;
            _multiclass = CharacterFactory.LevelUp(_multiclass, AvailableClasses.Rogue);
        }

        [Test]
        public void CannotMulticlassMonkWithout13Dex()
        {
            var monk = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Acolyte);
            Assert.Throws<RequirementsExpection>(() =>CharacterFactory.LevelUp(monk, AvailableClasses.Rogue));
        }

        [Test]
        public void CanMulticlassMonkWith13Dex()
        {
            Assert.AreEqual(2, _multiclass.Level);
        }

        [Test]
        public void RogueGainsAd8HitDice()
        {
            Assert.AreEqual(2, _multiclass.HitDice.Count);
            Assert.AreEqual(8, _multiclass.HitDice.First());
            Assert.AreEqual(8, _multiclass.HitDice.Last());
        }

        [Test]
        public void ProficienyBonusIsBasedOnTotalLevel()
        {
            TestingUtility.LevelTo(_multiclass, 5, AvailableClasses.Monk);
            Assert.AreEqual(3, _multiclass.ProficiencyBonus);
        }

        [Test]
        public void MulticlassingRogueOnlyGetsYouLightArmorOneSkillAndThievesTools()
        {
            foreach (var armor in Armory.LightArmor)
                Assert.IsTrue(_multiclass.ArmorProficiencies.Contains(armor));

            Assert.AreEqual(5, _multiclass.Skills.Max);

            Assert.IsTrue(_multiclass.Tools.Available.Contains(AvailableTool.ThievesTools));
            Assert.IsTrue(_multiclass.Tools.Chosen.Contains(AvailableTool.ThievesTools));

            Assert.IsFalse(_multiclass.WeaponProficiencies.Contains(AvailableWeapon.HandCrossbows));
            Assert.IsFalse(_multiclass.WeaponProficiencies.Contains(AvailableWeapon.LongSword));
            Assert.IsFalse(_multiclass.WeaponProficiencies.Contains(AvailableWeapon.Rapier));

            Assert.IsFalse(_multiclass.SavingThrows.Contains(SavingThrow.Intelligence));
        }

        [Test]
        public void YouStillGainMonkFeaturesOrRogueFeaturesBasedOnClassLevel()
        {
            Assert.IsFalse(_multiclass.Features.AllFeatures.ContainsKey("Ki"));
            CharacterFactory.LevelUp(_multiclass, AvailableClasses.Monk);
            Assert.IsTrue(_multiclass.Features.AllFeatures.ContainsKey("Ki"));

            Assert.IsFalse(_multiclass.Features.AllFeatures.ContainsKey("Cunning Action"));
            CharacterFactory.LevelUp(_multiclass, AvailableClasses.Rogue);
            Assert.IsTrue(_multiclass.Features.AllFeatures.ContainsKey("Cunning Action"));
        }
    }
}