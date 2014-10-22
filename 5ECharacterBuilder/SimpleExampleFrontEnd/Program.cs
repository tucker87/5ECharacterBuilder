using System;
using System.Collections.Generic;
using _5ECharacterBuilder;

namespace SimpleExampleFrontEnd
{
    class Program
    {
        static void Main()
        {
            var character = new Character(AvailableRaces.Human, AvailableClasses.Fighter, new CharacterAttributeScores(11,11,11,11,11,11));

            Console.WriteLine(character.Race + ", " + character.Class);
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
            character.AddArmors(new List<AvailableArmor> {AvailableArmor.Plate});
            Console.Write("Equipped Armor:");
            foreach (var armor in character.EquippedArmors)
                Console.Write(" " + armor);
            
            Console.WriteLine();
            Console.WriteLine("Armor Class: " + character.ArmorClass);
            Console.WriteLine("Initiative: " + character.Initiative);
            Console.WriteLine("Speed: " + character.Speed);
            Console.WriteLine();
            Console.WriteLine("Strength: " + character.Attributes.Strength.Score + " Modifier: " + character.Attributes.Strength.Modifier);
            Console.WriteLine("Dexterity: " + character.Attributes.Dexterity.Score + " Modifier: " + character.Attributes.Dexterity.Modifier);
            Console.WriteLine("Constitution: " + character.Attributes.Constitution.Score + " Modifier: " + character.Attributes.Constitution.Modifier);
            Console.WriteLine("Intelligence: " + character.Attributes.Intelligence.Score + " Modifier: " + character.Attributes.Intelligence.Modifier);
            Console.WriteLine("Wisdom: " + character.Attributes.Wisdom.Score + " Modifier: " + character.Attributes.Wisdom.Modifier);
            Console.WriteLine("Charisma: " + character.Attributes.Charisma.Score + " Modifier: " + character.Attributes.Charisma.Modifier);
            Console.WriteLine();
            Console.WriteLine("Feats: TODO"); //TODO
            Console.WriteLine("Features: TODO"); //TODO
            Console.Write("Weapon Proficiencies:");
            foreach (var weapon in character.WeaponProficiencies)
                Console.Write(" " + weapon);
            
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Class Skill Proficiencies:");
            foreach (var skill in character.ClassSkills)
                Console.Write(" " + skill);

            Console.WriteLine();
            Console.WriteLine("Class Skill Count: " + character.ClassSkillCount);
            character.AddSkills(new List<AvailableSkill>{AvailableSkill.Acrobat,AvailableSkill.History,AvailableSkill.Arcana});
            Console.Write("Choosen Skill Proficiencies:");
            foreach (var skill in character.SkillProficiencies)
                Console.Write(" " + skill);

            Console.WriteLine();
            Console.WriteLine();
            character.AddToolProfs(new List<AvailableTool>{AvailableTool.AlchemistsSupplies});
            Console.Write("Tool Proficiencies:");
            foreach (var tool in character.ToolProficiencies)
                Console.Write(" " + tool);

            Console.WriteLine();
            Console.WriteLine();
            character.AddInstrumentProfs(new List<AvailableInstrument> { AvailableInstrument.Lute });
            Console.Write("Instrument Proficiencies:");
            foreach (var instrument in character.InstrumentProficiencies)
                Console.Write(" " + instrument);

            Console.WriteLine();
            character.AddLanguages(new List<AvailableLanguages> {AvailableLanguages.Common});

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
            

            Console.ReadLine();
        }
    }
}
