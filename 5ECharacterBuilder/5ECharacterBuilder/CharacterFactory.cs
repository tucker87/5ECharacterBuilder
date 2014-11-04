using System;
using System.Linq;
using System.Reflection;
using _5ECharacterBuilder.CharacterBackgrounds;
using _5ECharacterBuilder.CharacterClasses;
using _5ECharacterBuilder.CharacterRaces;

namespace _5ECharacterBuilder
{
    public class CharacterFactory
    {
        public static Character BuildACharacter(AvailableRaces selectedRace, AvailableClasses selectedClass, AvailableBackgrounds selectedBackground)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var characterClass = currentAssembly.GetTypes()
                .First(t => typeof (CharacterClass).IsAssignableFrom(t) && t.Name == selectedClass.ToString());
            var characterRace = currentAssembly.GetTypes()
                .First(t => typeof(CharacterRace).IsAssignableFrom(t) && t.Name == selectedRace.ToString());
            var characterBackground = currentAssembly.GetTypes()
                .First(t => typeof(CharacterBackground).IsAssignableFrom(t) && t.Name == selectedBackground.ToString());

            var character = new CharacterBase();
            var monk = new Monk(character);
            var fighter = new Fighter(character);

            

            var newCharacter = (Character)Activator.CreateInstance(characterRace,
                BindingFlags.OptionalParamBinding,
                null, new object[] { new CharacterBase() }, null);

            newCharacter = (Character)Activator.CreateInstance(characterClass, 
                BindingFlags.OptionalParamBinding,
                null, new object[] { newCharacter }, null);

            newCharacter = (Character)Activator.CreateInstance(characterBackground,
                BindingFlags.OptionalParamBinding,
                null, new object[] { newCharacter }, null);

            return newCharacter;
        }


        public static Character BuildACharacter(AvailableRaces selectedRace, AvailableClasses selectedClass, AvailableBackgrounds selectedBackground, int level)
        {
            var character = BuildACharacter(selectedRace, selectedClass, selectedBackground);
            for (var l = 1; l < level; l++)
                character = AddClass(character, selectedClass);

            return character;
        }

        private static Character AddClass(Character character, AvailableClasses characterClass)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var characterClasses = currentAssembly.GetTypes()
                .First(t => typeof(CharacterClass).IsAssignableFrom(t) && t.Name == characterClass.ToString());

            character = (Character)Activator.CreateInstance(characterClasses,
                BindingFlags.OptionalParamBinding,
                null, new object[] { character }, null);

            return character;
        }
    }
}