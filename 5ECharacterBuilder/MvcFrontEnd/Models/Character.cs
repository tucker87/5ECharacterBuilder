using DelegateDecompiler;
using _5ECharacterBuilder;

namespace MvcFrontEnd.Models
{
    public class Character
    {
        public Character(ICharacter character)
        {
            Name = character.Name;
            Race = character.Race;
            Background = character.Background;
            Classes = character.ClassesString;

            Strength = new Ability(character.Abilities.Strength);
            Dexterity = new Ability(character.Abilities.Dexterity);
            Constitution = new Ability(character.Abilities.Constitution);
            Intelligence = new Ability(character.Abilities.Intelligence);
            Wisdom = new Ability(character.Abilities.Wisdom);
            Charisma = new Ability(character.Abilities.Charisma);
        }

        public Ability Strength { get; set; }
        public Ability Dexterity { get; set; }
        public Ability Constitution { get; set; }
        public Ability Intelligence { get; set; }
        public Ability Wisdom { get; set; }
        public Ability Charisma { get; set; }
        
        public string Name { get; set; }
        public string Race { get; set; }
        public string Background { get; set; }
        public string Classes { get; set; }
    }

    public class Ability
    {
        public Ability(CharacterAbility ability)
        {
            Score = ability.Score;
            Name = ability.Name;
        }

        public string Name { get; set; }
        public int Score;
        
        [Computed]
        public int Modifier => Score / 2 - 5;
    }
}