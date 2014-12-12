using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder.CharacterClasses
{
    class CharacterClass : ICharacter
    {
        private readonly ICharacter _character;

        protected CharacterClass(ICharacter character) { _character = character; }

        protected CharacterClass() { throw new System.NotImplementedException(); }
        
        public virtual int ArmorClass { get { return _character.ArmorClass; } }

        public virtual CharacterAbilities Abilities
        {
            get
            {
                var abilities = new CharacterAbilities(_character.Abilities)
                {
                    ImprovementPoints = GetImprovementPoints()
                };
                return abilities;
            }
        }

        public string Background { get { return _character.Background; } }
        public List<string> Classes { get { return _character.Classes; } }
        public virtual ClassPath ClassPath { get { return _character.ClassPath; } }
        public Currency Currency { get { return _character.Currency; } }
        public Armor EquippedArmor { get { return _character.EquippedArmor; } }
        public bool HasShield { get { return _character.HasShield; } }
        public HitDice HitDice { get { return _character.HitDice; } }
        public int Initiative { get { return _character.Initiative; } }
        public Proficiencies<AvailableInstrument> Instruments { get { return _character.Instruments; } }
        public int Level { get { return _character.Level; } } 
        public int MaxHp { get { return _character.MaxHp; } }
        public string Name { get { return _character.Name; } }
        public int ProficiencyBonus { get { return _character.ProficiencyBonus; } }
        public string Race { get { return _character.Race; } }
        public Languages Languages { get { return _character.Languages; } }
        public virtual SortedSet<SavingThrow> SavingThrows { get { return _character.SavingThrows; } }
        public virtual Skills Skills { get { return _character.Skills; } } 
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
        public void LearnTool(AvailableTool chosenTool) { _character.LearnTool(chosenTool); }
        public void LearnInstrument(AvailableInstrument chosenInstrument) { _character.LearnInstrument(chosenInstrument); }
        public void LearnLanguage(AvailableLanguages chosenLanguage) { _character.LearnLanguage(chosenLanguage); }

        public void ChosePath(AvailablePaths chosenPath)
        {
            if (ClassPath.Available.Contains(chosenPath))
                _character.ClassPath.Chosen = chosenPath;
        }
        public int ClassLevel(string className) { return _character.ClassLevel(className); }
        public int SkillBonus(AvailableSkill skill) { return _character.SkillBonus(skill); }
        public void LevelUp(AvailableClasses cclass) { _character.LevelUp(cclass); }

        internal static Dictionary<string, string> GetClassFeature(string feature)
        {
            return new Dictionary<string, string> {{feature, CharacterData.ClassFeatures[feature]}};
        }

        internal static Dictionary<string, string> GetClassFeature(IEnumerable<AvailablePaths> paths)
        {
            var features = new Dictionary<string, string>();
            foreach (var path in paths)
                features.Add(GetClassFeature("" + path));

            return features;
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

        public void LevelDown()
        {
            _character.HitDice.Remove(HitDice.Last());
            _character.Classes.Remove(Classes.Last());
        }

        public void ImproveAbility(string abilityName)
        {
            if (Abilities.ImprovementPoints <= Abilities.SpentAbilityImprovementPoints) return;
            var ability = (CharacterAbility)Abilities.GetType().GetProperty(abilityName).GetValue(Abilities);
            ability.ImprovementBonus += 1;
        }

        public void ChooseSkill(AvailableSkill chosenSkill)
        {
            if (Skills.Available.Contains(chosenSkill))
                if (Skills.Chosen.Count < Skills.Max)
                    Skills.Chosen.Add(chosenSkill);
                else
                    throw new TooManySkillsException(chosenSkill);
            else
                throw new SkillNotAvailableException(chosenSkill);
        }

        public void ChooseExpertise(AvailableSkill skill)
        {
            if (Skills.Expertise.Count + Tools.Expertise.Count < Skills.MaxExpertise)
                if (Skills.Chosen.Contains(skill))
                    Skills.Expertise.Add(skill);
        }

        public void ChooseExpertise(AvailableTool tool)
        {
            if (Skills.Expertise.Count + Tools.Expertise.Count < Skills.MaxExpertise)
                if (Tools.Chosen.Contains(tool))
                    Tools.Expertise.Add(tool);
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