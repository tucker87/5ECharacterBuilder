using _5EDatabase;

namespace _5ECharacterBuilder.CharacterRaces
{
    internal class Elf : CharacterRace
    {
        public Elf(ICharacter character): base(character)
        {
            Abilities.Dexterity.RacialBonus = 2;

            Languages.Chosen.Add(Language.Common);
            Languages.Chosen.Add(Language.Elvish);

            Skills.Available.Add(Skill.Perception);
            Skills.Chosen.Add(Skill.Perception);

            AddRaceFeature("Darkvision");
            AddRaceFeature("Keen Senses");
            AddRaceFeature("Fey Ancestry");
            AddRaceFeature("Trance");

            Languages.Max += 2;
        }
    }
}