using System.Linq;
using NUnit.Framework;
using _5ECharacterBuilder;
using _5EDatabase;

namespace _5ECharacterBuilderTests.CharacterClassTests
{
    [TestFixture]
    public class MonkClassTests
    {
        private static ICharacter _monk;

        [SetUp]
        public void SetUp()
        {
            _monk = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
        }
        
        [Test]
        public void MonksHave1D8HitDiceAtLevelOne()
        {
            Assert.AreEqual(8, _monk.HitDice[0]);
        }

        [Test]
        public void MonksCanChooseTwoSkillsFromTheirProficiencyList()
        {
            _monk.ChooseSkill(AvailableSkill.Acrobatics);
            _monk.ChooseSkill(AvailableSkill.Religion);
            Assert.IsTrue(_monk.Skills.Chosen.Contains(AvailableSkill.Acrobatics));
            Assert.IsTrue(_monk.Skills.Chosen.Contains(AvailableSkill.Religion));
        }
        
        [Test]
        public void MonksAreProfientWithNoArmor()
        {
            Assert.AreEqual(0, _monk.ArmorProficiencies.Count);
        }

        [Test]
        public void MonksAreProficientWithShortSwords()
        {
            Assert.IsTrue(_monk.WeaponProficiencies.Contains(AvailableWeapon.ShortSword));
        }

        [Test]
        public void MonksAreProficientWithSimpleWeapons()
        {
            foreach (var weapon in Armory.SimpleWeapons)
            {
                Assert.IsTrue(_monk.WeaponProficiencies.Contains(weapon));
            }
        }
        [Test]
        public void MonksAreProficientWithOneToolOrMusicalInstrument()
        {
            _monk.LearnTool(AvailableTool.AlchemistsSupplies);
            Assert.IsTrue(_monk.Tools.Chosen.Contains(AvailableTool.AlchemistsSupplies));
            _monk = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Monk, AvailableBackgrounds.Criminal);
            _monk.LearnInstrument(AvailableInstrument.Lute);
            Assert.AreEqual(_monk.Instruments.Chosen.First(), AvailableInstrument.Lute);
        }

        [Test]
        public void MonksAreProficientInStrengthSavingThrows()
        {
            Assert.IsTrue(_monk.SavingThrows.Contains(SavingThrow.Strength));
        }

        [Test]
        public void MonkAreProficientInDexteritySavingThrows()
        {
            Assert.IsTrue(_monk.SavingThrows.Contains(SavingThrow.Dexterity));
        }

        [Test]
        public void MonksGetUnarmoredDefenseAndMartialArtsAtFirstLevel()
        {
            Assert.IsTrue(_monk.Features.ClassFeatures.ContainsKey("Monk Unarmored Defense"));
            Assert.IsTrue(_monk.Features.ClassFeatures.ContainsKey("Martial Arts"));
        }

        [Test]
        public void MonksUnarmoredDefenseIs10PlusDexPlusWis()
        {
            _monk.Abilities.Dexterity.Score = 13;
            _monk.Abilities.Wisdom.Score = 13;
            Assert.AreEqual(14, _monk.ArmorClass);
        }

        [Test]
        public void MonksLoseUnarmoredDefenseWhenWearingArmor()
        {
            _monk.Abilities.Dexterity.Score = 13;
            _monk.Abilities.Wisdom.Score = 13;
            _monk.EquipArmor(AvailableArmor.Leather);
            Assert.AreEqual(13, _monk.ArmorClass);
        }

        [Test]
        public void MonksLoseUnarmoredDefenseWhenWearingAShield()
        {
            _monk.Abilities.Dexterity.Score = 13;
            _monk.Abilities.Wisdom.Score = 13;
            _monk.ToggleShield();
            Assert.AreEqual(14, _monk.ArmorClass);
        }

        [Test]
        public void MonksAtFirstLevelDoNotGetKi()
        {
            Assert.IsFalse(_monk.Features.ClassFeatures.ContainsKey("Ki"));
        }

        [Test]
        public void MonksGetOneKiPointPerLevelWhileTheyHaveKi()
        {
            Assert.AreEqual(0, _monk.ClassTraits.KiPoints);
            for (var i = 2; i <= 20; i++)
            {
                CharacterFactory.LevelUp(_monk, AvailableClasses.Monk);
                Assert.AreEqual(i, _monk.ClassTraits.KiPoints);
            }
        }

        [Test]
        public void MonksGetKiAndUnarmoredMovementAtSecondLevel()
        {
            CharacterFactory.LevelUp(_monk, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.ClassFeatures.Any(f => f.Key == "Ki"));
            Assert.IsTrue(_monk.Features.ClassFeatures.Any(f => f.Key == "Unarmored Movement"));
        }

        [Test]
        public void UnarmoredMovementGives10SpeedAt2()
        {
            Assert.AreEqual(30, _monk.Speed);
            CharacterFactory.LevelUp(_monk, AvailableClasses.Monk);
            Assert.AreEqual(40, _monk.Speed);
        }

        [Test]
        public void UnarmoredMovementGives15SpeedAt6()
        {
            TestingUtility.LevelTo(_monk, 6, AvailableClasses.Monk);
            Assert.AreEqual(45, _monk.Speed);
        }

        [Test]
        public void UnarmoredMovementGives20SpeedAt10()
        {
            TestingUtility.LevelTo(_monk, 10, AvailableClasses.Monk);
            Assert.AreEqual(50, _monk.Speed);
        }

        [Test]
        public void UnarmoredMovementGives25SpeedAt14()
        {
            TestingUtility.LevelTo(_monk, 14, AvailableClasses.Monk);
            Assert.AreEqual(55, _monk.Speed);
        }

        [Test]
        public void UnarmoredMovementGives30SpeedAt18()
        {
            TestingUtility.LevelTo(_monk, 18, AvailableClasses.Monk);
            Assert.AreEqual(60, _monk.Speed);
        }

        [Test]
        public void MonksMartialArtsDamageAtLevel1Is4()
        {
            Assert.AreEqual(4, _monk.MartialArts);
        }

        [Test]
        public void MonksMartialArtsDamageAtLevel5Is6()
        {
            TestingUtility.LevelTo(_monk, 6, AvailableClasses.Monk);
            Assert.AreEqual(6, _monk.MartialArts);
        }

        [Test]
        public void MonksMartialArtsDamageAtLevel11Is8()
        {
            TestingUtility.LevelTo(_monk, 11, AvailableClasses.Monk);
            Assert.AreEqual(8, _monk.MartialArts);
        }

        [Test]
        public void MonksMartialArtsDamageAtLevel17Is10()
        {
            TestingUtility.LevelTo(_monk, 17, AvailableClasses.Monk);
            Assert.AreEqual(10, _monk.MartialArts);
        }

        [Test]
        public void MonksHaveAChoiceOf3PathsAtLevel3()
        {
            TestingUtility.LevelTo(_monk, 3, AvailableClasses.Monk);

            Assert.IsTrue(_monk.ClassPath.Available.Contains(AvailablePaths.WayOfShadow));
            Assert.IsTrue(_monk.ClassPath.Available.Contains(AvailablePaths.WayOfTheFourElements));
            Assert.IsTrue(_monk.ClassPath.Available.Contains(AvailablePaths.WayOfTheOpenHand));
        }

        [Test]
        public void MonksCanChoseAPath()
        {
            TestingUtility.LevelTo(_monk, 3, AvailableClasses.Monk);

            _monk.ChosePath(AvailablePaths.WayOfShadow);

            Assert.IsTrue(_monk.ClassPath.Chosen == AvailablePaths.WayOfShadow);
        }

        [Test]
        public void MonksWithWayOfTheOpenHandGainTheOpenHandTechnique()
        {
            TestingUtility.LevelTo(_monk, 3, AvailableClasses.Monk);

            _monk.ChosePath(AvailablePaths.WayOfTheOpenHand);

            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Open Hand Technique"));
        }

        [Test]
        public void MonksWithWayOfTheOpenHandGainWholnessOfBodyAtLevel6()
        {
            TestingUtility.LevelTo(_monk, 3, AvailableClasses.Monk);

            _monk.ChosePath(AvailablePaths.WayOfTheOpenHand);

            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Wholeness Of Body"));
            TestingUtility.LevelTo(_monk, 6, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Wholeness Of Body"));
        }

        [Test]
        public void MonksWithWayOfTheOpenHandGainTranquilityAtLevel11()
        {
            TestingUtility.LevelTo(_monk, 3, AvailableClasses.Monk);

            _monk.ChosePath(AvailablePaths.WayOfTheOpenHand);

            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Tranquility"));
            TestingUtility.LevelTo(_monk, 11, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Tranquility"));
        }

        [Test]
        public void MonksWithWayOfTheOpenHandGainQuiveringPalmAtLevel17()
        {
            TestingUtility.LevelTo(_monk, 3, AvailableClasses.Monk);

            _monk.ChosePath(AvailablePaths.WayOfTheOpenHand);

            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Quivering Palm"));
            TestingUtility.LevelTo(_monk, 17, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Quivering Palm"));
        }

        [Test]
        public void MonksWithWayOfShadowGainShadowArts()
        {
            TestingUtility.LevelTo(_monk, 3, AvailableClasses.Monk);

            _monk.ChosePath(AvailablePaths.WayOfShadow);

            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Shadow Arts"));
        }

        [Test]
        public void MonksWithWayOfShadowGainShadowStepAtLevel6()
        {
            TestingUtility.LevelTo(_monk, 3, AvailableClasses.Monk);

            _monk.ChosePath(AvailablePaths.WayOfShadow);

            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Shadow Step"));
            TestingUtility.LevelTo(_monk, 6, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Shadow Step"));
        }

        [Test]
        public void MonksWithWayOfShadowGainCloakOfShadowsAtLevel11()
        {
            TestingUtility.LevelTo(_monk, 3, AvailableClasses.Monk);

            _monk.ChosePath(AvailablePaths.WayOfShadow);

            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Cloak Of Shadows"));
            TestingUtility.LevelTo(_monk, 11, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Cloak Of Shadows"));
        }

        [Test]
        public void MonksWithWayOfShadowGainOpportunistAtLevel17()
        {
            TestingUtility.LevelTo(_monk, 3, AvailableClasses.Monk);

            _monk.ChosePath(AvailablePaths.WayOfShadow);

            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Opportunist"));
            TestingUtility.LevelTo(_monk, 17, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Opportunist"));
        }

        [Test]
        public void MonksWithWayOfTheFourElementsGainDiscipleOfTheElements()
        {
            TestingUtility.LevelTo(_monk, 3, AvailableClasses.Monk);

            _monk.ChosePath(AvailablePaths.WayOfTheFourElements);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Disciple Of The Elements"));
        }

        [Test]
        public void MonksCanChangePaths()
        {
            TestingUtility.LevelTo(_monk, 3, AvailableClasses.Monk);

            _monk.ChosePath(AvailablePaths.WayOfTheOpenHand);
            _monk.ChosePath(AvailablePaths.WayOfShadow);

            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Shadow Arts"));
            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Open Hand Technique"));
        }

        [Test]
        public void MonksGainDeflectMissilesAtLevel3()
        {
            TestingUtility.LevelTo(_monk, 3, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Deflect Missiles"));
        }

        [Test]
        public void MonksGetSlowFallAtLevel4()
        {
            TestingUtility.LevelTo(_monk, 4, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Slow Fall"));
        }

        [Test]
        public void MonksGetAnExtraAttackAtLevel5()
        {
            Assert.AreEqual(1, _monk.AttacksPerTurn);
            TestingUtility.LevelTo(_monk, 5, AvailableClasses.Monk);
            Assert.AreEqual(2, _monk.AttacksPerTurn);
            
        }

        [Test]
        public void MonksGetStunningStrikeAtLevel5()
        {
            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Stunning Strike"));
            TestingUtility.LevelTo(_monk, 5, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Stunning Strike"));
        }

        [Test]
        public void MonksGetKiEmpoweredStrikesAtLevel6()
        {
            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Ki-Empowered Strikes"));
            TestingUtility.LevelTo(_monk, 6, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Ki-Empowered Strikes"));
        }

        [Test]
        public void MonksGetEvasionAtLevel7()
        {
            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Evasion"));
            TestingUtility.LevelTo(_monk, 7, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Evasion"));
        }

        [Test]
        public void MonksGetStillnessOfMindAtLevel7()
        {
            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Stillness Of Mind"));
            TestingUtility.LevelTo(_monk, 7, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Stillness Of Mind"));
        }

        [Test]
        public void MonksGetPurityOfBodyAtLevel10()
        {
            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Purity Of Body"));
            TestingUtility.LevelTo(_monk, 10, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Purity Of Body"));
        }

        [Test]
        public void MonksGetToungeOfTheSunAndMoonAtLevel13()
        {
            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Tounge Of The Sun And Moon"));
            TestingUtility.LevelTo(_monk, 13, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Tounge Of The Sun And Moon"));
        }

        [Test]
        public void MonksGetDiamondSoulAtLevel14()
        {
            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Diamond Soul"));
            Assert.IsTrue(_monk.SavingThrows.Contains(SavingThrow.Strength));
            Assert.IsTrue(_monk.SavingThrows.Contains(SavingThrow.Dexterity));
            Assert.IsFalse(_monk.SavingThrows.Contains(SavingThrow.Constitution));
            Assert.IsFalse(_monk.SavingThrows.Contains(SavingThrow.Intelligence));
            Assert.IsFalse(_monk.SavingThrows.Contains(SavingThrow.Wisdom));
            Assert.IsFalse(_monk.SavingThrows.Contains(SavingThrow.Charisma));

            TestingUtility.LevelTo(_monk, 14, AvailableClasses.Monk);

            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Diamond Soul"));
            Assert.IsTrue(_monk.SavingThrows.Contains(SavingThrow.Strength));
            Assert.IsTrue(_monk.SavingThrows.Contains(SavingThrow.Dexterity));
            Assert.IsTrue(_monk.SavingThrows.Contains(SavingThrow.Constitution));
            Assert.IsTrue(_monk.SavingThrows.Contains(SavingThrow.Intelligence));
            Assert.IsTrue(_monk.SavingThrows.Contains(SavingThrow.Wisdom));
            Assert.IsTrue(_monk.SavingThrows.Contains(SavingThrow.Charisma));
        }

        [Test]
        public void MonksGetTimelessBodyAtLevel15()
        {
            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Timeless Body"));
            TestingUtility.LevelTo(_monk, 15, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Timeless Body"));
        }

        [Test]
        public void MonksGetEmptyBodyAtLevel18()
        {
            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Empty Body"));
            TestingUtility.LevelTo(_monk, 18, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Empty Body"));
        }

        [Test]
        public void MonksGetPerfectSelfAtLevel20()
        {
            Assert.IsFalse(_monk.Features.AllFeatures.ContainsKey("Perfect Self"));
            TestingUtility.LevelTo(_monk, 20, AvailableClasses.Monk);
            Assert.IsTrue(_monk.Features.AllFeatures.ContainsKey("Perfect Self"));
        }
    }
}