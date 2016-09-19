using System.Linq;
using NUnit.Framework;
using _5ECharacterBuilder;
using _5EDatabase;

namespace _5ECharacterBuilderTests.CharacterClassTests
{
    [TestFixture]
    class ClericTests
    {
        private static ICharacter _cleric;

        [SetUp]
        public void SetUp()
        {
            _cleric = CharacterFactory.BuildACharacter(Race.Human, Class.Cleric, Background.Acolyte);
        }

        [Test]
        public void ClericsAreClerics()
        {
            Assert.IsTrue(_cleric.Classes.Contains(Class.Cleric));
        }

        [Test]
        public void ClericsHaveD8HitDice()
        {
            Assert.AreEqual(8, _cleric.HitDice[0]);
        }

        [Test]
        public void ClericsHaveLightArmorProficiency()
        {
            Assert.IsTrue(Armory.LightArmor.IsSubsetOf(_cleric.ArmorProficiencies));
        }

        [Test]
        public void ClericsHaveMediumArmorProficiency()
        {
            Assert.IsTrue(Armory.MediumArmor.IsSubsetOf(_cleric.ArmorProficiencies));
        }

        [Test]
        public void ClericsHaveShieldProficiency()
        {
            Assert.Contains(ArmorType.Shield, _cleric.ArmorProficiencies);
        }

        [Test]
        public void ClericsAreProficientWithSimpleWeapons()
        {
            Assert.IsTrue(Armory.SimpleWeapons.IsSubsetOf(_cleric.WeaponProficiencies));
        }

        [Test]
        public void ClericsAreProficientWithWisdomAndCharismaSavingThrows()
        {
            Assert.Contains(SavingThrow.Wisdom, _cleric.SavingThrows.ToList());
            Assert.Contains(SavingThrow.Charisma, _cleric.SavingThrows.ToList());
        }

        [Test]
        public void ClericsCanChoose2SkillsFromTheirList()
        {
            Assert.AreEqual(2, _cleric.Skills.Max - _cleric.Skills.Chosen.Count);

            Assert.Contains(Skill.History, _cleric.Skills.Available);
            Assert.Contains(Skill.Insight, _cleric.Skills.Available);
            Assert.Contains(Skill.Medicine, _cleric.Skills.Available);
            Assert.Contains(Skill.Persuasion, _cleric.Skills.Available);
            Assert.Contains(Skill.Religion, _cleric.Skills.Available);
        }

        [Test]
        public void ClericsGainTheirBaseFeatures()
        {
            Assert.Contains("Spellcasting", _cleric.ClassFeatures.Select(cf => cf.Name).ToList());
        }

        
    }
}
