using System.Collections.Generic;

namespace _5ECharacterBuilder
{
    class CharacterData
    {
        public static List<Armor> ArmorData
        {
            get
            {
                return new List<Armor>
                {
                    new Armor("Cloth", 0, 10, 0, ArmorCategory.None),
                    new Armor("Hide", 0, 12, 0, ArmorCategory.Medium, 2),
                    new Armor("Plate", 0, 18, 0, ArmorCategory.Heavy, 0)
                };
            }
        }

        public static List<Feature> FeatureData
        {
            get 
            { 
                return new List<Feature>
                {
                    new Feature("Darkvision", ""),
                    new Feature("Dwarven Resilience", ""),
                    new Feature("Dwarven Combat Training", ""),
                    new Feature("Stonecunning", ""),
                    new Feature("Superior Darkvision", ""),
                    new Feature("Sunlight Sensitivity", ""),
                    new Feature("Drow Magic", ""),
                    new Feature("Drow Weapon Training", ""),
                    new Feature("Keen Senses", ""),
                    new Feature("Fey Ancestry", ""),
                    new Feature("Trance", ""),
                    new Feature("Extra Language", ""),
                    new Feature("Elf Weapon Training", ""),
                    new Feature("Dwarven Toughness", ""),
                    new Feature("Dwarven Armor Training", ""),
                    new Feature("Mask of the Wild", ""),
                    new Feature("Fleet of Foot", ""),
                }; 
            }
        }
    }
}
