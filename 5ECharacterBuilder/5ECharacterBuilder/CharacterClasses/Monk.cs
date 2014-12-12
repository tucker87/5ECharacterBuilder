using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterClasses
{
    sealed class Monk : CharacterClass
    {
        private const string Class = "Monk";

        public Monk(ICharacter character) : base(character)
        {
            if (IsMulticlassing() && !MeetsRequirements())
                throw new RequirementsExpection();

            Classes.Add(Class);

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

        private bool MeetsRequirements()
        {
            return Abilities.Dexterity.Score >= 13 && Abilities.Wisdom.Score >= 13;
        }

        private bool IsMulticlassing()
        {
            return Level > 0 && ClassLevel(Class) == 0;
        }

        public override CharacterFeatures Features
        {
            get
            {
                var features = base.Features;
                var classLevel = ClassLevel(Class);
                var classFeatures = new Dictionary<string, string>(features.ClassFeatures);
                switch (classLevel)
                {
                    case 1:
                        classFeatures.Add(GetClassFeature("Unarmored Defense"));
                        classFeatures.Add(GetClassFeature("Martial Arts"));
                        break;
                    case 2:
                        classFeatures.Add(GetClassFeature("Ki"));
                        classFeatures.Add(GetClassFeature("Unarmored Movement"));
                        break;
                    case 3:
                        classFeatures.Add(GetClassFeature("Deflect Missiles"));
                        break;
                    case 4:
                        classFeatures.Add(GetClassFeature("Slow Fall"));
                        break;
                    case 5:
                        classFeatures.Add(GetClassFeature("Extra Attack"));
                        classFeatures.Add(GetClassFeature("Stunning Strike"));
                        break;
                    case 6:
                        classFeatures.Add(GetClassFeature("Ki-Empowered Strikes"));
                        break;
                    case 7:
                        classFeatures.Add(GetClassFeature("Evasion"));
                        classFeatures.Add(GetClassFeature("Stillness Of Mind"));
                        break;
                    case 10:
                        classFeatures.Add(GetClassFeature("Purity Of Body"));
                        break;
                    case 13:
                        classFeatures.Add(GetClassFeature("Tounge Of The Sun And Moon"));
                        break;
                    case 14:
                        classFeatures.Add(GetClassFeature("Diamond Soul"));
                        break;
                    case 15:
                        classFeatures.Add(GetClassFeature("Timeless Body"));
                        break;
                    case 18:
                        classFeatures.Add(GetClassFeature("Empty Body"));
                        break;
                    case 20:
                        classFeatures.Add(GetClassFeature("Perfect Self"));
                        break;
                }

                var classPathFeatures = new Dictionary<string, string>(features.ClassPathFeatures);
                if (ClassPath.Chosen != null)
                {
                    if (ClassPath.Chosen == AvailablePaths.WayOfTheOpenHand)
                    {
                        classPathFeatures.Add(GetClassFeature("Open Hand Technique"));
                        if (classLevel >= 6)
                            classPathFeatures.Add(GetClassFeature("Wholeness Of Body"));

                        if (classLevel >= 11)
                            classPathFeatures.Add(GetClassFeature("Tranquility"));

                        if (classLevel >= 17)
                            classPathFeatures.Add(GetClassFeature("Quivering Palm"));

                    }
                    if (ClassPath.Chosen == AvailablePaths.WayOfShadow)
                    {
                        classPathFeatures.Add(GetClassFeature("Shadow Arts"));
                        if (classLevel >= 6)
                            classPathFeatures.Add(GetClassFeature("Shadow Step"));

                        if (classLevel >= 11)
                            classPathFeatures.Add(GetClassFeature("Cloak Of Shadows"));

                        if (classLevel >= 17)
                            classPathFeatures.Add(GetClassFeature("Opportunist"));
                    }
                    if (ClassPath.Chosen == AvailablePaths.WayOfTheFourElements)
                        classPathFeatures.Add(GetClassFeature("Disciple Of The Elements"));
                }

                features.ClassFeatures = classFeatures;
                features.ClassPathFeatures = classPathFeatures;
                return features;
            }
        }

        public override ClassPath ClassPath
        {
            get
            {
                return ClassLevel(Class) >= 3 ? new ClassPath(base.ClassPath) {CharacterData.GetMonkPaths()} : base.ClassPath;
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

        public override SortedSet<SavingThrow> SavingThrows
        {
            get
            {
                if (ClassLevel(Class) >= 14)
                    return new SortedSet<SavingThrow>(base.SavingThrows)
                    {
                        SavingThrow.Constitution,
                        SavingThrow.Intelligence,
                        SavingThrow.Wisdom,
                        SavingThrow.Charisma
                    };

                return base.SavingThrows;
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