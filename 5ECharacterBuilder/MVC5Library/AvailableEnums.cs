using System.ComponentModel;

namespace MVC5Library
{
    public enum AvailableAbility
    {
        Strength,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma
    }

    public enum AvailableAlignmentFirst
    {
        Lawful,
        Neutral,
        Chaotic
    }

    public enum AvailableAlignmentSecond
    {
        Good,
        Neutral,
        Evil
    }

    public enum AvailableRaces {
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
    public enum AvailableClasses { 
        Monk, 
        Fighter, 
        Rogue,
        Barbarian
    }
    public enum AvailableBackgrounds { Acolyte, Criminal }
    public enum AvailableArmor { Cloth, Padded, Leather, StuddedLeather, Hide, ChainShirt, ScaleMail, Breastplate, HalfPlate, RingMail, ChainMail, Splint, Plate, Shield }
    public enum AvailableSkill { 
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
    public enum AvailableTool {
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
    public enum AvailableInstrument { Lute }
    public enum AvailableLanguages { Common, Elvish, Sylvan, Goblin, Draconic, Gnomish, Dwarvish }
    public enum AvailablePaths { WayOfTheOpenHand, WayOfShadow, WayOfTheFourElements,
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
}
