using MVC5Library;

namespace _5ECharacterBuilder.CharacterBackgrounds
{
    internal sealed class Acolyte : CharacterBackground
    {
        public Acolyte(ICharacter character) : base(character)
        {
            Skills.Chosen.Add(AvailableSkill.Insight);
            Skills.Chosen.Add(AvailableSkill.Religion);
            Languages.Max += 2;
        }

        public override string Background => "Acolyte";

        public override Skills Skills
        {
            get
            {
                var skills = new Skills(base.Skills);
                skills.Max += 2;
                return skills;
            }
        }
    }
}