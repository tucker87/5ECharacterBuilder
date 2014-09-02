using System;
using System.Collections.Generic;

namespace _5ECharacterBuilder
{
    public class CharacterClass : ICharacter
    {
        private readonly ICharacter _character;

        public CharacterClass(ICharacter character)
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

    public class Monk : CharacterClass
    {
        private readonly ICharacter _character;
        public Monk(ICharacter character, List<AvailableSkills> skillList = null, AvailableTools? artisanTool = null, AvailableInstruments? instrument = null)
            : base(character)
        {
            _character = character;
            _character.HitDice.Add(8);
            AddToolOrInstrumentProficiencies(artisanTool, instrument);
            _character.WeaponProficiencies.Add(AvailableWeapons.ShortSword);
            AddSimpleWeaponProficiencies();
            _character.SavingThrowProficiencies.Add(SavingThrows.Strength);
            _character.SavingThrowProficiencies.Add(SavingThrows.Dexterity);
            
            if (skillList == null) return;
            if (skillList.Count > 2) throw new Exception("Monks can only choose two skills from their list.");
            SetSkills(skillList, MonkSkills);
        }

        private void AddSimpleWeaponProficiencies()
        {
            var armory = new Armory();
            foreach (var weapon in armory.SimpleWeapons)
                _character.WeaponProficiencies.Add(weapon);
        }

        private void AddToolOrInstrumentProficiencies(AvailableTools? artisanTool, AvailableInstruments? instrument)
        {
            if (_character.ToolProficiencies.Count != 0 || _character.InstrumentProficiencies.Count != 0) return;
            if (artisanTool == null && instrument == null)
                throw new Exception("Monks must select one tool or instrument");

            if (artisanTool != null && instrument != null)
                throw new Exception("Monks can only select one tool or instrument");

            if (artisanTool != null)
                _character.ToolProficiencies.Add((AvailableTools) artisanTool);

            if (instrument != null)
                _character.InstrumentProficiencies.Add((AvailableInstruments) instrument);
        }

        private void SetSkills(IEnumerable<AvailableSkills> skillList, List<AvailableSkills> availableSkills)
        {
            foreach (var skill in skillList)
            {
                if (availableSkills.Contains(skill))
                {
                    _character.SkillProficiencies.Add(skill);
                }
                else
                {
                    throw new Exception(skill + " is not a skill available to this class.");
                }
            }
        }

        public override List<int> HitDice { get { return _character.HitDice; } }
        public override sealed List<AvailableSkills> SkillProficiencies { get { return _character.SkillProficiencies; } }
        public override int SkillProficiencyCount {get { return _character.SkillProficiencyCount + 2; } }
        public override List<AvailableWeapons> WeaponProficiencies { get { return _character.WeaponProficiencies; } }
        public override List<SavingThrows> SavingThrowProficiencies { get { return _character.SavingThrowProficiencies;} }
        private List<AvailableSkills> MonkSkills { get { return new List<AvailableSkills> { AvailableSkills.Acrobat, AvailableSkills.Athletics, AvailableSkills.History, AvailableSkills.Insight, AvailableSkills.Religion, AvailableSkills.Stealth }; }
        } 
    }
}