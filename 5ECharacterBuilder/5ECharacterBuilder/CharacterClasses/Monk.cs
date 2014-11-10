using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterClasses
{
    class Monk : CharacterClass
    {
        public Monk(ICharacter character): base(character)
        {
            HitDice.Add(8);
            
            WeaponProficiencies.AddRange(new List<AvailableWeapon>( Armory.SimpleWeapons){ AvailableWeapon.ShortSword });

            SavingThrowProficiencies.AddRange(new List<SavingThrow> {SavingThrow.Strength, SavingThrow.Dexterity});

            Classes.Add("Monk");
            AvailableSkills.AddRange(new List<AvailableSkill>{ 
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

        public override int SkillCount
        {
            get { return 2; }
        }

        public override List<string> RuleIssues
        {
            get
            {
                var currentIssues = base.RuleIssues;
                if (AvailableToolProficiencies.Count == 0 && AvailableInstrumentProficiencies.Count == 0)
                    currentIssues.Add("Has not chosen a Monk Tool or Instrument");

                if (AvailableToolProficiencies.Count > 0 && AvailableInstrumentProficiencies.Count > 0)
                    currentIssues.Add("Monks can only choose an Instrument or a Tool");

                return currentIssues;
            }
        }
    }
}

