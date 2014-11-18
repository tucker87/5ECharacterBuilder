using _5ECharacterBuilder;

namespace _5ECharacterBuilderTests
{
    class TestingUtility
    {
        public static void LevelTo(ICharacter character, int targetLevel, AvailableClasses cclass)
        {
            for (var i = character.Level; i < targetLevel; i++)
                CharacterFactory.LevelUp(character, cclass);
        }
    }
}
