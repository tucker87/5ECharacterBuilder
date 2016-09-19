using System.Collections.Generic;
using System.Linq;
using _5EDatabase;

namespace _5ECharacterBuilder.CharacterClasses
{
    internal abstract class CharacterClass : ICharacter
    {
        private readonly ICharacter _character;

        protected CharacterClass(ICharacter character) { _character = character; }
        
        internal virtual int CurrentLevel { get; }

        public virtual int ArmorClass => _character.ArmorClass;
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
        
        public string Background => _character.Background;
        public Class Class { get; internal set; }
        public virtual IEnumerable<Class> Classes => _character.Classes;
        public virtual List<Path> AvailablePaths => _character.AvailablePaths;
        public virtual Path? ClassPath { get; internal set; }

        public virtual List<Path> ChosenPaths => ClassPath == null ? _character.ChosenPaths : _character.ChosenPaths.Concat((Path)ClassPath).ToList();
        
        public Currency Currency => _character.Currency;
        public Armor EquippedArmor => _character.EquippedArmor;
        public bool HasShield => _character.HasShield;
        public virtual HitDice HitDice => new HitDice().Union(_character.HitDice.List).Union(AddedHitDice); 
        internal abstract int AddedHitDice { get; }
        public int Initiative => _character.Initiative;
        public Proficiencies<Instrument> Instruments => _character.Instruments;
        public int Level => _character.Level + 1;
        public int MaxHp => _character.CalculateMaxHp(HitDice);
        public string Name => _character.Name;
        public int ProficiencyBonus => CalculateProficiencyBonus(Level);
        public int CalculateProficiencyBonus(int level) => _character.CalculateProficiencyBonus(level);
        public string Race => _character.Race;
        public Languages Languages => _character.Languages;
        public virtual IEnumerable<SavingThrow> SavingThrows => _character.SavingThrows;
        public virtual Skills Skills => _character.Skills;
        public virtual int Speed => _character.Speed;
        public virtual Tools Tools => _character.Tools;
        public SortedSet<WeaponType> WeaponProficiencies => _character.WeaponProficiencies;
        public SortedSet<ArmorType> ArmorProficiencies => _character.ArmorProficiencies;
        public string Size => _character.Size;
        public virtual int MartialArts => _character.MartialArts;
        public virtual int AttacksPerTurn => _character.AttacksPerTurn;
        public virtual int SneakAttackDice => _character.SneakAttackDice;
        public virtual SortedSet<SpellcastingClass> SpellcastingClasses => _character.SpellcastingClasses;
        public virtual ClassTraits ClassTraits => _character.ClassTraits;

        public void EquipArmor(ArmorType armor) { _character.EquipArmor(armor); }
        public void SetAttributes(CharacterAbilities characterAbilities) { _character.SetAttributes(characterAbilities); }
        public void ToggleShield() { _character.ToggleShield(); }
        public void SetName(string name) { _character.SetName(name); }
        public void LearnTool(Tool chosenTool) { _character.LearnTool(chosenTool); }
        public void LearnInstrument(Instrument chosenInstrument) { _character.LearnInstrument(chosenInstrument); }
        public void LearnLanguage(Language chosenLanguage) { _character.LearnLanguage(chosenLanguage); }

        public void ChosePath(Path chosenPath)
        {
            if (AvailablePaths.Contains(chosenPath))
                ClassPath = chosenPath;
        }

        public int GetClassLevel(Class className)
        {
            return Classes.Count(x => x == className);
        }

        public int SkillBonus(Skill skill, int? profBonus = null) { return _character.SkillBonus(skill, profBonus ?? ProficiencyBonus); }
        //public void LevelUp(Class cclass) { _character.LevelUp(cclass); }

        public List<Feature> AllFeatures =>
            new List<Feature>()
                .Union(RaceFeatures)
                .Union(ClassFeatures)
                .Union(ClassPathFeatures)
                .ToList();

        public List<RaceFeature> RaceFeatures => _character.RaceFeatures;
        public List<ClassFeature> ClassFeatures =>
            _character.ClassFeatures.Union(ClassFeatureData.Where(cfd => cfd.LevelObtained <= ClassLevel)).ToList();

        public List<ClassPathFeature> ClassPathFeatures =>
            _character.ClassPathFeatures.Union(ClassPathFeatureData.Where(cfd => cfd.LevelObtained <= ClassLevel && ChosenPaths.Contains(cfd.ClassPath))).ToList();

        internal abstract List<ClassFeature> ClassFeatureData { get; }
        internal abstract List<ClassPathFeature> ClassPathFeatureData { get; }
        private int ClassLevel => GetClassLevel(Class);

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
            _character.Classes.ToList().Remove(Classes.Last());
        }

        public int CalculateMaxHp(HitDice hitDice) => _character.CalculateMaxHp(hitDice);


        public void ImproveAbility(string abilityName)
        {
            if (Abilities.ImprovementPoints <= Abilities.SpentAbilityImprovementPoints) return;
            var ability = (CharacterAbility)Abilities.GetType().GetProperty(abilityName).GetValue(Abilities);
            ability.ImprovementBonus += 1;
        }

        public void ChooseSkill(Skill chosenSkill)
        {
            if (Skills.Available.Contains(chosenSkill))
                if (Skills.Chosen.Count < Skills.Max)
                    Skills.Chosen.Add(chosenSkill);
                else
                    throw new TooManySkillsException(chosenSkill);
            else
                throw new SkillNotAvailableException(chosenSkill);
        }

        public void ChooseExpertise(Skill skill)
        {
            if (Skills.Expertise.Count + Tools.Expertise.Count < Skills.MaxExpertise && Skills.Chosen.Contains(skill))
                Skills.Expertise.Add(skill);
        }

        public void ChooseExpertise(Tool tool)
        {
            if (Skills.Expertise.Count + Tools.Expertise.Count < Skills.MaxExpertise && Tools.Chosen.Contains(tool))
                Tools.Expertise.Add(tool);
        }

        public string ClassesString
        {
            get
            {
                var classDictionary = new Dictionary<Class, int>();
                foreach (var characterClass in Classes)
                {
                    if (classDictionary.ContainsKey(characterClass))
                        classDictionary[characterClass]++;
                    else
                        classDictionary.Add(characterClass, 1);
                }

                return classDictionary.Aggregate("", (current, @class) => current + $" {@class.Key} {@class.Value}").Trim();

            }
        }
    }
}