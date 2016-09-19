using PetaPoco;

namespace _5EDatabase
{
    //public static class StaticData
    //{
    //    // ReSharper disable once InconsistentNaming
    //    public static string _5ECBConnectionStringName => "5ECB";
    //}

    //public static class ItemData
    //{
    //    public static ArmorType GetArmor(ArmorType armor)
    //    {
    //        using (var db = new Database(StaticData._5ECBConnectionStringName))
    //        {
    //            return db.First<ArmorType>(
    //            "SELECT * FROM Item.ArmorType a JOIN Item.Category c ON c.CategoryId = a.CategoryId LEFT JOIN Item.Cost ON Cost.CostId = a.CostId WHERE a.ArmorName = @0", armor.ToString());
    //        }
    //    }
    //}
}
