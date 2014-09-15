using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;
using _5ECharacterBuilder.CharacterClasses;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    class CharacterFactoryTests
    {
        [TestMethod]
        public void FactoryCanReturnAMonk()
        {
            var character = CharacterFactory.BuildACharacter(AvailableClasses.Monk, AvailableTools.AlchemistsSupplies);
            Assert.IsInstanceOfType(character, typeof(Monk));
        }

        [TestMethod]
        public void FactoryCanReturnAFighter()
        {
            var character = CharacterFactory.BuildACharacter(AvailableClasses.Fighter, AvailableTools.AlchemistsSupplies );
            Assert.IsInstanceOfType(character, typeof(Fighter));
        }
    }
}
