namespace _5ECharacterBuilder.CharacterRaces
{
    class HillDwarf : Dwarf
    {
        public HillDwarf(ICharacter character) : base(character)
        {
            Abilities.Wisdom.RacialBonus = 1;

            AddRaceFeature("Dwarven Toughness");
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