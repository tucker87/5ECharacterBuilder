using System.ComponentModel;

namespace _5EDatabase
{
    public enum Ability
    {
        Strength,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma
    }

    public enum AlignmentFirst
    {
        Lawful,
        Neutral,
        Chaotic
    }

    public enum AlignmentSecond
    {
        Good,
        Neutral,
        Evil
    }

    public enum Race {
        Any,
        Human,
        [Description("Hill Dwarf")]
        HillDwarf,
        [Description("Mountain Dwarf")]
        MountainDwarf,
        [Description("High Elf")]
        HighElf,
        [Description("Wood Elf")]
        WoodElf,
        Drow
    }
    public enum Class {
        Any,
        Monk, 
        Fighter, 
        Rogue,
        Barbarian,
        Cleric
        
    }
    public enum Background { Acolyte, Criminal }
    public enum ArmorType { Cloth, Padded, Leather, StuddedLeather, Hide, ChainShirt, ScaleMail, Breastplate, HalfPlate, RingMail, ChainMail, Splint, Plate, Shield }
    public enum Skill { 
        Acrobatics,
        [Description("Animal Handling")]
        AnimalHandling, 
        Arcana, 
        Athletics, 
        Deception, 
        History, 
        Insight,
        Intimidation,
        Investigation,
        Medicine, 
        Nature,
        Perception,
        Performance,
        Persuasion,
        Religion,
        [Description("Sleight Of Hand")]
        SleightOfHand,
        Stealth,
        Survival
    }
    public enum Tool {
        [Description("Alchemist's Supplies")]
        AlchemistsSupplies,
        [Description("Thieve's Tools")]
        ThievesTools,
        [Description("Dice Set")]
        DiceSet,
        [Description("Dragonchess Set")]
        DragonchessSet,
        [Description("PlayingCard Set")]
        PlayingCardSet,
        [Description("Three Dragon Ante Set")]
        ThreeDragonAnteSet,
        [Description("Smith's Tools")]
        SmithsTools,
        [Description("Brewer's Supplies")]
        BrewersSupplies,
        [Description("Masons Tools")]
        MasonsTools,
        [Description("Disguise Kit")]
        DisguiseKit,
        [Description("Poisoner's Kit")]
        PoisonersKit
    }
    public enum Instrument { Lute }
    public enum Language { Common, Elvish, Sylvan, Goblin, Draconic, Gnomish, Dwarvish }
    public enum Path { WayOfTheOpenHand, WayOfShadow, WayOfTheFourElements,
        Thief,
        Assassin,
        [Description("Arcane Trickster")]
        ArcaneTrickster,
        [Description("Path Of The Berserker")]
        PathOfTheBerserker,
        [Description("Path Of The Totem Warrior")]
        PathOfTheTotemWarrior
    }
    public enum SavingThrow { Strength, Constitution, Dexterity, Intelligence, Wisdom, Charisma }

    public enum ArmorCategory
    {
        None,
        Light,
        Medium,
        Heavy
    }

    public enum WeaponType
    {
        ShortSword,
        Club,
        Dagger,
        GreatSword,
        BattleAxe,
        HandAxe,
        ThrowingHammer,
        Warhammer,
        LongSword,
        ShortBow,
        LongBow,
        HandCrossbows,
        Rapier,
        Greatclub,
        Handaxe,
        Javelin,
        LightHammer,
        Mace,
        Quarterstaff,
        Sickle,
        Spear,
        UnarmedStrike,
        CrossbowLight,
        Dart,
        Sling,
        Flail,
        Glaive,
        Greataxe,
        Halberd,
        Lance,
        Maul,
        Morningstar,
        Pike,
        Scimitar,
        Trident,
        WarPick,
        Whip
    }
}
