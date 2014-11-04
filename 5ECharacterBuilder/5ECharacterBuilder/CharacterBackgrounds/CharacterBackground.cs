using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder.CharacterBackgrounds
{
    abstract class CharacterBackground : Character
    {
        private readonly Character _character;
        protected CharacterBackground(Character character) { _character = character; }

        public override int ArmorClass { get { return _character.ArmorClass; } }
        public override CharacterAttributes Attributes { get { return _character.Attributes; } set{ _character.Attributes = value; } }
        public override string Background { get { return _character.Background; } internal set { _character.Background = value; } }
        public override List<AvailableLanguages> BackgroundLanguages { get { return _character.BackgroundLanguages; } internal set { _character.BackgroundLanguages = value; } }
        public override int BackgroundLanguageCount { get { return _character.BackgroundLanguageCount; } internal set { _character.BackgroundLanguageCount = value; } }
        public override List<AvailableSkill> BackgroundSkills { get { return _character.BackgroundSkills; } internal set { _character.BackgroundSkills = value; } }

        internal override string Class { get { return _character.Class; }
            set { _character.Class = value; } }
        public override List<AvailableSkill> ClassSkills { get { return _character.ClassSkills; } internal set { _character.ClassSkills = value; } }
        public override int ClassSkillCount { get { return _character.ClassSkillCount; } internal set { _character.ClassSkillCount = value; } }
        public override Currency Currency { get { return _character.Currency; } internal set { _character.Currency = value; } }
        public override Armor EquippedArmor { get { return _character.EquippedArmor; } internal set { _character.EquippedArmor = value; } }
        public override bool HasShield { get { return _character.HasShield; } set { _character.HasShield = value; } }
        public override List<int> HitDice { get { return _character.HitDice; } internal set { _character.HitDice = value; } }
        public override int Initiative { get { return _character.Initiative; } }
        public override List<AvailableLanguages> Languages { get { return _character.Languages; } internal set { _character.Languages = value; } }
        public override int MaxHp { get { return _character.MaxHp; } }
        public override string Name { get { return _character.Name; } set { _character.Name = value; } }
        public override string Race { get { return _character.Race; } internal set { _character.Race = value; } }
        public override int RaceLanguageCount { get { return _character.RaceLanguageCount; } internal set { _character.RaceLanguageCount = value; } }
        public override int Speed { get { return _character.Speed; } internal set { _character.Speed = value; } }
        public override List<AvailableLanguages> RaceLanguages { get { return _character.RaceLanguages; } internal set { _character.RaceLanguages = value; } }
        public override List<AvailableSkill> TrainedSkills { get { return _character.TrainedSkills; } internal set { _character.TrainedSkills = value; } }
        public override List<AvailableArmor> ArmorProficiencies { get { return _character.ArmorProficiencies; } internal set { _character.ArmorProficiencies = value; } }
        public override List<AvailableWeapon> WeaponProficiencies { get { return _character.WeaponProficiencies; } internal set { _character.WeaponProficiencies = value; } }
        public override List<AvailableTool> ToolProficiencies { get { return _character.ToolProficiencies; } internal set { _character.ToolProficiencies = value; } }
        public override List<AvailableInstrument> InstrumentProficiencies { get { return _character.InstrumentProficiencies; } internal set { _character.InstrumentProficiencies = value; } }
        public override List<SavingThrow> SavingThrowProficiencies { get { return _character.SavingThrowProficiencies; } internal set { _character.SavingThrowProficiencies = value; } }
        public override string Size { get { return _character.Size; } internal set { _character.Size = value; } }
        public override List<AvailableSkill> Skills { get { return new List<AvailableSkill>(ClassSkills.Concat(BackgroundSkills).ToList()); } internal set { _character.Skills = value; } }

        public override List<string> RuleIssues
        {
            get
            {
                var currentIssues = _character.RuleIssues;

                if (BackgroundLanguages.Count > BackgroundLanguageCount)
                    currentIssues.Add(string.Format("{0}s can only choose {1} Language", _character.Background, _character.BackgroundLanguageCount));

                return currentIssues;
            }
        }
        
        public override void EquipArmor(AvailableArmor armor) { _character.EquipArmor(armor); }
        
    }
}
