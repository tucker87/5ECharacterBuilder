using System.Linq;

namespace _5ECharacterBuilder.CharacterRaces
{
    class Elf : CharacterRace
    {
        public Elf(ICharacter character): base(character)
        {
            Attributes.Dexterity.RacialBonus = 2;

            ChosenLanguages.Add(AvailableLanguages.Common);
            ChosenLanguages.Add(AvailableLanguages.Elvish);

            AvailableSkills.Add(AvailableSkill.Perception);
            ChosenSkills.Add(AvailableSkill.Perception);

            using (var db = new CharacterBuilderDB())
            {
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Darkvision"));
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Keen Senses"));
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Fey Ancestry"));
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Trance"));
            }
        }

        public override int LanguageCount
        {
            get { return 2; }
        }
    }
}