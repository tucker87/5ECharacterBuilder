using System;
using System.Collections;
using System.Collections.Generic;

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

    public class CharacterAbilities : IEnumerable<CharacterAbility>
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
            Strength.Score = strength;
            Dexterity.Score = dexterity;
            Constitution.Score = constitution;
            Intelligence.Score = intelligence;
            Wisdom.Score = wisdom;
            Charisma.Score = charisma;
        }

        public CharacterAbilities(CharacterAbilityScores abilityScores)
        {
            Strength.Score = abilityScores.Strength;
            Dexterity.Score = abilityScores.Dexterity;
            Constitution.Score = abilityScores.Constitution;
            Intelligence.Score = abilityScores.Intelligence;
            Wisdom.Score = abilityScores.Wisdom;
            Charisma.Score = abilityScores.Charisma;
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
        
        private List<CharacterAbility> List =>
                new List<CharacterAbility>
                {
                    Strength,
                    Dexterity,
                    Constitution,
                    Intelligence,
                    Wisdom,
                    Charisma
                };

        public CharacterAbility Strength { get; } = new CharacterAbility(AbilityName.Strength);
        public CharacterAbility Dexterity { get; } = new CharacterAbility(AbilityName.Dexterity);
        public CharacterAbility Constitution { get; } = new CharacterAbility(AbilityName.Constitution);
        public CharacterAbility Intelligence { get; } = new CharacterAbility(AbilityName.Intelligence);
        public CharacterAbility Wisdom { get; } = new CharacterAbility(AbilityName.Wisdom);
        public CharacterAbility Charisma { get; } = new CharacterAbility(AbilityName.Charisma);

        public int ImprovementPoints { get; internal set; }
        public int SpentAbilityImprovementPoints => Strength.ImprovementBonus +
                                                    Dexterity.ImprovementBonus +
                                                    Constitution.ImprovementBonus +
                                                    Intelligence.ImprovementBonus +
                                                    Wisdom.ImprovementBonus +
                                                    Charisma.ImprovementBonus;

        public IEnumerator<CharacterAbility> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class CharacterAbility
    {
        private int _score;
        private readonly AbilityName _name;

        public CharacterAbility(AbilityName name)
        {
            _name = name;
        }

        public int ClassBonus { get; internal set; }
        
        public int Score
        {
            get { return _score + RacialBonus + ImprovementBonus; }
            set { _score = value; }
        }

        public string Name => _name.ToString();
        public int MaxScore { get; internal set; }
        public int Modifier => CalculateModifier(Score);
        public int RacialBonus { get; internal set; }
        public int ImprovementBonus { get; internal set; }

        public static int CalculateModifier(int score)
        {
            return score/2 - 5;
        }
    }

    public enum AbilityName
    {
        Strength,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma
    }
}