using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    public class CreateCharacterTests
    {
        [TestMethod]
        public void CharactersCanHaveNames()
        {
            var character = new Character();
            Assert.AreEqual("", character.Name);
            character.Name = "John";
            Assert.AreEqual("John", character.Name);
            character = new Character(hitDice:"d4", name:"John");
            Assert.AreEqual("John", character.Name);
        }

        [TestMethod]
        public void CharactersMustHaveHitDice()
        {
            var character = new Character(hitDice: "d8");
        }
    }

    public class Character
    {
        public Character()
        {
            Name = "";
        }
        public Character(string hitDice, string name = "")
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
