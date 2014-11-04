using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterClasses
{
    internal sealed class Monk : CharacterClass
    {
        public Monk(Character character, List<AvailableSkill> skillList = null): base(character)
        {
            HitDice.Add(8);
            
            WeaponProficiencies.AddRange(new List<AvailableWeapon>( Armory.SimpleWeapons){ AvailableWeapon.ShortSword });

            SavingThrowProficiencies.AddRange(new List<SavingThrow> {SavingThrow.Strength, SavingThrow.Dexterity});

            Class += "Monk";
            ClassSkillCount = 2;
            ClassSkills.AddRange(new List<AvailableSkill>{ 
                    AvailableSkill.Acrobatics,
                    AvailableSkill.Athletics,
                    AvailableSkill.History,
                    AvailableSkill.Insight,
                    AvailableSkill.Religion,
                    AvailableSkill.Stealth
                });
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
        public override List<string> RuleIssues
        {
            get
            {
                var currentIssues = base.RuleIssues;
                if (ToolProficiencies.Count > 0 && InstrumentProficiencies.Count > 0)
                    currentIssues.Add("Monks can only choose an Instrument or a Tool");

                return currentIssues;
            }
        }
    }
}

