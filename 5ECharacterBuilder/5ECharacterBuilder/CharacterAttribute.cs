using System;

namespace _5ECharacterBuilder
{
    public class CharacterAttributeScores
    {
        public CharacterAttributeScores(int strength = 10, int dexterity = 10, int constitution = 10, int intelligence = 10, int wisdom = 10, int charisma = 10)
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

    public class RacialBonuses : CharacterAttributeScores
    {
        public RacialBonuses(int strength = 0, int dexterity = 0, int constitution = 0, int intelligence = 0, int wisdom = 0, int charisma = 0)
        {
            Strength = VerifyScore(strength);
            Dexterity = VerifyScore(dexterity);
            Constitution = VerifyScore(constitution);
            Intelligence = VerifyScore(intelligence);
            Wisdom = VerifyScore(wisdom);
            Charisma = VerifyScore(charisma);
        }
    }

    public class CharacterAttributes
    {
        public CharacterAttributes(CharacterAttributeScores scores, RacialBonuses racialBonuses = null)
        {
            if (racialBonuses == null) racialBonuses = new RacialBonuses();
            Strength = new CharacterAttribute(scores.Strength, racialBonuses.Strength);
            Dexterity = new CharacterAttribute(scores.Dexterity, racialBonuses.Dexterity);
            Constitution = new CharacterAttribute(scores.Constitution, racialBonuses.Constitution);
            Intelligence = new CharacterAttribute(scores.Intelligence, racialBonuses.Intelligence);
            Wisdom = new CharacterAttribute(scores.Wisdom, racialBonuses.Wisdom);
            Charisma = new CharacterAttribute(scores.Charisma, racialBonuses.Charisma);

        }

        public CharacterAttribute Strength { get; private set; }
        public CharacterAttribute Dexterity { get; private set; }
        public CharacterAttribute Constitution { get; private set; }
        public CharacterAttribute Intelligence { get; private set; }
        public CharacterAttribute Wisdom { get; private set; }
        public CharacterAttribute Charisma { get; private set; }
    }

    public class CharacterAttribute
    {
        private int _score;
        public CharacterAttribute(int score, int racialBonus)
        {
            _score = score;
            Modifier = CalculateModifier(score);
            RacialBonus = racialBonus;
        }

        public int Score
        {
            get
            {
                return _score + RacialBonus;
            }
            protected set { _score = value; }
        }

        public int Modifier { get; private set; }
        public int RacialBonus { get; private set; }
        
        private static int CalculateModifier(int score)
        {
            return score / 2 - 5;
        }
    }
}