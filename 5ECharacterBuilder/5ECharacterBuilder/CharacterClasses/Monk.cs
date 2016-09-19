using System.Collections.Generic;
using System.Linq;
using _5EDatabase;

namespace _5ECharacterBuilder.CharacterClasses
{
    internal sealed class Monk : CharacterClass
    {
        public Monk(ICharacter character) : base(character)
        {
            Class = Class.Monk;
            if (IsMulticlassing() && !MeetsRequirements())
                throw new RequirementsExpection();

            WeaponProficiencies.UnionWith(Armory.SimpleWeapons);

            WeaponProficiencies.Add(WeaponType.ShortSword);
            
            Skills.Available.UnionWith(new[]
            {
                Skill.Acrobatics,
                Skill.Athletics,
                Skill.History,
                Skill.Insight,
                Skill.Religion,
                Skill.Stealth
            });

            Skills.Max += 2;
        }

        public override IEnumerable<Class> Classes => base.Classes.Concat(Class);
        
        private bool MeetsRequirements()
        {
            return Abilities.Dexterity.Score >= 13 && Abilities.Wisdom.Score >= 13;
        }

        private bool IsMulticlassing()
        {
            return Level > 1 && GetClassLevel(Class) < 1;
        }

        public override List<Path> AvailablePaths => 
            GetClassLevel(Class) >= 3 
                ? base.AvailablePaths.Union(CharacterData.MonkPaths).ToList() 
                : base.AvailablePaths;

        internal override int AddedHitDice => 8;

        public override int ArmorClass => EquippedArmor.Name == ArmorType.Cloth && !HasShield 
            ? 10 + Abilities.Dexterity.Modifier + Abilities.Wisdom.Modifier
            : base.ArmorClass;

        public override int AttacksPerTurn => GetClassLevel(Class) == 5
            ? base.AttacksPerTurn + 1
            : base.AttacksPerTurn;

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
                var classLevel = GetClassLevel(Class);
                if (classLevel >= 17) return 10;
                if (classLevel >= 11) return 8;
                return classLevel >= 5 ? 6 : 4;
            }
        }

        public override IEnumerable<SavingThrow> SavingThrows => GetClassLevel(Class) >= 14
            ? new SortedSet<SavingThrow>(base.SavingThrows)
            {
                SavingThrow.Strength,
                SavingThrow.Dexterity,
                SavingThrow.Constitution,
                SavingThrow.Intelligence,
                SavingThrow.Wisdom,
                SavingThrow.Charisma
            }
            : base.SavingThrows.Union(SavingThrow.Strength).Union(SavingThrow.Dexterity);

        public override int Speed 
        {
            get
            {
                var classLevel = GetClassLevel(Class);
                if (classLevel == 18) return base.Speed + 5;
                if (classLevel == 14) return base.Speed + 5;
                if (classLevel == 10) return base.Speed + 5;
                if (classLevel == 6) return base.Speed + 5;
                if (classLevel == 2) return base.Speed + 10;
                return base.Speed;
            }
        }

        internal override List<ClassFeature> ClassFeatureData =>
            new List<ClassFeature>
            {
                new ClassFeature("Monk Unarmored Defense", 1, "Beginning at 1st level, while you are wearing no armor and not wielding a shield, your AC equals 10 + your Dexterity modifier + your Wisdom modifier."),
                new ClassFeature("Martial Arts", 1, "You gain the following benefits while you are unarmed or wielding only monk weapons and you aren’t wearing armor or wielding a shield: You can use Dexterity instead of Strength for the attack and damage rolls of your unarmed strikes and monk weapons. You can roll a d4 in place of the normal damage of your unarmed strike or monk weapon. This die changes as you gain monk levels, as shown in the Martial Arts column of the Monk table. When you use the Attack action with an unarmed strike or a monk weapon on your turn, you can make one unarmed strike as a bonus action. For example, if you take the Attack action and attack with a quarter- staff, you can also make an unarmed strike as a bonus action, assuming you haven't already taken a bonus action this turn."),
                new ClassFeature("Ki", 2, "See PHB"),
                new ClassFeature("Unarmored Movement", 2, "Starting at 2nd level, your speed increases by 10 feet while you are not wearing armor or wielding a shield. This bonus increases when you reach certain monk levels, as shown in the Monk table. At 9th level, you gain the ability to move along vertical surfaces and across liquids on your turn without falling during the move."),
                new ClassFeature("Deflect Missiles", 3, "You can use your reaction to deflect or catch the missile when you are hit by a ranged weapon attack. When you do so, the damage you take from the attack is reduced by 1d10 + your Dexterity modifier + your monk level. If you reduce the damage to 0, you can catch the missile if it is small enough for you to hold in one hand and you have at least one hand free. If you catch a missile in this way, you can spend 1 ki point to make a ranged attack with the weapon or piece of ammunition you just caught, as part of the same reaction. You make this attack with proficiency, regardless of your weapon proficiencies, and the missile counts as a monk weapon for the attack."),
                new ClassFeature("Slow Fall", 4, "You can use your reaction when you fall to reduce any falling damage you take by an amount equal to five times your monk level."),
                new ClassFeature("Stunning Strike", 5, "You can interfere with the flow of ki in an opponent’s body. When you hit another creature with a melee weapon attack, you can spend 1 ki point to attempt a stunning strike. The target must succeed on a Constitution saving throw or be stunned until the end of your next turn."),
                new ClassFeature("Ki-Empowered Strikes", 6, "Your unarmed strikes count as magical for the purpose of overcoming resistance and immunity to nonmagical attacks and damage."),
                new ClassFeature("Stillness Of Mind", 7, "You can use your action to end one effect on yourself that is causing you to be charmed or frightened."),
                new ClassFeature("Evasion", 7),
                new ClassFeature("Purity Of Body", 10, "Your mastery of the ki flowing through you makes you immune to disease and poison."),
                new ClassFeature("Tounge Of The Sun And Moon", 13, "You learn to touch the ki of other minds so that you understand all spoken languages. Moreover, any creature that can understand a language can understand what you say."),
                new ClassFeature("Diamond Soul", 14, "Your mastery of ki grants you proficiency in all saving throws. Additionally, whenever you make a saving throw and fail, you can spend 1 ki point to reroll it and take the second result."),
                new ClassFeature("Timeless Body", 15, "Your ki sustains you so that you suffer none of the frailty of old age, and you can't be aged magically. You can still die of old age, however. In addition, you no longer need food or water."),
                new ClassFeature("Empty Body", 18, "You can use your action to spend 4 ki points to become invisible for 1 minute. During that time, you also have resistance to all damage but force damage. Additionally, you can spend 8 ki points to cast the astral projection spell, without needing material components. When you do so, you can’t take any other creatures with you."),
                new ClassFeature("Perfect Self", 20, "When you roll for initiative and have no ki points remaining, you regain 4 ki points.")
            };

        internal override List<ClassPathFeature> ClassPathFeatureData =>
            new List<ClassPathFeature>
            {

                new ClassPathFeature("Open Hand Technique", Path.WayOfTheOpenHand, 3, "Whenever you hit a creature with one of the attacks granted by your Flurry of Blows, you can impose one of the following effects on that target: • It must succeed on a Dexterity saving throw or be knocked prone. • It must make a Strength saving throw. If it fails, you can push it up to 15 feet away from you. • It can’t take reactions until the end of your next turn."),
                new ClassPathFeature("Wholeness Of Body", Path.WayOfTheOpenHand, 6, "You gain the ability to heal yourself. As an action, you can regain hit points equal to three times, your monk level. You must finish a long rest before you can use this feature again."),
                new ClassPathFeature("Tranquility", Path.WayOfTheOpenHand, 11, "You can enter a special meditation that surrounds you with an aura of peace. At the end of a long rest, you gain the effect of a  sanctuary spell that lasts until the start of your next long rest (the spell can end early as normal). The saving throw DC for the spell equals 8 + your Wisdom modifier + your proficiency bonus."),
                new ClassPathFeature("Quivering Palm", Path.WayOfTheOpenHand, 17, "You gain the ability to set up lethal vibrations in someone’s body. When you hit a creature with an unarmed strike, you can spend 3 ki points to start these imperceptible vibrations, which last for a number of days equal to your monk level. The vibrations are harmless unless you use your action to end them. To do so, you and the target must be on the same plane of existence. When you use this action, the creature must make a Constitution saving throw. If it fails, it is reduced to 0 hit points. If it succeeds, it takes 10d10 necrotic damage. You can have only one creature under the effect of this feature at a time. You can choose to end the vibrations harmlessly without using an action."),

                new ClassPathFeature("Shadow Arts", Path.WayOfShadow, 3, "You can use your ki to duplicate the effects of certain spells. As an action, you can spend 2 ki points to cast  darkness, darkvision, pass without trace, or  silence, without providing material components. Additionally, you gain the  minor illusion cantrip if you don’t already know it." ),
                new ClassPathFeature("Shadow Step", Path.WayOfShadow, 6, "You gain the ability to step from one shadow into another. When you are in dim light or darkness, as a bonus action you can teleport up to 60 feet to an unoccupied space you can see that is also in dim light or darkness. You then have advantage on the first melee attack you make before the end of the turn."),
                new ClassPathFeature("Cloak Of Shadows", Path.WayOfShadow, 11, "You have learned to become one with the shadows. When you are in an area of dim light or darkness, you can use your action to become invisible. You remain invisible until you make an attack, cast a spell, or are in an area of bright light."),
                new ClassPathFeature("Opportunist", Path.WayOfShadow, 17, "You can exploit a creature's momentary distraction when it is hit by an attack. Whenever a creature within 5 feet of you is hit by an attack made by a creature other than you, you can use your reaction to make a melee attack against that creature."),

                new ClassPathFeature("Disciple Of The Elements", Path.WayOfTheFourElements, 3, "See PHB")
            };
    }
}