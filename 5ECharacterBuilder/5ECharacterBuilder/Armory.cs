using System.Collections.Generic;

namespace _5ECharacterBuilder
{
    public class Armory
    {
        public Armory()
        {
            SimpleWeapons = new List<AvailableWeapons>
            {
                AvailableWeapons.Club,
                AvailableWeapons.Dagger
            };
        }
        public List<AvailableWeapons> SimpleWeapons { get; private set; }
    }
}