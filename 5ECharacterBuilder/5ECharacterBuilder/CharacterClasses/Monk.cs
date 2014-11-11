using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterClasses
{
    class Monk : CharacterClass
    {
        public Monk(ICharacter character): base(character)
        {
            HitDice.Add(8);

            foreach (var weapon in Armory.SimpleWeapons)
                WeaponProficiencies.Add(weapon);

            WeaponProficiencies.Add(AvailableWeapon.ShortSword);

            SavingThrowProficiencies.Add(SavingThrow.Strength);
            SavingThrowProficiencies.Add(SavingThrow.Dexterity);

            Classes.Add("Monk");
            var skillList = new List<AvailableSkill>
            {
                AvailableSkill.Acrobatics,
                AvailableSkill.Athletics,
                AvailableSkill.History,
                AvailableSkill.Insight,
                AvailableSkill.Religion,
                AvailableSkill.Stealth
            };
            foreach (var availableSkill in skillList)
                if (!AvailableSkills.Contains(availableSkill))
                    AvailableSkills.Add(availableSkill);
            
        }

        public override int ArmorClass
        {
            get
            {
                if (EquippedArmor.Name == AvailableArmor.Cloth.ToString() && !HasShield)
                    return 10 + Attributes.Dexterity.Modifier + Attributes.Wisdom.Modifier;

                return base.ArmorClass;
            }
        }

        public override int SkillCount
        {
            get { return 2; }
        }

        public override int ToolProficiencyCount
        {
            get { return base.ToolProficiencyCount + 1; }
        }
    }
}

