using System.Collections.Generic;
using System.Linq;
using PetaPoco;

namespace _5EDatabase
{
    public class Armory
    {
        static Armory()
        {
            SimpleWeapons = new SortedSet<WeaponType>
            {
                WeaponType.Club,
                WeaponType.Dagger,
                WeaponType.Greatclub,
                WeaponType.Handaxe,
                WeaponType.Javelin,
                WeaponType.LightHammer,
                WeaponType.Mace,
                WeaponType.Quarterstaff,
                WeaponType.Sickle,
                WeaponType.Spear,
                WeaponType.UnarmedStrike
            };

            SimpleRangedWeapons = new SortedSet<WeaponType>
            {
                WeaponType.CrossbowLight,
                WeaponType.Dart,
                WeaponType.ShortBow,
                WeaponType.Sling
            };

            MartialWeapons = new SortedSet<WeaponType>
            {
                WeaponType.BattleAxe,
                WeaponType.Flail,
                WeaponType.Glaive,
                WeaponType.Greataxe,
                WeaponType.GreatSword,
                WeaponType.Halberd,
                WeaponType.Lance,
                WeaponType.LongSword,
                WeaponType.Maul,
                WeaponType.Morningstar,
                WeaponType.Pike,
                WeaponType.Rapier,
                WeaponType.Scimitar,
                WeaponType.ShortSword,
                WeaponType.Trident,
                WeaponType.WarPick,
                WeaponType.Warhammer,
                WeaponType.Whip
            };

            LightArmor = new SortedSet<ArmorType>
            {
                ArmorType.Padded,
                ArmorType.Leather,
                ArmorType.StuddedLeather
            };

            MediumArmor = new SortedSet<ArmorType>
            {
                ArmorType.Hide,
                ArmorType.ChainShirt,
                ArmorType.ScaleMail,
                ArmorType.Breastplate,
                ArmorType.HalfPlate
            };

            HeavyArmor = new SortedSet<ArmorType>
            {
                ArmorType.RingMail,
                ArmorType.ChainMail,
                ArmorType.Splint,
                ArmorType.Plate
            };

            AllArmor = new List<ArmorType> { ArmorType.Cloth };
            AllArmor.AddRange(LightArmor);
            AllArmor.AddRange(MediumArmor);
            AllArmor.AddRange(HeavyArmor);
        }

        public static SortedSet<WeaponType> SimpleRangedWeapons { get; set; }

        public static SortedSet<ArmorType> LightArmor { get; set; }
        public static SortedSet<ArmorType> MediumArmor { get; set; }
        public static SortedSet<ArmorType> HeavyArmor { get; set; }
        public static List<ArmorType> AllArmor { get; }

        public static SortedSet<WeaponType> SimpleWeapons { get; private set; }
        public static SortedSet<WeaponType> MartialWeapons { get; set; }
        
        public static Armor GetArmor(ArmorType armorName)
        {
            return CharacterData.ArmorData.First(a => a.Name == armorName);
        }

        public List<Tool> GetGamingSets() =>
            new List<Tool>
            {
                Tool.DiceSet,
                Tool.DragonchessSet,
                Tool.PlayingCardSet,
                Tool.ThreeDragonAnteSet
            };
    }

    public class Armor
    {
        public Armor()
        {
            
        }

        public Armor(ArmorType armorName, int cost, int baseArmor, int weight, ArmorCategory category, int? maxDexBonus = null, int? requiredStrength = null, bool stealthDisadvantage = false)
        {
            Name = armorName;
            Cost = cost;
            BaseArmor = baseArmor;
            MaxDexBonus = maxDexBonus;
            Weight = weight;
            Category = category;
            RequiredStrength = requiredStrength;
            StealthDisadvantage = stealthDisadvantage;
        }

        public int Id { get; set; }
        [Column("ArmorName")]
        public ArmorType Name { get; set; }
        //public int CostId { get; set; }
        public int Cost { get; set; }
        public int BaseArmor { get; set; }
        public int? MaxDexBonus { get; set; }
        public int? RequiredStrength { get; set; }
        public bool StealthDisadvantage { get; set; }
        public int Weight { get; set; }
        public ArmorCategory Category { get; set; }
    }
}