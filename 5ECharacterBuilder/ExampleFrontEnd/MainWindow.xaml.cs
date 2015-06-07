using System;
using System.Globalization;
using System.Linq;
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
            BackgroundBox.ItemsSource = Enum.GetNames(typeof (AvailableBackgrounds));
            ClassBox.SelectedIndex = 0;
            RaceBox.SelectedIndex = 0;
            BackgroundBox.SelectedIndex = 0;
            MakeNewCharacter();
        }

        

        private void RaceBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ClassBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void BackgroundBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ScoreTextBox_OnChange(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded) return;
            var score = GetAttributeScore((TextBox) sender);
            if (score < 1 || score > 20) return;
        }

        private void MakeNewCharacter()
        {
            if (RaceBox.SelectedIndex < 0 || ClassBox.SelectedIndex < 0) return;
            var selectedRace = (AvailableRaces) RaceBox.SelectedIndex;
            var selectedClass = (AvailableClasses) ClassBox.SelectedIndex;
            var selectedBackground = (AvailableBackgrounds) ClassBox.SelectedIndex;
            _character = "" + LevelLabel.Content == "1"
                ? CharacterFactory.BuildACharacter(selectedRace, selectedClass, selectedBackground)
                : CharacterFactory.BuildACharacter(selectedRace, selectedClass, selectedBackground, Convert.ToInt32(LevelLabel.Content));

            SetChracterScores();
            UpdateAll();
        }

        private void UpdateAll()
        {
            UpdateCharacterRacialBonuses();
            UpdateCharacterModifiers();
            UpdateBasicStats();
            UpdateLists();
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
            StrengthRacialLabel.Content = _character.Abilities.Strength.RacialBonus.ToString(CultureInfo.InvariantCulture);
            DexterityRacialLabel.Content = _character.Abilities.Dexterity.RacialBonus.ToString(CultureInfo.InvariantCulture);
            ConstitutionRacialLabel.Content = _character.Abilities.Constitution.RacialBonus.ToString(CultureInfo.InvariantCulture);
            IntelligenceRacialLabel.Content = _character.Abilities.Intelligence.RacialBonus.ToString(CultureInfo.InvariantCulture);
            WisdomRacialLabel.Content = _character.Abilities.Wisdom.RacialBonus.ToString(CultureInfo.InvariantCulture);
            CharismaRacialLabel.Content = _character.Abilities.Charisma.RacialBonus.ToString(CultureInfo.InvariantCulture);
        }

        private void UpdateCharacterModifiers()
        {
            StrengthModLabel.Content = "" + (_character.Abilities.Strength.Modifier);
            DexterityModLabel.Content = "" + (_character.Abilities.Dexterity.Modifier);
            ConstitutionModLabel.Content = "" + (_character.Abilities.Constitution.Modifier);
            IntelligenceModLabel.Content = "" + (_character.Abilities.Intelligence.Modifier);
            WisdomModLabel.Content = "" + (_character.Abilities.Wisdom.Modifier);
            CharismaModLabel.Content = "" + (_character.Abilities.Charisma.Modifier);
        }

        private void UpdateBasicStats()
        {
            LevelLabel.Content = "" + _character.Level;
            AcLabel.Content = "" + _character.ArmorClass;
            SizeLabel.Content = "" + _character.Size;
            SpeedLabel.Content = "" + _character.Speed;
            InitiativeLabel.Content = "" + _character.Initiative;
            EquippedLabel.Content = "" + _character.EquippedArmor.Name;
            HpLabel.Content = "" + _character.MaxHp;
            HdLabel.Content = "" + _character.HitDice;
        }

        private void UpdateLists()
        {
            UpdateAvailableSkillListBox();
            UpdateChosenSkillListBox();
        }

        private void UpdateAvailableSkillListBox()
        {
            AvailableSkillListBox.ItemsSource = _character.Skills.Available.Where(s => !_character.Skills.Chosen.Contains(s));
            AvailableSkillListBox.Items.Refresh();
        }

        private void UpdateChosenSkillListBox()
        {
            ChosenSkillListBox.ItemsSource = _character.Skills.Chosen;
            ChosenSkillListBox.Items.Refresh();
        }

        private void ScoreTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox) sender;
            if (textBox.Text == "") textBox.Text = "1";
            if (GetAttributeScore(textBox) < 1) textBox.Text = "1";
            if (GetAttributeScore(textBox) > 20) textBox.Text = "20";
        }

        private void LevelUpButton_OnClick(object sender, RoutedEventArgs e)
        {
            _character.LevelUp((AvailableClasses) Enum.Parse(typeof(AvailableClasses), "" + ClassBox.SelectedValue));
            UpdateAll();
        }

        private void SkillProfListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var item = "" + e.AddedItems[0];
                try
                {
                    _character.ChooseSkill((AvailableSkill) Enum.Parse(typeof (AvailableSkill), "" + item));
                }
                catch(TooManySkillsException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                UpdateAll();
            }
            var listBox = (ListBox) sender;
            listBox.SelectedIndex = -1;
        }
    }
}