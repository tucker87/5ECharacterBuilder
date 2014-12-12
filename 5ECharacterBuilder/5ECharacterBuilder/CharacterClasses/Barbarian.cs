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

            Features.ClassFeatures.Add(GetClassFeature("Rage"));
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
    }
}
