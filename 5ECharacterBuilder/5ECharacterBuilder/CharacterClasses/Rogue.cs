using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder.CharacterClasses
{
    sealed class Rogue : CharacterClass
    {
        private const string Class = "Rogue";

        public Rogue(ICharacter character) : base(character)
        {
            if (IsMulticlassing(Class) && !MeetsRequirements())
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

            if (!IsMulticlassing(Class))
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
        
        public override int SneakAttackDice
        {
            get { return ClassLevel(Class)/2+1; }
        }

        public override Tools Tools
        {
            get
            {
                var tools = new Tools(base.Tools);
                tools.Available.Add(AvailableTool.ThievesTools);
                tools.Chosen.Add(AvailableTool.ThievesTools);
                if (ClassPath.Chosen == AvailablePaths.Assassin)
                {
                    tools.Available = new SortedSet<AvailableTool>(tools.Available) {AvailableTool.DisguiseKit, AvailableTool.PoisonersKit};
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
                features.ClassFeatures = GetClassFeatures(classLevel).UnionDictionary(features.ClassFeatures);
                features.ClassPathFeatures = GetClassPathFeatures(classLevel).UnionDictionary(features.ClassPathFeatures);
                return features;
            }
        }

        private Dictionary<string, string> GetClassPathFeatures(int classLevel)
        {
            var classPathFeatures = new Dictionary<string, string>();
            if (ClassPath.Chosen == null) 
                return classPathFeatures;

            if (ClassPath.Chosen == AvailablePaths.Thief)
            {
                classPathFeatures.Add("Fast Hands", CharacterData.RogueFeatures["Fast Hands"]);
                classPathFeatures.Add("Second-Story Work", CharacterData.RogueFeatures["Second-Story Work"]);
                if (classLevel >= 9)
                    classPathFeatures.Add("Supreme Sneak", CharacterData.RogueFeatures["Supreme Sneak"]);

                if (classLevel >= 12)
                    classPathFeatures.Add("Use Magic Device", CharacterData.RogueFeatures["Use Magic Device"]);

                if (classLevel >= 17)
                    classPathFeatures.Add("Thief's Reflexes", CharacterData.RogueFeatures["Thief's Reflexes"]);
            }
            if (ClassPath.Chosen == AvailablePaths.Assassin)
            {
                classPathFeatures.Add("Assassinate", CharacterData.RogueFeatures["Assassinate"]);
                if (classLevel >= 9)
                    classPathFeatures.Add("Infiltration Expertise", CharacterData.RogueFeatures["Infiltration Expertise"]);

                if (classLevel >= 13)
                    classPathFeatures.Add("Imposter", CharacterData.RogueFeatures["Imposter"]);

                if (classLevel >= 17)
                    classPathFeatures.Add("Death Strike", CharacterData.RogueFeatures["Death Strike"]);
            }
            if (ClassPath.Chosen == AvailablePaths.ArcaneTrickster)
            {
                classPathFeatures.Add("Spellcasting", CharacterData.RogueFeatures["Spellcasting"]);
                classPathFeatures.Add("Mage Hand Legerdemain", CharacterData.RogueFeatures["Mage Hand Legerdemain"]);
                if (classLevel >= 9)
                    classPathFeatures.Add("Magical Ambush", CharacterData.RogueFeatures["Magical Ambush"]);

                if (classLevel >= 13)
                    classPathFeatures.Add("Versatile Trickster", CharacterData.RogueFeatures["Versatile Trickster"]);

                if (classLevel >= 17)
                    classPathFeatures.Add("Spell Thief", CharacterData.RogueFeatures["Spell Thief"]);
            }
            return classPathFeatures;
        }

        private static Dictionary<string, string> GetClassFeatures(int classLevel)
        {
            var classFeatures = new Dictionary<string, string>();

            classFeatures.Add(GetClassFeature("Expertise"));
            classFeatures.Add(GetClassFeature("Sneak Attack"));
            classFeatures.Add(GetClassFeature("Thieves' Cant"));

            if (classLevel >= 2)
                classFeatures.Add(GetClassFeature("Cunning Action"));

            if (classLevel >= 5)
                classFeatures.Add(GetClassFeature("Uncanny Dodge"));

            if (classLevel >= 7)
                classFeatures.Add(GetClassFeature("Evasion"));

            if (classLevel >= 11)
                classFeatures.Add(GetClassFeature("Reliable Talent"));

            if (classLevel >= 14)
                classFeatures.Add(GetClassFeature("Blindsense"));

            if (classLevel >= 15)
                classFeatures.Add(GetClassFeature("Slippery Mind"));

            if (classLevel >= 18)
                classFeatures.Add(GetClassFeature("Elusive"));

            if (classLevel >= 20)
                classFeatures.Add(GetClassFeature("Stroke Of Luck"));
            return classFeatures;
        }

        public override ClassPath ClassPath
        {
            get
            {
                return ClassLevel(Class) >= 3 ? new ClassPath(base.ClassPath) { CharacterData.RoguePaths } : base.ClassPath;
            }
        }

        public override SortedSet<SavingThrow> SavingThrows
        {
            get
            {
                return ClassLevel(Class) >= 15 ? new SortedSet<SavingThrow>(base.SavingThrows){SavingThrow.Wisdom} : base.SavingThrows;
            }
        }

        public override Skills Skills
        {
            get
            {
                var skills = new Skills(base.Skills);
                if (ClassLevel(Class) >= 6)
                    skills.MaxExpertise += 2;

                if (IsMulticlassing(Class))
                {
                    skills.Max += 1;
                }
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

                if (classLevel >= 3)
                {
                    arcaneTrickster.MaxCantrips = 3;
                    arcaneTrickster.MaxSpells = 3;
                    arcaneTrickster.SpellSlots.First = 2;
                }

                if (classLevel >= 4)
                {
                    arcaneTrickster.MaxSpells = 4;
                    arcaneTrickster.SpellSlots.First = 3;
                }

                if (classLevel >= 7)
                {
                    arcaneTrickster.MaxSpells = 5;
                    arcaneTrickster.SpellSlots.First = 4;
                    arcaneTrickster.SpellSlots.Second = 2;
                }

                if (classLevel >= 8)
                    arcaneTrickster.MaxSpells = 6;

                if (classLevel >= 10)
                {
                    arcaneTrickster.MaxCantrips = 4;
                    arcaneTrickster.MaxSpells = 7;
                    arcaneTrickster.SpellSlots.Second = 3;
                }

                if (classLevel >= 11)
                    arcaneTrickster.MaxSpells = 8;

                if (classLevel >= 13)
                {
                    arcaneTrickster.MaxSpells = 9;
                    arcaneTrickster.SpellSlots.Third = 2;
                }

                if (classLevel >= 14)
                    arcaneTrickster.MaxSpells = 10;

                if (classLevel >= 16)
                {
                    arcaneTrickster.MaxSpells = 11;
                    arcaneTrickster.SpellSlots.Third = 3;
                }

                if (classLevel >= 19)
                {
                    arcaneTrickster.MaxSpells = 12;
                    arcaneTrickster.SpellSlots.Fourth = 1;
                }

                if (classLevel >= 20)
                    arcaneTrickster.MaxSpells = 13;

                return new SortedSet<SpellcastingClass>(base.SpellcastingClasses.Concat(new[] {arcaneTrickster}));
            }
        }
    }
}
