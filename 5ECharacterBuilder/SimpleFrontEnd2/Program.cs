using System;
using System.Collections.Generic;
using _5ECharacterBuilder2;

namespace SimpleFrontEnd2
{
    class Program
    {
        static void Main()
        {
            var character = new Character(AvailableRaces.Human, AvailableClasses.Monk);
            
            Console.WriteLine(character.Class.Name);
            Console.WriteLine(character.Race.Name);
            foreach (var issue in character.GetAllIssues())
                Console.WriteLine(issue);
            
            foreach (var hitDice in character.HitDice)
                Console.WriteLine(hitDice);

            Console.WriteLine();
            Console.WriteLine("Level: " + character.Level + "XP: " + character.Experience);

            character.Experience = 750;
            Console.WriteLine("Level: " + character.Level + " XP: " + character.Experience);

            character.Level = 3;
            Console.WriteLine("Level: " + character.Level + " XP: " + character.Experience);

            Console.WriteLine();
            var attr = new List<string> {"Strength", "Dexterity", "Constitution", "Intelligence", "Wisdom", "Charisma"};
            foreach (var attribute in attr)
            {
                Console.Write(attribute);
                var property = (_5ECharacterBuilder2.Attribute)character.GetType().GetProperty(attribute).GetValue(character);
                Console.WriteLine(" Score: " + property.Score + " Modifier: " + property.Modifier);
            }

            Console.WriteLine();
            foreach (var attribute in attr)
            {
                Console.Write(attribute);
                var property = (_5ECharacterBuilder2.Attribute)character.GetType().GetProperty(attribute).GetValue(character);
                property.Score = 12;
                Console.WriteLine(" Score: " + property.Score + " Modifier: " + property.Modifier);
            }

            Console.ReadLine();
        }
    }
}
