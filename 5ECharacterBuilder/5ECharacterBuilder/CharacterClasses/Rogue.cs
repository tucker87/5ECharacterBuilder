using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder.CharacterClasses
{
    class Rogue : CharacterClass
    {
        public Rogue(ICharacter character) : base(character)
        {
            if (IsMulticlassing())
            {
                if (MeetsRequirements())
                {
                    Skills.Max += 1;
                }
                else throw new RequirementsExpection();
            }
            else
            {
                WeaponProficiencies.UnionWith(Armory.SimpleWeapons);
                
                var extraWeapons = new[]
                {
                    AvailableWeapon.HandCrossbows,
                    AvailableWeapon.LongSword,
                    AvailableWeapon.Rapier,
                    AvailableWeapon.ShortSword
                };

                WeaponProficiencies.UnionWith(extraWeapons);

                SavingThrows.Add(SavingThrow.Dexterity);
                SavingThrows.Add(SavingThrow.Intelligence);

                Skills.Max += 4;
            }

            Classes.Add("Rogue");
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

            switch (ClassLevel("Rogue"))
            {
                case 1:
                    ArmorProficiencies.UnionWith(Armory.LightArmor);
                    AddClassFeature("Expertise");
                    AddClassFeature("Sneak Attack");
                    AddClassFeature("Thieves' Cant");
                    Skills.MaxExpertise += 2;
                    break;
                case 2:
                    AddClassFeature("Cunning Action");
                    break;
                case 3:
                    AddClassPaths(CharacterData.GetRoguePaths());
                    break;
                case 5:
                    AddClassFeature("Uncanny Dodge");
                    break;
                case 6:
                    Skills.MaxExpertise += 2;
                    break;
                case 7:
                    AddClassFeature("Evasion");
                    break;
                case 10:
                    Abilities.ImprovementPoints += 2;
                    break;
                case 11:
                    AddClassFeature("Reliable Talent");
                    break;
                case 14:
                    AddClassFeature("Blindsense");
                    break;
                case 15:
                    AddClassFeature("Slippery Mind");
                    SavingThrows.Add(SavingThrow.Wisdom);
                    break;
                case 18:
                    AddClassFeature("Elusive");
                    break;
                case 20:
                    AddClassFeature("Stroke Of Luck");
                    break;
            }
        }

        private bool MeetsRequirements()
        {
            return Abilities.Dexterity.Score >= 13;
        }

        private bool IsMulticlassing()
        {
            return Level > 0 && ClassLevel("Rogue") == 0;
        }
        
        public override int SneakAttackDice
        {
            get { return ClassLevel("Rogue")/2+1; }
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
                var classPathFeatures = new Dictionary<string, string>();
                var classLevel = ClassLevel("Rogue");
                if (ClassPath.Chosen != null)
                {
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
                }
                features.ClassPathFeatures = classPathFeatures;
                return features;
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

                var classLevel = ClassLevel("Rogue");

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
