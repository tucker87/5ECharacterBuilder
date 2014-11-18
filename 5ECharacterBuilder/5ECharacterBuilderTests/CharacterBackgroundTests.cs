using Microsoft.VisualStudio.TestTools.UnitTesting;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestClass]
    public class CharacterBackgroundTests
    {
        [TestClass]
        public class CriminalTests
        {
            private static ICharacter _character;

            [TestInitialize]
            public static void Setup()
            {
                _character = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Fighter, AvailableBackgrounds.Criminal);
            }

            [TestMethod]
            public void CriminalsBackSetBackgroundName()
            {
                Assert.AreEqual("Criminal", _character.Background);
            }

            [TestMethod]
            public void CriminalsHaveDeceptionAndStealthSkillProfsAvailableAndTrained()
            {
                Assert.IsTrue(_character.Skills.Available.Contains(AvailableSkill.Deception));
                Assert.IsTrue(_character.Skills.Chosen.Contains(AvailableSkill.Deception));
                Assert.IsTrue(_character.Skills.Available.Contains(AvailableSkill.Stealth));
                Assert.IsTrue(_character.Skills.Chosen.Contains(AvailableSkill.Stealth));
            }

            [TestMethod]
            public void CriminalsHaveThievesToolsProf()
            {
                Assert.IsTrue(_character.Tools.Available.Contains(AvailableTool.ThievesTools));
                Assert.IsTrue(_character.Tools.Chosen.Contains(AvailableTool.ThievesTools));
            }
        }
    }
}