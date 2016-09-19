using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebGrease.Css.Extensions;
using _5ECharacterBuilder.CharacterBackgrounds;
using _5ECharacterBuilder.CharacterClasses;
using _5ECharacterBuilder.CharacterRaces;
using _5EDatabase;

namespace _5ECharacterBuilder
{
    public class CharacterFactory
    {
        public static ICharacter BuildACharacter(Race selectedRace, Class selectedClass, Background selectedBackground)
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


        public static ICharacter BuildACharacter(Race selectedRace, Class selectedClass, Background selectedBackground, int level)
        {
            var character = BuildACharacter(selectedRace, selectedClass, selectedBackground);
            for (var l = 1; l < level; l++)
                character = LevelUp(ref character, selectedClass);

            return character;
        }

        public static ICharacter BuildACharacter(Race selectedRace, List<Class> selectedClass, Background selectedBackground)
        {
            var character = BuildACharacter(selectedRace, selectedClass.First(), selectedBackground);

            selectedClass.Where(availableClass => availableClass != selectedClass.First()).ForEach(sClass => LevelUp(ref character, sClass));

            return character;
        }

        public static ICharacter LevelUp(ref ICharacter character, Class characterClass)
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