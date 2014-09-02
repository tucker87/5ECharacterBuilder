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
        public virtual List<string> SkillProficiencies { get { return _character.SkillProficiencies; } }
        public virtual int SkillProficiencyCount { get { return _character.SkillProficiencyCount; } }
        public virtual List<string> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public virtual List<AvailableWeapons> WeaponProficiencies { get { return _character.WeaponProficiencies; } }
    }

    public class Monk : CharacterClass
    {
        private readonly ICharacter _character;
        public Monk(ICharacter character, SkillList skillList = null)
            : base(character)
        {
            _character = character;
            _character.HitDice.Add(8);
            var armory = new Armory();
            _character.WeaponProficiencies.Add(AvailableWeapons.ShortSword);
            foreach (var weapon in armory.SimpleWeapons)
            {
                _character.WeaponProficiencies.Add(weapon);
            }
            if (skillList == null) return;
            var availableSkills = SetAvailableSkills();
            SetSkills(skillList, availableSkills);
        }

        private void SetSkills(SkillList skillList, List<string> availableSkills)
        {
            foreach (var skill in skillList.Skills)
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

        private static List<string> SetAvailableSkills()
        {
            var availableSkills = new List<string>
            {
                "Acrobat",
                "Athletics",
                "History",
                "Insight",
                "Religion",
                "Stealth"
            };
            return availableSkills;
        }

        public override List<int> HitDice
        {
            get { return _character.HitDice; }
        }
        public override sealed List<string> SkillProficiencies { get { return _character.SkillProficiencies; } }
        public override int SkillProficiencyCount {get { return _character.SkillProficiencyCount + 2; } }
        public override List<AvailableWeapons> WeaponProficiencies { get { return _character.WeaponProficiencies; } }
    }
}