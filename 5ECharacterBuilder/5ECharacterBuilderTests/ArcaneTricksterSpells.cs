using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    [TestFixture]
    public class ArcaneTricksterSpells
    {
        private ICharacter _character;

        [SetUp]
        public void SetUp()
        {
            _character = CharacterFactory.BuildACharacter(AvailableRaces.Human, AvailableClasses.Rogue,
                AvailableBackgrounds.Criminal);
        }

        [Test, TestCaseSource(typeof(CharacterData), nameof(CharacterData.Levels))]
        public int ArcaneTricksterSpellCounts(int level)
        {
            return GetArcaneTricksterMaxSpells(level);
        }

        public static class CharacterData
        {
            public static IEnumerable Levels
            {
                get
                {
                    yield return new TestCaseData(1).Returns(0);
                    yield return new TestCaseData(2).Returns(0);
                    yield return new TestCaseData(3).Returns(3);
                    yield return new TestCaseData(4).Returns(4);
                    yield return new TestCaseData(5).Returns(4);
                    yield return new TestCaseData(6).Returns(4);
                    yield return new TestCaseData(7).Returns(5);
                    yield return new TestCaseData(8).Returns(6);
                    yield return new TestCaseData(9).Returns(6);
                    yield return new TestCaseData(10).Returns(7);
                    yield return new TestCaseData(11).Returns(8);
                    yield return new TestCaseData(12).Returns(8);
                    yield return new TestCaseData(13).Returns(9);
                    yield return new TestCaseData(14).Returns(10);
                    yield return new TestCaseData(15).Returns(10);
                    yield return new TestCaseData(16).Returns(11);
                    yield return new TestCaseData(17).Returns(11);
                    yield return new TestCaseData(18).Returns(11);
                    yield return new TestCaseData(19).Returns(12);
                    yield return new TestCaseData(20).Returns(13);
                }
            }
        }

        private static int GetArcaneTricksterMaxSpells(int i)
        {
            switch (i)
            {
                case 3:
                    return 3;
                case 4:
                case 5:
                case 6:
                    return 4;
                case 7:
                    return 5;
                case 8:
                case 9:
                    return 6;
                case 10:
                    return 7;
                case 11:
                case 12:
                    return 8;
                case 13:
                    return 9;
                case 14:
                case 15:
                    return 10;
                case 16:
                case 17:
                case 18:
                    return 11;
                case 19:
                    return 12;
                case 20:
                    return 13;
                default:
                    return 0;
            }
        }
    }
}