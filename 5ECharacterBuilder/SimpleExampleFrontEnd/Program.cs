using System;
using System.Collections.Generic;
using _5ECharacterBuilder;
using static System.Console;

namespace SimpleExampleFrontEnd
{
    internal class Program
    {
        private static void Main()
        {
            var x = "of";
            var y = "$";
            var test = $"This is a test {x} {y}";

            WriteLine("Choose a Race: ");
            var characterRace = Generics.AskFor<AvailableRaces>();
            WriteLine("Choose a Class: ");
            var characterClass = Generics.AskFor<AvailableClasses>();
            WriteLine("Choose a Background: ");
            var characterBackground = Generics.AskFor<AvailableBackgrounds>();


            var character = CreateCharacter(characterRace, characterClass, characterBackground);
            
            var exit = false;
            while (!exit)
            {
                Clear();
                WriteCharacter(character);
                
                var selectedAction = Generics.AskFor<Menu.MenuOptions>();
                var result = Menu.RunSelectedAction(selectedAction, ref character);
                if (result == -1)
                    exit = true;
                
            }
        }

        private static ICharacter CreateCharacter(AvailableRaces characterRace, AvailableClasses characterClass,
            AvailableBackgrounds characterBackground)
        {
            var character = CharacterFactory.BuildACharacter(characterRace, characterClass, characterBackground);
            character.SetAttributes(new CharacterAbilities(11, 11, 11, 11, 11, 11));
            
            return character;
        }

        private static void WriteCharacter(ICharacter character)
        {
            WriteLine(character.Race + "," + character.ClassesString + ", " + character.Background);
            Write("Hit Dice: {0}", character.HitDice);

            WriteLine();
            WriteLine("Hit Points: " + character.MaxHp);

            WriteLine();
            Write("Armor Proficiencies:");
            foreach (var armor in character.ArmorProficiencies)
                Write(" " + armor);
            WriteLine();

            WriteLine("Equipped Armor: " + character.EquippedArmor.Name);


            WriteLine();
            WriteLine("Has Shield: " + (character.HasShield ? "Yes" : "No"));

            WriteLine("Armor Class: " + character.ArmorClass);
            WriteLine("Initiative: " + character.Initiative);
            WriteLine("Size: " + character.Size);
            WriteLine("Speed: " + character.Speed);
            WriteLine();
            WriteLine("Strength: {0} Modifier: {1}", character.Abilities.Strength.Score, character.Abilities.Strength.Modifier);
            WriteLine("Dexterity: {0} Modifier: {1}", character.Abilities.Dexterity.Score, character.Abilities.Dexterity.Modifier);
            WriteLine("Constitution: {0} Modifier: {1}", character.Abilities.Constitution.Score, character.Abilities.Constitution.Modifier);
            WriteLine("Intelligence: {0} Modifier: {1}", character.Abilities.Intelligence.Score, character.Abilities.Intelligence.Modifier);
            WriteLine("Wisdom: {0} Modifier: {1}", character.Abilities.Wisdom.Score, character.Abilities.Wisdom.Modifier);
            WriteLine("Charisma: {0} Modifier: {1}", character.Abilities.Charisma.Score, character.Abilities.Charisma.Modifier);
            WriteLine();
            WriteLine("Feats: TODO"); //TODO
            WriteLine("AllFeatures: ");
            foreach (var feature in character.Features.AllFeatures)
                WriteLine(" " + feature.Key);


            Write("Weapon Proficiencies:");
            foreach (var weapon in character.WeaponProficiencies)
                Write(" " + weapon);

            WriteLine();
            Write("Saving Throw Proficiencies:");
            foreach (var savingThrowProficiency in character.SavingThrows)
                Write(" " + savingThrowProficiency);


            WriteLine();
            WriteLine();
            Write("Available Skill Proficiencies:");
            foreach (var skill in character.Skills.Available)
                Write(" " + skill);
            
            WriteLine();
            WriteLine("Skill Scores:");
            WriteLine("Skill\tScore\tChosen\tExpertise");
            foreach (var skill in character.Skills.AllSkills)
            {
                WriteLine("{0} | {1} | {2} | {3}", skill, character.SkillBonus(skill), character.Skills.Chosen.Contains(skill) ? "*" : "", character.Skills.Expertise.Contains(skill) ? "*" : "");
            }

            WriteLine();

            Write("Tool Proficiencies:");
            foreach (var tool in character.Tools.Chosen)
                Write(" " + tool);

            WriteLine();
            WriteLine();

            Write("Instrument Proficiencies:");
            foreach (var instrument in character.Instruments.Chosen)
                Write(" " + instrument);

            WriteLine();


            WriteLine();
            Write("Languagues:");
            foreach (var language in character.Languages.Chosen)
            {
                Write(" " + language);
            }

            WriteLine();
            WriteLine("Gold: " + character.Currency.Gold);
            WriteLine("Silver: " + character.Currency.Silver);
            WriteLine("Copper: " + character.Currency.Copper);
            
            WriteLine();
        }
    }

    public class Menu
    {
        private static ICharacter _character;
        private static int _result;

        public Dictionary<MenuOptions, Action> SystemDetailsProcessDictionary => new Dictionary<MenuOptions, Action>
        {
            {MenuOptions.EquipArmor, EquipArmor},
            {MenuOptions.ToggleShield, ToggleShield},
            {MenuOptions.LearnSkill, LearnSkill},
            {MenuOptions.LearnTool, LearnTool},
            {MenuOptions.LearnInstrument, LearnInstrument},
            {MenuOptions.LevelUp, LevelUp},
            {MenuOptions.ShowFeatures, ShowFeatures},
            {MenuOptions.Exit, Exit}
        };


        //character.AddToolProfs(new All<AvailableTool> { AvailableTool.AlchemistsSupplies });
        //character.AddInstrumentProfs(new All<AvailableInstrument> { AvailableInstrument.Lute });
        //character.AddLanguages(new All<AvailableLanguages> { AvailableLanguages.Common });

        private static void EquipArmor()
        {
            var chosenArmor = Generics.AskFor<AvailableArmor>();
            _character.EquipArmor(chosenArmor);
        }

        private static void ToggleShield()
        {
            _character.ToggleShield();
        }

        private static void LearnSkill()
        {
            var chosenSkill = Generics.AskFor<AvailableSkill>();
            _character.ChooseSkill(chosenSkill);
        }

        private static void LearnTool()
        {
            var chosenTool = Generics.AskFor<AvailableTool>();
            _character.LearnTool(chosenTool);
        }

        private static void LearnInstrument()
        {
            var chosenInstrument = Generics.AskFor<AvailableInstrument>();
            _character.LearnInstrument(chosenInstrument);
        }

        private static void LevelUp()
        {
            var chosenClass = Generics.AskFor<AvailableClasses>();
            CharacterFactory.LevelUp(_character, chosenClass);
        }

        private static void ShowFeatures()
        {
            Clear();
            foreach (var feature in _character.Features.AllFeatures)
                WriteLine(feature.Key + " - " + feature.Value + "\r\n");
             
            WriteLine("Press any key to return to Character");
            ReadLine();

        }

        private static void Exit()
        {
            _result = -1;
        }

        public enum MenuOptions
        {
            EquipArmor,
            ToggleShield,
            LearnSkill,
            LearnTool,
            LearnInstrument,
            LevelUp,
            ShowFeatures,
            Exit
            
        }

        public static int RunSelectedAction(MenuOptions option, ref ICharacter character)
        {
            _character = character;
            new Menu().SystemDetailsProcessDictionary[option]();
            character = _character;
            return _result;
        }
    }

    public class Generics
    {
        public static T AskFor<T>() where T : struct, IConvertible
        {
            var i = 1;
            foreach (var race in (T[])Enum.GetValues(typeof(T)))
                WriteLine("{0}: {1}", i++, race);

            var chosen = Convert.ToInt32(ReadLine()) - 1;

            var characterRace = (T)(object)chosen;
            Clear();
            return characterRace;
        }
    }
}
