using System.Collections.Generic;

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

            LightArmor = new List<AvailableArmor>
            {
                AvailableArmor.Cloth,
                AvailableArmor.Leather
            };

            MediumArmor = new List<AvailableArmor>
            {
                AvailableArmor.Hide,
                AvailableArmor.ChainMail
            };

            HeavyArmor = new List<AvailableArmor>
            {
                AvailableArmor.Plate
            };

            AllArmor = new List<AvailableArmor>();
            AllArmor.AddRange(LightArmor);
            AllArmor.AddRange(MediumArmor);
            AllArmor.AddRange(HeavyArmor);
            Armors = new List<Armor>
            {
                new Armor("Cloth", 0, 10, false, 0),
                new Armor("Plate", 1500, 18, true, 65, 15,0),

            };
        }

        public static List<AvailableArmor> LightArmor { get; set; }
        public static List<AvailableArmor> MediumArmor { get; set; }
        public static List<AvailableArmor> HeavyArmor { get; set; }
        public static List<AvailableArmor> AllArmor { get; private set; }
        public static List<Armor> Armors { get; private set; } 

        public static List<AvailableWeapon> SimpleWeapons { get; private set; }
        public static List<AvailableWeapon> MartialWeapons { get; set; }
        
        public static Armor GetArmor(AvailableArmor armorName)
        {
            var armor = Armors.Find(a => a.Name == armorName.ToString()) ?? Armors[0];
            return armor;

        }

        public List<AvailableTool> GetGamingSets()
        {
            return new List<AvailableTool>{AvailableTool.DiceSet, AvailableTool.DragonchessSet, AvailableTool.PlayingCardSet, AvailableTool.ThreeDragonAnteSet};
        }
    }

    public class Armor
    {
        public Armor(string name, int cost, int baseArmor, bool stealthDisadvantage, int weight, int requiredStrength = 0, int maxDexBonus = -1)
        {
            Name = name;
            Cost = cost;
            BaseArmor = baseArmor;
            MaxDexterityBonus = maxDexBonus;
            RequiredStrength = requiredStrength;
            StealthDisadvantge = stealthDisadvantage;
            Weight = weight;
        }

        public string Name;
        public int Cost;
        public int BaseArmor;
        public int MaxDexterityBonus;
        public int RequiredStrength;
        public bool StealthDisadvantge;
        public int Weight;
    }

    public enum AvailableWeapon
    {
        ShortSword,
        Club,
        Dagger,
        GreatSword
    }
}