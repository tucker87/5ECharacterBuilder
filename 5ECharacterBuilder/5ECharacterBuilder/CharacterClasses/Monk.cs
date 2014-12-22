using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterClasses
{
    sealed class Monk : CharacterClass
    {
        public const string Class = "Monk";

        public Monk(ICharacter character) : base(character)
        {
            if (IsMulticlassing(Class) && !MeetsRequirements())
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

        public override CharacterFeatures Features
        {
            get
            {
                var features = base.Features;
                var classLevel = ClassLevel(Class);
                features.ClassFeatures = GetClassFeatures(classLevel).UnionDictionary(features.ClassFeatures);
                features.ClassPathFeatures = GetClassPathFeatures(classLevel).UnionDictionary(features.ClassPathFeatures);
                return features;
            }
        }

        private Dictionary<string, string> GetClassPathFeatures(int classLevel)
        {
            var classPathFeatures = new Dictionary<string, string>();
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
            return classPathFeatures;
        }

        private static Dictionary<string, string> GetClassFeatures(int classLevel)
        {
            var classFeatures = new Dictionary<string, string>();

            classFeatures.Add(GetClassFeature("Monk Unarmored Defense"));
            classFeatures.Add(GetClassFeature("Martial Arts"));

            if (classLevel >= 2)
            {
                classFeatures.Add(GetClassFeature("Ki"));
                classFeatures.Add(GetClassFeature("Unarmored Movement"));
            }
            if (classLevel >= 3)
                classFeatures.Add(GetClassFeature("Deflect Missiles"));

            if (classLevel >= 4)
                classFeatures.Add(GetClassFeature("Slow Fall"));

            if (classLevel >= 5)
            {
                classFeatures.Add(GetClassFeature("Extra Attack"));
                classFeatures.Add(GetClassFeature("Stunning Strike"));
            }
            if (classLevel >= 6)
                classFeatures.Add(GetClassFeature("Ki-Empowered Strikes"));

            if (classLevel >= 7)
            {
                classFeatures.Add(GetClassFeature("Evasion"));
                classFeatures.Add(GetClassFeature("Stillness Of Mind"));
            }
            if (classLevel >= 10)
                classFeatures.Add(GetClassFeature("Purity Of Body"));

            if (classLevel >= 13)
                classFeatures.Add(GetClassFeature("Tounge Of The Sun And Moon"));

            if (classLevel >= 14)
                classFeatures.Add(GetClassFeature("Diamond Soul"));

            if (classLevel >= 15)
                classFeatures.Add(GetClassFeature("Timeless Body"));

            if (classLevel >= 18)
                classFeatures.Add(GetClassFeature("Empty Body"));

            if (classLevel >= 20)
                classFeatures.Add(GetClassFeature("Perfect Self"));
            return classFeatures;
        }

        public override ClassPath ClassPath
        {
            get
            {
                return ClassLevel(Class) >= 3 ? new ClassPath(base.ClassPath) {CharacterData.MonkPaths} : base.ClassPath;
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
                if (ClassLevel(Class) + 1 >= 5)
                    return base.AttacksPerTurn + 1;

                return base.AttacksPerTurn;
            }
        }

        public override ClassTraits ClassTraits
        {
            get
            {
                var classTraits = base.ClassTraits;
                classTraits.KiPoints = Level >= 2 ? Level : classTraits.KiPoints;
                return classTraits;
            }
        }

        public override int MartialArts
        {
            get
            {
                var classLevel = ClassLevel(Class);
                if (classLevel >= 17)
                    return 10;

                if (classLevel >= 11)
                    return 8;

                if (classLevel >= 5)
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
                var classLevel = ClassLevel(Class);

                if (classLevel >= 18)
                    return base.Speed + 30;

                if (classLevel >= 14)
                    return base.Speed + 25;

                if (classLevel >= 10)
                    return base.Speed + 20;

                if (classLevel >= 6)
                    return base.Speed + 15;

                if (classLevel >= 2)
                    return base.Speed + 10;

                return base.Speed;
            }
        }
    }
}