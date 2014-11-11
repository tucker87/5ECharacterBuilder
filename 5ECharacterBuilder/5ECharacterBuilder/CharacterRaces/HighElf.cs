using System.Linq;

namespace _5ECharacterBuilder.CharacterRaces
{
    class HighElf : Elf
    {
        public HighElf(ICharacter character) : base(character)
        {
            Attributes.Intelligence.RacialBonus = 1;

            using (var db = new CharacterBuilderDB())
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Elf Weapon Training"));
        }

        public override string Race
        {
            get { return "High Elf"; }
        }
    }
}