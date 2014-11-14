using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterBackgrounds
{
    class CharacterBackground : ICharacter
    {
        private readonly ICharacter _character;
        protected CharacterBackground(ICharacter character) { _character = character; }

        public int ArmorClass { get { return _character.ArmorClass; } }
        public CharacterAttributes Attributes { get { return _character.Attributes; } }
        public virtual string Background { get { return _character.Background; } }
        public List<string> Classes { get { return _character.Classes; } }
        public string ClassesString { get { return _character.ClassesString; } }
        public Currency Currency { get { return _character.Currency; } }
        public Armor EquippedArmor { get { return _character.EquippedArmor; } }
        public bool HasShield { get { return _character.HasShield; } }
        public List<int> HitDice { get { return _character.HitDice; } }
        public int Initiative { get { return _character.Initiative; } }
        public Proficiencies<AvailableInstrument> Instruments { get { return _character.Instruments; } } 
        public int MaxHp { get { return _character.MaxHp; } }
        public string Name { get { return _character.Name; } }
        public string Race { get { return _character.Race; } }
        public Languages Languages { get { return _character.Languages; } }
        public SortedSet<SavingThrow> SavingThrows { get { return _character.SavingThrows; } } 
        public Proficiencies<AvailableSkill> Skills { get { return _character.Skills; } }
        public int Speed { get { return _character.Speed; } }
        public Proficiencies<AvailableTool> Tools { get { return _character.Tools; } } 
        public SortedSet<AvailableWeapon> WeaponProficiencies { get { return _character.WeaponProficiencies; } }
        public SortedSet<AvailableArmor> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public string Size { get { return _character.Size; } }
        public CharacterFeatures Features { get { return _character.Features; } }

        public void EquipArmor(AvailableArmor armor) { _character.EquipArmor(armor); }
        public void SetAttributes(CharacterAttributes characterAttributes) { _character.SetAttributes(characterAttributes); }
        public void ToggleShield() { _character.ToggleShield(); }
        public void SetName(string name) { _character.SetName(name); }
        public void LearnSkill(AvailableSkill chosenSkill) { _character.LearnSkill(chosenSkill); }
        public void LearnTool(AvailableTool chosenTool) { _character.LearnTool(chosenTool); }
        public void LearnInstrument(AvailableInstrument chosenInstrument) { _character.LearnInstrument(chosenInstrument); }
        public void LearnLanguage(AvailableLanguages chosenLanguage) { _character.LearnLanguage(chosenLanguage); }
    }
}
