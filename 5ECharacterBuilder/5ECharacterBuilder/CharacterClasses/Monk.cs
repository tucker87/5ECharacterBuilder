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

            var skillList = new List<AvailableSkills>
            {
                AvailableSkills.Acrobatics,
                AvailableSkills.Athletics,
                AvailableSkills.History,
                AvailableSkills.Insight,
                AvailableSkills.Religion,
                AvailableSkills.Stealth
            };

            foreach (var skill in skillList)
                Skills.Available.Add(skill);

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