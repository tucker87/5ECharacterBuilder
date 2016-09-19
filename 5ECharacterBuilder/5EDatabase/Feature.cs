namespace _5EDatabase
{
    public class Feature
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ClassFeature : Feature
    {
        public ClassFeature()
        {
            
        }

        public ClassFeature(string name, int level = 1, string description = null)
        {
            LevelObtained = level;
            Name = name;
            Description = description;
        }
        
        public int LevelObtained { get; set; }
    }

    public class ClassPathFeature : ClassFeature
    {
        public ClassPathFeature()
        {

        }
        
        public ClassPathFeature(string name, Path classPath, int level = 1, string description = null)
            :base(name, level, description)
        {
            ClassPath = classPath;
        }

        public Path ClassPath { get; set; }
    }

    public class RaceFeature : Feature
    {
        public RaceFeature()
        {
            
        }

        public RaceFeature(string name, Race race = Race.Any, string description = null)
        {
            Race = race;
            Name = name;
            Description = description;
        }

        public RaceFeature(string name, string description = null, Race race = Race.Any)
        {
            Race = race;
            Name = name;
            Description = description;
        }

        public Race Race { get; set; }
    }
}
