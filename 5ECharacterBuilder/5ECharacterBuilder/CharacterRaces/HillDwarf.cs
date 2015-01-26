namespace _5ECharacterBuilder.CharacterRaces
{
    class HillDwarf : Dwarf
    {
        public HillDwarf(ICharacter character) : base(character)
        {
            Abilities.Wisdom.RacialBonus = 1;

            AddRaceFeature("Dwarven Toughness");
        }

        public override string Race => "Hill Dwarf";

        public override int MaxHp => base.MaxHp + HitDice.Count;
    }
}