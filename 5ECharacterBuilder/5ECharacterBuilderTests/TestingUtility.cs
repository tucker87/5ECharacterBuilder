using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    class TestingUtility
    {
        public static void LevelTo(ICharacter monk, int target, AvailableClasses cclass)
        {
            for (var i = monk.Level; i < target; i++)
                CharacterFactory.LevelUp(monk, cclass);
        }
    }
}
