using System.Collections.Generic;
using System.Linq;
using _5EDatabase;

namespace _5ECharacterBuilder.CharacterClasses
{
    internal sealed class Rogue : CharacterClass
    {
        public Rogue(ICharacter character) : base(character)
        {
            Class = Class.Rogue;

            if (IsMulticlassing() && !MeetsRequirements())
                throw new RequirementsExpection();

            ArmorProficiencies.UnionWith(Armory.LightArmor);
            WeaponProficiencies.UnionWith(Armory.SimpleWeapons);

            var extraWeapons = new[]
            {
                WeaponType.HandCrossbows,
                WeaponType.LongSword,
                WeaponType.Rapier,
                WeaponType.ShortSword
            };

            if (!IsMulticlassing())
            {
                WeaponProficiencies.UnionWith(extraWeapons);
            }

            Skills.Available.UnionWith(new[]
                {
                    Skill.Acrobatics,
                    Skill.Athletics,
                    Skill.Deception,
                    Skill.Insight,
                    Skill.Intimidation,
                    Skill.Investigation,
                    Skill.Perception,
                    Skill.Performance,
                    Skill.Persuasion,
                    Skill.SleightOfHand,
                    Skill.Stealth
                });
        }

        private int? _currentLevel;
        internal override int CurrentLevel
        {
            get
            {
                if (_currentLevel == null)
                    _currentLevel = GetClassLevel(Class);

                return (int) _currentLevel;
            }
        }

        private bool MeetsRequirements()
        {
            return Abilities.Dexterity.Score >= 13;
        }

        private bool IsMulticlassing()
        {
            if (!Classes.Any())
                return false;

            return Classes.First() != Class;
        }

        public override IEnumerable<Class> Classes => base.Classes.Concat(Class);

        public override CharacterAbilities Abilities
        {
            get
            {
                var abilities = new CharacterAbilities(base.Abilities);
                if (Level >= 10)
                    abilities.ImprovementPoints += 2;
                return abilities;
            }
        }

        public override int SneakAttackDice => GetClassLevel(Class) / 2 + 1;

        public override Tools Tools
        {
            get
            {
                var tools = new Tools(base.Tools);
                tools.Available.Add(Tool.ThievesTools);
                tools.Chosen.Add(Tool.ThievesTools);
                if (ClassPath == Path.Assassin)
                {
                    tools.Available = new SortedSet<Tool>(tools.Available) { Tool.DisguiseKit, Tool.PoisonersKit };
                    tools.Chosen = new SortedSet<Tool>(tools.Chosen) { Tool.DisguiseKit, Tool.PoisonersKit };
                }
                return tools;
            }
        }

       public override List<Path> AvailablePaths => 
            GetClassLevel(Class) >= 3 
            ? base.AvailablePaths.Union(CharacterData.RoguePaths).ToList()
            : base.AvailablePaths;

        internal override int AddedHitDice => 8;

        public override IEnumerable<SavingThrow> SavingThrows
        {
            get
            {
                var savingThrows = base.SavingThrows.ToList();

                if (!IsMulticlassing())
                    savingThrows = savingThrows.Union(SavingThrow.Dexterity).Union(SavingThrow.Intelligence).ToList();

                if (GetClassLevel(Class) >= 15)
                    savingThrows = savingThrows.Union(SavingThrow.Wisdom).ToList();

                return savingThrows;
            }
        }

        public override Skills Skills
        {
            get
            {
                var skills = new Skills(base.Skills);
                if (CurrentLevel == 6)
                    skills.MaxExpertise += 2;

                if (CurrentLevel == 1) 
                {
                    if (IsMulticlassing())
                    {
                        skills.Max += 1;
                    }
                    else
                    {
                        skills.Max += 4;
                        skills.MaxExpertise += 2;
                    }
                }

                return skills;
            }
        }

        public override SortedSet<SpellcastingClass> SpellcastingClasses
        {
            get
            {
                if (ChosenPaths.All(cp => cp != Path.ArcaneTrickster))
                    return base.SpellcastingClasses;

                const int baseValue = 8;
                var arcaneTrickster = new SpellcastingClass("Arcane Trickster")
                {
                    SaveDc = baseValue + ProficiencyBonus + Abilities.Intelligence.Modifier,
                    AttackMod = ProficiencyBonus + Abilities.Intelligence.Modifier
                };

                var classLevel = GetClassLevel(Class);

                arcaneTrickster.MaxCantrips = classLevel >= 10 ? 4 : classLevel >= 3 ? 3 : 0;

                arcaneTrickster.SpellSlots.First = classLevel >= 7 ? 4 : classLevel >= 4 ? 3 : classLevel >= 3 ? 2 : 0;
                arcaneTrickster.SpellSlots.Second = classLevel >= 10 ? 3 : classLevel >= 7 ? 2 : 0;
                arcaneTrickster.SpellSlots.Third = classLevel >= 16 ? 3 : classLevel >= 13 ? 2 : 0;
                arcaneTrickster.SpellSlots.Fourth = classLevel >= 19 ? 1 : 0;
                
                var maxSpells = new Dictionary<int, int>
                {
                    {1, 0},
                    {2, 0},
                    {3, 3},
                    {4, 4},
                    {5, 4},
                    {6, 4},
                    {7, 5},
                    {8, 6},
                    {9, 6},
                    {10, 7},
                    {11, 8},
                    {12, 8},
                    {13, 9},
                    {14, 10},
                    {15, 10},
                    {16, 11},
                    {17, 11},
                    {18, 11},
                    {19, 12},
                    {20, 13}
                };

                arcaneTrickster.MaxSpells = maxSpells[classLevel];

                var spellCastingClasses = base.SpellcastingClasses;

                spellCastingClasses.RemoveWhere(scc => scc.Name == "Arcane Trickster");

                return new SortedSet<SpellcastingClass>(spellCastingClasses.Concat(new[] { arcaneTrickster }));
            }
        }

        internal override List<ClassFeature> ClassFeatureData =>
           new List<ClassFeature>
           {
                new ClassFeature("Expertise", 1, "Choose two of your skill proficiencies, or one of your skill proficiencies and your proficiency with thieves’ tools. Your proficiency bonus is doubled for any ability check you make that uses either of the chosen proficiencies. At 6th level, you can choose two more of your proficiencies (in skills or with thieves’ tools) to gain this benefit."),
                new ClassFeature("Sneak Attack", 1, "You know how to strike subtly and exploit a foe’s distraction. Once per turn, you can deal an extra 1d6 damage to one creature you hit with an attack if you have advantage on the attack roll. The attack must use a finesse or a ranged weapon. You don’t need advantage on the attack roll if another enemy of the target is within 5 feet of it, that enemy isn’t incapacitated, and you don’t have disadvantage on the attack roll."),
                new ClassFeature("Thieves' Cant", 1, "During your rogue training you learned thieves’ cant, a secret mix of dialect, jargon, and code that allows you to hide messages in seemingly normal conversation. Only another creature that knows thieves’ cant understands such messages. It takes four times longer to convey such a message than it does to speak the same idea plainly. In addition, you understand a set of secret signs and symbols used to convey short, simple messages, such as whether an area is dangerous or the territory of a thieves’ guild, whether loot is nearby, or whether the people in an area are easy marks or will provide a safe house for thieves on the run."),
                new ClassFeature("Cunning Action", 2, "Your quick thinking and agility allow you to move and act quickly. You can take a bonus action on each of your turns in combat. This action can be used only to take the Dash, Disengage, or Hide action."),
                new ClassFeature("Uncanny Dodge", 5, "When an attacker that you can see hits you with an attack, you can use your reaction to halve the attack’s damage against you."),
                new ClassFeature("Reliable Talent", 7, "You have refined your chosen skills until they approach perfection. Whenever you make an ability check that lets you add your proficiency bonus, you can treat a d20 roll of 9 or lower as a 10."),
                new ClassFeature("Evasion", 7),
                new ClassFeature("Blindsense", 14, "If you are able to hear, you are  aware of the location of any hidden or invisible creature within 10 feet of you."),
                new ClassFeature("Slippery Mind", 15, "You have acquired greater mental strength. You gain proficiency in W isdom saving throws."),
                new ClassFeature("Elusive", 18, "You are so evasive that attackers rarely gain the upper hand against you. No attack roll has advantage against you while you aren’t incapacitated."),
                new ClassFeature("Stroke Of Luck", 20, "You have an uncanny knack for succeeding when you need to. If your attack misses a target within range, you can turn the miss into a hit. Alternatively, if you fail an ability check, you can treat the d20 roll as a 20. Once you use this feature, you can’t use it again until you finish a short or long rest.")
           };

        internal override List<ClassPathFeature> ClassPathFeatureData =>
            new List<ClassPathFeature>
            {
                new ClassPathFeature("Fast Hands", Path.Thief, 3, "You can use the bonus action granted by your Cunning Action to make a Dexterity (Sleight of Hand) check, use your thieves’ tools to disarm a trap or open a lock, or take the Use an Object action."),
                new ClassPathFeature("Second-Story Work", Path.Thief, 3, "You gain the ability to climb faster than normal; climbing no longer costs you extra movement. In addition, when you make a running jump, the distance you cover increases by a number of feet equal to your Dexterity modifier."),
                new ClassPathFeature("Supreme Sneak", Path.Thief, 9, "You have advantage on a Dexterity (Stealth) check if you move no more than half your speed on the same turn."),
                new ClassPathFeature("Use Magic Device", Path.Thief, 12, "You have learned enough about the workings of magic that you can improvise the use of items even when they are not intended for you. You ignore all class, race, and level requirements on the use of magic items."),
                new ClassPathFeature("Thief's Reflexes", Path.Thief, 17, "You have become adept at laying ambushes and quickly escaping danger. You can take two turns during the first round of any combat. You take your first turn at your normal initiative and your second turn at your initiative minus 10. You can’t use this feature when you are surprised."),

                new ClassPathFeature("Assassinate", Path.Assassin, 3, "You are at your deadliest when you get the drop on your enemies. You have advantage on attack rolls against any creature that hasn’t taken a turn in the combat yet. In addition, any hit you score against a creature that is surprised is a critical hit."),
                new ClassPathFeature("Infiltration Expertise", Path.Assassin, 9, "You can unfailingly create false identities for yourself. You must spend seven days and 25 gp to establish the history, profession, and affiliations for an identity. You can’t establish an identity that belongs to someone else. For example, you might acquire appropriate clothing, letters of introduction, and official- looking certification to establish yourself as a member of a trading house from a remote city so you can insinuate yourself into the company of other wealthy merchants. Thereafter, if you adopt the new identity as a disguise, other creatures believe you to be that person until given an obvious reason not to."),
                new ClassPathFeature("Imposter", Path.Assassin, 13, "You gain the ability to unerringly mimic another person’s speech, writing, and behavior. You must spend at least three hours studying these three components of the person’s behavior, listening to speech, examining handwriting, and observing mannerisms. Your ruse is indiscernible to the casual observer. If a wary creature suspects something is amiss, you have advantage on any Charisma (Deception) check you make to avoid detection."),
                new ClassPathFeature("Death Strike", Path.Assassin, 17, "You become a master of instant death. When you attack and hit a creature that is surprised, it must make a Constitution saving throw (DC 8 + your Dexterity modifier + your proficiency bonus). On a failed save, double the damage of your attack against the creature."),

                new ClassPathFeature("Spellcasting", Path.ArcaneTrickster, 3, "See PHB:98"),
                new ClassPathFeature("Mage Hand Legerdemain", Path.ArcaneTrickster, 3, "When you cast  mage hand, you can make the spectral hand invisible, and you can perform the following additional tasks with it: • You can stow one object the hand is holding in a container worn or carried by another creature. • You can retrieve an object in a container worn or carried by another creature. • You can use thieves’ tools to pick locks and disarm traps at range. You can perform one of these tasks without being noticed by a creature if you succeed on a Dexterity (Sleight of Hand) check contested by the creature’s Wisdom (Perception) check. In addition, you can use the bonus action granted by your Cunning Action to control the hand."),
                new ClassPathFeature("Magical Ambush", Path.ArcaneTrickster, 9, "If you are hidden from a creature when you cast a spell on it, the creature has disadvantage on any saving throw it makes against the spell this turn."),
                new ClassPathFeature("Versatile Trickster", Path.ArcaneTrickster, 13, "You gain the ability to distract targets with your  mage hand. As a bonus action on your turn, you can designate a creature within 5 feet of the spectral hand created by the spell. Doing so gives you advantage on attack rolls against that creature until the end of the turn."),
                new ClassPathFeature("Spell Thief", Path.ArcaneTrickster, 17, "You gain the ability to magically steal the knowledge of how to cast a spell from another spellcaster. Immediately after a creature casts a spell that targets you or includes you in its area of effect, you can use your reaction to force the creature to make a saving throw with its spellcasting ability modifier. The DC equals your spell save DC. On a failed save, you negate the spell’s effect against you, and you steal the knowledge of the spell if it is at least 1st level and of a level you can cast (it doesn’t need to be a wizard spell). For the next 8 hours, you know the spell and can cast it using your spell slots. The creature can’t cast that spell until the 8 hours have passed. Once you use this feature, you can’t use it again until you finish a long rest.")
            };
    }
}
