using NUnit.Framework;
using _5ECharacterBuilder;

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
                _character = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Fighter, AvailableBackgrounds.Criminal);
            }

            [Test]
            public void CriminalsBackSetBackgroundName()
            {
                Assert.AreEqual("Criminal", _character.Background);
            }

            [Test]
            public void CriminalsHaveDeceptionAndStealthSkillProfsAvailableAndTrained()
            {
                Assert.IsTrue(_character.Skills.Chosen.Contains(AvailableSkill.Deception));
                Assert.IsTrue(_character.Skills.Chosen.Contains(AvailableSkill.Stealth));
            }

            [Test]
            public void CriminalsHaveThievesToolsProf()
            {
                Assert.IsTrue(_character.Tools.Available.Contains(AvailableTool.ThievesTools));
                Assert.IsTrue(_character.Tools.Chosen.Contains(AvailableTool.ThievesTools));
            }
        }
    }
}