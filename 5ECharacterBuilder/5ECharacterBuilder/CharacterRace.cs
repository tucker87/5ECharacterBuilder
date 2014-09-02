using System.Collections.Generic;

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
        public virtual int MaxHp { get { return CharacterBase.CalculateMaxHp(_character.HitDice, Attributes.Constitution.Modifier); } }
        public virtual string Name { get { return _character.Name; } }
        public virtual List<string> SkillProficiencies { get { return _character.SkillProficiencies; } }
        public virtual int SkillProficiencyCount { get { return _character.SkillProficiencyCount; } }
        public virtual List<string> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public virtual List<AvailableWeapons> WeaponProficiencies { get { return _character.WeaponProficiencies; } }
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