using System.Linq;
using NUnit.Framework;
using _5ECharacterBuilder;
using _5EDatabase;

namespace _5ECharacterBuilderTests.CharacterClassTests
{
    [TestFixture]
    class BarbarianTests
    {
        private static ICharacter _barbarian;

        [SetUp]
        public void SetUp()
        {
            _barbarian = CharacterFactory.BuildACharacter(Race.Human, Class.Barbarian, Background.Acolyte);
        }

        [Test]
        public void BarbariansAreBarbarians()
        {
            Assert.IsTrue(_barbarian.Classes.Contains(Class.Barbarian));
        }

        [Test]
        public void BarbariansHave1D12HitDiceAtLevel1()
        {
            Assert.AreEqual(12, _barbarian.HitDice[0]);
        }

        [Test]
        public void BarbariansAreProficientWithLightArmor()
        {
            Assert.IsTrue(_barbarian.ArmorProficiencies.Contains(ArmorType.Padded));
            Assert.IsTrue(_barbarian.ArmorProficiencies.Contains(ArmorType.Leather));
            Assert.IsTrue(_barbarian.ArmorProficiencies.Contains(ArmorType.StuddedLeather));
        }

        [Test]
        public void BarbariansAreProficientWithMediumArmor()
        {
            Assert.IsTrue(_barbarian.ArmorProficiencies.Contains(ArmorType.HalfPlate));
            Assert.IsTrue(_barbarian.ArmorProficiencies.Contains(ArmorType.ChainShirt));
            Assert.IsTrue(_barbarian.ArmorProficiencies.Contains(ArmorType.ScaleMail));
        }

        [Test]
        public void BarbariansAreProficientWithShields()
        {
            Assert.IsTrue(_barbarian.ArmorProficiencies.Contains(ArmorType.Shield));
        }

        [Test]
        public void BarbariansAreProficientWithSimpleWeapons()
        {
            Assert.IsTrue(Armory.SimpleWeapons.IsSubsetOf(_barbarian.WeaponProficiencies));
        }

        [Test]
        public void BarbariansAreProficientWithMartialWeapons()
        {
            Assert.IsTrue(Armory.MartialWeapons.IsSubsetOf(_barbarian.WeaponProficiencies));
        }

        [Test]
        public void BarbariansAreProficientInStrengthAndConstitutionSavingThrows()
        {
            Assert.IsTrue(_barbarian.SavingThrows.Contains(SavingThrow.Strength));
            Assert.IsTrue(_barbarian.SavingThrows.Contains(SavingThrow.Constitution));
        }

        [Test]
        public void BarbariansCanChoose2SkillsFromTheirList()
        {
            Assert.AreEqual(2, _barbarian.Skills.Max - _barbarian.Skills.Chosen.Count);

            Assert.IsTrue(_barbarian.Skills.Available.Contains(Skill.AnimalHandling));
            Assert.IsTrue(_barbarian.Skills.Available.Contains(Skill.Athletics));
            Assert.IsTrue(_barbarian.Skills.Available.Contains(Skill.Intimidation));
            Assert.IsTrue(_barbarian.Skills.Available.Contains(Skill.Nature));
            Assert.IsTrue(_barbarian.Skills.Available.Contains(Skill.Perception));
            Assert.IsTrue(_barbarian.Skills.Available.Contains(Skill.Survival));
        }

        [Test]
        public void BarbariansGetRageAndUnarmoredDefenseAtLevel1()
        {
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Rage"));
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Barbarian Unarmored Defense"));
        }

        [Test]
        public void BarbariansUnarmorDefenseIs10PlusDexPlusCon()
        {
            _barbarian.Abilities.Dexterity.Score = 13;
            _barbarian.Abilities.Constitution.Score = 13;
            Assert.AreEqual(14, _barbarian.ArmorClass);
        }

        [Test]
        public void BarbariansLoseUnarmorDefenseWhenWearingArmor()
        {
            _barbarian.Abilities.Dexterity.Score = 13;
            _barbarian.Abilities.Constitution.Score = 13;
            _barbarian.EquipArmor(ArmorType.Leather);
            Assert.AreEqual(13, _barbarian.ArmorClass);
        }

        [Test]
        public void BarbariansDoNotLoseUnarmorDefenseWhenWearingAShield()
        {
            _barbarian.Abilities.Dexterity.Score = 13;
            _barbarian.Abilities.Constitution.Score = 13;
            _barbarian.ToggleShield();
            Assert.AreEqual(16, _barbarian.ArmorClass);
        }

        [Test]
        public void BarbariansCanRage2TimesPerDayAtLevel1()
        {
            Assert.AreEqual(2, _barbarian.ClassTraits.RagesPerDay);
        }

        [Test]
        public void BarbariansCanRage3TimesPerDayAtLevel3()
        {
            TestingUtility.LevelTo(ref _barbarian, 3, Class.Barbarian);
            Assert.AreEqual(3, _barbarian.ClassTraits.RagesPerDay);
        }

        [Test]
        public void BarbariansCanRage4TimesPerDayAtLevel6()
        {
            TestingUtility.LevelTo(ref _barbarian, 6, Class.Barbarian);
            Assert.AreEqual(4, _barbarian.ClassTraits.RagesPerDay);
        }

        [Test]
        public void BarbariansCanRage5TimesPerDayAtLevel12()
        {
            TestingUtility.LevelTo(ref _barbarian, 12, Class.Barbarian);
            Assert.AreEqual(5, _barbarian.ClassTraits.RagesPerDay);
        }

        [Test]
        public void BarbariansCanRage6TimesPerDayAtLevel17()
        {
            TestingUtility.LevelTo(ref _barbarian, 17, Class.Barbarian);
            Assert.AreEqual(6, _barbarian.ClassTraits.RagesPerDay);
        }

        [Test]
        public void BarbariansDo2RageDamageAtLevel1()
        {
            Assert.AreEqual(2, _barbarian.ClassTraits.RageDamage);
        }

        [Test]
        public void BarbariansDo3RageDamageAtLevel9()
        {
            TestingUtility.LevelTo(ref _barbarian, 9, Class.Barbarian);
            Assert.AreEqual(3, _barbarian.ClassTraits.RageDamage);
        }

        [Test]
        public void BarbariansDo4RageDamageAtLevel16()
        {
            TestingUtility.LevelTo(ref _barbarian, 16, Class.Barbarian);
            Assert.AreEqual(4, _barbarian.ClassTraits.RageDamage);
        }

        [Test]
        public void BarbariansGetRecklessAttackAndDangerSenseAtLevel2()
        {
            TestingUtility.LevelTo(ref _barbarian, 2, Class.Barbarian);
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Reckless Attack"));
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Danger Sense"));
        }

        [Test]
        public void BarbariansGetAnExtraAttackAtLevel5()
        {
            TestingUtility.LevelTo(ref _barbarian, 5, Class.Barbarian);
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Extra Attack"));
            Assert.AreEqual(2, _barbarian.AttacksPerTurn);
        }

        [Test]
        public void BarbariansGetFastMovementAtLevel5()
        {
            TestingUtility.LevelTo(ref _barbarian, 5, Class.Barbarian);
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Fast Movement"));
            Assert.AreEqual(40, _barbarian.Speed);
        }

        [Test]
        public void BarbariansGetFeralInstinctAtLevel7()
        {
            TestingUtility.LevelTo(ref _barbarian, 7, Class.Barbarian);
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Feral Instict"));
        }

        [Test]
        public void BarbariansGetBrutalCriticalAtLevel9()
        {
            TestingUtility.LevelTo(ref _barbarian, 9, Class.Barbarian);
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Brutal Critical"));
        }

        [Test]
        public void BarbariansGetRelentlessRageAtLevel11()
        {
            TestingUtility.LevelTo(ref _barbarian, 11, Class.Barbarian);
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Relentless Rage"));
        }

        [Test]
        public void BarbariansGetPersistentRageAtLevel15()
        {
            TestingUtility.LevelTo(ref _barbarian, 15, Class.Barbarian);
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Persistent Rage"));
        }

        [Test]
        public void BarbariansGetIndomitableMightAtLevel18()
        {
            TestingUtility.LevelTo(ref _barbarian, 18, Class.Barbarian);
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Indomitable Might"));
        }

        [Test]
        public void BarbariansGetPrimalChampionAtLevel20()
        {
            _barbarian.Abilities.Strength.Score = 20;
            _barbarian.Abilities.Constitution.Score = 20;
            TestingUtility.LevelTo(ref _barbarian, 20, Class.Barbarian);

            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Primal Champion"));
           

            Assert.AreEqual(4, _barbarian.Abilities.Strength.ClassBonus);
            Assert.AreEqual(24, _barbarian.Abilities.Strength.MaxScore);
            Assert.AreEqual(24, _barbarian.Abilities.Strength.Score);

            Assert.AreEqual(4, _barbarian.Abilities.Constitution.ClassBonus);
            Assert.AreEqual(24, _barbarian.Abilities.Constitution.MaxScore);
            Assert.AreEqual(24, _barbarian.Abilities.Constitution.Score);
        }

        [Test]
        public void BarbariansGetAPathAtLevel3()
        {
            Assert.AreEqual(0, _barbarian.ChosenPaths.Count);
            TestingUtility.LevelTo(ref _barbarian, 3, Class.Barbarian);
            Assert.AreEqual(2, _barbarian.AvailablePaths.Count);
            Assert.IsTrue(_barbarian.AvailablePaths.Contains(Path.PathOfTheBerserker));
            Assert.IsTrue(_barbarian.AvailablePaths.Contains(Path.PathOfTheTotemWarrior));
        }

        [Test]
        public void BerserkerBarbariansGetFrenzyAtLevel3()
        {
            TestingUtility.LevelTo(ref _barbarian, 3, Class.Barbarian);
            _barbarian.ChosePath(Path.PathOfTheBerserker);
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Frenzy"));
        }

        [Test]
        public void BerserkerBarbariansGetMindLessRageAtLevel6()
        {
            TestingUtility.LevelTo(ref _barbarian, 6, Class.Barbarian);
            _barbarian.ChosePath(Path.PathOfTheBerserker);
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Mindless Rage"));
        }

        [Test]
        public void BerserkerBarbariansGetIntimidatingPresenceAtLevel10()
        {
            TestingUtility.LevelTo(ref _barbarian, 10, Class.Barbarian);
            _barbarian.ChosePath(Path.PathOfTheBerserker);
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Intimidating Presence"));
        }

        [Test]
        public void BerserkerBarbariansGetRetaliationAtLevel14()
        {
            TestingUtility.LevelTo(ref _barbarian, 14, Class.Barbarian);
            _barbarian.ChosePath(Path.PathOfTheBerserker);
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Retaliation"));
        }

        [Test]
        public void TotemBarbariansGetSpiritSeekerAndTotemSpiritAtLevel3()
        {
            TestingUtility.LevelTo(ref _barbarian, 3, Class.Barbarian);
            _barbarian.ChosePath(Path.PathOfTheTotemWarrior);
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Spirit Seeker"));
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Totem Spirit"));
        }

        [Test]
        public void TotemBarbariansGetAspectOfTheBeastAtLevel6()
        {
            TestingUtility.LevelTo(ref _barbarian, 6, Class.Barbarian);
            _barbarian.ChosePath(Path.PathOfTheTotemWarrior);
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Aspect of the Beast"));
        }

        [Test]
        public void TotemBarbariansGetSpiritWalkerAtLevel10()
        {
            TestingUtility.LevelTo(ref _barbarian, 10, Class.Barbarian);
            _barbarian.ChosePath(Path.PathOfTheTotemWarrior);
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Spirit Walker"));
        }

        [Test]
        public void TotemBarbariansGetTotemicAttunementAtLevel14()
        {
            TestingUtility.LevelTo(ref _barbarian, 14, Class.Barbarian);
            _barbarian.ChosePath(Path.PathOfTheTotemWarrior);
            Assert.IsTrue(_barbarian.AllFeatures.Any(af => af.Name == "Totemic Attunement"));
        }
    }
}
