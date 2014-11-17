using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using _5ECharacterBuilder;

namespace ExampleFrontEnd
{
    public partial class MainWindow
    {
        private ICharacter _character;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            ClassBox.ItemsSource = Enum.GetNames(typeof(AvailableClasses));
            RaceBox.ItemsSource = Enum.GetNames(typeof(AvailableRaces));
            ClassBox.SelectedIndex = 0;
            RaceBox.SelectedIndex = 0;
            MakeNewCharacter();
        }

        private void RaceClassBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MakeNewCharacter();
        }

        private void ScoreTextBox_OnChange(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded) return;
            var score = GetAttributeScore((TextBox) sender);
            if (score < 1 || score > 20) return;
            MakeNewCharacter();
        }

        private void MakeNewCharacter()
        {
            if (RaceBox.SelectedIndex < 0 || ClassBox.SelectedIndex < 0) return;
            var selectedRace = (AvailableRaces) RaceBox.SelectedIndex;
            var selectedClass = (AvailableClasses) ClassBox.SelectedIndex;
            _character = LevelBox.Text == "1"
                ? CharacterFactory.BuildACharacter(selectedRace, selectedClass, AvailableBackgrounds.Criminal)
                : CharacterFactory.BuildACharacter(selectedRace, selectedClass, AvailableBackgrounds.Criminal, Convert.ToInt32(LevelBox.Text));

            SetChracterScores();
            UpdateAll();
        }

        private void UpdateAll()
        {
            UpdateCharacterRacialBonuses();
            UpdateCharacterModifiers();
            UpdateBasicStats();
        }

        private void SetChracterScores()
        {
            _character.Abilities.Strength.Score = GetAttributeScore(StrengthScoreTextBox);
            _character.Abilities.Dexterity.Score = GetAttributeScore(DexterityScoreTextBox);
            _character.Abilities.Constitution.Score = GetAttributeScore(ConstitutionScoreTextBox);
            _character.Abilities.Intelligence.Score = GetAttributeScore(IntelligenceScoreTextBox);
            _character.Abilities.Wisdom.Score = GetAttributeScore(WisdomScoreTextBox);
            _character.Abilities.Charisma.Score = GetAttributeScore(CharismaScoreTextBox);
        }
        
        private static int GetAttributeScore(TextBox textBox)
        {
            return textBox.Text == "" ? 1 : Convert.ToInt32(textBox.Text);
        }

        private void UpdateCharacterRacialBonuses()
        {
            StrengthRacialTextBox.Text = _character.Abilities.Strength.RacialBonus.ToString(CultureInfo.InvariantCulture);
            DexterityRacialTextBox.Text = _character.Abilities.Dexterity.RacialBonus.ToString(CultureInfo.InvariantCulture);
            ConstitutionRacialTextBox.Text = _character.Abilities.Constitution.RacialBonus.ToString(CultureInfo.InvariantCulture);
            IntelligenceRacialTextBox.Text = _character.Abilities.Intelligence.RacialBonus.ToString(CultureInfo.InvariantCulture);
            WisdomRacialTextBox.Text = _character.Abilities.Wisdom.RacialBonus.ToString(CultureInfo.InvariantCulture);
            CharismaRacialTextBox.Text = _character.Abilities.Charisma.RacialBonus.ToString(CultureInfo.InvariantCulture);
        }

        private void UpdateCharacterModifiers()
        {
            StrengthModTextBox.Text = "" + (_character.Abilities.Strength.Modifier);
            DexterityModTextBox.Text = "" + (_character.Abilities.Dexterity.Modifier);
            ConstitutionModTextBox.Text = "" + (_character.Abilities.Constitution.Modifier);
            IntelligenceModTextBox.Text = "" + (_character.Abilities.Intelligence.Modifier);
            WisdomModTextBox.Text = "" + (_character.Abilities.Wisdom.Modifier);
            CharismaModTextBox.Text = "" + (_character.Abilities.Charisma.Modifier);
        }

        private void UpdateBasicStats()
        {
            AcTextBox.Text = "" + _character.ArmorClass;
            SizeTextBox.Text = "" + _character.Size;
            SpeedTextBox.Text = "" + _character.Speed;
            InitiativeTextBox.Text = "" + _character.Initiative;
            EquippedArmorTextBox.Text = "" + _character.EquippedArmor.Name;
            HpTextBox.Text = "" + _character.MaxHp;
            HdTextBox.Text = "1d" + string.Join(" 1d", _character.HitDice);

            ArmorProfListBox.ItemsSource = _character.ArmorProficiencies;
            SkillProfListBox.ItemsSource = _character.Skills.Available;
        }

        private void ScoreTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox) sender;
            if (textBox.Text == "") textBox.Text = "1";
            if (GetAttributeScore(textBox) < 1) textBox.Text = "1";
            if (GetAttributeScore(textBox) > 20) textBox.Text = "20";
        }
    }
}
