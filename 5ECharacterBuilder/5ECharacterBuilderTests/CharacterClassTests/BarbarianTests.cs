using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests.CharacterClassTests
{
    [TestClass]
    class BarbarianTests
    {
        private static ICharacter _barbarian;

        [TestInitialize]
        public void Setup()
        {
            _barbarian = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Barbarian, AvailableBackgrounds.Acolyte);
        }

        [TestMethod]
        public void BarbariansAreBarbarians()
        {
            Assert.IsTrue(_barbarian.Classes.Contains("Barbarian"));
        }

        [TestMethod]
        public void BarbariansHave1D12HitDiceAtLevel1()
        {
            Assert.AreEqual(12, _barbarian.HitDice[0]);
        }

        [TestMethod]
        public void BarbariansAreProficientWithLightArmor()
        {
            Assert.IsTrue(_barbarian.ArmorProficiencies.Contains(AvailableArmor.Padded));
            Assert.IsTrue(_barbarian.ArmorProficiencies.Contains(AvailableArmor.Leather));
            Assert.IsTrue(_barbarian.ArmorProficiencies.Contains(AvailableArmor.StuddedLeather));
        }

        [TestMethod]
        public void BarbariansAreProficientWithMediumArmor()
        {
            Assert.IsTrue(_barbarian.ArmorProficiencies.Contains(AvailableArmor.HalfPlate));
            Assert.IsTrue(_barbarian.ArmorProficiencies.Contains(AvailableArmor.ChainShirt));
            Assert.IsTrue(_barbarian.ArmorProficiencies.Contains(AvailableArmor.ScaleMail));
        }

        [TestMethod]
        public void BarbariansAreProficientWithShields()
        {
            Assert.IsTrue(_barbarian.ArmorProficiencies.Contains(AvailableArmor.Shield));
        }

        [TestMethod]
        public void BarbariansAreProficientWithSimpleWeapons()
        {
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Club));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Dagger));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Greatclub));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Handaxe));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Javelin));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.LightHammer));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Mace));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Quarterstaff));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Sickle));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Spear));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.UnarmedStrike));
        }

        [TestMethod]
        public void BarbariansAreProficientWithMartialeWeapons()
        {
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.BattleAxe));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Flail));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Glaive));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Greataxe));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.GreatSword));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Halberd));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Lance));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.LongSword));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Maul));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Morningstar));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Pike));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Rapier));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Scimitar));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.ShortSword));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Trident));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.WarPick));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Warhammer));
            Assert.IsTrue(_barbarian.WeaponProficiencies.Contains(AvailableWeapon.Whip));
        }

        [TestMethod]
        public void BarbariansAreProficientInStrengthAndConstitutionSavingThrows()
        {
            Assert.IsTrue(_barbarian.SavingThrows.Contains(SavingThrow.Strength));
            Assert.IsTrue(_barbarian.SavingThrows.Contains(SavingThrow.Constitution));
        }

        [TestMethod]
        public void BarbariansCanChoose2SkillsFromTheirList()
        {
            Assert.AreEqual(2, _barbarian.Skills.Max - _barbarian.Skills.Chosen.Count);

            Assert.IsTrue(_barbarian.Skills.Available.Contains(AvailableSkill.AnimalHandling));
            Assert.IsTrue(_barbarian.Skills.Available.Contains(AvailableSkill.Athletics));
            Assert.IsTrue(_barbarian.Skills.Available.Contains(AvailableSkill.Intimidation));
            Assert.IsTrue(_barbarian.Skills.Available.Contains(AvailableSkill.Nature));
            Assert.IsTrue(_barbarian.Skills.Available.Contains(AvailableSkill.Perception));
            Assert.IsTrue(_barbarian.Skills.Available.Contains(AvailableSkill.Survival));
        }

        [TestMethod]
        public void BarbariansGetRageAndUnarmoredDefenseAtLevel1()
        {
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Rage"));
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Barbarian Unarmored Defense"));
        }

        [TestMethod]
        public void BarbariansUnarmorDefenseIs10PlusDexPlusCon()
        {
            _barbarian.Abilities.Dexterity.Score = 13;
            _barbarian.Abilities.Constitution.Score = 13;
            Assert.AreEqual(14, _barbarian.ArmorClass);
        }

        [TestMethod]
        public void BarbariansLoseUnarmorDefenseWhenWearingArmor()
        {
            _barbarian.Abilities.Dexterity.Score = 13;
            _barbarian.Abilities.Constitution.Score = 13;
            _barbarian.EquipArmor(AvailableArmor.Leather);
            Assert.AreEqual(13, _barbarian.ArmorClass);
        }

        [TestMethod]
        public void BarbariansDoNotLoseUnarmorDefenseWhenWearingAShield()
        {
            _barbarian.Abilities.Dexterity.Score = 13;
            _barbarian.Abilities.Constitution.Score = 13;
            _barbarian.ToggleShield();
            Assert.AreEqual(16, _barbarian.ArmorClass);
        }

        [TestMethod]
        public void BarbariansCanRage2TimesPerDayAtLevel1()
        {
            Assert.AreEqual(2, _barbarian.ClassTraits.RagesPerDay);
        }

        [TestMethod]
        public void BarbariansCanRage3TimesPerDayAtLevel3()
        {
            TestingUtility.LevelTo(_barbarian, 3, AvailableClasses.Barbarian);
            Assert.AreEqual(3, _barbarian.ClassTraits.RagesPerDay);
        }

        [TestMethod]
        public void BarbariansCanRage4TimesPerDayAtLevel6()
        {
            TestingUtility.LevelTo(_barbarian, 6, AvailableClasses.Barbarian);
            Assert.AreEqual(4, _barbarian.ClassTraits.RagesPerDay);
        }

        [TestMethod]
        public void BarbariansCanRage5TimesPerDayAtLevel12()
        {
            TestingUtility.LevelTo(_barbarian, 12, AvailableClasses.Barbarian);
            Assert.AreEqual(5, _barbarian.ClassTraits.RagesPerDay);
        }

        [TestMethod]
        public void BarbariansCanRage6TimesPerDayAtLevel17()
        {
            TestingUtility.LevelTo(_barbarian, 17, AvailableClasses.Barbarian);
            Assert.AreEqual(6, _barbarian.ClassTraits.RagesPerDay);
        }

        [TestMethod]
        public void BarbariansDo2RageDamageAtLevel1()
        {
            Assert.AreEqual(2, _barbarian.ClassTraits.RageDamage);
        }

        [TestMethod]
        public void BarbariansDo3RageDamageAtLevel9()
        {
            TestingUtility.LevelTo(_barbarian, 9, AvailableClasses.Barbarian);
            Assert.AreEqual(3, _barbarian.ClassTraits.RageDamage);
        }

        [TestMethod]
        public void BarbariansDo4RageDamageAtLevel16()
        {
            TestingUtility.LevelTo(_barbarian, 16, AvailableClasses.Barbarian);
            Assert.AreEqual(4, _barbarian.ClassTraits.RageDamage);
        }

        [TestMethod]
        public void BarbariansGetRecklessAttackAndDangerSenseAtLevel2()
        {
            TestingUtility.LevelTo(_barbarian, 2, AvailableClasses.Barbarian);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Reckless Attack"));
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Danger Sense"));
        }

        [TestMethod]
        public void BarbariansGetAnExtraAttackAtLevel5()
        {
            TestingUtility.LevelTo(_barbarian, 5, AvailableClasses.Barbarian);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Extra Attack"));
            Assert.AreEqual(2, _barbarian.AttacksPerTurn);
        }

        [TestMethod]
        public void BarbariansGetFastMovementAtLevel5()
        {
            TestingUtility.LevelTo(_barbarian, 5, AvailableClasses.Barbarian);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Fast Movement"));
            Assert.AreEqual(40, _barbarian.Speed);
        }

        [TestMethod]
        public void BarbariansGetFeralInstinctAtLevel7()
        {
            TestingUtility.LevelTo(_barbarian, 7, AvailableClasses.Barbarian);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Feral Instict"));
        }

        [TestMethod]
        public void BarbariansGetBrutalCriticalAtLevel9()
        {
            TestingUtility.LevelTo(_barbarian, 9, AvailableClasses.Barbarian);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Brutal Critical"));
        }

        [TestMethod]
        public void BarbariansGetRelentlessRageAtLevel11()
        {
            TestingUtility.LevelTo(_barbarian, 11, AvailableClasses.Barbarian);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Relentless Rage"));
        }

        [TestMethod]
        public void BarbariansGetPersistentRageAtLevel15()
        {
            TestingUtility.LevelTo(_barbarian, 15, AvailableClasses.Barbarian);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Persistent Rage"));
        }

        [TestMethod]
        public void BarbariansGetIndomitableMightAtLevel18()
        {
            TestingUtility.LevelTo(_barbarian, 18, AvailableClasses.Barbarian);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Indomitable Might"));
        }

        [TestMethod]
        public void BarbariansGetPrimalChampionAtLevel20()
        {
            TestingUtility.LevelTo(_barbarian, 20, AvailableClasses.Barbarian);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Primal Champion"));
            _barbarian.Abilities.Strength.Score = 20;
            _barbarian.Abilities.Constitution.Score = 20;

            Assert.AreEqual(4, _barbarian.Abilities.Strength.ClassBonus);

            Assert.AreEqual(24, _barbarian.Abilities.Strength.Score);
            Assert.AreEqual(24, _barbarian.Abilities.Constitution.Score);
        }

        [TestMethod]
        public void BarbariansGetAPathAtLevel3()
        {
            Assert.AreEqual(0, _barbarian.ClassPath.Available.Count);
            TestingUtility.LevelTo(_barbarian, 3, AvailableClasses.Barbarian);
            Assert.AreEqual(2, _barbarian.ClassPath.Available.Count);
            Assert.IsTrue(_barbarian.ClassPath.Available.Contains(AvailablePaths.PathOfTheBerserker));
            Assert.IsTrue(_barbarian.ClassPath.Available.Contains(AvailablePaths.PathOfTheTotemWarrior));
        }

        [TestMethod]
        public void BerserkerBarbariansGetFrenzyAtLevel3()
        {
            TestingUtility.LevelTo(_barbarian, 3, AvailableClasses.Barbarian);
            _barbarian.ChoosePath(AvailablePaths.PathOfTheBerserker);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Frenzy"));
        }

        [TestMethod]
        public void BerserkerBarbariansGetMindLessRageAtLevel6()
        {
            TestingUtility.LevelTo(_barbarian, 6, AvailableClasses.Barbarian);
            _barbarian.ChoosePath(AvailablePaths.PathOfTheBerserker);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Mindless Rage"));
        }

        [TestMethod]
        public void BerserkerBarbariansGetIntimidatingPresenceAtLevel10()
        {
            TestingUtility.LevelTo(_barbarian, 10, AvailableClasses.Barbarian);
            _barbarian.ChoosePath(AvailablePaths.PathOfTheBerserker);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Intimidating Presence"));
        }

        [TestMethod]
        public void BerserkerBarbariansGetRetaliationAtLevel14()
        {
            TestingUtility.LevelTo(_barbarian, 14, AvailableClasses.Barbarian);
            _barbarian.ChoosePath(AvailablePaths.PathOfTheBerserker);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Retaliation"));
        }

        [TestMethod]
        public void TotemBarbariansGetSpiritSeekerAndTotemSpiritAtLevel3()
        {
            TestingUtility.LevelTo(_barbarian, 3, AvailableClasses.Barbarian);
            _barbarian.ChoosePath(AvailablePaths.PathOfTheTotemWarrior);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Spirit Seeker"));
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Totem Spirit"));
        }

        [TestMethod]
        public void TotemBarbariansGetAspectOfTheBeastAtLevel6()
        {
            TestingUtility.LevelTo(_barbarian, 6, AvailableClasses.Barbarian);
            _barbarian.ChoosePath(AvailablePaths.PathOfTheTotemWarrior);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Aspect of the Beast"));
        }

        [TestMethod]
        public void TotemBarbariansGetSpiritWalkerAtLevel10()
        {
            TestingUtility.LevelTo(_barbarian, 10, AvailableClasses.Barbarian);
            _barbarian.ChoosePath(AvailablePaths.PathOfTheTotemWarrior);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Spirit Walker"));
        }

        [TestMethod]
        public void TotemBarbariansGetTotemicAttunementAtLevel14()
        {
            TestingUtility.LevelTo(_barbarian, 14, AvailableClasses.Barbarian);
            _barbarian.ChoosePath(AvailablePaths.PathOfTheTotemWarrior);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Totemic Attunement"));
        }
    }
}
