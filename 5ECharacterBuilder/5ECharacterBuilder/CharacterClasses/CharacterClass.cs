using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder.CharacterClasses
{
    class CharacterClass : ICharacter
    {
        private readonly ICharacter _character;
        protected CharacterClass(ICharacter character) { _character = character; }

        public virtual int ArmorClass { get { return _character.ArmorClass; } }
        public CharacterAttributes Attributes { get { return _character.Attributes; } }
        public string Background { get { return _character.Background; } }
        public List<AvailableLanguages> BackgroundLanguages { get { return _character.BackgroundLanguages; } }
        public int BackgroundLanguageCount { get { return _character.BackgroundLanguageCount; } }
        public List<AvailableSkill> BackgroundSkills { get { return _character.BackgroundSkills; } }
        public List<string> Classes { get { return _character.Classes; } } 
        public List<AvailableSkill> ClassSkills { get { return _character.ClassSkills; } }
        public virtual int ClassSkillCount { get { return _character.ClassSkillCount; } }
        public Currency Currency { get { return _character.Currency; } }
        public Armor EquippedArmor { get { return _character.EquippedArmor; } }
        public bool HasShield { get { return _character.HasShield; } }
        public List<int> HitDice { get { return _character.HitDice; } }
        public int Initiative { get { return _character.Initiative; } }
        public List<AvailableLanguages> Languages { get { return _character.Languages; } }
        public int MaxHp { get { return _character.MaxHp; } }
        public string Name { get { return _character.Name; } }
        public string Race { get { return _character.Race; } }
        public int RaceLanguageCount { get { return _character.RaceLanguageCount; } }
        public int Speed { get { return _character.Speed; } }
        public List<AvailableLanguages> RaceLanguages { get { return _character.RaceLanguages; } }
        public List<AvailableSkill> TrainedSkills { get { return _character.TrainedSkills; } }
        public List<AvailableArmor> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public List<AvailableWeapon> WeaponProficiencies { get { return _character.WeaponProficiencies; } }
        public List<AvailableTool> ToolProficiencies { get { return _character.ToolProficiencies; } }
        public List<AvailableInstrument> InstrumentProficiencies { get { return _character.InstrumentProficiencies; } }
        public List<SavingThrow> SavingThrowProficiencies { get { return _character.SavingThrowProficiencies; } }
        public string Size { get { return _character.Size; } }
        public List<AvailableSkill> Skills { get { return new List<AvailableSkill>(ClassSkills.Concat(BackgroundSkills).ToList()); } }
        
        public virtual List<string> RuleIssues
        {
            get
            {
                var ruleIssues = _character.RuleIssues;

                if (TrainedSkills.ToList().Count > ClassSkillCount)
                    ruleIssues.Add(string.Format("{0}s can only choose {1} skills from their list.", Classes.Last(), ClassSkillCount));

                var classSkills = ClassSkills.ToList();

                ruleIssues.AddRange(from skill in TrainedSkills
                    where !classSkills.Contains(skill)
                    select skill + " is not a skill available to this class.");

                return ruleIssues;
            }
        }

        public void EquipArmor(AvailableArmor armor) { _character.EquipArmor(armor); }
        public void SetAttributes(CharacterAttributes characterAttributes)
        {
            _character.SetAttributes(characterAttributes);
        }

        public void ToggleShield() { _character.ToggleShield(); }
        public void SetName(string name) { _character.SetName(name); }

        public string ClassesString
        {
            get
            {
                var classDictionary = new Dictionary<string, int>();
                foreach (var characterClass in Classes)
                {
                    if (classDictionary.ContainsKey(characterClass))
                        classDictionary[characterClass]++;
                    else
                        classDictionary.Add(characterClass, 1);
                }

                var result = "";
                foreach (var @class in classDictionary)
                {
                    result += string.Format(" {0} {1}", @class.Key, @class.Value);
                }
                return result.Trim();
            }
        }
    }
}