using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _5ECharacterBuilder
{
    public class CharacterAbilityScores
    {
        public CharacterAbilityScores(int strength = 10, int dexterity = 10, int constitution = 10, int intelligence = 10, int wisdom = 10, int charisma = 10)
        {
            Strength = VerifyScore(strength);
            Dexterity = VerifyScore(dexterity);
            Constitution = VerifyScore(constitution);
            Intelligence = VerifyScore(intelligence);
            Wisdom = VerifyScore(wisdom);
            Charisma = VerifyScore(charisma);
        }

        public int Strength { get; protected set; }
        public int Dexterity { get; protected set; }
        public int Constitution { get; protected set; }
        public int Intelligence { get; protected set; }
        public int Wisdom { get; protected set; }
        public int Charisma { get; protected set; }

        protected static int VerifyScore(int score)
        {
            if (score < 1)
                throw new Exception("Score cannot be less than one");

            if (score > 20)
                throw new Exception("Score cannot be greater than twenty");

            return score;
        }
    }

    public class RacialBonuses : CharacterAbilityScores
    {
        public RacialBonuses(int strength = 0, int dexterity = 0, int constitution = 0, int intelligence = 0, int wisdom = 0, int charisma = 0)
        {
            Strength = strength;
            Dexterity = dexterity;
            Constitution = constitution;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Charisma = charisma;
        }
    }

    public class CharacterAbilities : IEnumerable
    {
        public CharacterAbilities() { }

        public CharacterAbilities(CharacterAbilities abilities)
        {
            Strength = abilities.Strength;
            Dexterity = abilities.Dexterity;
            Constitution = abilities.Constitution;
            Intelligence = abilities.Intelligence;
            Wisdom = abilities.Wisdom;
            Charisma = abilities.Charisma;

            ImprovementPoints = abilities.ImprovementPoints;
        }

        public CharacterAbilities(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
        {
            Strength = new CharacterAbility(strength);
            Dexterity = new CharacterAbility(dexterity);
            Constitution = new CharacterAbility(constitution);
            Intelligence = new CharacterAbility(intelligence);
            Wisdom = new CharacterAbility(wisdom);
            Charisma = new CharacterAbility(charisma);
        }

        public CharacterAbilities(CharacterAbilityScores abilityScores)
        {
            Strength = new CharacterAbility(abilityScores.Strength);
            Dexterity = new CharacterAbility(abilityScores.Dexterity);
            Constitution = new CharacterAbility(abilityScores.Constitution);
            Intelligence = new CharacterAbility(abilityScores.Intelligence);
            Wisdom = new CharacterAbility(abilityScores.Wisdom);
            Charisma = new CharacterAbility(abilityScores.Charisma);
        }

        public CharacterAbilities(CharacterAbilities abilities, CharacterAbilityScores racialBonuses)
        {
            Strength = abilities.Strength;
            Strength.RacialBonus = racialBonuses.Strength;
            Dexterity = abilities.Dexterity;
            Dexterity.RacialBonus = racialBonuses.Dexterity;
            Constitution = abilities.Constitution;
            Constitution.RacialBonus = racialBonuses.Constitution;
            Intelligence = abilities.Intelligence;
            Intelligence.RacialBonus = racialBonuses.Intelligence;
            Wisdom = abilities.Wisdom;
            Wisdom.RacialBonus = racialBonuses.Wisdom;
            Charisma = abilities.Charisma;
            Charisma.RacialBonus = racialBonuses.Charisma;
        }

        private Dictionary<string, CharacterAbility> Abilities
            =>
                new Dictionary<string, CharacterAbility>
                {
                    {"Strength", Strength},
                    {"Dexterity", Dexterity},
                    {"Constitution", Constitution},
                    {"Intelligence", Intelligence},
                    {"Wisdom", Wisdom},
                    {"Charisma", Charisma}
                };

        public CharacterAbility Strength { get; }
        public CharacterAbility Dexterity { get; }
        public CharacterAbility Constitution { get; }
        public CharacterAbility Intelligence { get; }
        public CharacterAbility Wisdom { get; }
        public CharacterAbility Charisma { get; }

        public int ImprovementPoints { get; internal set; }
        public int SpentAbilityImprovementPoints => Strength.ImprovementBonus +
                                                    Dexterity.ImprovementBonus +
                                                    Constitution.ImprovementBonus +
                                                    Intelligence.ImprovementBonus +
                                                    Wisdom.ImprovementBonus +
                                                    Charisma.ImprovementBonus;

        public Dictionary<string, CharacterAbility>.Enumerator GetEnumerator()
        {
            return Abilities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class CharacterAbility
    {
        private int _score;

        public CharacterAbility(int score, int racialBonus = 0)
        {
            _score = score;
            RacialBonus = racialBonus;
        }

        public int ClassBonus { get; internal set; }
        
        public int Score
        {
            get { return _score + RacialBonus + ImprovementBonus; }
            set { _score = value; }
        }

        public int MaxScore { get; internal set; }
        public int Modifier => CalculateModifier(Score);
        public int RacialBonus { get; internal set; }
        public int ImprovementBonus { get; internal set; }
        
        private static int CalculateModifier(int score)
        {
            return score / 2 - 5;
        }
    }
}