using System.Collections.Generic;
using System.Linq;
using _5EDatabase;

namespace _5ECharacterBuilder.CharacterClasses
{
    internal sealed class Barbarian : CharacterClass
    {
        public Barbarian(ICharacter character) : base(character)
        {
            Class = Class.Barbarian;
            ArmorProficiencies.UnionWith(Armory.LightArmor);
            ArmorProficiencies.UnionWith(Armory.MediumArmor);
            ArmorProficiencies.Add(ArmorType.Shield);

            WeaponProficiencies.UnionWith(Armory.SimpleWeapons);
            WeaponProficiencies.UnionWith(Armory.MartialWeapons);
            
            Skills.Available.UnionWith(new[]
                {
                    Skill.AnimalHandling,
                    Skill.Athletics,
                    Skill.Intimidation,
                    Skill.Nature,
                    Skill.Perception,
                    Skill.Survival
                });

            Skills.Max += 2;
        }

        public override IEnumerable<Class> Classes => base.Classes.Concat(Class.Barbarian);

        public override IEnumerable<SavingThrow> SavingThrows
            => base.SavingThrows.Union(SavingThrow.Strength).Union(SavingThrow.Constitution);

        public override CharacterAbilities Abilities => GetClassLevel(Class) < 20 
            ? base.Abilities
            : new CharacterAbilities(base.Abilities)
            {
                Strength = { ClassBonus = 4, MaxScore = 24},
                Constitution = { ClassBonus = 4, MaxScore = 24}
            };

        public override int ArmorClass => EquippedArmor.Name != ArmorType.Cloth
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
                
        public override List<Path> AvailablePaths =>
            GetClassLevel(Class) >= 3 
            ? base.AvailablePaths.Union(CharacterData.BarbarianPaths).ToList()
            : base.AvailablePaths;

        public override HitDice HitDice => base.HitDice.Union(12);
        internal override int AddedHitDice => 12;

        private int GetRageDamage()
        {
            var classLevel = GetClassLevel(Class);
            return classLevel >= 16 ? 4 : classLevel >= 9 ? 3 : 2;
        }

        private int GetRagesPerDay()
        {
            var classLevel = GetClassLevel(Class);
            return classLevel >= 20 ? 100 : classLevel >= 17 ? 6 : classLevel >= 12 ? 5 : classLevel >= 6 ? 4 : classLevel >= 3 ? 3 : 2;
        }

        public override int Speed => GetClassLevel(Class) >= 5 ? base.Speed + 10 : base.Speed;

        internal override List<ClassFeature> ClassFeatureData =>
            new List<ClassFeature>
            {
                new ClassFeature("Rage"),
                new ClassFeature("Barbarian Unarmored Defense", 1, "While you are not wearing any armor, your ArmorType Class equals 10 + your Dexterity modifier + your Constitution modifier. You can use a shield and still gain this benefit."),
                new ClassFeature("Reckless Attack", 2, "You can throw aside all concern for defense to attack with fierce desperation. When you make your first attack on your turn, you can decide to attack recklessly. Doing so gives you advantage on melee weapon attack rolls using Strength during this turn, but attack rolls against you have advantage until your next turn"),
                new ClassFeature("Danger Sense", 2, "You gain an uncanny sense of when things nearby aren’t as they should be, giving you an edge when you dodge away from danger. You have advantage on Dexterity saving throws against effects that you can see, such as traps and spells. To gain this benefit, you can’t be blinded, deafened, or incapacitated."),
                new ClassFeature("Fast Movement", 5, "Your speed increases by 10 feet while you aren’t wearing heavy armor."),
                new ClassFeature("Extra Attack", 5),
                new ClassFeature("Feral Instict", 7, "Your instincts are so honed that you have advantage on initiative rolls. Additionally, if you are surprised at the beginning of combat and aren’t incapacitated, you can act normally on your first turn, but only if you enter your rage before doing anything else on that turn."),
                new ClassFeature("Brutal Critical", 9, "You can roll one additional weapon damage die when determining the extra damage for a critical hit with a melee attack. This increases to two additional dice at 13th level and three additional dice at 17th level.") ,
                new ClassFeature("Relentless Rage", 11, "Your rage can keep you fighting despite grievous wounds. If you drop to 0 hit points while you’re raging and don’t die outright, you can make a DC 10 Constitution saving throw. If you succeed, you drop to 1 hit point instead. Each time you use this feature after the first, the DC increases by 5. When you finish a short or long rest, the DC resets to 10."),
                new ClassFeature("Persistent Rage", 15, "Beginning at 15th level, your rage is so fierce that it ends early only if you fall unconscious or if you choose to end it."),
                new ClassFeature("Indomitable Might", 18, "Beginning at 18th level, if your total for a Strength check is less than your Strength score, you can use that score in place of the total."),
                new ClassFeature("Primal Champion", 20, "At 20th level, you embody the power of the wilds. Your Strength and Constitution scores increase by 4. Your maximum for those scores is now 24."),
            };

        internal override List<ClassPathFeature> ClassPathFeatureData =>
            new List<ClassPathFeature>
            {
                new ClassPathFeature("Frenzy", Path.PathOfTheBerserker, 3),
                new ClassPathFeature("Mindless Rage", Path.PathOfTheBerserker, 6),
                new ClassPathFeature("Intimidating Presence", Path.PathOfTheBerserker, 10),
                new ClassPathFeature("Retaliation", Path.PathOfTheBerserker, 14),

                new ClassPathFeature("Spirit Seeker", Path.PathOfTheTotemWarrior, 3),
                new ClassPathFeature("Totem Spirit", Path.PathOfTheTotemWarrior, 3),
                new ClassPathFeature("Aspect of the Beast", Path.PathOfTheTotemWarrior, 6),
                new ClassPathFeature("Spirit Walker", Path.PathOfTheTotemWarrior, 10),
                new ClassPathFeature("Totemic Attunement", Path.PathOfTheTotemWarrior, 14)
            };
    }
}
