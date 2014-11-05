using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterRaces
{
    sealed class Human : CharacterRace
    {
        public Human(ICharacter character) : base(character)
        {
            foreach (var attribute in Attributes)
                attribute.RacialBonus = 1;
            
            RaceLanguages.AddRange(new List<AvailableLanguages>(RaceLanguages) { AvailableLanguages.Common });
        }

        public override string Race
        {
            get { return "Human"; }
        }

        public override int RaceLanguageCount
        {
            get { return 2; }
        }

        public override string Size
        {
            get { return "Medium"; }
        }

        public override int Speed
        {
            get { return 30; }
        }
    }
}