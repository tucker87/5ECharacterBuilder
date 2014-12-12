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
        public void BarbariansGetRageAtLevel1()
        {
            //TestingUtility.LevelTo(_barbarian, 2, AvailableClasses.Barbarian);
            Assert.IsTrue(_barbarian.Features.AllFeatures.ContainsKey("Rage"));
        }
    }
}
