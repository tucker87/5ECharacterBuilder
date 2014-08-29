using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    public class CharacterRaceTests
    {
        private static CharacterBase _characterBase;
        private static CharacterAttributeScores _characterAttributeScroreList;

        [TestInitialize]
        public static void Setup()
        {
            _characterAttributeScroreList = new CharacterAttributeScores(10,10,10,10,10,10);
            _characterBase = new CharacterBase(name: "John", hitDice: new List<int> {8},
                attributeScores: _characterAttributeScroreList);
        }

        //[TestMethod]
        //public void HumansMaxHpIs3More()
        //{
        //    var human = new Human(_characterBase);
        //    Assert.AreEqual(8, human.MaxHp);
        //}
    }
}
