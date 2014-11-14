using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace _5ECharacterBuilder
{
    [DefaultProperty("Test")]
    public class CharacterFeatures
    {
        private int _value;

        public CharacterFeatures()
        {
            RaceFeatures = new List<Feature>();
            ClassFeatures = new List<Feature>();
        }

        public int Test
        {
            get { return _value; }
            set { _value = value; }
        }
        public List<Feature> AlFeatures { get { return RaceFeatures.Concat(ClassFeatures).Distinct().ToList(); } } 
        public List<Feature> RaceFeatures { get; internal set; } 
        public List<Feature> ClassFeatures { get; internal set; } 
        //public List<Feature> BackgroundFeatures { get; internal set; } 
    }
}
