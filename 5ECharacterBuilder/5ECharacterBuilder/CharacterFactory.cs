using System;
using System.Linq;
using System.Reflection;
using _5ECharacterBuilder.CharacterClasses;

namespace _5ECharacterBuilder
{
    class CharacterFactory
    {
        public ICharacter BuildACharacter(AvailableRaces selectedRace, AvailableClasses selectedClass)
        {
            AvailableRaces? nullableRace = selectedRace;
            AvailableClasses? nullableClass = selectedClass;
            return BuildACharacter(nullableRace, nullableClass);
        }

        public static ICharacter BuildACharacter(AvailableRaces? selectedRace, AvailableClasses? selectedClass)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var characterClasses = currentAssembly.GetTypes()
                .First(t => typeof (CharacterClass).IsAssignableFrom(t) && t.Name == selectedClass.ToString());
            var characterRaces = currentAssembly.GetTypes()
                .First(t => typeof(CharacterRace).IsAssignableFrom(t) && t.Name == selectedRace.ToString());
            
            var newCharacter = (ICharacter)Activator.CreateInstance(characterRaces,
                BindingFlags.OptionalParamBinding,
                null, new[] { new CharacterBase() }, null);

            newCharacter = (ICharacter)Activator.CreateInstance(characterClasses, 
                BindingFlags.OptionalParamBinding,
                null, new[] { newCharacter }, null);

            return newCharacter;
        }

        public ICharacter AddClass(ICharacter character, AvailableClasses characterClass)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var characterClasses = currentAssembly.GetTypes()
                .First(t => typeof(CharacterClass).IsAssignableFrom(t) && t.Name == characterClass.ToString());

            character = (ICharacter)Activator.CreateInstance(characterClasses,
                BindingFlags.OptionalParamBinding,
                null, new[] { character }, null);

            return character;
        }
    }
}