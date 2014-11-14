using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder
{
    public interface ICharacter
    {
        int ArmorClass { get; }
        SortedSet<AvailableArmor> ArmorProficiencies { get; }
        CharacterAttributes Attributes { get; }
        string Background { get; }
        List<string> Classes { get; }
        string ClassesString { get; }
        Currency Currency { get; }
        Armor EquippedArmor { get; }
        bool HasShield { get; }
        List<int> HitDice { get; }
        int Initiative { get; }
        Proficiencies<AvailableInstrument> Instruments { get; }
        int MaxHp { get; }
        string Name { get; }
        string Race { get; }
        Languages Languages { get; }
        SortedSet<SavingThrow> SavingThrows { get; }
        string Size { get; }
        Proficiencies<AvailableSkill> Skills { get; }
        int Speed { get; }
        Proficiencies<AvailableTool> Tools { get; }
        SortedSet<AvailableWeapon> WeaponProficiencies { get; }
        CharacterFeatures Features { get; }

        void EquipArmor(AvailableArmor armor);
        void SetAttributes(CharacterAttributes characterAttributes);
        void ToggleShield();
        void SetName(string name);
        void LearnSkill(AvailableSkill chosenSkill);
        void LearnTool(AvailableTool chosenTool);
        void LearnInstrument(AvailableInstrument chosenInstrument);
        void LearnLanguage(AvailableLanguages chosenLanguage);
    }
    
    class CharacterBase : ICharacter
    {
        public CharacterBase(CharacterAttributeScores attributeScores = null, string name = "")
        {
            Name = name;

            EquipArmor(AvailableArmor.Cloth);
            
            attributeScores = attributeScores ?? new CharacterAttributeScores();
            Attributes = new CharacterAttributes(attributeScores);
            ArmorProficiencies = new SortedSet<AvailableArmor>(new List<AvailableArmor>());
            Instruments = new Proficiencies<AvailableInstrument>();
            Languages = new Languages();
            Tools = new Proficiencies<AvailableTool>();
            Skills = new Proficiencies<AvailableSkill>();
            WeaponProficiencies = new SortedSet<AvailableWeapon>(new List<AvailableWeapon>());
            SavingThrows = new SortedSet<SavingThrow>(new List<SavingThrow>());
            Features = new CharacterFeatures();
            HitDice = new List<int>(new int[0]);
            Currency = new Currency();
            Classes = new List<string>();
        }

        private int ShieldBonus { get { return HasShield ? 2 : 0; } }

        public int ArmorClass { get { return GetArmorClassBonus(EquippedArmor, Attributes.Dexterity.Modifier) + ShieldBonus; } }
        public SortedSet<AvailableArmor> ArmorProficiencies { get; private set; }
        public CharacterAttributes Attributes { get; private set; }
        public string Background { get; private set; }
        public List<string> Classes { get; private set; }
        public string ClassesString { get; private set; }
        public Currency Currency { get; private set; }
        public Armor EquippedArmor { get; private set; }
        public bool HasShield { get; private set; }
        public List<int> HitDice { get; private set; }
        public int Initiative { get; private set; }
        public Proficiencies<AvailableInstrument> Instruments { get; private set; }
        public int MaxHp { get { return CalculateMaxHp(HitDice, Attributes.Constitution.Modifier); } }
        public string Name { get; private set; }
        public string Race { get; private set; }
        public Languages Languages { get; private set; }
        public SortedSet<SavingThrow> SavingThrows { get; private set; }
        public string Size { get; private set; }
        public Proficiencies<AvailableSkill> Skills { get; private set; }
        public int Speed { get; private set; }
        public Proficiencies<AvailableTool> Tools { get; private set; }
        public SortedSet<AvailableWeapon> WeaponProficiencies { get; private set; }
        public CharacterFeatures Features { get; private set; }
        public void EquipArmor(AvailableArmor armor) { EquippedArmor = Armory.GetArmor(armor); }
        
        public void SetAttributes(CharacterAttributes characterAttributes)
        {
            var racialBonuses = new RacialBonuses(
                Attributes.Strength.RacialBonus,
                Attributes.Dexterity.RacialBonus,
                Attributes.Constitution.RacialBonus,
                Attributes.Intelligence.RacialBonus,
                Attributes.Wisdom.RacialBonus,
                Attributes.Charisma.RacialBonus);
            Attributes = new CharacterAttributes(characterAttributes, racialBonuses);
        }

        public void ToggleShield()
        {
            HasShield = !HasShield;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void LearnSkill(AvailableSkill chosenSkill)
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

