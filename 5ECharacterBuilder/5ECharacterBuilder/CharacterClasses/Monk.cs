using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterClasses
{
    class Monk : CharacterClass
    {
        public Monk(ICharacter character): base(character)
        {
            HitDice.Add(8);

            WeaponProficiencies.UnionWith(Armory.SimpleWeapons);

            WeaponProficiencies.Add(AvailableWeapon.ShortSword);

            SavingThrows.Add(SavingThrow.Strength);
            SavingThrows.Add(SavingThrow.Dexterity);
            
            Skills.Available.UnionWith(new[]
            {
                AvailableSkill.Acrobatics,
                AvailableSkill.Athletics,
                AvailableSkill.History,
                AvailableSkill.Insight,
                AvailableSkill.Religion,
                AvailableSkill.Stealth
            });

            Skills.Max += 2;

            Classes.Add("Monk");
            switch (Level)
            {
                case 1:
                    AddMonkFeature("Unarmored Defense");
                    AddMonkFeature("Martial Arts");
                    break;
                case 2:
                    AddMonkFeature("Ki");
                    AddMonkFeature("Unarmored Movement");
                    break;
                case 3:
                    AddClassPaths(CharacterData.GetMonkPaths());
                    AddMonkFeature("Deflect Missiles");
                    break;
                case 4:
                    AddMonkFeature("Slow Fall");
                    break;
                case 5:
                    AddClassFeature("Extra Attack");
                    AddMonkFeature("Stunning Strike");
                    break;
                case 6:
                    AddMonkFeature("Ki-Empowered Strikes");
                    break;
                case 7:
                    AddClassFeature("Evasion");
                    AddMonkFeature("Stillness Of Mind");
                    break;
                case 10:
                    AddMonkFeature("Purity Of Body");
                    break;
                case 13:
                    AddMonkFeature("Tounge Of The Sun And Moon");
                    break;
                case 14:
                    AddMonkFeature("Diamond Soul");
                    SavingThrows.Add(SavingThrow.Constitution);
                    SavingThrows.Add(SavingThrow.Intelligence);
                    SavingThrows.Add(SavingThrow.Wisdom);
                    SavingThrows.Add(SavingThrow.Charisma);
                    break;
                case 15:
                    AddMonkFeature("Timeless Body");
                    break;
                case 18:
                    AddMonkFeature("Empty Body");
                    break;
                case 20:
                    AddMonkFeature("Perfect Self");
                    break;
            }
        }

        private void AddMonkFeature(string feature)
        {
            Features.ClassFeatures.Add(feature, CharacterData.MonkFeatures[feature]);
        }

        public override CharacterFeatures Features
        {
            get
            {
                var features = base.Features;
                if (ClassPath.Chosen != null)
                    features.ClassPathFeatures = new Dictionary<string, string>(CharacterData.GetMonkPathFeatures((AvailablePaths) ClassPath.Chosen, Level));

                return features;
            }
        }

        public override int ArmorClass
        {
            get
            {
                if (EquippedArmor.Name == AvailableArmor.Cloth.ToString() && !HasShield)
                    return 10 + Abilities.Dexterity.Modifier + Abilities.Wisdom.Modifier;

                return base.ArmorClass;
            }
        }

        public override int AttacksPerTurn
        {
            get
            {
                if (Level + 1 >= 5)
                    return base.AttacksPerTurn + 1;

                return base.AttacksPerTurn;
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
    }
}