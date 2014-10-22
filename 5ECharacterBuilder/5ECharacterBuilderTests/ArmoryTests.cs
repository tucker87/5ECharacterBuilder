using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    public class ArmoryTests
    {
        private Character _character;
        private Armory _armory;
        [TestInitialize]
        public void Setup()
        {
            _character = new Character(AvailableRaces.Human, AvailableClasses.Fighter);
            _armory = new Armory();
        }

        [TestMethod]
        public void ClothArmorGivesFullDexBonus()
        {
            _character.Attributes = new CharacterAttributes(new CharacterAttributeScores(dexterity:20));
            _character.AddArmor(AvailableArmor.Cloth);
            Assert.AreEqual(15, _character.ArmorClass);
        }

        [TestMethod]
        public void PlateArmorGiveNoDexBonus()
        {
            _character.Attributes = new CharacterAttributes(new CharacterAttributeScores(dexterity: 20));
            _character.AddArmor(AvailableArmor.Plate);
            Assert.AreEqual(18, _character.ArmorClass);
        }

        [TestMethod]
        public void ShieldsGive2AcOnTopOfDex()
        {
            _character.Attributes = new CharacterAttributes(new CharacterAttributeScores(dexterity: 20));
            _character.EquipShield();
            Assert.AreEqual(17, _character.ArmorClass);
        }

        [TestMethod]
        public void ShieldsGive2AcOnTopOfArmor()
        {
            _character.Attributes = new CharacterAttributes(new CharacterAttributeScores(dexterity: 20));
            _character.AddArmor(AvailableArmor.Plate);
            _character.EquipShield();
            Assert.AreEqual(20, _character.ArmorClass);
        }
    }
}
