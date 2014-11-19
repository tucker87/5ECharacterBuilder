using System.Collections.Generic;
using System.Linq;

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
                    AddRogueFeature("Thieves' Cant");
                    Skills.MaxExpertise += 2;
                    break;
                case 2:
                    AddRogueFeature("Cunning Action");
                    break;
                case 3:
                    AddClassPaths(CharacterData.GetRoguePaths());
                    break;
                case 5:
                    AddRogueFeature("Uncanny Dodge");
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
                    AddRogueFeature("Reliable Talent");
                    break;
                case 14:
                    AddRogueFeature("Blindsense");
                    break;
                case 15:
                    AddRogueFeature("Slippery Mind");
                    SavingThrows.Add(SavingThrow.Wisdom);
                    break;
                case 18:
                    AddRogueFeature("Elusive");
                    break;
                case 20:
                    AddRogueFeature("Stroke Of Luck");
                    break;
            }
        }

        private void AddRogueFeature(string feature)
        {
            Features.ClassFeatures.Add(feature, CharacterData.RogueFeatures[feature]);
        }

        public override int SneakAttackDice
        {
            get { return Level/2+1; }
        }

        public override Tools Tools
        {
            get
            {
                var tools = base.Tools;
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
                if (ClassPath.Chosen != null)
                {
                    if (ClassPath.Chosen == AvailablePaths.Thief)
                    {
                        classPathFeatures.Add("Fast Hands", CharacterData.RogueFeatures["Fast Hands"]);
                        classPathFeatures.Add("Second-Story Work", CharacterData.RogueFeatures["Second-Story Work"]);
                        if (Level >= 9)
                            classPathFeatures.Add("Supreme Sneak", CharacterData.RogueFeatures["Supreme Sneak"]);

                        if (Level >= 12)
                            classPathFeatures.Add("Use Magic Device", CharacterData.RogueFeatures["Use Magic Device"]);

                        if (Level >= 17)
                            classPathFeatures.Add("Thief's Reflexes", CharacterData.RogueFeatures["Thief's Reflexes"]);

                    }
                    if (ClassPath.Chosen == AvailablePaths.Assassin)
                    {
                        classPathFeatures.Add("Assassinate", CharacterData.RogueFeatures["Assassinate"]);
                        if (Level >= 9)
                            classPathFeatures.Add("Infiltration Expertise", CharacterData.RogueFeatures["Infiltration Expertise"]);

                        if (Level >= 13)
                            classPathFeatures.Add("Imposter", CharacterData.RogueFeatures["Imposter"]);

                        if (Level >= 17)
                            classPathFeatures.Add("Death Strike", CharacterData.RogueFeatures["Death Strike"]);
                    }
                    if (ClassPath.Chosen == AvailablePaths.ArcaneTrickster)
                    {
                        classPathFeatures.Add("Spellcasting", CharacterData.RogueFeatures["Spellcasting"]);
                        classPathFeatures.Add("Mage Hand Legerdemain", CharacterData.RogueFeatures["Mage Hand Legerdemain"]);
                        if (Level >= 9)
                            classPathFeatures.Add("Magical Ambush", CharacterData.RogueFeatures["Magical Ambush"]);

                        if (Level >= 13)
                            classPathFeatures.Add("Versatile Trickster", CharacterData.RogueFeatures["Versatile Trickster"]);

                        if (Level >= 17)
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
                var spellCastingClass = base.SpellcastingClasses;
                if (ClassPath.Chosen != AvailablePaths.ArcaneTrickster) return base.SpellcastingClasses;

                var arcaneTrickster = new SpellcastingClass("Arcane Trickster");
                if (Level >= 3)
                {
                    arcaneTrickster.MaxCantrips = 3;
                    arcaneTrickster.MaxSpells = 3;
                    arcaneTrickster.SpellSlots.First = 2;
                }

                if (Level >= 4)
                {
                    arcaneTrickster.MaxSpells = 4;
                    arcaneTrickster.SpellSlots.First = 3;
                }

                if (Level >= 7)
                {
                    arcaneTrickster.MaxSpells = 5;
                    arcaneTrickster.SpellSlots.First = 4;
                    arcaneTrickster.SpellSlots.Second = 2;
                }

                if (Level >= 8)
                    arcaneTrickster.MaxSpells = 6;

                if (Level >= 10)
                {
                    arcaneTrickster.MaxCantrips = 4;
                    arcaneTrickster.MaxSpells = 7;
                    arcaneTrickster.SpellSlots.Second = 3;
                }

                if (Level >= 11)
                    arcaneTrickster.MaxSpells = 8;

                if (Level >= 13)
                {
                    arcaneTrickster.MaxSpells = 9;
                    arcaneTrickster.SpellSlots.Third = 2;
                }

                if (Level >= 14)
                    arcaneTrickster.MaxSpells = 10;

                if (Level >= 16)
                {
                    arcaneTrickster.MaxSpells = 11;
                    arcaneTrickster.SpellSlots.Third = 3;
                }

                if (Level >= 19)
                {
                    arcaneTrickster.MaxSpells = 12;
                    arcaneTrickster.SpellSlots.Fourth = 1;
                }

                if (Level >= 20)
                    arcaneTrickster.MaxSpells = 13;

                spellCastingClass.Add(arcaneTrickster);
                return base.SpellcastingClasses;
            }
        }
    }
}
