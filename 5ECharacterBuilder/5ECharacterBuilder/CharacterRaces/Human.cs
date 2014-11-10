using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterRaces
{
    class Human : CharacterRace
    {
        public Human(ICharacter character) : base(character)
        {
            foreach (var attribute in Attributes)
                attribute.RacialBonus = 1;

            ChosenLanguages.AddRange(new List<AvailableLanguages>(ChosenLanguages) { AvailableLanguages.Common });
        }

        public override string Race
        {
            get { return "Human"; }
        }

        public override int LanguageCount
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