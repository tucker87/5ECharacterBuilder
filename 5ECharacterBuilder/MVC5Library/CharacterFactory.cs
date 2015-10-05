using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MVC5Library;
using _5ECharacterBuilder.CharacterBackgrounds;
using _5ECharacterBuilder.CharacterClasses;
using _5ECharacterBuilder.CharacterRaces;

namespace _5ECharacterBuilder
{
    public class CharacterFactory
    {
        public static ICharacter BuildACharacter(AvailableRaces selectedRace, AvailableClasses selectedClass, AvailableBackgrounds selectedBackground)
        {
            GenerateDictionaries();

            var currentAssembly = Assembly.GetExecutingAssembly();
            var characterClass = currentAssembly.GetTypes()
                .First(t => typeof (CharacterClass).IsAssignableFrom(t) && t.Name == selectedClass.ToString());
            var characterRace = currentAssembly.GetTypes()
                .First(t => typeof(CharacterRace).IsAssignableFrom(t) && t.Name == selectedRace.ToString());
            var characterBackground = currentAssembly.GetTypes()
                .First(t => typeof(CharacterBackground).IsAssignableFrom(t) && t.Name == selectedBackground.ToString());
            
            var newCharacter = (ICharacter)Activator.CreateInstance(characterRace,
                BindingFlags.OptionalParamBinding,
                null, new object[] { new CharacterBase() }, null);

            newCharacter = (ICharacter)Activator.CreateInstance(characterClass, 
                BindingFlags.OptionalParamBinding,
                null, new object[] { newCharacter }, null);

            newCharacter = (ICharacter)Activator.CreateInstance(characterBackground,
                BindingFlags.OptionalParamBinding,
                null, new object[] { newCharacter }, null);

            return newCharacter;
        }


        public static ICharacter BuildACharacter(AvailableRaces selectedRace, AvailableClasses selectedClass, AvailableBackgrounds selectedBackground, int level)
        {
            var character = BuildACharacter(selectedRace, selectedClass, selectedBackground);
            for (var l = 1; l < level; l++)
                character = LevelUp(character, selectedClass);

            return character;
        }

        public static ICharacter BuildACharacter(AvailableRaces selectedRace, List<AvailableClasses> selectedClass, AvailableBackgrounds selectedBackground)
        {
            var character = BuildACharacter(selectedRace, selectedClass.First(), selectedBackground);

            return selectedClass.Where(availableClass => availableClass != selectedClass.First()).Aggregate(character, LevelUp);
        }

        public static ICharacter LevelUp(ICharacter character, AvailableClasses characterClass)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var characterClasses = currentAssembly.GetTypes()
                .First(
                    t => typeof (CharacterClass).IsAssignableFrom(t) && t.Name == characterClass.ToString());
            try
            {
                character = (ICharacter) Activator.CreateInstance(characterClasses,
                    BindingFlags.OptionalParamBinding,
                    null, new object[] {character}, null);
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException.GetType() == typeof (RequirementsExpection))
                    throw ex.InnerException;
            }
            return character;
        }

        public static void LevelDown(ICharacter character)
        {
            character.LevelDown();
        }

        private static void GenerateDictionaries()
        {
            
        }
    }
}