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

            Skills.Available.Add(AvailableSkill.Acrobatics);
            Skills.Available.Add(AvailableSkill.AnimalHandling);
            Skills.Available.Add(AvailableSkill.Athletics);
            Skills.Available.Add(AvailableSkill.History);
            Skills.Available.Add(AvailableSkill.Insight);
            Skills.Available.Add(AvailableSkill.Intimidation);
            Skills.Available.Add(AvailableSkill.Perception);
            Skills.Available.Add(AvailableSkill.Survival);

            Skills.Max = 2;
        }
    }
}
