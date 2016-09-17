using PetaPoco;
using _5ECharacterBuilder;

namespace _5EDatabase
{
    //Unsafe
    public static class StaticData
    {
        // ReSharper disable once InconsistentNaming
        public static string _5ECBConnectionStringName => "5ECB";
    }

    public static class ItemData
    {
        public static Armor GetArmor(AvailableArmor armor)
        {
            using (var db = new Database(StaticData._5ECBConnectionStringName))
            {
                return db.First<Armor>(
                "SELECT * FROM Item.Armor a JOIN Item.Category c ON c.CategoryId = a.CategoryId LEFT JOIN Item.Cost ON Cost.CostId = a.CostId WHERE a.ArmorName = @0", armor.ToString());
            }
        }
    }
}
