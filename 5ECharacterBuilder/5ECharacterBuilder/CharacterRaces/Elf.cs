namespace _5ECharacterBuilder.CharacterRaces
{
    class Elf : CharacterRace
    {
        public Elf(ICharacter character): base(character)
        {
            Attributes.Dexterity.RacialBonus = 2;

            Languages.Chosen.Add(AvailableLanguages.Common);
            Languages.Chosen.Add(AvailableLanguages.Elvish);

            Skills.Available.Add(_5ECharacterBuilder.AvailableSkills.Perception);
            Skills.Chosen.Add(_5ECharacterBuilder.AvailableSkills.Perception);

            AddRaceFeature("Darkvision");
            AddRaceFeature("Keen Senses");
            AddRaceFeature("Fey Ancestry");
            AddRaceFeature("Trance");

            Languages.Max += 2;
        }
    }
}