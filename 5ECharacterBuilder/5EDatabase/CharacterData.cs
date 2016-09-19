using System.Collections.Generic;
using System.Linq;

namespace _5EDatabase
{
    public class CharacterData
    {
        public static List<Armor> ArmorData => new List<Armor>
        {
            new Armor(ArmorType.Cloth, 0, 10, 0, ArmorCategory.None),
            new Armor(ArmorType.Padded, 5,  11, 10, ArmorCategory.Light),
            new Armor(ArmorType.Leather, 10, 11, 8, ArmorCategory.Light, stealthDisadvantage: true),
            new Armor(ArmorType.StuddedLeather, 45, 12, 13, ArmorCategory.Light),
                      
            new Armor(ArmorType.Hide, 0, 12, 0, ArmorCategory.Medium, 2),
            new Armor(ArmorType.ChainShirt,50,13,20,ArmorCategory.Medium, 2),
            new Armor(ArmorType.ScaleMail,50,14,45,ArmorCategory.Medium, 2, stealthDisadvantage:true),
            new Armor(ArmorType.Breastplate, 400, 14, 20, ArmorCategory.Medium, 2),
            new Armor(ArmorType.HalfPlate, 750, 15, 40, ArmorCategory.Medium,2, stealthDisadvantage:true),
                      
            new Armor(ArmorType.RingMail, 30, 14, 40, ArmorCategory.Heavy, 0, stealthDisadvantage:true),
            new Armor(ArmorType.ChainMail, 75, 16,55, ArmorCategory.Heavy, 0,  13, true),
            new Armor(ArmorType.Splint, 200, 17,60, ArmorCategory.Heavy, 0, 15, true),
            new Armor(ArmorType.Plate, 0, 18, 0, ArmorCategory.Heavy, 0)
        };

        //public static List<ClassFeature> GeneralClassFeatures => 
        //    new List<ClassFeature>
        //    {
        //        new ClassFeature("Extra Attack",  "You can attack twice, instead of once, whenever you take the Attack action on your turn."),
        //        new ClassFeature("Evasion", "Your instinctive agility lets you dodge out of the way of certain area effects, such as a blue dragon’s lightning breath or a  fireball spell. When you are subjected to an effect that allows you to make a Dexterity saving throw to take only half damage, you instead take no damage if you succeed on the saving throw, and only half damage if you fail.")
        //    };

        public static List<RaceFeature> RaceFeatures =>
            new List<RaceFeature>()
            .Union(DwarvenFeatures)
            .Union(ElvenFeatures)
            .Union(HumanFeatures)
            .ToList();

        public static List<RaceFeature> DwarvenFeatures => new List<RaceFeature>
        {
            new RaceFeature("Darkvision", "Accustomed to life underground, you have superior vision in dark and dim conditions. You can see in dim light within 60 feet of you as if it were bright light, and in darkness as if it were dim light. You can’t discern color in darkness, only shades of gray."),
            new RaceFeature("Dwarven Resilience", "You have advantage on saving throws against poison, and you have resistance against poison damage."),
            new RaceFeature("Dwarven Combat Training", "You have proficiency with the battleaxe, handaxe, throwing hammer, and warhammer."),
            new RaceFeature("Stonecunning", "Whenever you make an Intelligence (History) check related to the origin of stonework, you are considered proficient in the History skill and add double your proficiency bonus to the check, instead of your normal proficiency bonus."),
            new RaceFeature("Dwarven Toughness", "Your hit point maximum increases by 1, and it increases by 1 every time you gain a level."),
            new RaceFeature("Dwarven Armor Training", "You have proficiency with light and medium armor.")
        };

        public static List<RaceFeature> ElvenFeatures => new List<RaceFeature>
        {
            new RaceFeature("Superior Darkvision", "Your darkvision has a radius of 120 feet."),
            new RaceFeature("Sunlight Sensitivity", "You have disadvantage on attack rolls and on Wisdom (Perception) checks that rely on sight when you, the target of your attack, or whatever you are trying to perceive is in direct sunlight."),
            new RaceFeature("Drow Magic", "You know the  dancing lights cantrip. When you reach 3rd level, you can cast the  faerie fire spell once per day. When you reach 5th level, you can also cast the  darkness spell once per day. Charisma is your spellcasting ability for these spells."),
            new RaceFeature("Drow Weapon Training", "You have proficiency with rapiers, shortswords, and hand crossbows."),
            new RaceFeature("Keen Senses", "You have proficiency in the Perception skill."),
            new RaceFeature("Fey Ancestry", "You have advantage on saving throws against being charmed, and magic can’t put you to sleep."),
            new RaceFeature("Trance", "Elves don’t need to sleep. Instead, they meditate deeply, remaining semiconscious, for 4 hours a day. (The Common word for such meditation is “trance.”) While meditating, you can dream after a fashion; such dreams are actually mental exercises that have become reflexive through years of practice. After resting in this way, you gain the same benefit that a human does from 8 hours of sleep."),
            new RaceFeature("Extra Language", "You can speak, read, and write one extra language of your choice."),
            new RaceFeature("Elf Weapon Training", "You have proficiency with the longsword, shortsword, shortbow, and longbow."),
            new RaceFeature("Mask of the Wild", "You can attempt to hide even when you are only lightly obscured by foliage, heavy rain, falling snow, mist, and other natural phenomena."),
            new RaceFeature("Fleet of Foot", "Your base walking speed increases to 35 feet.")
        };

        public static List<RaceFeature> HumanFeatures => new List<RaceFeature>
        {
            new RaceFeature("Ability Score Increase", "Your ability scores each increase by 1.")
        };

        public static List<ClassPathFeature> ClericPathFeatures => new List<ClassPathFeature>
        {
            
        };

       

        public static Dictionary<Skill, Ability> SkillMods =>
            new Dictionary<Skill, Ability>
            {
                {Skill.Acrobatics, Ability.Dexterity},
                {Skill.AnimalHandling, Ability.Wisdom},
                {Skill.Arcana, Ability.Intelligence},
                {Skill.Athletics, Ability.Strength},
                {Skill.Deception, Ability.Charisma},
                {Skill.History, Ability.Intelligence},
                {Skill.Insight, Ability.Wisdom},
                {Skill.Intimidation, Ability.Charisma},
                {Skill.Investigation, Ability.Intelligence},
                {Skill.Medicine, Ability.Wisdom},
                {Skill.Nature, Ability.Intelligence},
                {Skill.Perception, Ability.Wisdom},
                {Skill.Performance, Ability.Charisma},
                {Skill.Persuasion, Ability.Charisma},
                {Skill.Religion, Ability.Intelligence},
                {Skill.SleightOfHand, Ability.Dexterity},
                {Skill.Stealth, Ability.Dexterity},
                {Skill.Survival, Ability.Wisdom}
            };

        public static List<Path> RoguePaths =>
            new List<Path>
            {
                Path.Thief,
                Path.Assassin,
                Path.ArcaneTrickster
            };

        public static List<Path> MonkPaths =>
            new List<Path>
            {
                Path.WayOfShadow,
                Path.WayOfTheFourElements,
                Path.WayOfTheOpenHand
            };
    
        public static List<Path> BarbarianPaths =>
            new List<Path>
            {
                Path.PathOfTheBerserker,
                Path.PathOfTheTotemWarrior
            };
    }
}
