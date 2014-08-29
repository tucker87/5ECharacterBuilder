namespace _5ECharacterBuilder
{
    public abstract class CharacterRace : ICharacter
    {
        private readonly ICharacter _character;
        protected CharacterRace(ICharacter character)
        {
            _character = character;
        }
        //public virtual int MaxHp { get { return _character.MaxHp; } }
    }

    public class Human : CharacterRace
    {
        private readonly ICharacter _character;
        public Human(ICharacter character) : base(character)
        {
            _character = character;
        }

        //public override int MaxHp { get { return _character.MaxHp + 3; } }
    }
}