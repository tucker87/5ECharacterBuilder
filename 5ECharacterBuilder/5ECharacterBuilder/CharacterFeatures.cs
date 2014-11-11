using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder
{
    public class CharacterFeatures
    {
        public CharacterFeatures()
        {
            RaceFeatures = new List<Feature>();
            ClassFeatures = new List<Feature>();
        }
        public List<Feature> AllFeatures { get { return RaceFeatures.Concat(ClassFeatures).Distinct().ToList(); } } 
        public List<Feature> RaceFeatures { get; internal set; } 
        public List<Feature> ClassFeatures { get; internal set; } 
        //public List<Feature> BackgroundFeatures { get; internal set; } 
    }
}
