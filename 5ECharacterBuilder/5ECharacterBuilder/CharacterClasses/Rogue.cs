namespace _5ECharacterBuilder.CharacterClasses
{
    class Rogue : CharacterClass
    {
        public Rogue(ICharacter character) : base(character)
        {
            HitDice.Add(8);
            
            ArmorProficiencies.UnionWith(Armory.LightArmor);
            WeaponProficiencies.UnionWith(Armory.SimpleWeapons);


            var extraWeapons = new[]
            {
                AvailableWeapon.HandCrossbows,
                AvailableWeapon.LongSword,
                AvailableWeapon.Rapier,
                AvailableWeapon.ShortSword
            };

            WeaponProficiencies.UnionWith(extraWeapons);

            Tools.Available.Add(AvailableTool.ThievesTools);
            Tools.Chosen.Add(AvailableTool.ThievesTools);

            SavingThrows.Add(SavingThrow.Dexterity);
            SavingThrows.Add(SavingThrow.Intelligence);

            Skills.Max += 4;

            Skills.Available.UnionWith(new[]
            {
                AvailableSkill.Acrobatics,
                AvailableSkill.Athletics,
                AvailableSkill.Deception,
                AvailableSkill.Insight,
                AvailableSkill.Intimidation,
                AvailableSkill.Investigation,
                AvailableSkill.Perception,
                AvailableSkill.Performance,
                AvailableSkill.Persuasion,
                AvailableSkill.SleightOfHand,
                AvailableSkill.Stealth
            });

            Classes.Add("Rogue");
            switch (Level)
            {
                case 1:
                    AddRogueFeature("Expertise");
                    AddRogueFeature("Sneak Attack");
                    break;
            }
            
            Skills.MaxExpertise += 2;
        }

        private void AddRogueFeature(string feature)
        {
            Features.ClassFeatures.Add(feature, CharacterData.RogueFeatures[feature]);
        }

        public override int SneakAttackDice
        {
            get { return Level/2+1; }
        }
    }
}
