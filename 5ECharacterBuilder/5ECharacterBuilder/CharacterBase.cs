using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace _5ECharacterBuilder
{
    public class CharacterBase : ICharacter
    {
        //private readonly List<int> _hitDice;

        public CharacterBase(IEnumerable<int> hitDice,  CharacterAttributeScores attributeScores, string name = "")
        {
            //_hitDice = new List<int>(hitDice);
            Name = name;
            Attributes = new CharacterAttributes(attributeScores);
            //MaxHp = CalculateMaxHp();
        }

        private int CalculateMaxHp()
        {
            return 1;
            //return _hitDice.Sum(hitDie => (hitDie / 2) + 1)+ Attributes.Constitution.Modifier;
        }

        public CharacterAttributes Attributes { get; private set; }
        //public ReadOnlyCollection<int> HitDice { get { return _hitDice.AsReadOnly(); } }
        //public int MaxHp { get; private set; }
        public string Name { get; set; }
    }

    public interface ICharacter
    {
    }
}
