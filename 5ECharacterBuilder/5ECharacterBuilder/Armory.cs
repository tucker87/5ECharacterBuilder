using System;
using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder
{
    public class Armory
    {
        static Armory()
        {
            SimpleWeapons = new List<AvailableWeapon>
            {
                AvailableWeapon.Club,
                AvailableWeapon.Dagger,
                AvailableWeapon.ShortSword
            };

            MartialWeapons = new List<AvailableWeapon>
            {
                AvailableWeapon.GreatSword
            };

            using (var db = new CharacterBuilderDB())
            {
                LightArmor = new List<AvailableArmor>(GetArmorsByCategory(db, "Light"));
                MediumArmor = new List<AvailableArmor>(GetArmorsByCategory(db, "Medium"));
                HeavyArmor = new List<AvailableArmor>(GetArmorsByCategory(db, "Heavy"));

                ArmorData = db.Armors.ToList();
            }

            AllArmor = new List<AvailableArmor> { AvailableArmor.Cloth };
            AllArmor.AddRange(LightArmor);
            AllArmor.AddRange(MediumArmor);
            AllArmor.AddRange(HeavyArmor);
        }

        private static IEnumerable<AvailableArmor> GetArmorsByCategory(CharacterBuilderDB db, string category)
        {
                return db.Armors.Where(a => a.Category == category).Select(a => (AvailableArmor) Enum.Parse(typeof(AvailableArmor), a.Name.Replace(" ", string.Empty))).ToList();
        }

        public static List<AvailableArmor> LightArmor { get; set; }
        public static List<AvailableArmor> MediumArmor { get; set; }
        public static List<AvailableArmor> HeavyArmor { get; set; }
        public static List<AvailableArmor> AllArmor { get; private set; }
        public static List<Armor> ArmorData { get; private set; } 

        public static List<AvailableWeapon> SimpleWeapons { get; private set; }
        public static List<AvailableWeapon> MartialWeapons { get; set; }
        
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
        WarHammer
    }
}