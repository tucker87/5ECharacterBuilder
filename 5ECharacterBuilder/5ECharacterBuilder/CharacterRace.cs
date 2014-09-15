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
        public virtual List<AvailableSkills> SkillProficiencies { get { return _character.SkillProficiencies; } }
        public virtual int SkillProficiencyCount { get { return _character.SkillProficiencyCount; } }
        public virtual List<string> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public virtual List<AvailableWeapons> WeaponProficiencies { get { return _character.WeaponProficiencies; } }
        public virtual List<AvailableTools> ToolProficiencies { get { return _character.ToolProficiencies; } }
        public virtual List<AvailableInstruments> InstrumentProficiencies { get { return _character.InstrumentProficiencies; } }
        public virtual List<SavingThrows> SavingThrowProficiencies { get { return _character.SavingThrowProficiencies; } }
    }

    public class Human : CharacterRace
    {
        public Human(CharacterBase character) : base(character)
        {
            character.Attributes = new CharacterAttributes(character.Attributes RacialBonuses(1,1,1,1,1,1);
        }
    }
}