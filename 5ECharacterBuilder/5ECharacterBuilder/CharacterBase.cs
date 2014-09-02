using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder
{
    public interface ICharacter
    {
        CharacterAttributes Attributes { get; }
        List<int> HitDice { get; }
        int MaxHp { get; }
        string Name { get; }
    }

    public class CharacterBase
    {
        public CharacterBase(CharacterAttributeScores attributeScores, string name = "")
        {
            Name = name;
            Attributes = new CharacterAttributes(attributeScores);
            HitDice = new List<int>(new int[0]);
            MaxHp = CalculateMaxHp();
        }

        private int CalculateMaxHp()
        {
            return HitDice.Sum(hitDie => (hitDie/2) + 1) + Attributes.Constitution.Modifier;
        }

        public CharacterAttributes Attributes { get; private set; }
        public List<int> HitDice { get; private set; }
        public int MaxHp { get; private set; }
        public string Name { get; set; }
    }

    public enum AvailableRaces {Human}
    public enum AvailableClasses {Fighter}
}
