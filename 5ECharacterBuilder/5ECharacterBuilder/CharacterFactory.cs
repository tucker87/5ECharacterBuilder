using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _5ECharacterBuilder.CharacterBackgrounds;
using _5ECharacterBuilder.CharacterClasses;
using _5ECharacterBuilder.CharacterRaces;

namespace _5ECharacterBuilder
{
    public class CharacterFactory
    {
        public static ICharacter BuildACharacter(AvailableRaces selectedRace, AvailableClasses selectedClass, AvailableBackgrounds selectedBackground)
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

        public static ICharacter LevelUp(ICharacter character, AvailableClasses characterClass)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var characterClasses = currentAssembly.GetTypes()
                .First(t => typeof(CharacterClass).IsAssignableFrom(t) && t.Name == characterClass.ToString());

            character = (ICharacter)Activator.CreateInstance(characterClasses,
                BindingFlags.OptionalParamBinding,
                null, new object[] { character }, null);

            return character;
        }

        internal static List<string> GetRuleIssues(ICharacter character)
        {
            var currentIssues = new List<string>();

            if (character.EquippedArmor.Name != AvailableArmor.Cloth.ToString())
                if (!character.ArmorProficiencies.Contains((AvailableArmor)Enum.Parse(typeof(AvailableArmor), character.EquippedArmor.Name)))
                    currentIssues.Add(String.Format("Character is not proficient with {0} Armor. Penalties will apply.", character.EquippedArmor.Name));

            if (character.HasShield && !character.ArmorProficiencies.Contains(AvailableArmor.Shield))
                currentIssues.Add("Character is not proficient with Shields. Penalties will apply.");

            if (character.ChosenLanguages.Count > character.LanguageCount)
                currentIssues.Add(string.Format("Character can only choose {0} Language", character.LanguageCount));

            var classSkills = character.AvailableSkills.ToList();

            currentIssues.AddRange(from skill in character.TrainedSkills
                                   where !classSkills.Contains(skill)
                                   select skill + " is not a skill available to this character.");

            if (character.TrainedSkills.ToList().Count > character.ClassSkillCount)
                currentIssues.Add(string.Format("Character can only choose {0} skills from their list.", character.ClassSkillCount));

            return currentIssues;
        } 
    }
}