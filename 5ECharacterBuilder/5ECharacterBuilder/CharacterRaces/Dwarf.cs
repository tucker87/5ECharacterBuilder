using System.Collections.Generic;

namespace _5ECharacterBuilder.CharacterRaces
{
    class Dwarf : CharacterRace
    {
        public Dwarf(ICharacter character) : base(character)
        {
            Attributes.Constitution.RacialBonus = 2;

            ChosenLanguages.AddRange(new List<AvailableLanguages>(ChosenLanguages) { AvailableLanguages.Common, AvailableLanguages.Dwarvish });
            WeaponProficiencies.AddRange(new List<AvailableWeapon>(WeaponProficiencies) {AvailableWeapon.BattleAxe});
            WeaponProficiencies.AddRange(new List<AvailableWeapon>(WeaponProficiencies) {AvailableWeapon.HandAxe});
            WeaponProficiencies.AddRange(new List<AvailableWeapon>(WeaponProficiencies) {AvailableWeapon.ThrowingHammer});
            WeaponProficiencies.AddRange(new List<AvailableWeapon>(WeaponProficiencies) {AvailableWeapon.WarHammer});
        }

        public override int LanguageCount
        {
            get { return 2; }
        }

        public override string Size
        {
            get { return "Medium"; }
        }

        public override int Speed
        {
            get { return 25; }
        }
    }
}