using System.Collections.Generic;

namespace _5ECharacterBuilder
{
    public class Armory
    {
        public Armory()
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
            _armors = new List<Armor>
            {
                new Armor("Cloth", 0, 10, false, 0),
                new Armor("Plate", 1500, 18, true, 65, 15,0),

            };
        }

        public List<AvailableArmor> LightArmor { get; set; }
        public List<AvailableArmor> MediumArmor { get; set; }
        public List<AvailableArmor> HeavyArmor { get; set; }
        public List<AvailableArmor> AllArmor { get; private set; }
        private readonly List<Armor> _armors;
        public List<Armor> Armors { get { return _armors; } } 

        public List<AvailableWeapon> SimpleWeapons { get; private set; }
        public List<AvailableWeapon> MartialWeapons { get; set; }
        
        public class Armor
        {
            public Armor(string name, int cost, int baseArmor, bool stealthDisadvantage, int weight, int requiredStrength = 0, int? maxDexBonus = null)
            {
                Name = name;
                Cost = cost;
                _baseArmor = baseArmor;
                MaxDexterityBonus = maxDexBonus;
                RequiredStrength = requiredStrength;
                StealthDisadvantge = stealthDisadvantage;
                Weight = weight;
            }

            public string Name;
            public int Cost;
            private readonly int _baseArmor;
            public int? MaxDexterityBonus;
            public int ArmorClass(int dex)
            {
                if (MaxDexterityBonus != null)
                    if (dex > MaxDexterityBonus)
                        dex = (int) MaxDexterityBonus;

                return _baseArmor + dex;
            }
            public int RequiredStrength;
            public bool StealthDisadvantge;
            public int Weight;
        }

        public int GetArmorClassBonus(AvailableArmor armorName, int dex)
        {
            var armor = Armors.Find(a => a.Name == armorName.ToString()) ?? Armors[0];
            return armor.ArmorClass(dex);

        }
    }
    public enum AvailableWeapon
    {
        ShortSword,
        Club,
        Dagger,
        GreatSword
    }
}