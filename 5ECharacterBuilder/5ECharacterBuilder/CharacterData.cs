using System.Collections.Generic;

namespace _5ECharacterBuilder
{
    class CharacterData
    {
        public static List<Armor> ArmorData
        {
            get
            {
                return new List<Armor>
                {
                    new Armor("Cloth", 0, 10, 0, ArmorCategory.None),
                    new Armor("Padded",	5,	11, 10, ArmorCategory.Light),
                    new Armor("Leather", 10, 11, 8,	ArmorCategory.Light, stealthDisadvantage: true),
                    new Armor("Studded Leather", 45, 12, 13, ArmorCategory.Light),

                    new Armor("Hide", 0, 12, 0, ArmorCategory.Medium, 2),
                    new Armor("Chain Shirt",50,13,20,ArmorCategory.Medium, 2),
                    new Armor("Scale Mail",50,14,45,ArmorCategory.Medium, 2, stealthDisadvantage:true),
                    new Armor("Breastplate", 400, 14, 20, ArmorCategory.Medium, 2),
                    new Armor("Half Plate",	750, 15, 40, ArmorCategory.Medium,2, stealthDisadvantage:true),

                    new Armor("Ring Mail", 30, 14, 40, ArmorCategory.Heavy,	0, stealthDisadvantage:true),
                    new Armor("Chain Mail",	75,	16,55, ArmorCategory.Heavy,	0,	13,	true),
                    new Armor("Splint",	200,	17,60, ArmorCategory.Heavy,	0,	15,	true),
                    new Armor("Plate", 0, 18, 0, ArmorCategory.Heavy, 0)
                };
            }
        }

        public static Dictionary<string, string> FeatureData
        {
            get 
            { 
                return new Dictionary<string, string>
                {
                    {"Darkvision", "Accustomed to life underground, you have superior vision in dark and dim conditions. You can see in dim light within 60 feet of you as if it were bright light, and in darkness as if it were dim light. You can’t discern color in darkness, only shades of gray."},
                    {"Dwarven Resilience", "You have advantage on saving throws against poison, and you have resistance against poison damage."},
                    {"Dwarven Combat Training", "You have proficiency with the battleaxe, handaxe, throwing hammer, and warhammer."},
                    {"Stonecunning", "Whenever you make an Intelligence (History) check related to the origin of stonework, you are considered proficient in the History skill and add double your proficiency bonus to the check, instead of your normal proficiency bonus."},
                    {"Superior Darkvision", "Your darkvision has a radius of 120 feet."},
                    {"Sunlight Sensitivity", "You have disadvantage on attack rolls and on Wisdom (Perception) checks that rely on sight when you, the target of your attack, or whatever you are trying to perceive is in direct sunlight."},
                    {"Drow Magic", "You know the  dancing lights cantrip. When you reach 3rd level, you can cast the  faerie fire spell once per day. When you reach 5th level, you can also cast the  darkness spell once per day. Charisma is your spellcasting ability for these spells."},
                    {"Drow Weapon Training", "You have proficiency with rapiers, shortswords, and hand crossbows."},
                    {"Keen Senses", "You have proficiency in the Perception skill."},
                    {"Fey Ancestry", "You have advantage on saving throws against being charmed, and magic can’t put you to sleep."},
                    {"Trance", "Elves don’t need to sleep. Instead, they meditate deeply, remaining semiconscious, for 4 hours a day. (The Common word for such meditation is “trance.”) While meditating, you can dream after a fashion; such dreams are actually mental exercises that have become reflexive through years of practice. After resting in this way, you gain the same benefit that a human does from 8 hours of sleep."},
                    {"Extra Language", "You can speak, read, and write one extra language of your choice."},
                    {"Elf Weapon Training", "You have proficiency with the longsword, shortsword, shortbow, and longbow."},
                    {"Dwarven Toughness", "Your hit point maximum increases by 1, and it increases by 1 every time you gain a level."},
                    {"Dwarven Armor Training", "You have proficiency with light and medium armor."},
                    {"Mask of the Wild", "You can attempt to hide even when you are only lightly obscured by foliage, heavy rain, falling snow, mist, and other natural phenomena."},
                    {"Fleet of Foot", "Your base walking speed increases to 35 feet."},
                }; 
            }
        }

        public static Dictionary<string,string> MonkFeatures
        {
            get
            {
                return new Dictionary<string, string>
                {
                    {"Unarmored Defense", "Beginning at 1st level, while you are wearing no armor and not wielding a shield, your AC equals 10 + your Dexterity modifier + your Wisdom modifier."},
                    {"Martial Arts", "You gain the following benefits while you are unarmed or wielding only monk weapons and you aren’t wearing armor or wielding a shield: You can use Dexterity instead of Strength for the attack and damage rolls of your unarmed strikes and monk weapons. You can roll a d4 in place of the normal damage of your unarmed strike or monk weapon. This die changes as you gain monk levels, as shown in the Martial Arts column of the Monk table. When you use the Attack action with an unarmed strike or a monk weapon on your turn, you can make one unarmed strike as a bonus action. For example, if you take the Attack action and attack with a quarter- staff, you can also make an unarmed strike as a bonus action, assuming you haven't already taken a bonus action this turn."},
                    {"Ki", "THIS THING IS LONG!"},
                    {"Unarmored Movement", "Starting at 2nd level, your speed increases by 10 feet while you are not wearing armor or wielding a shield. This bonus increases when you reach certain monk levels, as shown in the Monk table. At 9th level, you gain the ability to move along vertical surfaces and across liquids on your turn without falling during the move."},
                    {"Open Hand Technique", "Whenever you hit a creature with one of the attacks granted by your Flurry of Blows, you can impose one of the following effects on that target: • It must succeed on a Dexterity saving throw or be knocked prone. • It must make a Strength saving throw. If it fails, you can push it up to 15 feet away from you. • It can’t take reactions until the end of your next turn."} 
                };
            }
        }
        
        public static List<AvailablePaths> GetMonkPaths()
        {
            return new List<AvailablePaths>
            {
                
                AvailablePaths.WayOfShadow,
                AvailablePaths.WayOfTheFourElements,
                AvailablePaths.WayOfTheOpenHand
            };
        }

        internal static class MonkPathFeatures
        {
            public static Dictionary<string, string> WayOfTheOpenPalm
            {
                get
                {
                    return new Dictionary<string, string>
                    {
                        {"Open Hand Technique", MonkFeatures["Open Hand Technique"]}
                    };
                }
            }
        }
    }
}
