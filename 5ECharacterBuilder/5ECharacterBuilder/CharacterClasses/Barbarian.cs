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
        }

        public override int ArmorClass
        {
            get
            {
                if (EquippedArmor.Name == AvailableArmor.Cloth.ToString())
                    return 10 + Abilities.Dexterity.Modifier + Abilities.Constitution.Modifier + (HasShield ? 2 : 0);

                return base.ArmorClass;
            }
        }

        public override int AttacksPerTurn
        {
            get
            {
                if (Level >= 5)
                    return base.AttacksPerTurn + 1;

                return base.AttacksPerTurn;
            }
        }

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

                if (classLevel >= 7)
                    classFeatures.Add(GetClassFeature("Feral Instict"));

                if (classLevel >= 9)
                    classFeatures.Add(GetClassFeature("Brutal Critical"));

                if (classLevel >= 11)
                    classFeatures.Add(GetClassFeature("Relentless Rage"));

                if (classLevel >= 15)
                    classFeatures.Add(GetClassFeature("Persistent Rage"));

                if (classLevel >= 18)
                    classFeatures.Add(GetClassFeature("Indomitable Might"));

                if (classLevel >= 20)
                    classFeatures.Add(GetClassFeature("Primal Champion"));
                
                features.ClassFeatures = classFeatures.UnionDictionary(base.Features.ClassFeatures);
                //features.ClassPathFeatures = classPathFeatures;
                return features;
            }
        }

        private int GetRageDamage()
        {
            var classLevel = ClassLevel(Class);
            return classLevel >= 16 ? 4 : classLevel >= 9 ? 3 : 2;
        }

        private int GetRagesPerDay()
        {
            var classLevel = ClassLevel(Class);
            return classLevel >= 17 ? 6 : classLevel >= 12 ? 5 : classLevel >= 6 ? 4 : classLevel >= 3 ? 3 : 2;
        }

        public override Skills Skills
        {
            get
            {
                var skills = new Skills(base.Skills);
                if(Level == 1)
                    skills.Max += 2;
                return skills;
            }
        }

        public override int Speed
        {
            get
            {
                if (ClassLevel(Class) >= 5)
                    return base.Speed + 10;

                return base.Speed;
            }
        }
    }
}
