using System.Collections.Generic;

namespace _5ECharacterBuilder2
{
    public class Character
    {
        public Character(AvailableRaces characterRace, AvailableClasses characterClass)
        {
            Class = new Class();
            Class.Name = characterClass.ToString();
            Race = new Race();
            Race.Name = characterRace.ToString();
            Level = 1;
            Experience = 0;
            HitDice = new List<int>();

            Strength = new Attribute();
            Dexterity = new Attribute();
            Constitution = new Attribute();
            Intelligence = new Attribute();
            Wisdom = new Attribute();
            Charisma = new Attribute();
            
        }

        private int _experience;
        public int Experience
        {
            get { return _experience; }
            set
            {
                _experience = value;
                if (Experience >= 750)
                    _level = 2;
            }
        }

        private int _level;
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                if (Level == 3)
                    _experience = 1000;
            }
        }

        public Race Race { get; set; }
        public Class Class { get; set; }
        public List<int> HitDice { get; private set; }
        public Attribute Strength { get; set; }
        public Attribute Dexterity { get; set; }
        public Attribute Constitution { get; set; }
        public Attribute Intelligence { get; set; }
        public Attribute Wisdom { get; set; }
        public Attribute Charisma { get; set; }

        public List<string> GetAllIssues()
        {
            var issues = new List<string>();
            if (HitDice.Count == 0)
                issues.Add("Must have at least 1 HitDice");

            return issues;
        }

        private void AddHitDice(int hitDice)
        {
            HitDice.Add(hitDice);
        }
    }

    public class Attribute
    {
        public Attribute()
        {
            Score = 10;
        }

        private int _score;

        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                Modifier = (_score - 10)/2;
            }
        }

        public int Modifier { get; private set; }
    }

    public class Class
    {
        public string Name { get; set; }
    }

    public class Race
    {
        public string Name { get; set; }
    }
}
