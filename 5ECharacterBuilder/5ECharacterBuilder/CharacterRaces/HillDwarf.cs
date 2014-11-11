using System.Linq;

namespace _5ECharacterBuilder.CharacterRaces
{
    class HillDwarf : Dwarf
    {
        public HillDwarf(ICharacter character) : base(character)
        {
            Attributes.Wisdom.RacialBonus = 1;

            using (var db = new CharacterBuilderDB())
                Features.RaceFeatures.Add(db.Features.Single(f => f.Name == "Dwarven Toughness"));
        }

        public override int MaxHp
        {
            get { return base.MaxHp + HitDice.Count; }
        }
    }
}