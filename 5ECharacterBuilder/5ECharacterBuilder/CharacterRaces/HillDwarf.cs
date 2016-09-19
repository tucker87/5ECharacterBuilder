namespace _5ECharacterBuilder.CharacterRaces
{
    internal class HillDwarf : Dwarf
    {
        public HillDwarf(ICharacter character) : base(character)
        {
            Abilities.Wisdom.RacialBonus = 1;
            AddRaceFeature("Dwarven Toughness");
        }

        public override string Race => "Hill Dwarf";
        
        public override int CalculateMaxHp(HitDice hitDice) => base.CalculateMaxHp(hitDice) + hitDice.Count;
    }
}