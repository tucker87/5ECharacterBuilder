using System.Linq;
using NUnit.Framework;
using _5ECharacterBuilder;
using _5EDatabase;

namespace _5ECharacterBuilderTests.CharacterClassTests
{
    [TestFixture]
    class RogueTests
    {
        private static ICharacter _rogue;

        [SetUp]
        public static void SetUp()
        {
            _rogue = CharacterFactory.BuildACharacter(Race.Human, Class.Rogue, Background.Acolyte);
        }

        [Test]
        public void RoguesAreRogues()
        {
            Assert.IsTrue(_rogue.Classes.Contains(Class.Rogue));
        }

        [Test]
        public void RoguesHave1D8HitDiceAtLevelOne()
        {
            Assert.AreEqual(8, _rogue.HitDice[0]);
        }

        [Test]
        public void RoguesAreProficientWithLightArmor()
        {
            Assert.IsTrue(_rogue.ArmorProficiencies.Contains(ArmorType.Padded));
            Assert.IsTrue(_rogue.ArmorProficiencies.Contains(ArmorType.Leather));
            Assert.IsTrue(_rogue.ArmorProficiencies.Contains(ArmorType.StuddedLeather));
        }

        [Test]
        public void RogueAreProficientWithSimpleWeapons()
        {
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(WeaponType.Club));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(WeaponType.Dagger));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(WeaponType.Greatclub));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(WeaponType.Handaxe));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(WeaponType.Javelin));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(WeaponType.LightHammer));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(WeaponType.Mace));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(WeaponType.Quarterstaff));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(WeaponType.Sickle));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(WeaponType.Spear));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(WeaponType.UnarmedStrike));
        }

        [Test]
        public void RoguesAreProficientWithHandCrossbowsLongswordsRapiersAndShortswords()
        {
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(WeaponType.HandCrossbows));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(WeaponType.LongSword));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(WeaponType.Rapier));
            Assert.IsTrue(_rogue.WeaponProficiencies.Contains(WeaponType.ShortSword));
        }

        [Test]
        public void RoguesAreProficientWithTheivesTools()
        {
            Assert.IsTrue(_rogue.Tools.Available.Contains(Tool.ThievesTools));
            Assert.IsTrue(_rogue.Tools.Chosen.Contains(Tool.ThievesTools));
        }

        [Test]
        public void RoguesAreProficientInDexterityAndIntelligenceSavingThrows()
        {
            Assert.IsTrue(_rogue.SavingThrows.Contains(SavingThrow.Dexterity));
            Assert.IsTrue(_rogue.SavingThrows.Contains(SavingThrow.Intelligence));
        }

        [Test]
        public void RoguesCanChoose4SkillsFromTheirList()
        {
            Assert.AreEqual(4, _rogue.Skills.Max - _rogue.Skills.Chosen.Count);

            Assert.IsTrue(_rogue.Skills.Available.Contains(Skill.Acrobatics));
            Assert.IsTrue(_rogue.Skills.Available.Contains(Skill.Athletics));
            Assert.IsTrue(_rogue.Skills.Available.Contains(Skill.Deception));
            Assert.IsTrue(_rogue.Skills.Available.Contains(Skill.Insight));
            Assert.IsTrue(_rogue.Skills.Available.Contains(Skill.Intimidation));
            Assert.IsTrue(_rogue.Skills.Available.Contains(Skill.Investigation));
            Assert.IsTrue(_rogue.Skills.Available.Contains(Skill.Perception));
            Assert.IsTrue(_rogue.Skills.Available.Contains(Skill.Performance));
            Assert.IsTrue(_rogue.Skills.Available.Contains(Skill.Persuasion));
            Assert.IsTrue(_rogue.Skills.Available.Contains(Skill.SleightOfHand));
            Assert.IsTrue(_rogue.Skills.Available.Contains(Skill.Stealth));
        }

        [Test]
        public void RoguesGetExpertiseOn2SkillsTheyHaveChosen()
        {
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Expertise"));

            _rogue.ChooseSkill(Skill.Acrobatics);
            _rogue.ChooseSkill(Skill.Athletics);
            _rogue.ChooseExpertise(Skill.Acrobatics);
            _rogue.ChooseExpertise(Skill.Athletics);

            Assert.IsTrue(_rogue.Skills.Expertise.Contains(Skill.Acrobatics));
            Assert.IsTrue(_rogue.Skills.Expertise.Contains(Skill.Athletics));
        }

        [Test]
        public void RoguesCannotChooseExpertiseInASkillTheyAreNotProficientIn()
        {
            _rogue.ChooseSkill(Skill.Acrobatics);
            _rogue.ChooseExpertise(Skill.Acrobatics);
            _rogue.ChooseExpertise(Skill.Athletics);

            Assert.IsTrue(_rogue.Skills.Expertise.Contains(Skill.Acrobatics));
            Assert.IsFalse(_rogue.Skills.Expertise.Contains(Skill.Athletics));
        }

        [Test]
        public void RouguesCannotChooseMoreExpertiseThanTheyHaveAvailable()
        {
            _rogue.ChooseSkill(Skill.Acrobatics);
            _rogue.ChooseSkill(Skill.Athletics);
            Assert.Throws<TooManySkillsException>(() => _rogue.ChooseSkill(Skill.Deception));
        }

        [Test]
        public void RougesCanChooseExpertiseInThievesTools()
        {
            _rogue.LearnTool(Tool.ThievesTools);
            _rogue.ChooseExpertise(Tool.ThievesTools);
            Assert.IsTrue(_rogue.Tools.Expertise.Contains(Tool.ThievesTools));
        }

        [Test]
        public void RougesChoiceOfToolCountsAgainstTheirExpertiseMax()
        {
            _rogue.LearnTool(Tool.ThievesTools);
            _rogue.ChooseSkill(Skill.Acrobatics);
            _rogue.ChooseSkill(Skill.Athletics);

            _rogue.ChooseExpertise(Tool.ThievesTools);
            _rogue.ChooseExpertise(Skill.Acrobatics);
            _rogue.ChooseExpertise(Skill.Athletics);

            Assert.IsTrue(_rogue.Tools.Expertise.Contains(Tool.ThievesTools));
            Assert.IsTrue(_rogue.Skills.Expertise.Contains(Skill.Acrobatics));
            Assert.IsFalse(_rogue.Skills.Expertise.Contains(Skill.Athletics));
        }

        [Test]
        public void RoguesGetSneakAttackWith1Dice()
        {
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Sneak Attack"));
            Assert.AreEqual(1, _rogue.SneakAttackDice);
        }

        [Test]
        public void RougesSneakAttackGoesTo2DiceAtLevel3()
        {
            TestingUtility.LevelTo(ref _rogue, 3, Class.Rogue);
            Assert.AreEqual(2, _rogue.SneakAttackDice);
        }
        
        [Test]
        public void RougesSneakAttackGoesTo3DiceAtLevel5()
        {
            TestingUtility.LevelTo(ref _rogue, 5, Class.Rogue);
            Assert.AreEqual(3, _rogue.SneakAttackDice);
        }

        [Test]
        public void RougesSneakAttackGoesTo4DiceAtLevel7()
        {
            TestingUtility.LevelTo(ref _rogue, 7, Class.Rogue);
            Assert.AreEqual(4, _rogue.SneakAttackDice);
        }

        [Test]
        public void RougesSneakAttackGoesTo5DiceAtLevel9()
        {
            TestingUtility.LevelTo(ref _rogue, 9, Class.Rogue);
            Assert.AreEqual(5, _rogue.SneakAttackDice);
        }

        [Test]
        public void RougesSneakAttackGoesTo6DiceAtLevel11()
        {
            TestingUtility.LevelTo(ref _rogue, 11, Class.Rogue);
            Assert.AreEqual(6, _rogue.SneakAttackDice);
        }

        [Test]
        public void RougesSneakAttackGoesTo7DiceAtLevel13()
        {
            TestingUtility.LevelTo(ref _rogue, 13, Class.Rogue);
            Assert.AreEqual(7, _rogue.SneakAttackDice);
        }

        [Test]
        public void RougesSneakAttackGoesTo8DiceAtLevel15()
        {
            TestingUtility.LevelTo(ref _rogue, 15, Class.Rogue);
            Assert.AreEqual(8, _rogue.SneakAttackDice);
        }

        [Test]
        public void RougesSneakAttackGoesTo9DiceAtLevel17()
        {
            TestingUtility.LevelTo(ref _rogue, 17, Class.Rogue);
            Assert.AreEqual(9, _rogue.SneakAttackDice);
        }

        [Test]
        public void RougesSneakAttackGoesTo10DiceAtLevel19()
        {
            TestingUtility.LevelTo(ref _rogue, 19, Class.Rogue);
            Assert.AreEqual(10, _rogue.SneakAttackDice);
        }

        [Test]
        public void RougesGetThievesCant()
        {
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Thieves' Cant"));
        }

        [Test]
        public void FeaturesAreAddedEvenIfLevelIsSkipped()
        {
            TestingUtility.LevelTo(ref _rogue, 2, Class.Rogue);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Thieves' Cant"));
        }

        [Test]
        public void RoguesGetCunningActionAtLevel2()
        {
            TestingUtility.LevelTo(ref _rogue, 2, Class.Rogue);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Cunning Action"));
        }

        [Test]
        public void LevelingInAnotherClassDoesNotCountTowardRogueLevel()
        {
            _rogue.Abilities.Wisdom.Score = 13;
            _rogue.Abilities.Dexterity.Score = 13;
            TestingUtility.LevelTo(ref _rogue, 2, Class.Monk);
            Assert.IsFalse(_rogue.AllFeatures.Any(af => af.Name =="Cunning Action"));
        }

        [Test]
        public void RoguesHaveAChoiceOf3PathsAtLevel3()
        {
            TestingUtility.LevelTo(ref _rogue, 3, Class.Rogue);

            Assert.IsTrue(_rogue.AvailablePaths.Contains(Path.Thief));
            Assert.IsTrue(_rogue.AvailablePaths.Contains(Path.Assassin));
            Assert.IsTrue(_rogue.AvailablePaths.Contains(Path.ArcaneTrickster));
        }

        [Test]
        public void RoguesGetUncannyDodgeAtLevel5()
        {
            TestingUtility.LevelTo(ref _rogue, 5, Class.Rogue);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Uncanny Dodge"));
        }

        [Test]
        public void RoguesGetTwoMoreExpertisePointsAtLevel6()
        {
            TestingUtility.LevelTo(ref _rogue, 6, Class.Rogue);
            Assert.AreEqual(4, _rogue.Skills.MaxExpertise);
        }

        [Test]
        public void RoguesGetEvasionAtLevel7()
        {
            TestingUtility.LevelTo(ref _rogue, 7, Class.Rogue);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Evasion"));
        }

        [Test]
        public void RoguesGetAnExtraAbilityScoreImprovementAtLevel10()
        {
            TestingUtility.LevelTo(ref _rogue, 10, Class.Rogue);
            Assert.AreEqual(6, _rogue.Abilities.ImprovementPoints);
        }

        [Test]
        public void RoguesGetReliableTalentAtLevel11()
        {
            TestingUtility.LevelTo(ref _rogue, 11, Class.Rogue);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Reliable Talent"));
        }

        [Test]
        public void RoguesGetBlindSenseAtLevel14()
        {
            TestingUtility.LevelTo(ref _rogue, 14, Class.Rogue);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Blindsense"));
        }

        [Test]
        public void RoguesGetSlipperyMindAtLevel15()
        {
            TestingUtility.LevelTo(ref _rogue, 15, Class.Rogue);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Slippery Mind"));
            Assert.IsTrue(_rogue.SavingThrows.Contains(SavingThrow.Wisdom));
        }

        [Test]
        public void RoguesGetElusiveAtLevel18()
        {
            TestingUtility.LevelTo(ref _rogue, 18, Class.Rogue);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Elusive"));
        }

        [Test]
        public void RoguesGetStrokeOfLuckAtLevel20()
        {
            TestingUtility.LevelTo(ref _rogue, 20, Class.Rogue);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Stroke Of Luck"));
        }

        [Test]
        public void RoguesCannotChoosePathsFromOtherClasses()
        {
            _rogue.ChosePath(Path.WayOfShadow);
            Assert.IsFalse(_rogue.ChosenPaths.Contains(Path.WayOfShadow));
        }

        [Test]
        public void ThievesGetFastHandsAtLevel3()
        {
            TestingUtility.LevelTo(ref _rogue, 3, Class.Rogue);
            _rogue.ChosePath(Path.Thief);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Fast Hands"));
        }

        [Test]
        public void ThievesGetSecondStoryWorkAtLevel3()
        {
            TestingUtility.LevelTo(ref _rogue, 3, Class.Rogue);
            _rogue.ChosePath(Path.Thief);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Second-Story Work"));
        }

        [Test]
        public void ThievesGetSupremeSneakAtLevel9()
        {
            TestingUtility.LevelTo(ref _rogue, 9, Class.Rogue);
            _rogue.ChosePath(Path.Thief);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Supreme Sneak"));
        }

        [Test]
        public void ThievesGetUseMagicDeviceAtLevel13()
        {
            TestingUtility.LevelTo(ref _rogue, 13, Class.Rogue);
            _rogue.ChosePath(Path.Thief);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Use Magic Device"));
        }

        [Test]
        public void ThievesGetThiefsReflexesAtLevel17()
        {
            TestingUtility.LevelTo(ref _rogue, 17, Class.Rogue);
            _rogue.ChosePath(Path.Thief);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Thief's Reflexes"));
        }

        [Test]
        public void AssassinsGetBonusProficiences()
        {
            TestingUtility.LevelTo(ref _rogue, 3, Class.Rogue);
            _rogue.ChosePath(Path.Assassin);
            Assert.IsTrue(_rogue.Tools.Available.Contains(Tool.DisguiseKit));
            Assert.IsTrue(_rogue.Tools.Available.Contains(Tool.PoisonersKit));
            Assert.IsTrue(_rogue.Tools.Chosen.Contains(Tool.DisguiseKit));
            Assert.IsTrue(_rogue.Tools.Chosen.Contains(Tool.PoisonersKit));
        }

        [Test]
        public void ChangingPathsRemovesProficiences()
        {
            TestingUtility.LevelTo(ref _rogue, 3, Class.Rogue);
            _rogue.ChosePath(Path.Assassin);
            Assert.IsTrue(_rogue.Tools.Available.Contains(Tool.DisguiseKit));
            _rogue.ChosePath(Path.ArcaneTrickster);

            Assert.IsFalse(_rogue.Tools.Available.Contains(Tool.DisguiseKit));
            Assert.IsFalse(_rogue.Tools.Available.Contains(Tool.PoisonersKit));
            Assert.IsFalse(_rogue.Tools.Chosen.Contains(Tool.DisguiseKit));
            Assert.IsFalse(_rogue.Tools.Chosen.Contains(Tool.PoisonersKit));
        }

        [Test]
        public void AssassinsGetAssassinate()
        {
            TestingUtility.LevelTo(ref _rogue, 3, Class.Rogue);
            _rogue.ChosePath(Path.Assassin);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Assassinate"));
        }

        [Test]
        public void AssassinsGetInfiltrationExpertiseAtLevel9()
        {
            TestingUtility.LevelTo(ref _rogue, 9, Class.Rogue);
            _rogue.ChosePath(Path.Assassin);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Infiltration Expertise"));
        }

        [Test]
        public void AssassinsGetImposterAtLevel13()
        {
            TestingUtility.LevelTo(ref _rogue, 13, Class.Rogue);
            _rogue.ChosePath(Path.Assassin);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Imposter"));
        }

        [Test]
        public void AssassinsGetDeathStrikeAtLevel17()
        {
            TestingUtility.LevelTo(ref _rogue, 17, Class.Rogue);
            _rogue.ChosePath(Path.Assassin);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Death Strike"));
        }

        [Test]
        public void ArcaneTrickstersGetSpellcasting()
        {
            TestingUtility.LevelTo(ref _rogue, 3, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Spellcasting"));
        }

        [Test]
        public void ArcaneTrickstersGetMageHandLegerdemain()
        {
            TestingUtility.LevelTo(ref _rogue, 3, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Mage Hand Legerdemain"));
        }

        [Test]
        public void ArcaneTrickstersGetMagicalAmbushAtLevel9()
        {
            TestingUtility.LevelTo(ref _rogue, 9, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Magical Ambush"));
        }

        [Test]
        public void ArcaneTrickstersGetVersatileTricksterAtLevel13()
        {
            TestingUtility.LevelTo(ref _rogue, 13, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Versatile Trickster"));
        }

        [Test]
        public void ArcaneTrickstersGetSpellThiefAtLevel17()
        {
            TestingUtility.LevelTo(ref _rogue, 17, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.IsTrue(_rogue.AllFeatures.Any(af => af.Name =="Spell Thief"));
        }

        [Test]
        public void ArcaneTrickerIsAddedAsASpellcastingClass()
        {
            TestingUtility.LevelTo(ref _rogue, 3, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual("Arcane Trickster", _rogue.SpellcastingClasses.First().Name);
        }

        [Test]
        public void ArcaneTrickstersGet3CantripsAtLevel3()
        {
            TestingUtility.LevelTo(ref _rogue, 3, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxCantrips);
        }

        [Test]
        public void ArcaneTrickstersGet3SpellsAtLevel3()
        {
            TestingUtility.LevelTo(ref _rogue, 3, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxSpells);
        }

        [Test]
        public void ArcaneTrickstersGet2Level1SpellSlotsAtLevel3()
        {
            TestingUtility.LevelTo(ref _rogue, 3, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().SpellSlots.First);
        }

        [Test]
        public void ArcaneTrickstersGet3I4I3AtLevel4()
        {
            TestingUtility.LevelTo(ref _rogue, 4, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.First);
        }

        [Test]
        public void ArcaneTricksterDoesNotGetSpellSlotsForLevelsInOtherClasses()
        {
            TestingUtility.LevelTo(ref _rogue, 3, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            _rogue.Abilities.Dexterity.Score = 13;
            _rogue.Abilities.Wisdom.Score = 13;
            TestingUtility.LevelTo(ref _rogue, 4, Class.Monk);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().SpellSlots.First);
        }

        [Test]
        public void ArcaneTrickstersGet3I4I3AtLevel5()
        {
            TestingUtility.LevelTo(ref _rogue, 5, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.First);
        }

        [Test]
        public void ArcaneTrickstersGet3I4I3AtLevel6()
        {
            TestingUtility.LevelTo(ref _rogue, 6, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.First);
        }

        [Test]
        public void ArcaneTrickstersGet3I5I4I2AtLevel7()
        {
            TestingUtility.LevelTo(ref _rogue, 7, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(5, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().SpellSlots.Second);
        }

        [Test]
        public void ArcaneTrickstersGet3I6I4I2AtLevel8()
        {
            TestingUtility.LevelTo(ref _rogue, 8, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(6, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().SpellSlots.Second);
        }

        [Test]
        public void ArcaneTrickstersGet3I6I4I2AtLevel9()
        {
            TestingUtility.LevelTo(ref _rogue, 9, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(6, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().SpellSlots.Second);
        }

        [Test]
        public void ArcaneTrickstersGet4I7I4I3AtLevel10()
        {
            TestingUtility.LevelTo(ref _rogue, 10, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(7, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
        }

        [Test]
        public void ArcaneTrickstersGet4I8I4I3AtLevel11()
        {
            TestingUtility.LevelTo(ref _rogue, 11, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(8, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
        }

        [Test]
        public void ArcaneTrickstersGet4I8I4I3AtLevel12()
        {
            TestingUtility.LevelTo(ref _rogue, 12, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(8, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
        }

        [Test]
        public void ArcaneTrickstersGet4I9I4I3I2AtLevel13()
        {
            TestingUtility.LevelTo(ref _rogue, 13, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(9, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().SpellSlots.Third);
        }

        [Test]
        public void ArcaneTrickstersGet4I10I4I3I2AtLevel14()
        {
            TestingUtility.LevelTo(ref _rogue, 14, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(10, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().SpellSlots.Third);
        }

        [Test]
        public void ArcaneTrickstersGet4I10I4I3I2AtLevel15()
        {
            TestingUtility.LevelTo(ref _rogue, 15, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(10, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().SpellSlots.Third);
        }

        [Test]
        public void ArcaneTrickstersGet4I11I4I3I3AtLevel16()
        {
            TestingUtility.LevelTo(ref _rogue, 16, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(11, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Third);
        }

        [Test]
        public void ArcaneTrickstersGet4I11I4I3I3AtLevel17()
        {
            TestingUtility.LevelTo(ref _rogue, 17, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(11, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Third);
        }

        [Test]
        public void ArcaneTrickstersGet4I11I4I3I3AtLevel18()
        {
            TestingUtility.LevelTo(ref _rogue, 18, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(11, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Third);
        }

        [Test]
        public void ArcaneTrickstersGet4I12I4I3I3I1AtLevel19()
        {
            TestingUtility.LevelTo(ref _rogue, 19, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(12, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Third);
            Assert.AreEqual(1, _rogue.SpellcastingClasses.First().SpellSlots.Fourth);
        }

        [Test]
        public void ArcaneTrickstersGet4I13I4I3I3I1AtLevel20()
        {
            TestingUtility.LevelTo(ref _rogue, 20, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().MaxCantrips);
            Assert.AreEqual(13, _rogue.SpellcastingClasses.First().MaxSpells);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().SpellSlots.First);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Second);
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().SpellSlots.Third);
            Assert.AreEqual(1, _rogue.SpellcastingClasses.First().SpellSlots.Fourth);
        }

        [Test]
        public void ArcaneTricksterSaveDcIsCalculated()
        {
            TestingUtility.LevelTo(ref _rogue, 3, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(10, _rogue.SpellcastingClasses.First().SaveDc); //Initial 8+Prof+Int
            _rogue.Abilities.Intelligence.Score = 12;
            Assert.AreEqual(11, _rogue.SpellcastingClasses.First().SaveDc);
            TestingUtility.LevelTo(ref _rogue, 5, Class.Rogue);
            Assert.AreEqual(12, _rogue.SpellcastingClasses.First().SaveDc);
        }

        [Test]
        public void SpellAttackModIsCalculated()
        {
            TestingUtility.LevelTo(ref _rogue, 3, Class.Rogue);
            _rogue.ChosePath(Path.ArcaneTrickster);
            Assert.AreEqual(2, _rogue.SpellcastingClasses.First().AttackMod); //Initial Prof+Int
            _rogue.Abilities.Intelligence.Score = 12;
            Assert.AreEqual(3, _rogue.SpellcastingClasses.First().AttackMod);
            TestingUtility.LevelTo(ref _rogue, 5, Class.Rogue);
            Assert.AreEqual(4, _rogue.SpellcastingClasses.First().AttackMod); 
        }

        [Test]
        public void RoguesKeepTheirSkillsWhenTheyLevel()
        {
            _rogue.ChooseSkill(Skill.Acrobatics);
            Assert.IsTrue(_rogue.Skills.Chosen.Contains(Skill.Acrobatics));
            CharacterFactory.LevelUp(ref _rogue, Class.Rogue);
            Assert.IsTrue(_rogue.Skills.Chosen.Contains(Skill.Acrobatics));
        }
    }
}
