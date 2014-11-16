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
            _monk = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
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
            Assert.IsTrue(_monk.Skills.Chosen.Contains(AvailableSkill.Acrobatics));
            Assert.IsTrue(_monk.Skills.Chosen.Contains(AvailableSkill.Religion));
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
            _monk.LearnTool(AvailableTool.AlchemistsSupplies);
            Assert.IsTrue(_monk.Tools.Chosen.Contains(AvailableTool.AlchemistsSupplies));
            _monk = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
            _monk.LearnInstrument(AvailableInstrument.Lute);
            Assert.AreEqual(_monk.Instruments.Chosen.First(), AvailableInstrument.Lute);
        }

        [TestMethod]
        public void MonksAreProficientInStrengthSavingThrows()
        {
            Assert.IsTrue(_monk.SavingThrows.Contains(SavingThrow.Strength));
        }

        [TestMethod]
        public void MonkAreProficientInDexteritySavingThrows()
        {
            Assert.IsTrue(_monk.SavingThrows.Contains(SavingThrow.Dexterity));
        }

        [TestMethod]
        public void MonksGetUnarmoredDefenseAndMartialArtsAtFirstLevel()
        {
            Assert.IsTrue(_monk.Features.ClassFeatures.Any(f => f.Name == "Unarmored Defense"));
            Assert.IsTrue(_monk.Features.ClassFeatures.Any(f => f.Name == "Martial Arts"));
        }

        [TestMethod]
        public void MonksAtFirstLevelDoNotGetKi()
        {
            Assert.IsFalse(_monk.Features.ClassFeatures.Any(f => f.Name == "Ki"));
        }

        [TestMethod]
        public void MonksGetOneKiPointPerLevelWhileTheyHaveKi()
        {
            Assert.AreEqual(0, _monk.KiPoints);
            for (var i = 2; i <= 20; i++)
            {
                CharacterFactory.LevelUp(_monk, AvailableClasses.Monk);
                Assert.AreEqual(i, _monk.KiPoints);
            }
        }

        [TestMethod]
        public void MonksGetKiAndUnarmoredMovementAtSecondLevel()
        {
            CharacterFactory.LevelUp(_monk, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.ClassFeatures.Any(f => f.Name == "Ki"));
            Assert.IsTrue(_monk.Features.ClassFeatures.Any(f => f.Name == "Unarmored Movement"));
            
        }

        [TestMethod]
        public void UnarmoredMovementGives10SpeedAt2()
        {
            Assert.AreEqual(30, _monk.Speed);
            CharacterFactory.LevelUp(_monk, AvailableClasses.Monk);
            Assert.AreEqual(40, _monk.Speed);
        }

        [TestMethod]
        public void UnarmoredMovementGives15SpeedAt6()
        {
            for(var i=1;i<6;i++)
                CharacterFactory.LevelUp(_monk, AvailableClasses.Monk);
            Assert.AreEqual(45, _monk.Speed);
        }

        [TestMethod]
        public void UnarmoredMovementGives20SpeedAt10()
        {
            for (var i = 1; i < 10; i++)
                CharacterFactory.LevelUp(_monk, AvailableClasses.Monk);
            Assert.AreEqual(50, _monk.Speed);
        }

        [TestMethod]
        public void UnarmoredMovementGives25SpeedAt14()
        {
            for (var i = 1; i < 14; i++)
                CharacterFactory.LevelUp(_monk, AvailableClasses.Monk);
            Assert.AreEqual(55, _monk.Speed);
        }

        [TestMethod]
        public void UnarmoredMovementGives30SpeedAt18()
        {
            for (var i = 1; i < 18; i++)
                CharacterFactory.LevelUp(_monk, AvailableClasses.Monk);
            Assert.AreEqual(60, _monk.Speed);
        }

        [TestMethod]
        public void MonksMartialArtsDamageAtLevel1Is4()
        {
            Assert.AreEqual(4, _monk.MartialArts);
        }

        [TestMethod]
        public void MonksMartialArtsDamageAtLevel5Is6()
        {
            for (var i = 1; i < 6; i++)
                CharacterFactory.LevelUp(_monk, AvailableClasses.Monk);
            Assert.AreEqual(6, _monk.MartialArts);
        }

        [TestMethod]
        public void MonksMartialArtsDamageAtLevel11Is8()
        {
            for (var i = 1; i < 11; i++)
                CharacterFactory.LevelUp(_monk, AvailableClasses.Monk);
            Assert.AreEqual(8, _monk.MartialArts);
        }

        [TestMethod]
        public void MonksMartialArtsDamageAtLevel17Is10()
        {
            for (var i = 1; i < 17; i++)
                CharacterFactory.LevelUp(_monk, AvailableClasses.Monk);
            Assert.AreEqual(10, _monk.MartialArts);
        }
    }
}