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

            SavingThrows.Add(SavingThrow.Strength);
            SavingThrows.Add(SavingThrow.Dexterity);

            var skillList = new List<AvailableSkill>
            {
                AvailableSkill.Acrobatics,
                AvailableSkill.Athletics,
                AvailableSkill.History,
                AvailableSkill.Insight,
                AvailableSkill.Religion,
                AvailableSkill.Stealth
            };

            foreach (var skill in skillList)
                Skills.Available.Add(skill);

            Skills.Max += 2;

            Classes.Add("Monk");
            switch (Level)
            {
                case 1:
                    AddClassFeature("Unarmored Defense");
                    AddClassFeature("Martial Arts");
                    break;
                case 2:
                    AddClassFeature("Ki");
                    AddClassFeature("Unarmored Movement");
                    break;
            }
        }

        private void AddClassFeature(string feature)
        {
            Features.ClassFeatures.Add(MonkFeatures.Find(f => f.Name == feature));
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

        public override int KiPoints 
        {
            get
            {
                return Level >= 2 ? Level : base.KiPoints;
            }
        }

        public override int MartialArts
        {
            get
            {
                if (Level >= 17)
                    return 10;

                if (Level >= 11)
                    return 8;

                if (Level >= 5)
                    return 6;

                return 4;
            }
        }

        public override int Speed 
        {
            get
            {
                if (Level >= 18)
                    return base.Speed + 30;
                if (Level >= 14)
                    return base.Speed + 25;
                if (Level >= 10)
                    return base.Speed + 20;
                if (Level >= 6)
                    return base.Speed + 15;
                if (Level >= 2)
                    return base.Speed + 10;

                return base.Speed;
            }
        }

        private static List<Feature> MonkFeatures
        {
            get
            {
                return new List<Feature>
                {
                    new Feature("Unarmored Defense",
                        "Beginning at 1st level, while you are wearing no armor and not wielding a shield, your AC equals 10 + your Dexterity modifier + your Wisdom modifier."),
                    new Feature("Martial Arts",
                        "You gain the following benefits while you are unarmed or wielding only monk weapons and you aren’t wearing armor or wielding a shield: You can use Dexterity instead of Strength for the attack and damage rolls of your unarmed strikes and monk weapons. You can roll a d4 in place of the normal damage of your unarmed strike or monk weapon. This die changes as you gain monk levels, as shown in the Martial Arts column of the Monk table. When you use the Attack action with an unarmed strike or a monk weapon on your turn, you can make one unarmed strike as a bonus action. For example, if you take the Attack action and attack with a quarter- staff, you can also make an unarmed strike as a bonus action, assuming you haven't already taken a bonus action this turn."),
                    new Feature("Ki",
                        "THIS THING IS LONG!"),
                    new Feature("Unarmored Movement",
                        "Starting at 2nd level, your speed increases by 10 feet while you are not wearing armor or wielding a shield. This bonus increases when you reach certain monk levels, as shown in the Monk table. At 9th level, you gain the ability to move along vertical surfaces and across liquids on your turn without falling during the move."),
                            
                };
            }
        }
    }
}