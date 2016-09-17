using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using _5ECharacterBuilder;

namespace _5EDatabase
{
    public class Armory
    {
        static Armory()
        {
            SimpleWeapons = new SortedSet<AvailableWeapon>
            {
                AvailableWeapon.Club,
                AvailableWeapon.Dagger,
                AvailableWeapon.Greatclub,
                AvailableWeapon.Handaxe,
                AvailableWeapon.Javelin,
                AvailableWeapon.LightHammer,
                AvailableWeapon.Mace,
                AvailableWeapon.Quarterstaff,
                AvailableWeapon.Sickle,
                AvailableWeapon.Spear,
                AvailableWeapon.UnarmedStrike
            };

            SimpleRangedWeapons = new SortedSet<AvailableWeapon>
            {
                AvailableWeapon.CrossbowLight,
                AvailableWeapon.Dart,
                AvailableWeapon.ShortBow,
                AvailableWeapon.Sling
            };

            MartialWeapons = new SortedSet<AvailableWeapon>
            {
                AvailableWeapon.BattleAxe,
                AvailableWeapon.Flail,
                AvailableWeapon.Glaive,
                AvailableWeapon.Greataxe,
                AvailableWeapon.GreatSword,
                AvailableWeapon.Halberd,
                AvailableWeapon.Lance,
                AvailableWeapon.LongSword,
                AvailableWeapon.Maul,
                AvailableWeapon.Morningstar,
                AvailableWeapon.Pike,
                AvailableWeapon.Rapier,
                AvailableWeapon.Scimitar,
                AvailableWeapon.ShortSword,
                AvailableWeapon.Trident,
                AvailableWeapon.WarPick,
                AvailableWeapon.Warhammer,
                AvailableWeapon.Whip
            };

            LightArmor = new SortedSet<AvailableArmor>
            {
                AvailableArmor.Padded,
                AvailableArmor.Leather,
                AvailableArmor.StuddedLeather
            };

            MediumArmor = new SortedSet<AvailableArmor>
            {
                AvailableArmor.Hide,
                AvailableArmor.ChainShirt,
                AvailableArmor.ScaleMail,
                AvailableArmor.Breastplate,
                AvailableArmor.HalfPlate
            };

            HeavyArmor = new SortedSet<AvailableArmor>
            {
                AvailableArmor.RingMail,
                AvailableArmor.ChainMail,
                AvailableArmor.Splint,
                AvailableArmor.Plate
            };

            AllArmor = new List<AvailableArmor> { AvailableArmor.Cloth };
            AllArmor.AddRange(LightArmor);
            AllArmor.AddRange(MediumArmor);
            AllArmor.AddRange(HeavyArmor);
        }

        public static SortedSet<AvailableWeapon> SimpleRangedWeapons { get; set; }

        public static SortedSet<AvailableArmor> LightArmor { get; set; }
        public static SortedSet<AvailableArmor> MediumArmor { get; set; }
        public static SortedSet<AvailableArmor> HeavyArmor { get; set; }
        public static List<AvailableArmor> AllArmor { get; }

        public static SortedSet<AvailableWeapon> SimpleWeapons { get; private set; }
        public static SortedSet<AvailableWeapon> MartialWeapons { get; set; }
        
        public static Armor GetArmor(AvailableArmor armorName)
        {
            return ItemData.GetArmor(armorName);
        }

        public List<AvailableTool> GetGamingSets() =>
            new List<AvailableTool>
            {
                AvailableTool.DiceSet,
                AvailableTool.DragonchessSet,
                AvailableTool.PlayingCardSet,
                AvailableTool.ThreeDragonAnteSet
            };
    }

    public class Armor
    {
        public int Id { get; set; }
        [Column("ArmorName")]
        public string Name { get; set; }
        public int CostId { get; set; }
        public int BaseArmor { get; set; }
        public int? MaxDexBonus { get; set; }
        public int? RequiredStrength { get; set; }
        public bool StealthDisadvantage { get; set; }
        public int Weight { get; set; }
        public ArmorCategory Category { get; set; }
    }

    public enum ArmorCategory
    {
        None,
        Light,
        Medium,
        Heavy
    }

    public enum AvailableWeapon
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