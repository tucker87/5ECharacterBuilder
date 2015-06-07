using System;
using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder.CharacterClasses
{
    sealed class Rogue : CharacterClass
    {
        private const string Class = "Rogue";

        public Rogue(ICharacter character) : base(character)
        {

            if (IsMulticlassing() && !MeetsRequirements())
                throw new RequirementsExpection();

            ArmorProficiencies.UnionWith(Armory.LightArmor);
            WeaponProficiencies.UnionWith(Armory.SimpleWeapons);

            var extraWeapons = new[]
            {
                AvailableWeapon.HandCrossbows,
                AvailableWeapon.LongSword,
                AvailableWeapon.Rapier,
                AvailableWeapon.ShortSword
            };

            if (!IsMulticlassing())
            {
                WeaponProficiencies.UnionWith(extraWeapons);

                SavingThrows.Add(SavingThrow.Dexterity);
                SavingThrows.Add(SavingThrow.Intelligence);
            }

            Classes.Add(Class);
            HitDice.Add(8);

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
        }

        private bool MeetsRequirements()
        {
            return Abilities.Dexterity.Score >= 13;
        }

        private bool IsMulticlassing()
        {
            if (Classes.Count == 0)
                return false;

            return Classes.First() != Class;
        }

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

        public override int SneakAttackDice => ClassLevel(Class) / 2 + 1;

        public override Tools Tools
        {
            get
            {
                var tools = new Tools(base.Tools);
                tools.Available.Add(AvailableTool.ThievesTools);
                tools.Chosen.Add(AvailableTool.ThievesTools);
                if (ClassPath.Chosen == AvailablePaths.Assassin)
                {
                    tools.Available = new SortedSet<AvailableTool>(tools.Available) { AvailableTool.DisguiseKit, AvailableTool.PoisonersKit };
                    tools.Chosen = new SortedSet<AvailableTool>(tools.Chosen) { AvailableTool.DisguiseKit, AvailableTool.PoisonersKit };
                }
                return tools;
            }
        }

        public override CharacterFeatures Features
        {
            get
            {
                var features = base.Features;
                var classLevel = ClassLevel(Class);

                var classFeatures = new Dictionary<string, string>();

                classFeatures.Add(GetClassFeature("Expertise"));
                classFeatures.Add(GetClassFeature("Sneak Attack"));
                classFeatures.Add(GetClassFeature("Thieves' Cant"));

                if (classLevel >= 2) classFeatures.Add(GetClassFeature("Cunning Action"));
                if (classLevel >= 5) classFeatures.Add(GetClassFeature("Uncanny Dodge"));
                if (classLevel >= 7) classFeatures.Add(GetClassFeature("Evasion"));
                if (classLevel >= 11) classFeatures.Add(GetClassFeature("Reliable Talent"));
                if (classLevel >= 14) classFeatures.Add(GetClassFeature("Blindsense"));
                if (classLevel >= 15) classFeatures.Add(GetClassFeature("Slippery Mind"));
                if (classLevel >= 18) classFeatures.Add(GetClassFeature("Elusive"));
                if (classLevel >= 20) classFeatures.Add(GetClassFeature("Stroke Of Luck"));

                features.ClassFeatures = classFeatures.UnionDictionary(features.ClassFeatures);

                var classPathFeatures = new Dictionary<string, string>();
                if (ClassPath.Chosen != null)
                {
                    if (ClassPath.Chosen == AvailablePaths.Thief)
                    {
                        classPathFeatures.Add("Fast Hands", CharacterData.RogueFeatures["Fast Hands"]);
                        classPathFeatures.Add("Second-Story Work", CharacterData.RogueFeatures["Second-Story Work"]);
                        if (classLevel >= 9) classPathFeatures.Add("Supreme Sneak", CharacterData.RogueFeatures["Supreme Sneak"]);
                        if (classLevel >= 12) classPathFeatures.Add("Use Magic Device", CharacterData.RogueFeatures["Use Magic Device"]);
                        if (classLevel >= 17) classPathFeatures.Add("Thief's Reflexes", CharacterData.RogueFeatures["Thief's Reflexes"]);
                    }
                    if (ClassPath.Chosen == AvailablePaths.Assassin)
                    {
                        classPathFeatures.Add("Assassinate", CharacterData.RogueFeatures["Assassinate"]);
                        if (classLevel >= 9) classPathFeatures.Add("Infiltration Expertise", CharacterData.RogueFeatures["Infiltration Expertise"]);
                        if (classLevel >= 13) classPathFeatures.Add("Imposter", CharacterData.RogueFeatures["Imposter"]);
                        if (classLevel >= 17) classPathFeatures.Add("Death Strike", CharacterData.RogueFeatures["Death Strike"]);
                    }
                    if (ClassPath.Chosen == AvailablePaths.ArcaneTrickster)
                    {
                        classPathFeatures.Add("Spellcasting", CharacterData.RogueFeatures["Spellcasting"]);
                        classPathFeatures.Add("Mage Hand Legerdemain", CharacterData.RogueFeatures["Mage Hand Legerdemain"]);
                        if (classLevel >= 9) classPathFeatures.Add("Magical Ambush", CharacterData.RogueFeatures["Magical Ambush"]);
                        if (classLevel >= 13) classPathFeatures.Add("Versatile Trickster", CharacterData.RogueFeatures["Versatile Trickster"]);
                        if (classLevel >= 17) classPathFeatures.Add("Spell Thief", CharacterData.RogueFeatures["Spell Thief"]);
                    }
                }
                features.ClassPathFeatures = classPathFeatures.UnionDictionary(features.ClassPathFeatures);
                return features;
            }
        }

        public override ClassPath ClassPath => ClassLevel(Class) >= 3 ? new ClassPath(base.ClassPath) { CharacterData.GetRoguePaths() } : base.ClassPath;

        public override SortedSet<SavingThrow> SavingThrows => ClassLevel(Class) >= 15 ? new SortedSet<SavingThrow>(base.SavingThrows) { SavingThrow.Wisdom } : base.SavingThrows;

        public override Skills Skills
        {
            get
            {
                var skills = new Skills(base.Skills);
                if (ClassLevel(Class) >= 6)
                    skills.MaxExpertise += 2;

                if (IsMulticlassing())
                    skills.Max += 1;
                else
                {
                    skills.Max += 4;
                    skills.MaxExpertise += 2;
                }

                return skills;
            }
        }

        public override SortedSet<SpellcastingClass> SpellcastingClasses
        {
            get
            {
                if (ClassPath.Chosen != AvailablePaths.ArcaneTrickster)
                    return base.SpellcastingClasses;

                const int baseValue = 8;
                var arcaneTrickster = new SpellcastingClass("Arcane Trickster")
                {
                    SaveDc = baseValue + ProficiencyBonus + Abilities.Intelligence.Modifier,
                    AttackMod = ProficiencyBonus + Abilities.Intelligence.Modifier
                };

                var classLevel = ClassLevel(Class);

                arcaneTrickster.MaxCantrips = classLevel >= 10 ? 4 : classLevel >= 3 ? 3 : 0;

                arcaneTrickster.SpellSlots.First = classLevel >= 7 ? 4 : classLevel >= 4 ? 3 : classLevel >= 3 ? 2 : 0;
                arcaneTrickster.SpellSlots.Second = classLevel >= 10 ? 3 : classLevel >= 7 ? 2 : 0;
                arcaneTrickster.SpellSlots.Third = classLevel >= 16 ? 3 : classLevel >= 13 ? 2 : 0;
                arcaneTrickster.SpellSlots.Fourth = classLevel >= 19 ? 1 : 0;
                
                //Version 1
                arcaneTrickster.MaxSpells = new Func<int>(() =>
                {
                    switch (classLevel)
                    {
                        case 3:
                            return 3;
                        case 4:
                        case 5:
                        case 6:
                            return 4;
                        case 7:
                            return 5;
                        case 8:
                        case 9:
                            return 6;
                        case 10:
                            return 7;
                        case 11:
                        case 12:
                            return 8;
                        case 13:
                            return 9;
                        case 14:
                        case 15:
                            return 10;
                        case 16:
                        case 17:
                        case 18:
                            return 11;
                        case 19:
                            return 12;
                        case 20:
                            return 13;
                        default:
                            return 0;
                    }
                })();

                //Version 2
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

                //Version 3
                arcaneTrickster.MaxSpells = classLevel >= 20
                    ? 13
                    : classLevel >= 19
                        ? 12
                        : classLevel >= 16
                            ? 11
                            : classLevel >= 14
                                ? 10
                                : classLevel >= 13
                                    ? 9
                                    : classLevel >= 11
                                        ? 8
                                        : classLevel >= 10
                                            ? 7
                                            : classLevel >= 8
                                                ? 6
                                                : classLevel >= 7
                                                    ? 5
                                                    : classLevel >= 4
                                                        ? 4
                                                        : classLevel >= 3
                                                            ? 3
                                                            : 0;

                return new SortedSet<SpellcastingClass>(base.SpellcastingClasses.Concat(new[] { arcaneTrickster }));
            }
        }
    }
}
