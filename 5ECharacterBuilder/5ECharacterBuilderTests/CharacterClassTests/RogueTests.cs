using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests.CharacterClassTests
{
    [TestClass]
    class RogueTests
    {
        private static ICharacter _rogue;

        [TestInitialize]
        public static void Setup()
        {
            _rogue = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Rogue, AvailableBackgrounds.Acolyte);
        }

        [TestMethod]
        public void RoguesAreRogues()
        {
            Assert.IsTrue(_rogue.Classes.Contains("Rogue"));
        }

        [TestMethod]
        public void RoguesHave1D8HitDiceAtLevelOne()
        {
            Assert.AreEqual(8, _rogue.HitDice[0]);
        }

        [TestMethod]
        public void RoguesAreProficientWithLightArmor()
        {
            Assert.IsTrue(_rogue.ArmorProficiencies.Contains(AvailableArmor.Padded));
            Assert.IsTrue(_rogue.ArmorProficiencies.Contains(AvailableArmor.Leather));
            Assert.IsTrue(_rogue.ArmorProficiencies.Contains(AvailableArmor.StuddedLeather));
        }

        [TestMethod]
        public void RogueAreProficientWithSimpleWeapons()
        {
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(AvailableWeapon.Club));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(AvailableWeapon.Dagger));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(AvailableWeapon.Greatclub));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(AvailableWeapon.Handaxe));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(AvailableWeapon.Javelin));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(AvailableWeapon.LightHammer));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(AvailableWeapon.Mace));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(AvailableWeapon.Quarterstaff));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(AvailableWeapon.Sickle));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(AvailableWeapon.Spear));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(AvailableWeapon.UnarmedStrike));
        }

        [TestMethod]
        public void RoguesAreProficientWithHandCrossbowsLongswordsRapiersAndShortswords()
        {
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(AvailableWeapon.HandCrossbows));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(AvailableWeapon.LongSword));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(AvailableWeapon.Rapier));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(AvailableWeapon.ShortSword));
        }

        [TestMethod]
        public void RoguesAreProficientWithTheivesTools()
        {
            Assert.IsTrue(_rogue.Tools.Available.Contains(AvailableTool.ThievesTools));
            Assert.IsTrue(_rogue.Tools.Chosen.Contains(AvailableTool.ThievesTools));
        }

        [TestMethod]
        public void RoguesAreProficientInDexterityAndIntelligenceSavingThrows()
        {
            Assert.IsTrue(_rogue.SavingThrows.Contains(SavingThrow.Dexterity));
            Assert.IsTrue(_rogue.SavingThrows.Contains(SavingThrow.Intelligence));
        }

        [TestMethod]
        public void RoguesCanChoose4SkillsFromTheirList()
        {
            Assert.AreEqual(2, _rogue.Skills.Max - _rogue.Skills.Chosen.Count);

            Assert.IsTrue(_rogue.Skills.Available.Contains(AvailableSkill.Acrobatics));
            Assert.IsTrue(_rogue.Skills.Available.Contains(AvailableSkill.Athletics));
            Assert.IsTrue(_rogue.Skills.Available.Contains(AvailableSkill.Deception));
            Assert.IsTrue(_rogue.Skills.Available.Contains(AvailableSkill.Insight));
            Assert.IsTrue(_rogue.Skills.Available.Contains(AvailableSkill.Intimidation));
            Assert.IsTrue(_rogue.Skills.Available.Contains(AvailableSkill.Investigation));
            Assert.IsTrue(_rogue.Skills.Available.Contains(AvailableSkill.Perception));
            Assert.IsTrue(_rogue.Skills.Available.Contains(AvailableSkill.Performance));
            Assert.IsTrue(_rogue.Skills.Available.Contains(AvailableSkill.Persuasion));
            Assert.IsTrue(_rogue.Skills.Available.Contains(AvailableSkill.SleightOfHand));
            Assert.IsTrue(_rogue.Skills.Available.Contains(AvailableSkill.Stealth));
        }

        [TestMethod]
        public void RoguesGetExpertiseOn2SkillsTheyHaveChosen()
        {
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Expertise"));

            _rogue.ChooseSkill(AvailableSkill.Acrobatics);
            _rogue.ChooseSkill(AvailableSkill.Athletics);
            _rogue.ChooseExpertise(AvailableSkill.Acrobatics);
            _rogue.ChooseExpertise(AvailableSkill.Athletics);

            Assert.IsTrue(_rogue.Skills.Expertise.Contains(AvailableSkill.Acrobatics));
            Assert.IsTrue(_rogue.Skills.Expertise.Contains(AvailableSkill.Athletics));
        }

        [TestMethod]
        public void RoguesCannotChooseExpertiseInASkillTheyAreNotProficientIn()
        {
            _rogue.ChooseSkill(AvailableSkill.Acrobatics);
            _rogue.ChooseExpertise(AvailableSkill.Acrobatics);
            _rogue.ChooseExpertise(AvailableSkill.Athletics);

            Assert.IsTrue(_rogue.Skills.Expertise.Contains(AvailableSkill.Acrobatics));
            Assert.IsFalse(_rogue.Skills.Expertise.Contains(AvailableSkill.Athletics));
        }

        [TestMethod]
        public void RouguesCannotChooseMoreExpertiseThanTheyHaveAvailable()
        {
            _rogue.ChooseSkill(AvailableSkill.Acrobatics);
            _rogue.ChooseSkill(AvailableSkill.Athletics);
            _rogue.ChooseSkill(AvailableSkill.Deception);
            _rogue.ChooseExpertise(AvailableSkill.Acrobatics);
            _rogue.ChooseExpertise(AvailableSkill.Athletics);
            _rogue.ChooseExpertise(AvailableSkill.Deception);

            Assert.IsTrue(_rogue.Skills.Expertise.Contains(AvailableSkill.Acrobatics));
            Assert.IsTrue(_rogue.Skills.Expertise.Contains(AvailableSkill.Athletics));
            Assert.IsFalse(_rogue.Skills.Expertise.Contains(AvailableSkill.Deception));
        }

        [TestMethod]
        public void RougesCanChooseExpertiseInThievesTools()
        {
            _rogue.LearnTool(AvailableTool.ThievesTools);
            _rogue.ChooseExpertise(AvailableTool.ThievesTools);
            Assert.IsTrue(_rogue.Tools.Expertise.Contains(AvailableTool.ThievesTools));
        }

        [TestMethod]
        public void RougesChoiceOfToolCountsAgainstTheirExpertiseMax()
        {
            _rogue.LearnTool(AvailableTool.ThievesTools);
            _rogue.ChooseSkill(AvailableSkill.Acrobatics);
            _rogue.ChooseSkill(AvailableSkill.Athletics);

            _rogue.ChooseExpertise(AvailableTool.ThievesTools);
            _rogue.ChooseExpertise(AvailableSkill.Acrobatics);
            _rogue.ChooseExpertise(AvailableSkill.Athletics);

            Assert.IsTrue(_rogue.Tools.Expertise.Contains(AvailableTool.ThievesTools));
            Assert.IsTrue(_rogue.Skills.Expertise.Contains(AvailableSkill.Acrobatics));
            Assert.IsFalse(_rogue.Skills.Expertise.Contains(AvailableSkill.Athletics));
        }

        [TestMethod]
        public void RoguesGetSneakAttackWith1Dice()
        {
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Sneak Attack"));
            Assert.AreEqual(1, _rogue.SneakAttackDice);
        }

        [TestMethod]
        public void RougesSneakAttackGoesTo2DiceAtLevel3()
        {
            TestingUtility.LevelTo(_rogue, 3, AvailableClasses.Rogue);
            Assert.AreEqual(2, _rogue.SneakAttackDice);
        }
        
        [TestMethod]
        public void RougesSneakAttackGoesTo3DiceAtLevel5()
        {
            TestingUtility.LevelTo(_rogue, 5, AvailableClasses.Rogue);
            Assert.AreEqual(3, _rogue.SneakAttackDice);
        }

        [TestMethod]
        public void RougesSneakAttackGoesTo4DiceAtLevel7()
        {
            TestingUtility.LevelTo(_rogue, 7, AvailableClasses.Rogue);
            Assert.AreEqual(4, _rogue.SneakAttackDice);
        }

        [TestMethod]
        public void RougesSneakAttackGoesTo5DiceAtLevel9()
        {
            TestingUtility.LevelTo(_rogue, 9, AvailableClasses.Rogue);
            Assert.AreEqual(5, _rogue.SneakAttackDice);
        }

        [TestMethod]
        public void RougesSneakAttackGoesTo6DiceAtLevel11()
        {
            TestingUtility.LevelTo(_rogue, 11, AvailableClasses.Rogue);
            Assert.AreEqual(6, _rogue.SneakAttackDice);
        }

        [TestMethod]
        public void RougesSneakAttackGoesTo7DiceAtLevel13()
        {
            TestingUtility.LevelTo(_rogue, 13, AvailableClasses.Rogue);
            Assert.AreEqual(7, _rogue.SneakAttackDice);
        }

        [TestMethod]
        public void RougesSneakAttackGoesTo8DiceAtLevel15()
        {
            TestingUtility.LevelTo(_rogue, 15, AvailableClasses.Rogue);
            Assert.AreEqual(8, _rogue.SneakAttackDice);
        }

        [TestMethod]
        public void RougesSneakAttackGoesTo9DiceAtLevel17()
        {
            TestingUtility.LevelTo(_rogue, 17, AvailableClasses.Rogue);
            Assert.AreEqual(9, _rogue.SneakAttackDice);
        }

        [TestMethod]
        public void RougesSneakAttackGoesTo10DiceAtLevel19()
        {
            TestingUtility.LevelTo(_rogue, 19, AvailableClasses.Rogue);
            Assert.AreEqual(10, _rogue.SneakAttackDice);
        }

        [TestMethod]
        public void RougesGetThievesCant()
        {
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Thieves' Cant"));
        }

        [TestMethod]
        public void RoguesGetCunningActionAtLevel2()
        {
            TestingUtility.LevelTo(_rogue, 2, AvailableClasses.Rogue);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Cunning Action"));
        }

        [TestMethod]
        public void LevelingInAnotherClassDoesNotCountTowardRogueLevel()
        {
            _rogue.Abilities.Wisdom.Score = 13;
            _rogue.Abilities.Dexterity.Score = 13;
            TestingUtility.LevelTo(_rogue, 2, AvailableClasses.Monk);
            Assert.IsFalse(_rogue.Features.AllFeatures.ContainsKey("Cunning Action"));
        }

        [TestMethod]
        public void RoguesHaveAChoiceOf3PathsAtLevel3()
        {
            TestingUtility.LevelTo(_rogue, 3, AvailableClasses.Rogue);

            Assert.IsTrue(_rogue.ClassPath.Available.Contains(AvailablePaths.Thief));
            Assert.IsTrue(_rogue.ClassPath.Available.Contains(AvailablePaths.Assassin));
            Assert.IsTrue(_rogue.ClassPath.Available.Contains(AvailablePaths.ArcaneTrickster));
        }

        [TestMethod]
        public void RoguesGetUncannyDodgeAtLevel5()
        {
            TestingUtility.LevelTo(_rogue, 5, AvailableClasses.Rogue);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Uncanny Dodge"));
        }

        [TestMethod]
        public void RoguesGetTwoMoreExpertisePointsAtLevel6()
        {
            TestingUtility.LevelTo(_rogue, 6, AvailableClasses.Rogue);
            Assert.AreEqual(4, _rogue.Skills.MaxExpertise);
        }

        [TestMethod]
        public void RoguesGetEvasionAtLevel7()
        {
            TestingUtility.LevelTo(_rogue, 7, AvailableClasses.Rogue);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Evasion"));
        }

        [TestMethod]
        public void RoguesGetAnExtraAbilityScoreImprovementAtLevel10()
        {
            TestingUtility.LevelTo(_rogue, 10, AvailableClasses.Rogue);
            Assert.AreEqual(6, _rogue.Abilities.ImprovementPoints);
        }

        [TestMethod]
        public void RoguesGetReliableTalentAtLevel11()
        {
            TestingUtility.LevelTo(_rogue, 11, AvailableClasses.Rogue);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Reliable Talent"));
        }

        [TestMethod]
        public void RoguesGetBlindSenseAtLevel14()
        {
            TestingUtility.LevelTo(_rogue, 14, AvailableClasses.Rogue);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Blindsense"));
        }

        [TestMethod]
        public void RoguesGetSlipperyMindAtLevel15()
        {
            TestingUtility.LevelTo(_rogue, 15, AvailableClasses.Rogue);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Slippery Mind"));
            Assert.IsTrue(_rogue.SavingThrows.Contains(SavingThrow.Wisdom));
        }

        [TestMethod]
        public void RoguesGetElusiveAtLevel18()
        {
            TestingUtility.LevelTo(_rogue, 18, AvailableClasses.Rogue);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Elusive"));
        }

        [TestMethod]
        public void RoguesGetStrokeOfLuckAtLevel20()
        {
            TestingUtility.LevelTo(_rogue, 20, AvailableClasses.Rogue);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Stroke Of Luck"));
        }

        [TestMethod]
        public void RoguesCannotChoosePathsFromOtherClasses()
        {
            _rogue.ChosePath(AvailablePaths.WayOfShadow);
            Assert.IsFalse(_rogue.ClassPath.Chosen == AvailablePaths.WayOfShadow);
        }

        [TestMethod]
        public void ThievesGetFastHandsAtLevel3()
        {
            TestingUtility.LevelTo(_rogue, 3, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.Thief);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Fast Hands"));
        }

        [TestMethod]
        public void ThievesGetSecondStoryWorkAtLevel3()
        {
            TestingUtility.LevelTo(_rogue, 3, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.Thief);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Second-Story Work"));
        }

        [TestMethod]
        public void ThievesGetSupremeSneakAtLevel9()
        {
            TestingUtility.LevelTo(_rogue, 9, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.Thief);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Supreme Sneak"));
        }

        [TestMethod]
        public void ThievesGetUseMagicDeviceAtLevel13()
        {
            TestingUtility.LevelTo(_rogue, 13, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.Thief);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Use Magic Device"));
        }

        [TestMethod]
        public void ThievesGetThiefsReflexesAtLevel17()
        {
            TestingUtility.LevelTo(_rogue, 17, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.Thief);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Thief's Reflexes"));
        }

        [TestMethod]
        public void AssassinsGetBonusProficiences()
        {
            TestingUtility.LevelTo(_rogue, 3, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.Assassin);
            Assert.IsTrue(_rogue.Tools.Available.Contains(AvailableTool.DisguiseKit));
            Assert.IsTrue(_rogue.Tools.Available.Contains(AvailableTool.PoisonersKit));
            Assert.IsTrue(_rogue.Tools.Chosen.Contains(AvailableTool.DisguiseKit));
            Assert.IsTrue(_rogue.Tools.Chosen.Contains(AvailableTool.PoisonersKit));
        }

        [TestMethod]
        public void ChangingPathsRemovesProficiences()
        {
            TestingUtility.LevelTo(_rogue, 3, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.Assassin);
            Assert.IsTrue(_rogue.Tools.Available.Contains(AvailableTool.DisguiseKit));
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);

            Assert.IsFalse(_rogue.Tools.Available.Contains(AvailableTool.DisguiseKit));
            Assert.IsFalse(_rogue.Tools.Available.Contains(AvailableTool.PoisonersKit));
            Assert.IsFalse(_rogue.Tools.Chosen.Contains(AvailableTool.DisguiseKit));
            Assert.IsFalse(_rogue.Tools.Chosen.Contains(AvailableTool.PoisonersKit));
        }

        [TestMethod]
        public void AssassinsGetAssassinate()
        {
            TestingUtility.LevelTo(_rogue, 3, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.Assassin);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Assassinate"));
        }

        [TestMethod]
        public void AssassinsGetInfiltrationExpertiseAtLevel9()
        {
            TestingUtility.LevelTo(_rogue, 9, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.Assassin);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Infiltration Expertise"));
        }

        [TestMethod]
        public void AssassinsGetImposterAtLevel13()
        {
            TestingUtility.LevelTo(_rogue, 13, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.Assassin);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Imposter"));
        }

        [TestMethod]
        public void AssassinsGetDeathStrikeAtLevel17()
        {
            TestingUtility.LevelTo(_rogue, 17, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.Assassin);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Death Strike"));
        }

        [TestMethod]
        public void ArcaneTrickstersGetSpellcasting()
        {
            TestingUtility.LevelTo(_rogue, 3, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Spellcasting"));
        }

        [TestMethod]
        public void ArcaneTrickstersGetMageHandLegerdemain()
        {
            TestingUtility.LevelTo(_rogue, 3, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Mage Hand Legerdemain"));
        }

        [TestMethod]
        public void ArcaneTrickstersGetMagicalAmbushAtLevel9()
        {
            TestingUtility.LevelTo(_rogue, 9, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Magical Ambush"));
        }

        [TestMethod]
        public void ArcaneTrickstersGetVersatileTricksterAtLevel13()
        {
            TestingUtility.LevelTo(_rogue, 13, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Versatile Trickster"));
        }

        [TestMethod]
        public void ArcaneTrickstersGetSpellThiefAtLevel17()
        {
            TestingUtility.LevelTo(_rogue, 17, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.IsTrue(_rogue.Features.AllFeatures.ContainsKey("Spell Thief"));
        }

        [TestMethod]
        public void ArcaneTrickerIsAddedAsASpellcastingClass()
        {
            TestingUtility.LevelTo(_rogue, 3, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual("Arcane Trickster", _rogue.SpellcastingClasses.First().Name);
        }

        [TestMethod]
        public void ArcaneTrickstersGet3CantripsAtLevel3()
        {
            TestingUtility.LevelTo(_rogue, 3, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxCantrips);
        }

        [TestMethod]
        public void ArcaneTrickstersGet3SpellsAtLevel3()
        {
            TestingUtility.LevelTo(_rogue, 3, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxSpells);
        }

        [TestMethod]
        public void ArcaneTrickstersGet2Level1SpellSlotsAtLevel3()
        {
            TestingUtility.LevelTo(_rogue, 3, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().SpellSlots.First);
        }

        [TestMethod]
        public void ArcaneTrickstersGet3I4I3AtLevel4()
        {
            TestingUtility.LevelTo(_rogue, 4, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.First);
        }

        [TestMethod]
        public void ArcaneTricksterDoesNotGetSpellSlotsForLevelsInOtherClasses()
        {
            TestingUtility.LevelTo(_rogue, 3, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            TestingUtility.LevelTo(_rogue, 4, AvailableClasses.Monk);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().SpellSlots.First);
        }

        [TestMethod]
        public void ArcaneTrickstersGet3I4I3AtLevel5()
        {
            TestingUtility.LevelTo(_rogue, 5, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.First);
        }

        [TestMethod]
        public void ArcaneTrickstersGet3I4I3AtLevel6()
        {
            TestingUtility.LevelTo(_rogue, 6, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.First);
        }

        [TestMethod]
        public void ArcaneTrickstersGet3I5I4I2AtLevel7()
        {
            TestingUtility.LevelTo(_rogue, 7, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(5, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().SpellSlots.Second);
        }

        [TestMethod]
        public void ArcaneTrickstersGet3I6I4I2AtLevel8()
        {
            TestingUtility.LevelTo(_rogue, 8, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(6, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().SpellSlots.Second);
        }

        [TestMethod]
        public void ArcaneTrickstersGet3I6I4I2AtLevel9()
        {
            TestingUtility.LevelTo(_rogue, 9, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(6, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().SpellSlots.Second);
        }

        [TestMethod]
        public void ArcaneTrickstersGet4I7I4I3AtLevel10()
        {
            TestingUtility.LevelTo(_rogue, 10, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(7, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
        }

        [TestMethod]
        public void ArcaneTrickstersGet4I8I4I3AtLevel11()
        {
            TestingUtility.LevelTo(_rogue, 11, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(8, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
        }

        [TestMethod]
        public void ArcaneTrickstersGet4I8I4I3AtLevel12()
        {
            TestingUtility.LevelTo(_rogue, 12, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(8, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
        }

        [TestMethod]
        public void ArcaneTrickstersGet4I9I4I3I2AtLevel13()
        {
            TestingUtility.LevelTo(_rogue, 13, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(9, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().SpellSlots.Third);
        }

        [TestMethod]
        public void ArcaneTrickstersGet4I10I4I3I2AtLevel14()
        {
            TestingUtility.LevelTo(_rogue, 14, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(10, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().SpellSlots.Third);
        }

        [TestMethod]
        public void ArcaneTrickstersGet4I10I4I3I2AtLevel15()
        {
            TestingUtility.LevelTo(_rogue, 15, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(10, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().SpellSlots.Third);
        }

        [TestMethod]
        public void ArcaneTrickstersGet4I11I4I3I3AtLevel16()
        {
            TestingUtility.LevelTo(_rogue, 16, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(11, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Third);
        }

        [TestMethod]
        public void ArcaneTrickstersGet4I11I4I3I3AtLevel17()
        {
            TestingUtility.LevelTo(_rogue, 17, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(11, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Third);
        }

        [TestMethod]
        public void ArcaneTrickstersGet4I11I4I3I3AtLevel18()
        {
            TestingUtility.LevelTo(_rogue, 18, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(11, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Third);
        }

        [TestMethod]
        public void ArcaneTrickstersGet4I12I4I3I3I1AtLevel19()
        {
            TestingUtility.LevelTo(_rogue, 19, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(12, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Third);
            Assert.AreEqual(1, _rogue.SpellcastingClasses.First().SpellSlots.Fourth);
        }

        [TestMethod]
        public void ArcaneTrickstersGet4I13I4I3I3I1AtLevel20()
        {
            TestingUtility.LevelTo(_rogue, 20, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(13, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Third);
            Assert.AreEqual(1, _rogue.SpellcastingClasses.First().SpellSlots.Fourth);
        }

        [TestMethod]
        public void ArcaneTricksterSaveDcIsCalculated()
        {
            TestingUtility.LevelTo(_rogue, 3, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(10, _rogue.SpellcastingClasses.First().SaveDc); //Initial 8+Prof+Int
            _rogue.Abilities.Intelligence.Score = 12;
            Assert.AreEqual(11, _rogue.SpellcastingClasses.First().SaveDc); 
            TestingUtility.LevelTo(_rogue, 5, AvailableClasses.Rogue);
            Assert.AreEqual(12, _rogue.SpellcastingClasses.First().SaveDc); 
        }

        [TestMethod]
        public void SpellAttackModIsCalculated()
        {
            TestingUtility.LevelTo(_rogue, 3, AvailableClasses.Rogue);
            _rogue.ChosePath(AvailablePaths.ArcaneTrickster);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().AttackMod); //Initial Prof+Int
            _rogue.Abilities.Intelligence.Score = 12;
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().AttackMod);
            TestingUtility.LevelTo(_rogue, 5, AvailableClasses.Rogue);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().AttackMod); 
        }

        [TestMethod]
        public void RoguesKeepTheirSkillsWhenTheyLevel()
        {
            _rogue.ChooseSkill(AvailableSkill.Acrobatics);
            Assert.IsTrue(_rogue.Skills.Chosen.Contains(AvailableSkill.Acrobatics));
            CharacterFactory.LevelUp(_rogue, AvailableClasses.Rogue);
            Assert.IsTrue(_rogue.Skills.Chosen.Contains(AvailableSkill.Acrobatics));
        }
    }
}
