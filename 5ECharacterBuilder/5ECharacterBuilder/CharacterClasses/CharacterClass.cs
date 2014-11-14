using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterClasses
{
    class CharacterClass : ICharacter
    {
        private readonly ICharacter _character;
        protected CharacterClass(ICharacter character) { _character = character; }

        public virtual int ArmorClass { get { return _character.ArmorClass; } }
        public CharacterAttributes Attributes { get { return _character.Attributes; } }
        public string Background { get { return _character.Background; } }
        public List<string> Classes { get { return _character.Classes; } } 
        public Currency Currency { get { return _character.Currency; } }
        public Armor EquippedArmor { get { return _character.EquippedArmor; } }
        public bool HasShield { get { return _character.HasShield; } }
        public List<int> HitDice { get { return _character.HitDice; } }
        public int Initiative { get { return _character.Initiative; } }
        public SortedSet<AvailableInstrument> ChosenInstrumentProficiencies { get { return _character.ChosenInstrumentProficiencies; } }
        public int MaxHp { get { return _character.MaxHp; } }
        public string Name { get { return _character.Name; } }
        public string Race { get { return _character.Race; } }
        public SortedSet<AvailableLanguages> ChosenLanguages { get { return _character.ChosenLanguages; } }
        public int Speed { get { return _character.Speed; } }
        public SortedSet<AvailableSkill> ChosenSkills { get { return _character.ChosenSkills; } }
        public SortedSet<AvailableArmor> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public SortedSet<AvailableTool> ChosenToolProficiencies { get { return _character.ChosenToolProficiencies; } }
        public SortedSet<AvailableWeapon> WeaponProficiencies { get { return _character.WeaponProficiencies; } }
        public int ClassSkillCount { get { return _character.ClassSkillCount; } }
        public int LanguageCount { get { return _character.LanguageCount; } }
        public SortedSet<AvailableTool> AvailableToolProficiencies { get { return _character.AvailableToolProficiencies; } }
        public SortedSet<AvailableInstrument> AvailableInstrumentProficiencies { get { return _character.AvailableInstrumentProficiencies; } }
        public SortedSet<SavingThrow> SavingThrowProficiencies { get { return _character.SavingThrowProficiencies; } }
        public string Size { get { return _character.Size; } }
        public SortedSet<AvailableSkill> AvailableSkills { get { return _character.AvailableSkills; } }
        public virtual int ToolProficiencyCount { get { return _character.ToolProficiencyCount; } }
        public CharacterFeatures Features { get { return _character.Features; } }

        public void EquipArmor(AvailableArmor armor) { _character.EquipArmor(armor); }
        public void SetAttributes(CharacterAttributes characterAttributes) { _character.SetAttributes(characterAttributes); }
        public void ToggleShield() { _character.ToggleShield(); }
        public void SetName(string name) { _character.SetName(name); }
        public void LearnSkill(AvailableSkill chosenSkill) { _character.LearnSkill(chosenSkill); }
        public void LearnTool(AvailableTool chosenTool) { _character.LearnTool(chosenTool); }

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
                    result += string.Format(" {0} {1}", @class.Key, @class.Value);
                
                return result.Trim();
            }
        }

        public virtual int SkillCount
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}