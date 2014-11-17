using System.Linq;

namespace _5ECharacterBuilder.CharacterClasses
{
    class Fighter : CharacterClass
    {
        public Fighter(ICharacter character) : base(character)
        {
            HitDice.Add(10);
            foreach (var armor in Armory.AllArmor)
                ArmorProficiencies.Add(armor);
            
            ArmorProficiencies.Add(AvailableArmor.Shield);
            foreach (var weapon in Armory.SimpleWeapons.Concat(Armory.MartialWeapons))
                WeaponProficiencies.Add(weapon);
            
            SavingThrows.Add(SavingThrow.Strength);
            SavingThrows.Add( SavingThrow.Constitution);

            Classes.Add("Fighter");

            Skills.Available.Add(AvailableSkills.Acrobatics);
            Skills.Available.Add(AvailableSkills.AnimalHandling);
            Skills.Available.Add(AvailableSkills.Athletics);
            Skills.Available.Add(AvailableSkills.History);
            Skills.Available.Add(AvailableSkills.Insight);
            Skills.Available.Add(AvailableSkills.Intimidation);
            Skills.Available.Add(AvailableSkills.Perception);
            Skills.Available.Add(AvailableSkills.Survival);

            Skills.Max = 2;
        }
    }
}
