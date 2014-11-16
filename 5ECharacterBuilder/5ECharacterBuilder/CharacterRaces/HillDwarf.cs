using System.Linq;

namespace _5ECharacterBuilder.CharacterRaces
{
    class HillDwarf : Dwarf
    {
        public HillDwarf(ICharacter character) : base(character)
        {
            Attributes.Wisdom.RacialBonus = 1;

            Features.RaceFeatures.Add(CharacterData.FeatureData.Single(f => f.Name == "Dwarven Toughness"));
        }

        public override string Race
        {
            get { return "Hill Dwarf"; }
        }

        public override int MaxHp
        {
            get { return base.MaxHp + HitDice.Count; }
        }
    }
}