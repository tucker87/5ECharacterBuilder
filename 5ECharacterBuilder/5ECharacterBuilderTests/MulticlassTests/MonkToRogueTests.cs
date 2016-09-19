using System.Linq;
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
            _multiclass = CharacterFactory.BuildACharacter(Race.Human, Class.Monk, Background.Acolyte);
            _multiclass.Abilities.Dexterity.Score = 13;
            _multiclass = CharacterFactory.LevelUp(ref _multiclass, Class.Rogue);
        }

        [Test]
        public void CannotMulticlassMonkWithout13Dex()
        {
            var monk = CharacterFactory.BuildACharacter(Race.Human, Class.Monk, Background.Acolyte);
            Assert.Throws<RequirementsExpection>(() =>CharacterFactory.LevelUp(ref monk, Class.Rogue));
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
            TestingUtility.LevelTo(ref _multiclass, 5, Class.Monk);
            Assert.AreEqual(3, _multiclass.ProficiencyBonus);
        }

        [Test]
        public void MulticlassingRogueOnlyGetsYouLightArmorOneSkillAndThievesTools()
        {
            foreach (var armor in Armory.LightArmor)
                Assert.IsTrue(_multiclass.ArmorProficiencies.Contains(armor));

            Assert.AreEqual(5, _multiclass.Skills.Max);

            Assert.IsTrue(_multiclass.Tools.Available.Contains(Tool.ThievesTools));
            Assert.IsTrue(_multiclass.Tools.Chosen.Contains(Tool.ThievesTools));

            Assert.IsFalse(_multiclass.WeaponProficiencies.Contains(WeaponType.HandCrossbows));
            Assert.IsFalse(_multiclass.WeaponProficiencies.Contains(WeaponType.LongSword));
            Assert.IsFalse(_multiclass.WeaponProficiencies.Contains(WeaponType.Rapier));

            Assert.IsFalse(_multiclass.SavingThrows.Contains(SavingThrow.Intelligence));
        }

        [Test]
        public void YouStillGainMonkFeaturesOrRogueFeaturesBasedOnClassLevel()
        {
            Assert.IsFalse(_multiclass.AllFeatures.Any(af => af.Name == "Ki"));
            CharacterFactory.LevelUp(ref _multiclass, Class.Monk);
            Assert.IsTrue(_multiclass.AllFeatures.Any(af => af.Name == "Ki"));

            Assert.IsFalse(_multiclass.AllFeatures.Any(af => af.Name == "Cunning Action"));
            CharacterFactory.LevelUp(ref _multiclass, Class.Rogue);
            Assert.IsTrue(_multiclass.AllFeatures.Any(af => af.Name == "Cunning Action"));
        }
    }
}