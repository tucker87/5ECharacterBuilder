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
                    new Armor("Splint",	200, 17,60, ArmorCategory.Heavy, 0, 15,	true),
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
                    {"Ki", "See PHB"},
                    {"Unarmored Movement", "Starting at 2nd level, your speed increases by 10 feet while you are not wearing armor or wielding a shield. This bonus increases when you reach certain monk levels, as shown in the Monk table. At 9th level, you gain the ability to move along vertical surfaces and across liquids on your turn without falling during the move."},
                    {"Open Hand Technique", "Whenever you hit a creature with one of the attacks granted by your Flurry of Blows, you can impose one of the following effects on that target: • It must succeed on a Dexterity saving throw or be knocked prone. • It must make a Strength saving throw. If it fails, you can push it up to 15 feet away from you. • It can’t take reactions until the end of your next turn."},
                    { "Shadow Arts", "You can use your ki to duplicate the effects of certain spells. As an action, you can spend 2 ki points to cast  darkness, darkvision, pass without trace, or  silence, without providing material components. Additionally, you gain the  minor illusion cantrip if you don’t already know it." },
                    { "Wholeness Of Body", "You gain the ability to heal yourself. As an action, you can regain hit points equal to three times, your monk level. You must finish a long rest before you can use this feature again."},
                    {"Tranquility", "You can enter a special meditation that surrounds you with an aura of peace. At the end of a long rest, you gain the effect of a  sanctuary spell that lasts until the start of your next long rest (the spell can end early as normal). The saving throw DC for the spell equals 8 + your Wisdom modifier + your proficiency bonus."},
                    {"Quivering Palm", "You gain the ability to set up lethal vibrations in someone’s body. When you hit a creature with an unarmed strike, you can spend 3 ki points to start these imperceptible vibrations, which last for a number of days equal to your monk level. The vibrations are harmless unless you use your action to end them. To do so, you and the target must be on the same plane of existence. When you use this action, the creature must make a Constitution saving throw. If it fails, it is reduced to 0 hit points. If it succeeds, it takes 10d10 necrotic damage. You can have only one creature under the effect of this feature at a time. You can choose to end the vibrations harmlessly without using an action."},
                    {"Shadow Step", "You gain the ability to step from one shadow into another. When you are in dim light or darkness, as a bonus action you can teleport up to 60 feet to an unoccupied space you can see that is also in dim light or darkness. You then have advantage on the first melee attack you make before the end of the turn."},
                    {"Cloak Of Shadows", "You have learned to become one with the shadows. When you are in an area of dim light or darkness, you can use your action to become invisible. You remain invisible until you make an attack, cast a spell, or are in an area of bright light."},
                    {"Opportunist", "You can exploit a creature's momentary distraction when it is hit by an attack. Whenever a creature within 5 feet of you is hit by an attack made by a creature other than you, you can use your reaction to make a melee attack against that creature."},
                    {"Disciple Of The Elements", "See PHB"},
                    {"Deflect Missiles", "You can use your reaction to deflect or catch the missile when you are hit by a ranged weapon attack. When you do so, the damage you take from the attack is reduced by 1d10 + your Dexterity modifier + your monk level. If you reduce the damage to 0, you can catch the missile if it is small enough for you to hold in one hand and you have at least one hand free. If you catch a missile in this way, you can spend 1 ki point to make a ranged attack with the weapon or piece of ammunition you just caught, as part of the same reaction. You make this attack with proficiency, regardless of your weapon proficiencies, and the missile counts as a monk weapon for the attack."},
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

        public static Dictionary<string, string> GetMonkPathFeatures(AvailablePaths path, int level)
        {
            var features = new Dictionary<string, string>();

            if (path == AvailablePaths.WayOfTheOpenHand)
            {
                features.Add("Open Hand Technique", MonkFeatures["Open Hand Technique"]);
                if (level >= 6)
                    features.Add("Wholeness Of Body", MonkFeatures["Wholeness Of Body"]);

                if(level >= 11)
                    features.Add("Tranquility", MonkFeatures["Tranquility"]);

                if(level >= 17)
                    features.Add("Quivering Palm", MonkFeatures["Quivering Palm"]);
                    
            }
            if (path == AvailablePaths.WayOfShadow)
            {
                features.Add("Shadow Arts", MonkFeatures["Shadow Arts"]);
                if (level >= 6)
                    features.Add("Shadow Step", MonkFeatures["Shadow Step"]);

                if (level >= 11)
                    features.Add("Cloak Of Shadows", MonkFeatures["Cloak Of Shadows"]);

                if (level >= 17)
                    features.Add("Opportunist", MonkFeatures["Opportunist"]);
            }
            if (path == AvailablePaths.WayOfTheFourElements)
            {
                features.Add("Disciple Of The Elements", MonkFeatures["Disciple Of The Elements"]);
            }
            return features;

        }
    }
}
