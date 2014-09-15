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
            UpdateChracterScores();
            UpdateCharacterModifiers();
        }

        private void RaceBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _character = new CharacterBase(GetCharacterAttributeScores());
        }

        private void ClassBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void ScoreTextBox_OnChange(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded) return;
            var score = GetAttributeScore((TextBox) sender);
            if (score < 1 || score > 20) return;
            UpdateChracterScores();
            UpdateCharacterModifiers();
        }

        private static int GetAttributeScore(TextBox textBox)
        {
            return textBox.Text == "" ? 1 : Convert.ToInt32(textBox.Text);
        }

        private void UpdateChracterScores()
        {
            var scores = GetCharacterAttributeScores();
            _character = new CharacterBase(scores);
        }

        private CharacterAttributeScores GetCharacterAttributeScores()
        {
            var scores = new CharacterAttributeScores(
                GetAttributeScore(StrengthScoreTextBox),
                GetAttributeScore(DexterityScoreTextBox),
                GetAttributeScore(ConstitutionScoreTextBox),
                GetAttributeScore(IntelligenceScoreTextBox),
                GetAttributeScore(WisdomScoreTextBox),
                GetAttributeScore(CharismaScoreTextBox));
            return scores;
        }

        private void UpdateCharacterModifiers()
        {
            StrengthModTextBox.Text = _character.Attributes.Strength.Modifier.ToString(CultureInfo.InvariantCulture);
            DexterityModTextBox.Text = _character.Attributes.Dexterity.Modifier.ToString(CultureInfo.InvariantCulture);
            ConstitutionModTextBox.Text = _character.Attributes.Constitution.Modifier.ToString(CultureInfo.InvariantCulture);
            IntelligenceModTextBox.Text = _character.Attributes.Intelligence.Modifier.ToString(CultureInfo.InvariantCulture);
            WisdomModTextBox.Text = _character.Attributes.Wisdom.Modifier.ToString(CultureInfo.InvariantCulture);
            CharismaModTextBox.Text = _character.Attributes.Charisma.Modifier.ToString(CultureInfo.InvariantCulture);
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
