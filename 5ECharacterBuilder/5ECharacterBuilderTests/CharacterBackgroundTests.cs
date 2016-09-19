using NUnit.Framework;
using _5ECharacterBuilder;
using _5EDatabase;

namespace _5ECharacterBuilderTests
{
    [TestFixture]
    public class CharacterBackgroundTests
    {
        [TestFixture]
        public class CriminalTests
        {
            private static ICharacter _character;

            [SetUp]
            public void SetUp()
            {
                _character = CharacterFactory.BuildACharacter(Race.Human, Class.Fighter, Background.Criminal);
            }

            [Test]
            public void CriminalsBackSetBackgroundName()
            {
                Assert.AreEqual("Criminal", _character.Background);
            }

            [Test]
            public void CriminalsHaveDeceptionAndStealthSkillProfsAvailableAndTrained()
            {
                Assert.IsTrue(_character.Skills.Chosen.Contains(Skill.Deception));
                Assert.IsTrue(_character.Skills.Chosen.Contains(Skill.Stealth));
            }

            [Test]
            public void CriminalsHaveThievesToolsProf()
            {
                Assert.IsTrue(_character.Tools.Available.Contains(Tool.ThievesTools));
                Assert.IsTrue(_character.Tools.Chosen.Contains(Tool.ThievesTools));
            }
        }
    }
}