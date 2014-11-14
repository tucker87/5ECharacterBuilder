using System.Linq;

namespace _5ECharacterBuilder.CharacterRaces
{
    class Elf : CharacterRace
    {
        public Elf(ICharacter character): base(character)
        {
            Attributes.Dexterity.RacialBonus = 2;

            Languages.Chosen.Add(AvailableLanguages.Common);
            Languages.Chosen.Add(AvailableLanguages.Elvish);

            Skills.Available.Add(AvailableSkill.Perception);
            Skills.Chosen.Add(AvailableSkill.Perception);

            using (var db = new CharacterBuilderDB())
            {
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Darkvision"));
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Keen Senses"));
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Fey Ancestry"));
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Trance"));
            }

            Languages.Max += 2;
        }
    }
}