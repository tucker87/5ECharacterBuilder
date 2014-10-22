using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using _5ECharacterBuilder;

namespace ExampleFrontEnd
{
    public partial class MainWindow
    {
        private Character _character;
        
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
            _character = LevelBox.Text == "1'" ? 
                new Character((AvailableRaces)RaceBox.SelectedValue, (AvailableClasses)ClassBox.SelectedValue) : 
                new Character((AvailableRaces)RaceBox.SelectedValue, (AvailableClasses)ClassBox.SelectedValue, Convert.ToInt32(LevelBox.Text));

            SetChracterScores();
            UpdateCharacterRacialBonuses();
            UpdateCharacterModifiers();
        }

        private void SetChracterScores()
        {
            _character.Attributes.Strength.Score = GetAttributeScore(StrengthScoreTextBox);
            _character.Attributes.Dexterity.Score = GetAttributeScore(DexterityScoreTextBox);
            _character.Attributes.Constitution.Score = GetAttributeScore(ConstitutionScoreTextBox);
            _character.Attributes.Intelligence.Score = GetAttributeScore(IntelligenceScoreTextBox);
            _character.Attributes.Wisdom.Score = GetAttributeScore(WisdomScoreTextBox);
            _character.Attributes.Charisma.Score = GetAttributeScore(CharismaScoreTextBox);
        }
        
        private static int GetAttributeScore(TextBox textBox)
        {
            return textBox.Text == "" ? 1 : Convert.ToInt32(textBox.Text);
        }

        private void UpdateCharacterRacialBonuses()
        {
            StrengthRacialTextBox.Text = _character.Attributes.Strength.RacialBonus.ToString(CultureInfo.InvariantCulture);
            DexterityRacialTextBox.Text = _character.Attributes.Strength.RacialBonus.ToString(CultureInfo.InvariantCulture);
            ConstitutionRacialTextBox.Text = _character.Attributes.Strength.RacialBonus.ToString(CultureInfo.InvariantCulture);
            IntelligenceRacialTextBox.Text = _character.Attributes.Strength.RacialBonus.ToString(CultureInfo.InvariantCulture);
            WisdomRacialTextBox.Text = _character.Attributes.Strength.RacialBonus.ToString(CultureInfo.InvariantCulture);
            CharismaRacialTextBox.Text = _character.Attributes.Strength.RacialBonus.ToString(CultureInfo.InvariantCulture);
        }

        private void UpdateCharacterModifiers()
        {
            StrengthModTextBox.Text = "" + (_character.Attributes.Strength.Modifier + _character.Attributes.Strength.RacialBonus);
            DexterityModTextBox.Text = "" + (_character.Attributes.Dexterity.Modifier + _character.Attributes.Dexterity.RacialBonus);
            ConstitutionModTextBox.Text = "" + (_character.Attributes.Constitution.Modifier + _character.Attributes.Constitution.RacialBonus);
            IntelligenceModTextBox.Text = "" + (_character.Attributes.Intelligence.Modifier + _character.Attributes.Intelligence.RacialBonus);
            WisdomModTextBox.Text = "" + (_character.Attributes.Wisdom.Modifier + _character.Attributes.Wisdom.RacialBonus);
            CharismaModTextBox.Text = "" + (_character.Attributes.Charisma.Modifier + _character.Attributes.Charisma.RacialBonus);
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
