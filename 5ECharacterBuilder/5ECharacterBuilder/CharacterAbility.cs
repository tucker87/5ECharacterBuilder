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
            Strength = new CharacterAbility(strength, "Strength");
            Dexterity = new CharacterAbility(dexterity, "Dexterity");
            Constitution = new CharacterAbility(constitution, "Constitution");
            Intelligence = new CharacterAbility(intelligence, "Intelligence");
            Wisdom = new CharacterAbility(wisdom, "Wisdom");
            Charisma = new CharacterAbility(charisma, "Charisma");
        }

        public CharacterAbilities(CharacterAbilityScores abilityScores)
        {
            Strength = new CharacterAbility(abilityScores.Strength, "Strength");
            Dexterity = new CharacterAbility(abilityScores.Dexterity, "Dexterity");
            Constitution = new CharacterAbility(abilityScores.Constitution, "Constution");
            Intelligence = new CharacterAbility(abilityScores.Intelligence, "Intelligence");
            Wisdom = new CharacterAbility(abilityScores.Wisdom, "Wisdom");
            Charisma = new CharacterAbility(abilityScores.Charisma, "Charisma");
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

        private IEnumerable<CharacterAbility> Abilities
        {
            get
            {
                return new List<CharacterAbility> {Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma};
            }
        }

        public CharacterAbility Strength { get; private set; }
        public CharacterAbility Dexterity { get; private set; }
        public CharacterAbility Constitution { get; private set; }
        public CharacterAbility Intelligence { get; private set; }
        public CharacterAbility Wisdom { get; private set; }
        public CharacterAbility Charisma { get; private set; }

        public int ImprovementPoints { get; internal set; }
        public int SpentAbilityImprovementPoints
        {
            get
            {
                return Strength.ImprovementBonus +
                       Dexterity.ImprovementBonus +
                       Constitution.ImprovementBonus +
                       Intelligence.ImprovementBonus +
                       Wisdom.ImprovementBonus +
                       Charisma.ImprovementBonus;
            }
        }

        public IEnumerator<CharacterAbility> GetEnumerator()
        {
            return Abilities.TakeWhile(o => o != null).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class CharacterAbility
    {
        private int _score;

        public CharacterAbility(int score, string name, int racialBonus = 0)
        {
            _score = score;
            RacialBonus = racialBonus;
            MaxScore = 20;
            Name = name;
        }

        public int Score
        {
            get
            {
                var score = _score + RacialBonus + ImprovementBonus + ClassBonus;
                return score > MaxScore ? MaxScore : score;
            }
            set { _score = value; }
        }

        public string Name { get; internal set; }
        public int MaxScore { get; internal set; }
        public int Modifier { get { return CalculateModifier(Score); } }
        public int RacialBonus { get; internal set; }
        public int ImprovementBonus { get; internal set; }
        public int ClassBonus { get; internal set; }

        private static int CalculateModifier(int score)
        {
            return score / 2 - 5;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}