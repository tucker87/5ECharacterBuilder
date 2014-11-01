using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace _5ECharacterBuilder.CharacterClasses
{
    internal sealed class Monk : CharacterClass
    {
        public Monk(ICharacter character, List<AvailableSkill> skillList = null) : base(character)
        {
            HitDice.Add(8);

            AddWeaponProfs(Armory.SimpleWeapons);
            AddWeaponProfs(new List<AvailableWeapon> {AvailableWeapon.ShortSword});

            AddSavingThrows(new List<SavingThrow> {SavingThrow.Strength, SavingThrow.Dexterity});
            if (skillList != null)
                PickSkills(skillList);
        }

        public override string Class
        {
            get { return "Monk"; }
        }

        public override int ClassSkillCount
        {
            get { return 2; }
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

        public override ReadOnlyCollection<AvailableSkill> ClassSkills
        {
            get
            {
                return new ReadOnlyCollection<AvailableSkill>(new[]
                {
                    AvailableSkill.Acrobatics,
                    AvailableSkill.Athletics,
                    AvailableSkill.History,
                    AvailableSkill.Insight,
                    AvailableSkill.Religion,
                    AvailableSkill.Stealth
                });
            }
        }

        public override List<string> RuleIssues
        {
            get
            {
                var currentIssues = base.RuleIssues;
                if (ToolProficiencies.Count > 0 && InstrumentProficiencies.Count > 0)
                    currentIssues.Add("Monks can only choose an instrument or a Tool");

                return currentIssues;
            }
        }

    }
}

