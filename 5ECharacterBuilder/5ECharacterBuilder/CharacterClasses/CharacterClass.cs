using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterClasses
{
    class CharacterClass : ICharacter
    {
        private readonly ICharacter _character;

        protected CharacterClass(ICharacter character)
        {
            _character = character;
            Abilities.ImprovementPoints = GetImprovementPoints();
        }

        private int GetImprovementPoints()
        {
            {
                var level = Level + 1;
                if (level >= 19)
                    return 10;

                if (level >= 16)
                    return 8;

                if (level >= 12)
                    return 6;

                if (level >= 8)
                    return 4;

                if (level >= 4)
                    return 2;

                return 0;
            }
        }

        public virtual int ArmorClass { get { return _character.ArmorClass; } }
        public CharacterAbilities Abilities { get { return _character.Abilities; } }
        public string Background { get { return _character.Background; } }
        public List<string> Classes { get { return _character.Classes; } }
        public ClassPath ClassPath { get { return _character.ClassPath; } }
        public Currency Currency { get { return _character.Currency; } }
        public Armor EquippedArmor { get { return _character.EquippedArmor; } }
        public bool HasShield { get { return _character.HasShield; } }
        public List<int> HitDice { get { return _character.HitDice; } }
        public int Initiative { get { return _character.Initiative; } }
        public Proficiencies<AvailableInstrument> Instruments { get { return _character.Instruments; } }
        public int Level { get { return _character.Level; } } 
        public int MaxHp { get { return _character.MaxHp; } }
        public string Name { get { return _character.Name; } }
        public int ProficiencyBonus { get { return _character.ProficiencyBonus; } }
        public string Race { get { return _character.Race; } }
        public Languages Languages { get { return _character.Languages; } }
        public SortedSet<SavingThrow> SavingThrows { get { return _character.SavingThrows; } }
        public Skills Skills { get { return _character.Skills; } } 
        public virtual int Speed { get { return _character.Speed; } }
        public virtual Tools Tools { get { return _character.Tools; } } 
        public SortedSet<AvailableWeapon> WeaponProficiencies { get { return _character.WeaponProficiencies; } } 
        public SortedSet<AvailableArmor> ArmorProficiencies { get { return _character.ArmorProficiencies; } }
        public string Size { get { return _character.Size; } }
        public virtual CharacterFeatures Features { get { return _character.Features; } }
        public virtual int KiPoints { get { return _character.KiPoints; } }
        public virtual int MartialArts { get { return _character.MartialArts; } }
        public virtual int AttacksPerTurn { get { return _character.AttacksPerTurn; } }
        public virtual int SneakAttackDice { get { return _character.SneakAttackDice; } }
        public virtual SortedSet<SpellcastingClass> SpellcastingClasses { get { return _character.SpellcastingClasses; } }

        public void EquipArmor(AvailableArmor armor) { _character.EquipArmor(armor); }
        public void SetAttributes(CharacterAbilities characterAbilities) { _character.SetAttributes(characterAbilities); }
        public void ToggleShield() { _character.ToggleShield(); }
        public void SetName(string name) { _character.SetName(name); }
        public void ChooseSkill(AvailableSkill chosenSkill) { _character.ChooseSkill(chosenSkill); }
        public void LearnTool(AvailableTool chosenTool) { _character.LearnTool(chosenTool); }
        public void LearnInstrument(AvailableInstrument chosenInstrument) { _character.LearnInstrument(chosenInstrument); }
        public void LearnLanguage(AvailableLanguages chosenLanguage) { _character.LearnLanguage(chosenLanguage); }
        public void ChosePath(AvailablePaths chosenPath) { _character.ChosePath(chosenPath); }
        public void ImproveAbility(string ability)  { _character.ImproveAbility(ability); }
        public void ChooseExpertise(AvailableSkill skill) { _character.ChooseExpertise(skill); }
        public void ChooseExpertise(AvailableTool tool) { _character.ChooseExpertise(tool); }
        public int ClassLevel(string className) { return _character.ClassLevel(className); }
        internal void AddClassPaths(AvailablePaths[] paths)
        {
            foreach (var path in paths)
                ClassPath.Available.Add(path);
        }

        internal void AddClassFeature(string feature)
        {
            Features.ClassFeatures.Add(feature, CharacterData.ClassFeatures[feature]);
        }

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

    }
}