using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    public class ArmoryTests
    {
        private ICharacter _character;
        private Armory _armory;
        [TestInitialize]
        public void Setup()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Fighter, AvailableBackgrounds.Criminal);
            _armory = new Armory();
        }

        [TestMethod]
        public void ClothArmorGivesFullDexBonus()
        {
            _character.Attributes = new CharacterAttributes(new CharacterAttributeScores(dexterity:20));
            _character.EquipArmor(AvailableArmor.Cloth);
            Assert.AreEqual(15, _character.ArmorClass);
        }

        [TestMethod]
        public void PlateArmorGiveNoDexBonus()
        {
            _character.Attributes = new CharacterAttributes(new CharacterAttributeScores(dexterity: 20));
            _character.EquipArmor(AvailableArmor.Plate);
            Assert.AreEqual(18, _character.ArmorClass);
        }

        [TestMethod]
        public void ShieldsGive2AcOnTopOfDex()
        {
            _character.Attributes = new CharacterAttributes(new CharacterAttributeScores(dexterity: 20));
            _character.HasShield = true;
            Assert.AreEqual(17, _character.ArmorClass);
        }

        [TestMethod]
        public void ShieldsGive2AcOnTopOfArmor()
        {
            _character.Attributes = new CharacterAttributes(new CharacterAttributeScores(dexterity: 20));
            _character.EquipArmor(AvailableArmor.Plate);
            _character.HasShield = true;
            Assert.AreEqual(20, _character.ArmorClass);
        }
    }
}
