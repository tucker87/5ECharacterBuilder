using _5ECharacterBuilder;
using _5EDatabase;

namespace _5ECharacterBuilderTests
{
    class TestingUtility
    {
        public static void LevelTo(ref ICharacter character, int targetLevel, Class cclass)
        {
            for (var i = character.Level; i < targetLevel; i++)
                CharacterFactory.LevelUp(ref character, cclass);
        }
    }
}
