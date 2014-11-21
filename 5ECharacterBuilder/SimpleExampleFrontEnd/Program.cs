using System;
using System.Collections.Generic;
using _5ECharacterBuilder;

namespace SimpleExampleFrontEnd
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Choose a Race: ");
            var characterRace = Generics.AskFor<AvailableRaces>();
            Console.WriteLine("Choose a Class: ");
            var characterClass = Generics.AskFor<AvailableClasses>();
            Console.WriteLine("Choose a Background: ");
            var characterBackground = Generics.AskFor<AvailableBackgrounds>();


            var character = CreateCharacter(characterRace, characterClass, characterBackground);
            
            var exit = false;
            while (!exit)
            {
                Console.Clear();
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
            Console.WriteLine(character.Race + "," + character.ClassesString + ", " + character.Background);
            Console.Write("Hit Dice: {0}", character.HitDice);

            Console.WriteLine();
            Console.WriteLine("Hit Points: " + character.MaxHp);

            Console.WriteLine();
            Console.Write("Armor Proficiencies:");
            foreach (var armor in character.ArmorProficiencies)
                Console.Write(" " + armor);
            Console.WriteLine();

            Console.WriteLine("Equipped Armor: " + character.EquippedArmor.Name);


            Console.WriteLine();
            Console.WriteLine("Has Shield: " + (character.HasShield ? "Yes" : "No"));

            Console.WriteLine("Armor Class: " + character.ArmorClass);
            Console.WriteLine("Initiative: " + character.Initiative);
            Console.WriteLine("Size: " + character.Size);
            Console.WriteLine("Speed: " + character.Speed);
            Console.WriteLine();
            Console.WriteLine("Strength: {0} Modifier: {1}", character.Abilities.Strength.Score, character.Abilities.Strength.Modifier);
            Console.WriteLine("Dexterity: {0} Modifier: {1}", character.Abilities.Dexterity.Score, character.Abilities.Dexterity.Modifier);
            Console.WriteLine("Constitution: {0} Modifier: {1}", character.Abilities.Constitution.Score, character.Abilities.Constitution.Modifier);
            Console.WriteLine("Intelligence: {0} Modifier: {1}", character.Abilities.Intelligence.Score, character.Abilities.Intelligence.Modifier);
            Console.WriteLine("Wisdom: {0} Modifier: {1}", character.Abilities.Wisdom.Score, character.Abilities.Wisdom.Modifier);
            Console.WriteLine("Charisma: {0} Modifier: {1}", character.Abilities.Charisma.Score, character.Abilities.Charisma.Modifier);
            Console.WriteLine();
            Console.WriteLine("Feats: TODO"); //TODO
            Console.WriteLine("AllFeatures: ");
            foreach (var feature in character.Features.AllFeatures)
                Console.WriteLine(" " + feature.Key);


            Console.Write("Weapon Proficiencies:");
            foreach (var weapon in character.WeaponProficiencies)
                Console.Write(" " + weapon);

            Console.WriteLine();
            Console.Write("Saving Throw Proficiencies:");
            foreach (var savingThrowProficiency in character.SavingThrows)
                Console.Write(" " + savingThrowProficiency);


            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Available Skill Proficiencies:");
            foreach (var skill in character.Skills.Available)
                Console.Write(" " + skill);
            
            Console.WriteLine();
            Console.WriteLine("Skill Scores:");
            Console.WriteLine("Skill\tScore\tChosen\tExpertise");
            foreach (var skill in character.Skills.AllSkills)
            {
                Console.WriteLine("{0} | {1} | {2} | {3}", skill, character.SkillBonus(skill), character.Skills.Chosen.Contains(skill) ? "*" : "", character.Skills.Expertise.Contains(skill) ? "*" : "");
            }

            Console.WriteLine();

            Console.Write("Tool Proficiencies:");
            foreach (var tool in character.Tools.Chosen)
                Console.Write(" " + tool);

            Console.WriteLine();
            Console.WriteLine();

            Console.Write("Instrument Proficiencies:");
            foreach (var instrument in character.Instruments.Chosen)
                Console.Write(" " + instrument);

            Console.WriteLine();


            Console.WriteLine();
            Console.Write("Languagues:");
            foreach (var language in character.Languages.Chosen)
            {
                Console.Write(" " + language);
            }

            Console.WriteLine();
            Console.WriteLine("Gold: " + character.Currency.Gold);
            Console.WriteLine("Silver: " + character.Currency.Silver);
            Console.WriteLine("Copper: " + character.Currency.Copper);
            
            Console.WriteLine();
        }
    }

    public class Menu
    {
        private static ICharacter _character;
        private static int _result;

        public Dictionary<MenuOptions, Action> SystemDetailsProcessDictionary
        {
            get
            {
                return new Dictionary<MenuOptions, Action>
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
            }
        }

        
        //character.AddToolProfs(new List<AvailableTool> { AvailableTool.AlchemistsSupplies });
        //character.AddInstrumentProfs(new List<AvailableInstrument> { AvailableInstrument.Lute });
        //character.AddLanguages(new List<AvailableLanguages> { AvailableLanguages.Common });

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
            Console.Clear();
            foreach (var feature in _character.Features.AllFeatures)
                Console.WriteLine(feature.Key + " - " + feature.Value + "\r\n");
             
            Console.WriteLine("Press any key to return to Character");
            Console.ReadLine();

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
                Console.WriteLine("{0}: {1}", i++, race);

            var chosen = Convert.ToInt32(Console.ReadLine()) - 1;

            var characterRace = (T)(object)chosen;
            Console.Clear();
            return characterRace;
        }
    }
}
