using MVC5Library;

namespace _5ECharacterBuilder.CharacterRaces
{
    class Elf : CharacterRace
    {
        public Elf(ICharacter character): base(character)
        {
            Abilities.Dexterity.RacialBonus = 2;

            Languages.Chosen.Add(AvailableLanguages.Common);
            Languages.Chosen.Add(AvailableLanguages.Elvish);

            Skills.Available.Add(AvailableSkill.Perception);
            Skills.Chosen.Add(AvailableSkill.Perception);

            AddRaceFeature("Darkvision");
            AddRaceFeature("Keen Senses");
            AddRaceFeature("Fey Ancestry");
            AddRaceFeature("Trance");

            Languages.Max += 2;
        }
    }
}