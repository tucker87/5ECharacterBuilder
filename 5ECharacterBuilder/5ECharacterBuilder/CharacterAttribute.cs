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
            Strength = strength;
            Dexterity = dexterity;
            Constitution = constitution;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Charisma = charisma;
        }
    }

    public class CharacterAttributes
    {
        public CharacterAttributes()
        {
            
        }

        public CharacterAttributes(CharacterAttributes attributes)
        {
            Strength = attributes.Strength;
            Dexterity = attributes.Dexterity;
            Constitution = attributes.Constitution;
            Intelligence = attributes.Intelligence;
            Wisdom = attributes.Wisdom;
            Charisma = attributes.Charisma;
        }

        public CharacterAttributes(CharacterAttributeScores attributeScores)
        {
            Strength = new CharacterAttribute(attributeScores.Strength);
            Dexterity = new CharacterAttribute(attributeScores.Dexterity);
            Constitution = new CharacterAttribute(attributeScores.Constitution);
            Intelligence = new CharacterAttribute(attributeScores.Intelligence);
            Wisdom = new CharacterAttribute(attributeScores.Wisdom);
            Charisma = new CharacterAttribute(attributeScores.Charisma);
        }

        public CharacterAttributes(CharacterAttributes attributes, CharacterAttributeScores racialBonuses)
        {
            Strength = attributes.Strength;
            Strength.RacialBonus = racialBonuses.Strength;
            Dexterity = attributes.Dexterity;
            Dexterity.RacialBonus = racialBonuses.Dexterity;
            Constitution = attributes.Constitution;
            Constitution.RacialBonus = racialBonuses.Constitution;
            Intelligence = attributes.Intelligence;
            Intelligence.RacialBonus = racialBonuses.Intelligence;
            Wisdom = attributes.Wisdom;
            Wisdom.RacialBonus = racialBonuses.Wisdom;
            Charisma = attributes.Charisma;
            Charisma.RacialBonus = racialBonuses.Charisma;
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

        public CharacterAttribute(int score, int racialBonus = 0)
        {
            _score = score;
            RacialBonus = racialBonus;
        }

        public int Score
        {
            get { return _score + RacialBonus; }
            set { _score = value; }
        }

        public int Modifier { get { return CalculateModifier(Score); } }
        public int RacialBonus { get; internal set; }
        
        private static int CalculateModifier(int score)
        {
            return score / 2 - 5;
        }
    }
}