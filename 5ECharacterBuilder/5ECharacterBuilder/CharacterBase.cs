using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder
{
    public interface ICharacter
    {
        int ArmorClass { get; }
        SortedSet<AvailableArmor> ArmorProficiencies { get; }
        CharacterAbilities Abilities { get; }
        string Background { get; }
        List<string> Classes { get; }
        ClassPath ClassPath { get; } 
        string ClassesString { get; }
        Currency Currency { get; }
        Armor EquippedArmor { get; }
        bool HasShield { get; }
        List<int> HitDice { get; }
        int Initiative { get; }
        Proficiencies<AvailableInstrument> Instruments { get; }
        int Level { get; }
        int MaxHp { get; }
        string Name { get; }
        int ProficiencyBonus { get; }
        string Race { get; }
        Languages Languages { get; }
        SortedSet<SavingThrow> SavingThrows { get; }
        string Size { get; }
        Proficiencies<AvailableSkills> Skills { get; }
        int Speed { get; }
        Proficiencies<AvailableTool> Tools { get; }
        SortedSet<AvailableWeapon> WeaponProficiencies { get; }
        CharacterFeatures Features { get; }
        int KiPoints { get; }
        int MartialArts { get; }
        void EquipArmor(AvailableArmor armor);
        void SetAttributes(CharacterAbilities characterAbilities);
        void ToggleShield();
        void SetName(string name);
        void LearnSkill(AvailableSkills chosenSkill);
        void LearnTool(AvailableTool chosenTool);
        void LearnInstrument(AvailableInstrument chosenInstrument);
        void LearnLanguage(AvailableLanguages chosenLanguage);
        void ChosePath(AvailablePaths chosenPath);
        void ImproveAbility(string ability);
    }
    
    class CharacterBase : ICharacter
    {
        public CharacterBase(CharacterAbilityScores abilityScores = null, string name = "")
        {
            Name = name;

            EquipArmor(AvailableArmor.Cloth);
            
            abilityScores = abilityScores ?? new CharacterAbilityScores();
            Abilities = new CharacterAbilities(abilityScores);
            ArmorProficiencies = new SortedSet<AvailableArmor>(new List<AvailableArmor>());
            ClassPath = new ClassPath();
            Instruments = new Proficiencies<AvailableInstrument>();
            Languages = new Languages();
            Tools = new Proficiencies<AvailableTool>();
            Skills = new Proficiencies<AvailableSkills>();
            WeaponProficiencies = new SortedSet<AvailableWeapon>(new List<AvailableWeapon>());
            SavingThrows = new SortedSet<SavingThrow>(new List<SavingThrow>());
            Features = new CharacterFeatures();
            HitDice = new List<int>(new int[0]);
            Currency = new Currency();
            Classes = new List<string>();
        }

        private int ShieldBonus { get { return HasShield ? 2 : 0; } }

        public int ArmorClass { get { return GetArmorClassBonus(EquippedArmor, Abilities.Dexterity.Modifier) + ShieldBonus; } }
        public SortedSet<AvailableArmor> ArmorProficiencies { get; private set; }
        public CharacterAbilities Abilities { get; private set; }
        public string Background { get; private set; }
        public List<string> Classes { get; private set; }
        public ClassPath ClassPath { get; private set; }
        public string ClassesString { get; private set; }
        public Currency Currency { get; private set; }
        public Armor EquippedArmor { get; private set; }
        public bool HasShield { get; private set; }
        public List<int> HitDice { get; private set; }
        public int Initiative { get; private set; }
        public Proficiencies<AvailableInstrument> Instruments { get; private set; }
        public int Level { get { return Classes.Count; } }
        public int MaxHp { get { return CalculateMaxHp(HitDice, Abilities.Constitution.Modifier); } }
        public string Name { get; private set; }

        public int ProficiencyBonus
        {
            get
            {
                if (Level >= 17)
                    return 6;

                if (Level >= 13)
                    return 5;

                if (Level >= 9)
                    return 4;

                if (Level >= 5)
                    return 3;

                return 2;
            }
        }

        public string Race { get; private set; }
        public Languages Languages { get; private set; }
        public SortedSet<SavingThrow> SavingThrows { get; private set; }
        public string Size { get; private set; }
        public Proficiencies<AvailableSkills> Skills { get; private set; }
        public int Speed { get; private set; }
        public Proficiencies<AvailableTool> Tools { get; private set; }
        public SortedSet<AvailableWeapon> WeaponProficiencies { get; private set; }
        public CharacterFeatures Features { get; private set; }
        public int KiPoints { get; private set; }
        public int MartialArts { get; private set; }
        
        public void EquipArmor(AvailableArmor armor) { EquippedArmor = Armory.GetArmor(armor); }

        public void SetAttributes(CharacterAbilities characterAbilities)
        {
            var racialBonuses = new RacialBonuses(
                Abilities.Strength.RacialBonus,
                Abilities.Dexterity.RacialBonus,
                Abilities.Constitution.RacialBonus,
                Abilities.Intelligence.RacialBonus,
                Abilities.Wisdom.RacialBonus,
                Abilities.Charisma.RacialBonus);
            Abilities = new CharacterAbilities(characterAbilities, racialBonuses);
        }

        public void ToggleShield()
        {
            HasShield = !HasShield;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void LearnSkill(AvailableSkills chosenSkill)
        {
            if (Skills.Available.Contains(chosenSkill) && Skills.Chosen.Count < Skills.Max)
                Skills.Chosen.Add(chosenSkill);
        }

        public void LearnTool(AvailableTool chosenTool)
        {
            Tools.Chosen.Add(chosenTool);
        }

        public void LearnInstrument(AvailableInstrument chosenInstrument)
        {
            Instruments.Chosen.Add(chosenInstrument);
        }

        public void LearnLanguage(AvailableLanguages chosenLanguage)
        {
            Languages.Chosen.Add(chosenLanguage);
        }

        public void ChosePath(AvailablePaths chosenPath)
        {
            ClassPath.Chosen = chosenPath;
        }

        public void ImproveAbility(string abilityName)
        {
            if (Abilities.ImprovementPoints <= Abilities.SpentAbilityImprovementPoints) return;
            var ability = (CharacterAbility) Abilities.GetType().GetProperty(abilityName).GetValue(Abilities);
            ability.ImprovementBonus += 1;
        }

        private static int GetArmorClassBonus(Armor armor, int dex)
        {
            if (armor.MaxDexBonus == -1)
                return armor.BaseArmor + dex;

            if (dex > armor.MaxDexBonus)
                dex = armor.MaxDexBonus;

            return armor.BaseArmor + dex;
        }

        private static int CalculateMaxHp(List<int> hitDice, int constitutionMod)
        {
            return hitDice[0] + hitDice.GetRange(1, hitDice.Count - 1).Sum(hitDie => (hitDie / 2) + 1) + constitutionMod;
        }
    }
}

