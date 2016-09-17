using NUnit.Framework;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestFixture]
    public class ArmoryTests
    {
        private ICharacter _character;
        [SetUp]
        public void SetUp()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Fighter, AvailableBackgrounds.Criminal);
            
        }

        [Test]
        public void ClothArmorGivesFullDexBonus()
        {
            _character.SetAttributes(new CharacterAbilities(new CharacterAbilityScores(dexterity:20)));
            _character.EquipArmor(AvailableArmor.Cloth);
            Assert.AreEqual(15, _character.ArmorClass);
        }

        [Test]
        public void HideArmorGivesMax2DexBonus()
        {
            _character.SetAttributes(new CharacterAbilities(new CharacterAbilityScores(dexterity: 20)));
            _character.EquipArmor(AvailableArmor.Hide);
            Assert.AreEqual(14, _character.ArmorClass);
        }

        [Test]
        public void PlateArmorGiveNoDexBonus()
        {
            _character.SetAttributes(new CharacterAbilities(new CharacterAbilityScores(dexterity: 20)));
            _character.EquipArmor(AvailableArmor.Plate);
            Assert.AreEqual(18, _character.ArmorClass);
        }

        [Test]
        public void ShieldsGive2AcOnTopOfDex()
        {
            _character.SetAttributes(new CharacterAbilities(new CharacterAbilityScores(dexterity: 20)));
            _character.ToggleShield();
            Assert.AreEqual(17, _character.ArmorClass);
        }

        [Test]
        public void ShieldsGive2AcOnTopOfArmor()
        {
            _character.SetAttributes(new CharacterAbilities(new CharacterAbilityScores(dexterity: 20)));
            _character.EquipArmor(AvailableArmor.Plate);
            _character.ToggleShield();
            Assert.AreEqual(20, _character.ArmorClass);
        }
    }
}
