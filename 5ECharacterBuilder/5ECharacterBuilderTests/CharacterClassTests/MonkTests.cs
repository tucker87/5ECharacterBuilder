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
            _monk = CharacterFactory.BuildACharacter(Race.Human, Class.Monk, Background.Criminal);
        }
        
        [Test]
        public void MonksHave1D8HitDiceAtLevelOne()
        {
            Assert.AreEqual(8, _monk.HitDice[0]);
        }

        [Test]
        public void MonksCanChooseTwoSkillsFromTheirProficiencyList()
        {
            _monk.ChooseSkill(Skill.Acrobatics);
            _monk.ChooseSkill(Skill.Religion);
            Assert.IsTrue(_monk.Skills.Chosen.Contains(Skill.Acrobatics));
            Assert.IsTrue(_monk.Skills.Chosen.Contains(Skill.Religion));
        }
        
        [Test]
        public void MonksAreProfientWithNoArmor()
        {
            Assert.AreEqual(0, _monk.ArmorProficiencies.Count);
        }

        [Test]
        public void MonksAreProficientWithShortSwords()
        {
            Assert.IsTrue(_monk.WeaponProficiencies.Contains(WeaponType.ShortSword));
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
            _monk.LearnTool(Tool.AlchemistsSupplies);
            Assert.IsTrue(_monk.Tools.Chosen.Contains(Tool.AlchemistsSupplies));
            _monk = CharacterFactory.BuildACharacter(Race.Human, Class.Monk, Background.Criminal);
            _monk.LearnInstrument(Instrument.Lute);
            Assert.AreEqual(_monk.Instruments.Chosen.First(), Instrument.Lute);
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
            Assert.IsTrue(_monk.ClassFeatures.Any(af => af.Name =="Monk Unarmored Defense"));
            Assert.IsTrue(_monk.ClassFeatures.Any(af => af.Name =="Martial Arts"));
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
            _monk.EquipArmor(ArmorType.Leather);
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
            Assert.IsFalse(_monk.ClassFeatures.Any(af => af.Name =="Ki"));
        }

        [Test]
        public void MonksGetOneKiPointPerLevelWhileTheyHaveKi()
        {
            Assert.AreEqual(0, _monk.ClassTraits.KiPoints);
            for (var i = 2; i <= 20; i++)
            {
                CharacterFactory.LevelUp(ref _monk, Class.Monk);
                Assert.AreEqual(i, _monk.ClassTraits.KiPoints);
            }
        }

        [Test]
        public void MonksGetKiAndUnarmoredMovementAtSecondLevel()
        {
            CharacterFactory.LevelUp(ref _monk, Class.Monk);
            Assert.IsTrue(_monk.ClassFeatures.Any(f => f.Name == "Ki"));
            Assert.IsTrue(_monk.ClassFeatures.Any(f => f.Name == "Unarmored Movement"));
        }

        [Test]
        public void UnarmoredMovementGives10SpeedAt2()
        {
            Assert.AreEqual(30, _monk.Speed);
            CharacterFactory.LevelUp(ref _monk, Class.Monk);
            Assert.AreEqual(40, _monk.Speed);
            CharacterFactory.LevelUp(ref _monk, Class.Monk);
            Assert.AreEqual(40, _monk.Speed);
        }

        [Test]
        public void UnarmoredMovementGives15SpeedAt6()
        {
            TestingUtility.LevelTo(ref _monk, 6, Class.Monk);
            Assert.AreEqual(45, _monk.Speed);
        }

        [Test]
        public void UnarmoredMovementGives20SpeedAt10()
        {
            TestingUtility.LevelTo(ref _monk, 10, Class.Monk);
            Assert.AreEqual(50, _monk.Speed);
        }

        [Test]
        public void UnarmoredMovementGives25SpeedAt14()
        {
            TestingUtility.LevelTo(ref _monk, 14, Class.Monk);
            Assert.AreEqual(55, _monk.Speed);
        }

        [Test]
        public void UnarmoredMovementGives30SpeedAt18()
        {
            TestingUtility.LevelTo(ref _monk, 18, Class.Monk);
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
            TestingUtility.LevelTo(ref _monk, 6, Class.Monk);
            Assert.AreEqual(6, _monk.MartialArts);
        }

        [Test]
        public void MonksMartialArtsDamageAtLevel11Is8()
        {
            TestingUtility.LevelTo(ref _monk, 11, Class.Monk);
            Assert.AreEqual(8, _monk.MartialArts);
        }

        [Test]
        public void MonksMartialArtsDamageAtLevel17Is10()
        {
            TestingUtility.LevelTo(ref _monk, 17, Class.Monk);
            Assert.AreEqual(10, _monk.MartialArts);
        }

        [Test]
        public void MonksHaveAChoiceOf3PathsAtLevel3()
        {
            TestingUtility.LevelTo(ref _monk, 3, Class.Monk);

            Assert.IsTrue(_monk.AvailablePaths.Contains(Path.WayOfShadow));
            Assert.IsTrue(_monk.AvailablePaths.Contains(Path.WayOfTheFourElements));
            Assert.IsTrue(_monk.AvailablePaths.Contains(Path.WayOfTheOpenHand));
        }

        [Test]
        public void MonksCanChoseAPath()
        {
            TestingUtility.LevelTo(ref _monk, 3, Class.Monk);

            _monk.ChosePath(Path.WayOfShadow);

            Assert.IsTrue(_monk.ChosenPaths.Contains(Path.WayOfShadow));
        }

        [Test]
        public void MonksWithWayOfTheOpenHandGainTheOpenHandTechnique()
        {
            TestingUtility.LevelTo(ref _monk, 3, Class.Monk);

            _monk.ChosePath(Path.WayOfTheOpenHand);

            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Open Hand Technique"));
        }

        [Test]
        public void MonksWithWayOfTheOpenHandGainWholnessOfBodyAtLevel6()
        {
            TestingUtility.LevelTo(ref _monk, 3, Class.Monk);

            _monk.ChosePath(Path.WayOfTheOpenHand);

            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Wholeness Of Body"));
            TestingUtility.LevelTo(ref _monk, 6, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Wholeness Of Body"));
        }

        [Test]
        public void MonksWithWayOfTheOpenHandGainTranquilityAtLevel11()
        {
            TestingUtility.LevelTo(ref _monk, 3, Class.Monk);

            _monk.ChosePath(Path.WayOfTheOpenHand);

            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Tranquility"));
            TestingUtility.LevelTo(ref _monk, 11, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Tranquility"));
        }

        [Test]
        public void MonksWithWayOfTheOpenHandGainQuiveringPalmAtLevel17()
        {
            TestingUtility.LevelTo(ref _monk, 3, Class.Monk);

            _monk.ChosePath(Path.WayOfTheOpenHand);

            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Quivering Palm"));
            TestingUtility.LevelTo(ref _monk, 17, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Quivering Palm"));
        }

        [Test]
        public void MonksWithWayOfShadowGainShadowArts()
        {
            TestingUtility.LevelTo(ref _monk, 3, Class.Monk);

            _monk.ChosePath(Path.WayOfShadow);

            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Shadow Arts"));
        }

        [Test]
        public void MonksWithWayOfShadowGainShadowStepAtLevel6()
        {
            TestingUtility.LevelTo(ref _monk, 3, Class.Monk);

            _monk.ChosePath(Path.WayOfShadow);

            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Shadow Step"));
            TestingUtility.LevelTo(ref _monk, 6, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Shadow Step"));
        }

        [Test]
        public void MonksWithWayOfShadowGainCloakOfShadowsAtLevel11()
        {
            TestingUtility.LevelTo(ref _monk, 3, Class.Monk);

            _monk.ChosePath(Path.WayOfShadow);

            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Cloak Of Shadows"));
            TestingUtility.LevelTo(ref _monk, 11, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Cloak Of Shadows"));
        }

        [Test]
        public void MonksWithWayOfShadowGainOpportunistAtLevel17()
        {
            TestingUtility.LevelTo(ref _monk, 3, Class.Monk);

            _monk.ChosePath(Path.WayOfShadow);

            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Opportunist"));
            TestingUtility.LevelTo(ref _monk, 17, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Opportunist"));
        }

        [Test]
        public void MonksWithWayOfTheFourElementsGainDiscipleOfTheElements()
        {
            TestingUtility.LevelTo(ref _monk, 3, Class.Monk);

            _monk.ChosePath(Path.WayOfTheFourElements);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Disciple Of The Elements"));
        }

        [Test]
        public void MonksCanChangePaths()
        {
            TestingUtility.LevelTo(ref _monk, 3, Class.Monk);

            _monk.ChosePath(Path.WayOfTheOpenHand);
            _monk.ChosePath(Path.WayOfShadow);

            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Shadow Arts"));
            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Open Hand Technique"));
        }

        [Test]
        public void MonksGainDeflectMissilesAtLevel3()
        {
            TestingUtility.LevelTo(ref _monk, 3, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Deflect Missiles"));
        }

        [Test]
        public void MonksGetSlowFallAtLevel4()
        {
            TestingUtility.LevelTo(ref _monk, 4, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Slow Fall"));
        }

        [Test]
        public void MonksGetAnExtraAttackAtLevel5()
        {
            Assert.AreEqual(1, _monk.AttacksPerTurn);
            TestingUtility.LevelTo(ref _monk, 5, Class.Monk);
            Assert.AreEqual(2, _monk.AttacksPerTurn);            
        }

        [Test]
        public void MonksGetStunningStrikeAtLevel5()
        {
            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Stunning Strike"));
            TestingUtility.LevelTo(ref _monk, 5, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Stunning Strike"));
        }

        [Test]
        public void MonksGetKiEmpoweredStrikesAtLevel6()
        {
            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Ki-Empowered Strikes"));
            TestingUtility.LevelTo(ref _monk, 6, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Ki-Empowered Strikes"));
        }

        [Test]
        public void MonksGetEvasionAtLevel7()
        {
            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Evasion"));
            TestingUtility.LevelTo(ref _monk, 7, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Evasion"));
        }

        [Test]
        public void MonksGetStillnessOfMindAtLevel7()
        {
            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Stillness Of Mind"));
            TestingUtility.LevelTo(ref _monk, 7, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Stillness Of Mind"));
        }

        [Test]
        public void MonksGetPurityOfBodyAtLevel10()
        {
            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Purity Of Body"));
            TestingUtility.LevelTo(ref _monk, 10, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Purity Of Body"));
        }

        [Test]
        public void MonksGetToungeOfTheSunAndMoonAtLevel13()
        {
            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Tounge Of The Sun And Moon"));
            TestingUtility.LevelTo(ref _monk, 13, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Tounge Of The Sun And Moon"));
        }

        [Test]
        public void MonksGetDiamondSoulAtLevel14()
        {
            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Diamond Soul"));
            Assert.IsTrue(_monk.SavingThrows.Contains(SavingThrow.Strength));
            Assert.IsTrue(_monk.SavingThrows.Contains(SavingThrow.Dexterity));
            Assert.IsFalse(_monk.SavingThrows.Contains(SavingThrow.Constitution));
            Assert.IsFalse(_monk.SavingThrows.Contains(SavingThrow.Intelligence));
            Assert.IsFalse(_monk.SavingThrows.Contains(SavingThrow.Wisdom));
            Assert.IsFalse(_monk.SavingThrows.Contains(SavingThrow.Charisma));

            TestingUtility.LevelTo(ref _monk, 14, Class.Monk);

            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Diamond Soul"));
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
            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Timeless Body"));
            TestingUtility.LevelTo(ref _monk, 15, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Timeless Body"));
        }

        [Test]
        public void MonksGetEmptyBodyAtLevel18()
        {
            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Empty Body"));
            TestingUtility.LevelTo(ref _monk, 18, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Empty Body"));
        }

        [Test]
        public void MonksGetPerfectSelfAtLevel20()
        {
            Assert.IsFalse(_monk.AllFeatures.Any(af => af.Name =="Perfect Self"));
            TestingUtility.LevelTo(ref _monk, 20, Class.Monk);
            Assert.IsTrue(_monk.AllFeatures.Any(af => af.Name =="Perfect Self"));
        }
    }
}