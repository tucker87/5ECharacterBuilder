using System;

namespace _5ECharacterBuilder
{
    public class CharacterAttributeScores
    {
        public CharacterAttributeScores(int strength, int constitution, int dexterity, int intelligence, int wisdom, int charisma)
        {
            Strength = VerifyScore(strength);
            Constitution = VerifyScore(constitution);
            Dexterity = VerifyScore(dexterity);
            Intelligence = VerifyScore(intelligence);
            Wisdom = VerifyScore(wisdom);
            Charisma = VerifyScore(charisma);
        }

        public int Strength { get; private set; }
        public int Constitution { get; private set; }
        public int Dexterity { get; private set; }
        public int Intelligence { get; private set; }
        public int Wisdom { get; private set; }
        public int Charisma { get; private set; }

        private static int VerifyScore(int score)
        {
            if (score < 1)
                throw new Exception("Score cannot be less than one");

            if (score > 20)
                throw new Exception("Score cannot be greater than twenty");

            return score;
        }
    }

    public class CharacterAttributes
    {
        public CharacterAttributes(CharacterAttributeScores scores)
        {
            Strength = new CharacterAttribute(scores.Strength);
            Constitution = new CharacterAttribute(scores.Constitution);
            Dexterity = new CharacterAttribute(scores.Dexterity);
            Intelligence = new CharacterAttribute(scores.Intelligence);
            Wisdom = new CharacterAttribute(scores.Wisdom);
            Charisma = new CharacterAttribute(scores.Charisma);
        }

        public CharacterAttribute Strength { get; private set; }
        public CharacterAttribute Constitution { get; private set; }
        public CharacterAttribute Dexterity { get; private set; }
        public CharacterAttribute Intelligence { get; private set; }
        public CharacterAttribute Wisdom { get; private set; }
        public CharacterAttribute Charisma { get; private set; }
    }

    public class CharacterAttribute
    {
        public CharacterAttribute(int score)
        {
            Score = score;
            Modifier = CalculateModifier(score);
        }

        public int Score { get; private set; }
        public int Modifier { get; private set; }
        
        private static int CalculateModifier(int score)
        {
            return score / 2 - 5;
        }
    }
}