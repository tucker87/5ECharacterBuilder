using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests.MulticlassTests
{
    [TestClass]
    class MonkToRogue
    {
        private ICharacter _multiclass;
        [TestInitialize]
        public void TestSetup()
        {
            _multiclass = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Acolyte);
            _multiclass.Abilities.Dexterity.Score = 13;
            _multiclass = CharacterFactory.LevelUp(_multiclass, AvailableClasses.Rogue);
        }

        [TestMethod]
        public void CannotMulticlassMonkWithout13Dex()
        {
            var monk = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Acolyte);
            CharacterFactory.LevelUp(monk, AvailableClasses.Rogue);
            Assert.IsFalse(monk.Classes.Contains("Rogue"));
            Assert.AreEqual(1, monk.Level);
        }

        [TestMethod]
        public void CanMulticlassMonkWith13Dex()
        {
            Assert.AreEqual(2, _multiclass.Level);
        }

        [TestMethod]
        public void RogueGainsAd8HitDice()
        {
            Assert.AreEqual(2, _multiclass.HitDice.Count);
            Assert.AreEqual(8, _multiclass.HitDice.First());
            Assert.AreEqual(8, _multiclass.HitDice.Last());
        }

        [TestMethod]
        public void ProficienyBonusIsBasedOnTotalLevel()
        {
            TestingUtility.LevelTo(_multiclass, 5, AvailableClasses.Monk);
            Assert.AreEqual(3, _multiclass.ProficiencyBonus);
        }

        [TestMethod]
        public void MulticlassingRogueOnlyGetsYouLightArmorOneSkillAndThievesTools()
        {
            foreach (var armor in Armory.LightArmor)
                Assert.IsTrue(_multiclass.ArmorProficiencies.Contains(armor));

            Assert.AreEqual(3, _multiclass.Skills.Max);

            Assert.IsTrue(_multiclass.Tools.Available.Contains(AvailableTool.ThievesTools));
            Assert.IsTrue(_multiclass.Tools.Chosen.Contains(AvailableTool.ThievesTools));

            Assert.IsFalse(_multiclass.WeaponProficiencies.Contains(AvailableWeapon.HandCrossbows));
            Assert.IsFalse(_multiclass.WeaponProficiencies.Contains(AvailableWeapon.LongSword));
            Assert.IsFalse(_multiclass.WeaponProficiencies.Contains(AvailableWeapon.Rapier));

            Assert.IsFalse(_multiclass.SavingThrows.Contains(SavingThrow.Intelligence));
        }

        [TestMethod]
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