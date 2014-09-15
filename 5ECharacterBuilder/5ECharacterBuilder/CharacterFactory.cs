using System;
using System.Linq;
using System.Reflection;
using _5ECharacterBuilder.CharacterClasses;

namespace _5ECharacterBuilder
{
    public class CharacterFactory
    {
        public static ICharacter BuildACharacter(AvailableClasses selectedClass, AvailableTools tool)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var characterClasses = currentAssembly.GetTypes()
                .First(t => typeof (CharacterClass).IsAssignableFrom(t) && t.Name == selectedClass.ToString());

            var newCharacter = (CharacterClass)Activator.CreateInstance(characterClasses, 
                BindingFlags.OptionalParamBinding,
                null, new[] {new CharacterBase(), Type.Missing, tool, Type.Missing}, null);
            return newCharacter;
        }
    }
}