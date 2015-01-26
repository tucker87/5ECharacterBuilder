using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterClasses
{
    sealed class Barbarian : CharacterClass
    {
        private const string Class = "Barbarian";

        public Barbarian(ICharacter character) : base(character)
        {
            HitDice.Add(12);
            ArmorProficiencies.UnionWith(Armory.LightArmor);
            ArmorProficiencies.UnionWith(Armory.MediumArmor);
            ArmorProficiencies.Add(AvailableArmor.Shield);

            WeaponProficiencies.UnionWith(Armory.SimpleWeapons);
            WeaponProficiencies.UnionWith(Armory.MartialWeapons);

            SavingThrows.Add(SavingThrow.Strength);
            SavingThrows.Add(SavingThrow.Constitution);

            Classes.Add(Class);

            Skills.Available.UnionWith(new[]
                {
                    AvailableSkill.AnimalHandling,
                    AvailableSkill.Athletics,
                    AvailableSkill.Intimidation,
                    AvailableSkill.Nature,
                    AvailableSkill.Perception,
                    AvailableSkill.Survival
                });

            Skills.Max += 2;
        }
        
        public override CharacterAbilities Abilities => ClassLevel(Class) < 20 
            ? base.Abilities
            : new CharacterAbilities(base.Abilities)
            {
                Strength = { ClassBonus = 4, MaxScore = 24},
                Constitution = { ClassBonus = 4, MaxScore = 24}
            };

        public override int ArmorClass => EquippedArmor.Name != AvailableArmor.Cloth.ToString()
            ? base.ArmorClass
            : 10 + Abilities.Dexterity.Modifier + Abilities.Constitution.Modifier + (HasShield ? 2 : 0);

        public override int AttacksPerTurn => Level >= 5 
            ? base.AttacksPerTurn + 1
            : base.AttacksPerTurn;

        public override ClassTraits ClassTraits
        {
            get
            {
                var classTraits = base.ClassTraits;
                classTraits.RagesPerDay = GetRagesPerDay();
                classTraits.RageDamage = GetRageDamage();
                return classTraits;
            }
        }

        public override CharacterFeatures Features
        {
            get
            {
                var features = base.Features;
                var classLevel = ClassLevel(Class);
                
                features.ClassFeatures = GetClassFeatures(classLevel).UnionDictionary(base.Features.ClassFeatures);
                features.ClassPathFeatures = GetClassPathFeatures(classLevel).UnionDictionary(base.Features.ClassPathFeatures);
                return features;
            }
        }

        private static Dictionary<string, string> GetClassFeatures(int classLevel)
        {
            var classFeatures = new Dictionary<string, string>();

            classFeatures.Add(GetClassFeature("Rage"));
            classFeatures.Add(GetClassFeature("Barbarian Unarmored Defense"));

            if (classLevel >= 2)
            {
                classFeatures.Add(GetClassFeature("Reckless Attack"));
                classFeatures.Add(GetClassFeature("Danger Sense"));
            }

            if (classLevel >= 5)
            {
                classFeatures.Add(GetClassFeature("Extra Attack"));
                classFeatures.Add(GetClassFeature("Fast Movement"));
            }

            if (classLevel >= 7) classFeatures.Add(GetClassFeature("Feral Instict"));
            if (classLevel >= 9) classFeatures.Add(GetClassFeature("Brutal Critical"));
            if (classLevel >= 11) classFeatures.Add(GetClassFeature("Relentless Rage"));
            if (classLevel >= 15) classFeatures.Add(GetClassFeature("Persistent Rage"));
            if (classLevel >= 18) classFeatures.Add(GetClassFeature("Indomitable Might"));
            if (classLevel >= 20) classFeatures.Add(GetClassFeature("Primal Champion"));

            return classFeatures;
        }

        private Dictionary<string, string> GetClassPathFeatures(int classLevel)
        {
            var classPathFeatures = new Dictionary<string, string>();
            if (ClassPath.Chosen == AvailablePaths.PathOfTheBerserker)
            {
                classPathFeatures.Add(GetClassFeature("Frenzy"));
                if (classLevel >= 6) classPathFeatures.Add(GetClassFeature("Mindless Rage"));
                if (classLevel >= 10) classPathFeatures.Add(GetClassFeature("Intimidating Presence"));
                if (classLevel >= 14) classPathFeatures.Add(GetClassFeature("Retaliation"));
            }

            if (ClassPath.Chosen == AvailablePaths.PathOfTheTotemWarrior)
            {
                classPathFeatures.Add(GetClassFeature("Spirit Seeker"));
                classPathFeatures.Add(GetClassFeature("Totem Spirit"));
                if (classLevel >= 6) classPathFeatures.Add(GetClassFeature("Aspect of the Beast"));
                if (classLevel >= 10) classPathFeatures.Add(GetClassFeature("Spirit Walker"));
                if (classLevel >= 14) classPathFeatures.Add(GetClassFeature("Totemic Attunement"));
            }
            return classPathFeatures;
        }

        public override ClassPath ClassPath => ClassLevel(Class) >= 3 ? new ClassPath(base.ClassPath) { CharacterData.GetBarbarianPaths() } : base.ClassPath;

        private int GetRageDamage()
        {
            var classLevel = ClassLevel(Class);
            return classLevel >= 16 ? 4 : classLevel >= 9 ? 3 : 2;
        }

        private int GetRagesPerDay()
        {
            var classLevel = ClassLevel(Class);
            return classLevel >= 20 ? 100 : classLevel >= 17 ? 6 : classLevel >= 12 ? 5 : classLevel >= 6 ? 4 : classLevel >= 3 ? 3 : 2;
        }

        public override int Speed => ClassLevel(Class) >= 5 ? base.Speed + 10 : base.Speed;
    }
}
