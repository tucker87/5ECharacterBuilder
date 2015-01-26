using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder
{
    class CharacterData
    {
        public static List<Armor> ArmorData => new List<Armor>
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

        public static Dictionary<string, string> AllFeatures
        {
            get
            {
                return new Dictionary<string, string>(GeneralClassFeatures).Union(RaceFeatures).Union(ClassFeatures).ToDictionary(k => k.Key, v => v.Value);
            }
        } 

        public static Dictionary<string, string> GeneralClassFeatures => new Dictionary<string, string>
        {
            {"Extra Attack",  "You can attack twice, instead of once, whenever you take the Attack action on your turn."},
            {"Evasion", "Your instinctive agility lets you dodge out of the way of certain area effects, such as a blue dragon’s lightning breath or a  fireball spell. When you are subjected to an effect that allows you to make a Dexterity saving throw to take only half damage, you instead take no damage if you succeed on the saving throw, and only half damage if you fail."},
        };

        public static Dictionary<string, string> RaceFeatures
        {
            get
            {
                return new Dictionary<string, string>(DwarvenFeatures)
                {
                    {"Ability Score Increase", "Your ability scores each increase by 1."},
                }.Union(ElvenFeatures).ToDictionary(k => k.Key, v => v.Value);
            }
        }

        public static Dictionary<string, string> DwarvenFeatures => new Dictionary<string, string>
        {
            {"Darkvision", "Accustomed to life underground, you have superior vision in dark and dim conditions. You can see in dim light within 60 feet of you as if it were bright light, and in darkness as if it were dim light. You can’t discern color in darkness, only shades of gray."},
            {"Dwarven Resilience", "You have advantage on saving throws against poison, and you have resistance against poison damage."},
            {"Dwarven Combat Training", "You have proficiency with the battleaxe, handaxe, throwing hammer, and warhammer."},
            {"Stonecunning", "Whenever you make an Intelligence (History) check related to the origin of stonework, you are considered proficient in the History skill and add double your proficiency bonus to the check, instead of your normal proficiency bonus."},
            {"Dwarven Toughness", "Your hit point maximum increases by 1, and it increases by 1 every time you gain a level."},
            {"Dwarven Armor Training", "You have proficiency with light and medium armor."}
        };

        public static Dictionary<string, string> ElvenFeatures => new Dictionary<string, string>
        {
            {"Superior Darkvision", "Your darkvision has a radius of 120 feet."},
            {"Sunlight Sensitivity", "You have disadvantage on attack rolls and on Wisdom (Perception) checks that rely on sight when you, the target of your attack, or whatever you are trying to perceive is in direct sunlight."},
            {"Drow Magic", "You know the  dancing lights cantrip. When you reach 3rd level, you can cast the  faerie fire spell once per day. When you reach 5th level, you can also cast the  darkness spell once per day. Charisma is your spellcasting ability for these spells."},
            {"Drow Weapon Training", "You have proficiency with rapiers, shortswords, and hand crossbows."},
            {"Keen Senses", "You have proficiency in the Perception skill."},
            {"Fey Ancestry", "You have advantage on saving throws against being charmed, and magic can’t put you to sleep."},
            {"Trance", "Elves don’t need to sleep. Instead, they meditate deeply, remaining semiconscious, for 4 hours a day. (The Common word for such meditation is “trance.”) While meditating, you can dream after a fashion; such dreams are actually mental exercises that have become reflexive through years of practice. After resting in this way, you gain the same benefit that a human does from 8 hours of sleep."},
            {"Extra Language", "You can speak, read, and write one extra language of your choice."},
            {"Elf Weapon Training", "You have proficiency with the longsword, shortsword, shortbow, and longbow."},
            {"Mask of the Wild", "You can attempt to hide even when you are only lightly obscured by foliage, heavy rain, falling snow, mist, and other natural phenomena."},
            {"Fleet of Foot", "Your base walking speed increases to 35 feet."}
        };

        public static Dictionary<string, string> ClassFeatures
        {
            get
            {
                return new Dictionary<string, string>(MonkFeatures).Union(RogueFeatures).Union(BarbarianFeatures).Union(GeneralClassFeatures).ToDictionary(k => k.Key, v => v.Value);
            }
        }

        public static Dictionary<string, string> BarbarianFeatures => new Dictionary<string, string>
        {
            {"Rage", ""},
            {"Barbarian Unarmored Defense", "While you are not wearing any armor, your Armor Class equals 10 + your Dexterity modifier + your Constitution modifier. You can use a shield and still gain this benefit."},
            {"Reckless Attack", "You can throw aside all concern for defense to attack with fierce desperation. When you make your first attack on your turn, you can decide to attack recklessly. Doing so gives you advantage on melee weapon attack rolls using Strength during this turn, but attack rolls against you have advantage until your next turn"},
            {"Danger Sense", "You gain an uncanny sense of when things nearby aren’t as they should be, giving you an edge when you dodge away from danger. You have advantage on Dexterity saving throws against effects that you can see, such as traps and spells. To gain this benefit, you can’t be blinded, deafened, or incapacitated."},
            {"Fast Movement", "Your speed increases by 10 feet while you aren’t wearing heavy armor."},
            {"Feral Instict", "Your instincts are so honed that you have advantage on initiative rolls. Additionally, if you are surprised at the beginning of combat and aren’t incapacitated, you can act normally on your first turn, but only if you enter your rage before doing anything else on that turn."},
            {"Brutal Critical", "You can roll one additional weapon damage die when determining the extra damage for a critical hit with a melee attack. This increases to two additional dice at 13th level and three additional dice at 17th level."} ,
            {"Relentless Rage", "Your rage can keep you fighting despite grievous wounds. If you drop to 0 hit points while you’re raging and don’t die outright, you can make a DC 10 Constitution saving throw. If you succeed, you drop to 1 hit point instead. Each time you use this feature after the first, the DC increases by 5. When you finish a short or long rest, the DC resets to 10."},
            {"Persistent Rage", "Beginning at 15th level, your rage is so fierce that it ends early only if you fall unconscious or if you choose to end it."},
            {"Indomitable Might", "Beginning at 18th level, if your total for a Strength check is less than your Strength score, you can use that score in place of the total."},
            {"Primal Champion", "At 20th level, you embody the power of the wilds. Your Strength and Constitution scores increase by 4. Your maximum for those scores is now 24."}
        };

        public static Dictionary<string,string> MonkFeatures => new Dictionary<string, string>
        {
            {"Monk Unarmored Defense", "Beginning at 1st level, while you are wearing no armor and not wielding a shield, your AC equals 10 + your Dexterity modifier + your Wisdom modifier."},
            {"Martial Arts", "You gain the following benefits while you are unarmed or wielding only monk weapons and you aren’t wearing armor or wielding a shield: You can use Dexterity instead of Strength for the attack and damage rolls of your unarmed strikes and monk weapons. You can roll a d4 in place of the normal damage of your unarmed strike or monk weapon. This die changes as you gain monk levels, as shown in the Martial Arts column of the Monk table. When you use the Attack action with an unarmed strike or a monk weapon on your turn, you can make one unarmed strike as a bonus action. For example, if you take the Attack action and attack with a quarter- staff, you can also make an unarmed strike as a bonus action, assuming you haven't already taken a bonus action this turn."},
            {"Ki", "See PHB"},
            {"Unarmored Movement", "Starting at 2nd level, your speed increases by 10 feet while you are not wearing armor or wielding a shield. This bonus increases when you reach certain monk levels, as shown in the Monk table. At 9th level, you gain the ability to move along vertical surfaces and across liquids on your turn without falling during the move."},
            {"Open Hand Technique", "Whenever you hit a creature with one of the attacks granted by your Flurry of Blows, you can impose one of the following effects on that target: • It must succeed on a Dexterity saving throw or be knocked prone. • It must make a Strength saving throw. If it fails, you can push it up to 15 feet away from you. • It can’t take reactions until the end of your next turn."},
            {"Shadow Arts", "You can use your ki to duplicate the effects of certain spells. As an action, you can spend 2 ki points to cast  darkness, darkvision, pass without trace, or  silence, without providing material components. Additionally, you gain the  minor illusion cantrip if you don’t already know it." },
            {"Wholeness Of Body", "You gain the ability to heal yourself. As an action, you can regain hit points equal to three times, your monk level. You must finish a long rest before you can use this feature again."},
            {"Tranquility", "You can enter a special meditation that surrounds you with an aura of peace. At the end of a long rest, you gain the effect of a  sanctuary spell that lasts until the start of your next long rest (the spell can end early as normal). The saving throw DC for the spell equals 8 + your Wisdom modifier + your proficiency bonus."},
            {"Quivering Palm", "You gain the ability to set up lethal vibrations in someone’s body. When you hit a creature with an unarmed strike, you can spend 3 ki points to start these imperceptible vibrations, which last for a number of days equal to your monk level. The vibrations are harmless unless you use your action to end them. To do so, you and the target must be on the same plane of existence. When you use this action, the creature must make a Constitution saving throw. If it fails, it is reduced to 0 hit points. If it succeeds, it takes 10d10 necrotic damage. You can have only one creature under the effect of this feature at a time. You can choose to end the vibrations harmlessly without using an action."},
            {"Shadow Step", "You gain the ability to step from one shadow into another. When you are in dim light or darkness, as a bonus action you can teleport up to 60 feet to an unoccupied space you can see that is also in dim light or darkness. You then have advantage on the first melee attack you make before the end of the turn."},
            {"Cloak Of Shadows", "You have learned to become one with the shadows. When you are in an area of dim light or darkness, you can use your action to become invisible. You remain invisible until you make an attack, cast a spell, or are in an area of bright light."},
            {"Opportunist", "You can exploit a creature's momentary distraction when it is hit by an attack. Whenever a creature within 5 feet of you is hit by an attack made by a creature other than you, you can use your reaction to make a melee attack against that creature."},
            {"Disciple Of The Elements", "See PHB"},
            {"Deflect Missiles", "You can use your reaction to deflect or catch the missile when you are hit by a ranged weapon attack. When you do so, the damage you take from the attack is reduced by 1d10 + your Dexterity modifier + your monk level. If you reduce the damage to 0, you can catch the missile if it is small enough for you to hold in one hand and you have at least one hand free. If you catch a missile in this way, you can spend 1 ki point to make a ranged attack with the weapon or piece of ammunition you just caught, as part of the same reaction. You make this attack with proficiency, regardless of your weapon proficiencies, and the missile counts as a monk weapon for the attack."},
            {"Slow Fall", "You can use your reaction when you fall to reduce any falling damage you take by an amount equal to five times your monk level."},
            {"Stunning Strike", "You can interfere with the flow of ki in an opponent’s body. When you hit another creature with a melee weapon attack, you can spend 1 ki point to attempt a stunning strike. The target must succeed on a Constitution saving throw or be stunned until the end of your next turn."},
            {"Ki-Empowered Strikes", "Your unarmed strikes count as magical for the purpose of overcoming resistance and immunity to nonmagical attacks and damage."},
            {"Stillness Of Mind", "You can use your action to end one effect on yourself that is causing you to be charmed or frightened."},
            {"Purity Of Body", "Your mastery of the ki flowing through you makes you immune to disease and poison."},
            {"Tounge Of The Sun And Moon", "You learn to touch the ki of other minds so that you understand all spoken languages. Moreover, any creature that can understand a language can understand what you say."},
            {"Diamond Soul", "Your mastery of ki grants you proficiency in all saving throws. Additionally, whenever you make a saving throw and fail, you can spend 1 ki point to reroll it and take the second result."},
            {"Timeless Body", "Your ki sustains you so that you suffer none of the frailty of old age, and you can't be aged magically. You can still die of old age, however. In addition, you no longer need food or water."},
            {"Empty Body", "You can use your action to spend 4 ki points to become invisible for 1 minute. During that time, you also have resistance to all damage but force damage. Additionally, you can spend 8 ki points to cast the astral projection spell, without needing material components. When you do so, you can’t take any other creatures with you."},
            {"Perfect Self", "When you roll for initiative and have no ki points remaining, you regain 4 ki points."}
        };

        public static Dictionary<string, string> RogueFeatures => new Dictionary<string, string>
        {
            {"Expertise", "Choose two of your skill proficiencies, or one of your skill proficiencies and your proficiency with thieves’ tools. Your proficiency bonus is doubled for any ability check you make that uses either of the chosen proficiencies. At 6th level, you can choose two more of your proficiencies (in skills or with thieves’ tools) to gain this benefit."},
            {"Sneak Attack", "You know how to strike subtly and exploit a foe’s distraction. Once per turn, you can deal an extra 1d6 damage to one creature you hit with an attack if you have advantage on the attack roll. The attack must use a finesse or a ranged weapon. You don’t need advantage on the attack roll if another enemy of the target is within 5 feet of it, that enemy isn’t incapacitated, and you don’t have disadvantage on the attack roll."},
            {"Thieves' Cant", "During your rogue training you learned thieves’ cant, a secret mix of dialect, jargon, and code that allows you to hide messages in seemingly normal conversation. Only another creature that knows thieves’ cant understands such messages. It takes four times longer to convey such a message than it does to speak the same idea plainly. In addition, you understand a set of secret signs and symbols used to convey short, simple messages, such as whether an area is dangerous or the territory of a thieves’ guild, whether loot is nearby, or whether the people in an area are easy marks or will provide a safe house for thieves on the run."},
            {"Cunning Action", "Your quick thinking and agility allow you to move and act quickly. You can take a bonus action on each of your turns in combat. This action can be used only to take the Dash, Disengage, or Hide action."},
            {"Uncanny Dodge", "When an attacker that you can see hits you with an attack, you can use your reaction to halve the attack’s damage against you."},
            {"Reliable Talent", "You have refined your chosen skills until they approach perfection. Whenever you make an ability check that lets you add your proficiency bonus, you can treat a d20 roll of 9 or lower as a 10."},
            {"Blindsense", "If you are able to hear, you are  aware of the location of any hidden or invisible creature within 10 feet of you."},
            {"Slippery Mind", "You have acquired greater mental strength. You gain proficiency in W isdom saving throws."},
            {"Elusive", "You are so evasive that attackers rarely gain the upper hand against you. No attack roll has advantage against you while you aren’t incapacitated."},
            {"Stroke Of Luck", "You have an uncanny knack for succeeding when you need to. If your attack misses a target within range, you can turn the miss into a hit. Alternatively, if you fail an ability check, you can treat the d20 roll as a 20. Once you use this feature, you can’t use it again until you finish a short or long rest."},
            {"Fast Hands", "You can use the bonus action granted by your Cunning Action to make a Dexterity (Sleight of Hand) check, use your thieves’ tools to disarm a trap or open a lock, or take the Use an Object action."},
            {"Second-Story Work", "You gain the ability to climb faster than normal; climbing no longer costs you extra movement. In addition, when you make a running jump, the distance you cover increases by a number of feet equal to your Dexterity modifier."},
            {"Supreme Sneak", "You have advantage on a Dexterity (Stealth) check if you move no more than half your speed on the same turn."},
            {"Use Magic Device", "You have learned enough about the workings of magic that you can improvise the use of items even when they are not intended for you. You ignore all class, race, and level requirements on the use of magic items."},
            {"Thief's Reflexes", "You have become adept at laying ambushes and quickly escaping danger. You can take two turns during the first round of any combat. You take your first turn at your normal initiative and your second turn at your initiative minus 10. You can’t use this feature when you are surprised."},
            {"Assassinate", "You are at your deadliest when you get the drop on your enemies. You have advantage on attack rolls against any creature that hasn’t taken a turn in the combat yet. In addition, any hit you score against a creature that is surprised is a critical hit."},
            {"Infiltration Expertise", "You can unfailingly create false identities for yourself. You must spend seven days and 25 gp to establish the history, profession, and affiliations for an identity. You can’t establish an identity that belongs to someone else. For example, you might acquire appropriate clothing, letters of introduction, and official- looking certification to establish yourself as a member of a trading house from a remote city so you can insinuate yourself into the company of other wealthy merchants. Thereafter, if you adopt the new identity as a disguise, other creatures believe you to be that person until given an obvious reason not to."},
            {"Imposter", "You gain the ability to unerringly mimic another person’s speech, writing, and behavior. You must spend at least three hours studying these three components of the person’s behavior, listening to speech, examining handwriting, and observing mannerisms. Your ruse is indiscernible to the casual observer. If a wary creature suspects something is amiss, you have advantage on any Charisma (Deception) check you make to avoid detection."},
            {"Death Strike", "You become a master of instant death. When you attack and hit a creature that is surprised, it must make a Constitution saving throw (DC 8 + your Dexterity modifier + your proficiency bonus). On a failed save, double the damage of your attack against the creature."},
            {"Spellcasting", "See PHB:98"},
            {"Mage Hand Legerdemain", "When you cast  mage hand, you can make the spectral hand invisible, and you can perform the following additional tasks with it: • You can stow one object the hand is holding in a container worn or carried by another creature. • You can retrieve an object in a container worn or carried by another creature. • You can use thieves’ tools to pick locks and disarm traps at range. You can perform one of these tasks without being noticed by a creature if you succeed on a Dexterity (Sleight of Hand) check contested by the creature’s Wisdom (Perception) check. In addition, you can use the bonus action granted by your Cunning Action to control the hand."},
            {"Magical Ambush", "If you are hidden from a creature when you cast a spell on it, the creature has disadvantage on any saving throw it makes against the spell this turn."},
            {"Versatile Trickster", "You gain the ability to distract targets with your  mage hand. As a bonus action on your turn, you can designate a creature within 5 feet of the spectral hand created by the spell. Doing so gives you advantage on attack rolls against that creature until the end of the turn."},
            {"Spell Thief", "You gain the ability to magically steal the knowledge of how to cast a spell from another spellcaster. Immediately after a creature casts a spell that targets you or includes you in its area of effect, you can use your reaction to force the creature to make a saving throw with its spellcasting ability modifier. The DC equals your spell save DC. On a failed save, you negate the spell’s effect against you, and you steal the knowledge of the spell if it is at least 1st level and of a level you can cast (it doesn’t need to be a wizard spell). For the next 8 hours, you know the spell and can cast it using your spell slots. The creature can’t cast that spell until the 8 hours have passed. Once you use this feature, you can’t use it again until you finish a long rest."}
        };

        public static Dictionary<AvailableSkill, AvailableAbility> SkillMods => new Dictionary<AvailableSkill, AvailableAbility>
        {
            {AvailableSkill.Acrobatics, AvailableAbility.Dexterity},
            {AvailableSkill.AnimalHandling, AvailableAbility.Wisdom},
            {AvailableSkill.Arcana, AvailableAbility.Intelligence},
            {AvailableSkill.Athletics, AvailableAbility.Strength},
            {AvailableSkill.Deception, AvailableAbility.Charisma},
            {AvailableSkill.History, AvailableAbility.Intelligence},
            {AvailableSkill.Insight, AvailableAbility.Wisdom},
            {AvailableSkill.Intimidation,AvailableAbility.Charisma},
            {AvailableSkill.Investigation, AvailableAbility.Intelligence},
            {AvailableSkill.Medicine, AvailableAbility.Wisdom},
            {AvailableSkill.Nature, AvailableAbility.Intelligence},
            {AvailableSkill.Perception, AvailableAbility.Wisdom},
            {AvailableSkill.Performance, AvailableAbility.Charisma},
            {AvailableSkill.Persuasion, AvailableAbility.Charisma},
            {AvailableSkill.Religion, AvailableAbility.Intelligence},
            {AvailableSkill.SleightOfHand, AvailableAbility.Dexterity},
            {AvailableSkill.Stealth, AvailableAbility.Dexterity},
            {AvailableSkill.Survival,  AvailableAbility.Wisdom}
        };

        public static AvailablePaths[] GetRoguePaths()
        {
            return new[]
            {
                
                AvailablePaths.Thief,
                AvailablePaths.Assassin,
                AvailablePaths.ArcaneTrickster
            };
        }
        
        public static AvailablePaths[] GetMonkPaths()
        {
            return new[]
            {
                
                AvailablePaths.WayOfShadow,
                AvailablePaths.WayOfTheFourElements,
                AvailablePaths.WayOfTheOpenHand
            };
        }
    }
}
