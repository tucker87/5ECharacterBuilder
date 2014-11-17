using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterRaces
{
    class CharacterRace : ICharacter
    {
        private readonly ICharacter _character;
        protected CharacterRace(ICharacter character) { _character = character; }

        public int ArmorClass { get { return _character.ArmorClass; } }
        public CharacterAttributes Attributes { get { return _character.Attributes; } }
        public string Background { get { return _character.Background; } }
        public List<string> Classes { get { return _character.Classes; } }
        public ClassPath ClassPath { get { return _character.ClassPath; } } 
        public string ClassesString { get { return _character.ClassesString; } }
        public Currency Currency { get { return _character.Currency; } }
        public Armor EquippedArmor { get { return _character.EquippedArmor; } }
        public bool HasShield { get { return _character.HasShield; } }
        public List<int> HitDice { get { return _character.HitDice; } }
        public int Initiative { get { return _character.Initiative; } }
        public Proficiencies<AvailableInstrument> Instruments { get { return _character.Instruments; } }
        public int Level { get { return _character.Level; } }
        public virtual int MaxHp { get { return _character.MaxHp; } }
        public string Name { get { return _character.Name; } }
        public int ProficiencyBonus { get { return _character.ProficiencyBonus; } }
        public virtual string Race { get { return _character.Race; } }
        public Languages Languages { get { return _character.Languages; } }
        public SortedSet<SavingThrow> SavingThrows { get { return _character.SavingThrows; } } 
        public Proficiencies<AvailableSkills> Skills { get { return _character.Skills; } } 
        public virtual int Speed { get { return 30; } }
        public Proficiencies<AvailableTool> Tools { get { return _character.Tools; } } 
        public SortedSet<AvailableWeapon> WeaponProficiencies { get { return _character.WeaponProficiencies; } }
        public SortedSet<AvailableArmor> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public CharacterFeatures Features { get { return _character.Features; } }
        public int KiPoints { get { return _character.KiPoints; } }
        public int MartialArts { get { return _character.MartialArts; } }
        public virtual string Size { get { return "Medium"; } }

        public void EquipArmor(AvailableArmor armor) { _character.EquipArmor(armor); }
        public void SetAttributes(CharacterAttributes characterAttributes) { _character.SetAttributes(characterAttributes); }
        public void ToggleShield()  { _character.ToggleShield(); }
        public void SetName(string name) { _character.SetName(name); }
        public void LearnSkill(AvailableSkills chosenSkill) { _character.LearnSkill(chosenSkill); }
        public void LearnTool(AvailableTool chosenTool) { _character.LearnTool(chosenTool); }
        public void LearnInstrument(AvailableInstrument chosenInstrument) { _character.LearnInstrument(chosenInstrument); }
        public void LearnLanguage(AvailableLanguages chosenLanguage) { _character.LearnLanguage(chosenLanguage); }
        public void ChosePath(AvailablePaths chosenPath) { _character.ChosePath(chosenPath); }
        internal void AddRaceFeature(string feature) { Features.RaceFeatures.Add(feature, CharacterData.FeatureData[feature]); }
    }
}