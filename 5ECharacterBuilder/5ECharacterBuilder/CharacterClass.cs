using System.Collections.Generic;

namespace _5ECharacterBuilder
{
    public class CharacterClass : ICharacter
    {
        private readonly CharacterBase _character;

        public CharacterClass(CharacterBase character)
        {
            _character = character;
        }

        public virtual CharacterAttributes Attributes { get; private set; }
        public virtual List<int> HitDice { get; private set; }
        public virtual int MaxHp { get; private set; }
        public virtual string Name { get; private set; }
    }

    public class Monk : CharacterClass
    {
        private readonly CharacterBase _character;
        public Monk(CharacterBase character)
            : base(character)
        {
            _character = character;
            _character.HitDice.Add(8);
        }

        public override List<int> HitDice
        {
            get { return _character.HitDice; }
        }
    }
}