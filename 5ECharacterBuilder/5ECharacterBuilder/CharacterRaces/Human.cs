using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterRaces
{
    sealed class Human : CharacterRace
    {
        public Human(Character character) : base(character)
        {
            foreach (var attribute in Attributes)
                attribute.RacialBonus = 1;

            Race = "Human";
            RaceLanguageCount = 2;

            Speed = 30;
            Size = "Medium";

            RaceLanguages.AddRange(new List<AvailableLanguages>(RaceLanguages) { AvailableLanguages.Common });
        }
    }
}