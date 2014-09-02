using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace _5ECharacterBuilder
{
    public abstract class CharacterRace : ICharacter
    {
        private readonly CharacterBase _character;
        protected CharacterRace(CharacterBase character)
        {
            _character = character;
        }

        public virtual CharacterAttributes Attributes { get { return _character.Attributes; } }
        public virtual List<int> HitDice { get { return _character.HitDice; } }
        public virtual int MaxHp { get { return _character.MaxHp; } }
        public virtual string Name { get { return _character.Name; } }
    }

    public class Human : CharacterRace
    {
        private readonly CharacterBase _character;
        public Human(CharacterBase character) : base(character)
        {
            _character = character;
            _character.Attributes.Strength.Score += 1;
            _character.Attributes.Constitution.Score += 1;
            _character.Attributes.Dexterity.Score += 1;
            _character.Attributes.Intelligence.Score += 1;
            _character.Attributes.Wisdom.Score += 1;
            _character.Attributes.Charisma.Score += 1;
        }

        public override CharacterAttributes Attributes
        {
            get { return _character.Attributes; }
        }
    }
}