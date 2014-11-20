using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterClasses
{
    class Monk : CharacterClass
    {
        public Monk(ICharacter character) : base(character)
        {
            if (IsMulticlassing())
            {
                if (MeetsRequirements())
                {
                    
                }
                else return;

            }
            else
            {
                Classes.Add("Monk");

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
            }

            switch (ClassLevel("Monk"))
            {
                case 1:
                    AddClassFeature("Unarmored Defense");
                    AddClassFeature("Martial Arts");
                    break;
                case 2:
                    AddClassFeature("Ki");
                    AddClassFeature("Unarmored Movement");
                    break;
                case 3:
                    AddClassPaths(CharacterData.GetMonkPaths());
                    AddClassFeature("Deflect Missiles");
                    break;
                case 4:
                    AddClassFeature("Slow Fall");
                    break;
                case 5:
                    AddClassFeature("Extra Attack");
                    AddClassFeature("Stunning Strike");
                    break;
                case 6:
                    AddClassFeature("Ki-Empowered Strikes");
                    break;
                case 7:
                    AddClassFeature("Evasion");
                    AddClassFeature("Stillness Of Mind");
                    break;
                case 10:
                    AddClassFeature("Purity Of Body");
                    break;
                case 13:
                    AddClassFeature("Tounge Of The Sun And Moon");
                    break;
                case 14:
                    AddClassFeature("Diamond Soul");
                    SavingThrows.Add(SavingThrow.Constitution);
                    SavingThrows.Add(SavingThrow.Intelligence);
                    SavingThrows.Add(SavingThrow.Wisdom);
                    SavingThrows.Add(SavingThrow.Charisma);
                    break;
                case 15:
                    AddClassFeature("Timeless Body");
                    break;
                case 18:
                    AddClassFeature("Empty Body");
                    break;
                case 20:
                    AddClassFeature("Perfect Self");
                    break;
            }
        }

        private bool MeetsRequirements()
        {
            return Abilities.Dexterity.Score >= 13 && Abilities.Wisdom.Score >= 13;
        }

        private bool IsMulticlassing()
        {
            return Level > 0 && ClassLevel("Monk") == 0;
        }

        public override CharacterFeatures Features
        {
            get
            {
                var features = base.Features;
                var classPathFeatures = new Dictionary<string, string>();
                var classLevel = ClassLevel("Monk");
                if (ClassPath.Chosen != null)
                {
                    if (ClassPath.Chosen == AvailablePaths.WayOfTheOpenHand)
                    {
                        classPathFeatures.Add("Open Hand Technique", CharacterData.MonkFeatures["Open Hand Technique"]);
                        if (classLevel >= 6)
                            classPathFeatures.Add("Wholeness Of Body", CharacterData.MonkFeatures["Wholeness Of Body"]);

                        if (classLevel >= 11)
                            classPathFeatures.Add("Tranquility", CharacterData.MonkFeatures["Tranquility"]);

                        if (classLevel >= 17)
                            classPathFeatures.Add("Quivering Palm", CharacterData.MonkFeatures["Quivering Palm"]);

                    }
                    if (ClassPath.Chosen == AvailablePaths.WayOfShadow)
                    {
                        classPathFeatures.Add("Shadow Arts", CharacterData.MonkFeatures["Shadow Arts"]);
                        if (classLevel >= 6)
                            classPathFeatures.Add("Shadow Step", CharacterData.MonkFeatures["Shadow Step"]);

                        if (classLevel >= 11)
                            classPathFeatures.Add("Cloak Of Shadows", CharacterData.MonkFeatures["Cloak Of Shadows"]);

                        if (classLevel >= 17)
                            classPathFeatures.Add("Opportunist", CharacterData.MonkFeatures["Opportunist"]);
                    }
                    if (ClassPath.Chosen == AvailablePaths.WayOfTheFourElements)
                        classPathFeatures.Add("Disciple Of The Elements", CharacterData.MonkFeatures["Disciple Of The Elements"]);
                }
                features.ClassPathFeatures = classPathFeatures;
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