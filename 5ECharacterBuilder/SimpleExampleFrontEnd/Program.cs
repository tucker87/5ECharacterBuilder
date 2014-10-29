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
            var characterRace = AskFor<AvailableRaces>();
            Console.WriteLine("Choose a Class: ");
            var characterClass = AskFor<AvailableClasses>();
            Console.WriteLine("Choose a Background: ");
            var characterBackground = AskFor<AvailableBackgrounds>();


            var character = CreateCharacter(characterRace, characterClass, characterBackground);
            
            var exit = false;
            while (!exit)
            {
                Console.Clear();
                WriteCharacter(character);

                var i = 1;
                foreach (var option in Enum.GetValues(typeof(Menu.MenuOptions)))
                    Console.WriteLine("{0}: {1}", i++, option);

                var chosen = Convert.ToInt32(Console.ReadLine()) -1;
                var result = Menu.MenuSelection((Menu.MenuOptions) chosen, ref character);
                if (result == -1)
                    exit = true;
                
            }

            character.EquipArmor(AvailableArmor.Plate);

            //character.AddSkills(new List<AvailableSkill>
            //{
            //    AvailableSkill.Acrobatics,
            //    AvailableSkill.History,
            //    AvailableSkill.Arcana
            //});

            //character.AddToolProfs(new List<AvailableTool> { AvailableTool.AlchemistsSupplies });
            //character.AddInstrumentProfs(new List<AvailableInstrument> { AvailableInstrument.Lute });
            //character.AddLanguages(new List<AvailableLanguages> { AvailableLanguages.Common });
        }

        private static T AskFor<T>() where T : struct, IConvertible
        {
            var i = 1;
            foreach (var race in (T[]) Enum.GetValues(typeof (T)))
                Console.WriteLine("{0}: {1}", i++, race);

            var chosen = Convert.ToInt32(Console.ReadLine()) - 1;

            var characterRace = (T) (object) chosen;
            Console.Clear();
            return characterRace;
        }

        private static Character CreateCharacter(AvailableRaces characterRace, AvailableClasses characterClass,
            AvailableBackgrounds characterBackground)
        {
            var character = new Character(characterRace, characterClass, characterBackground,
                new CharacterAttributeScores(11, 11, 11, 11, 11, 11));
            
            return character;
        }

        private static void WriteCharacter(Character character)
        {
            Console.WriteLine(character.Race + ", " + character.Class + ", " + character.Background);
            Console.Write("Hit Dice:");
            foreach (var hitDie in character.HitDice)
                Console.Write(" 1d" + hitDie);

            Console.WriteLine();
            Console.WriteLine("Hit Points: " + character.MaxHp);

            Console.WriteLine();
            Console.Write("Armor Proficiencies:");
            foreach (var armor in character.ArmorProficiencies)
                Console.Write(" " + armor);
            Console.WriteLine();

            Console.WriteLine("Equipped Armor: " + character.EquippedArmor.Name);


            Console.WriteLine();
            Console.WriteLine("Has Shield: " + (character.HasShieldEquipped ? "Yes" : "No"));

            Console.WriteLine("Armor Class: " + character.ArmorClass);
            Console.WriteLine("Initiative: " + character.Initiative);
            Console.WriteLine("Size: " + character.Size);
            Console.WriteLine("Speed: " + character.Speed);
            Console.WriteLine();
            Console.WriteLine("Strength: {0} Modifier: {1}", character.Attributes.Strength.Score, character.Attributes.Strength.Modifier);
            Console.WriteLine("Dexterity: {0} Modifier: {1}", character.Attributes.Dexterity.Score, character.Attributes.Dexterity.Modifier);
            Console.WriteLine("Constitution: {0} Modifier: {1}", character.Attributes.Constitution.Score, character.Attributes.Constitution.Modifier);
            Console.WriteLine("Intelligence: {0} Modifier: {1}", character.Attributes.Intelligence.Score, character.Attributes.Intelligence.Modifier);
            Console.WriteLine("Wisdom: {0} Modifier: {1}", character.Attributes.Wisdom.Score, character.Attributes.Wisdom.Modifier);
            Console.WriteLine("Charisma: {0} Modifier: {1}", character.Attributes.Charisma.Score, character.Attributes.Charisma.Modifier);
            Console.WriteLine();
            Console.WriteLine("Feats: TODO"); //TODO
            Console.WriteLine("Features: TODO"); //TODO
            Console.Write("Weapon Proficiencies:");
            foreach (var weapon in character.WeaponProficiencies)
                Console.Write(" " + weapon);

            Console.WriteLine();
            Console.Write("Saving Throw Proficiencies:");
            foreach (var savingThrowProficiency in character.SavingThrowProficiencies)
                Console.Write(" " + savingThrowProficiency);


            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Class Skill Proficiencies:");
            foreach (var skill in character.Skills)
                Console.Write(" " + skill);

            Console.WriteLine();
            Console.WriteLine("Class Skill Count: " + character.ClassSkillCount);

            Console.Write("Choosen Skill Proficiencies:");
            foreach (var skill in character.SkillProficiencies)
                Console.Write(" " + skill);

            Console.WriteLine();
            Console.WriteLine();

            Console.Write("Tool Proficiencies:");
            foreach (var tool in character.ToolProficiencies)
                Console.Write(" " + tool);

            Console.WriteLine();
            Console.WriteLine();

            Console.Write("Instrument Proficiencies:");
            foreach (var instrument in character.InstrumentProficiencies)
                Console.Write(" " + instrument);

            Console.WriteLine();


            Console.WriteLine();
            Console.Write("Languagues:");
            foreach (var language in character.Languages)
            {
                Console.Write(" " + language);
            }

            Console.WriteLine();
            Console.WriteLine("Gold: " + character.Currency.Gold);
            Console.WriteLine("Silver: " + character.Currency.Silver);
            Console.WriteLine("Copper: " + character.Currency.Copper);

            Console.WriteLine();
            Console.WriteLine("Issues: ");
            foreach (var ruleIssue in character.RuleIssues)
                Console.WriteLine(ruleIssue);

            Console.WriteLine();
        }
    }

    public class Menu
    {
        private static Character _character;
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
                    {MenuOptions.Exit, Exit}
                };
            }
        }

        private static void EquipArmor()
        {
            throw new NotImplementedException();
        }

        private static void ToggleShield()
        {
            _character.HasShieldEquipped = !_character.HasShieldEquipped;
        }

        private static void LearnSkill()
        {
            throw new NotImplementedException();
        }

        private static void LearnTool()
        {
            throw new NotImplementedException();
        }

        private static void LearnInstrument()
        {
            throw new NotImplementedException();
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
            Exit
        }

        public static int MenuSelection(MenuOptions option, ref Character character)
        {
            _character = character;
            new Menu().SystemDetailsProcessDictionary[option]();
            character = _character;
            return _result;
        }
    }

}
