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
            _rogue = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Rogue, AvailableBackgrounds.Criminal);
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
            Assert.AreEqual(4, _rogue.Skills.Max - _rogue.Skills.Chosen.Count);

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
        public void RougesChoiceOfToolCountsAgainstTheirSkillMax()
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
    }
}
