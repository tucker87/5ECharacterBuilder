using System;
using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder
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

            using (var db = new CharacterBuilderDB())
                ArmorData = db.Armors.ToList();

            AllArmor = new List<AvailableArmor> { AvailableArmor.Cloth };
            AllArmor.AddRange(LightArmor);
            AllArmor.AddRange(MediumArmor);
            AllArmor.AddRange(HeavyArmor);
        }

        public static SortedSet<AvailableWeapon> SimpleRangedWeapons { get; set; }

        public static SortedSet<AvailableArmor> LightArmor { get; set; }
        public static SortedSet<AvailableArmor> MediumArmor { get; set; }
        public static SortedSet<AvailableArmor> HeavyArmor { get; set; }
        public static List<AvailableArmor> AllArmor { get; private set; }
        public static List<Armor> ArmorData { get; private set; } 

        public static SortedSet<AvailableWeapon> SimpleWeapons { get; private set; }
        public static SortedSet<AvailableWeapon> MartialWeapons { get; set; }
        
        public static Armor GetArmor(AvailableArmor armorName)
        {
            var armor = ArmorData.Find(a => a.Name == armorName.ToString()) ?? ArmorData[0];
            return armor;

        }

        public List<AvailableTool> GetGamingSets()
        {
            return new List<AvailableTool>{AvailableTool.DiceSet, AvailableTool.DragonchessSet, AvailableTool.PlayingCardSet, AvailableTool.ThreeDragonAnteSet};
        }
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