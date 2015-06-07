using System;
using System.Windows;
using _5ECharacterBuilder;

namespace ExampleFrontEnd
{
    public partial class NewCharacter
    {
        public NewCharacter()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            ClassBox.ItemsSource = Enum.GetNames(typeof(AvailableClasses));
            RaceBox.ItemsSource = Enum.GetNames(typeof(AvailableRaces));
            BackgroundBox.ItemsSource = Enum.GetNames(typeof(AvailableBackgrounds));
            ClassBox.SelectedIndex = 0;
            RaceBox.SelectedIndex = 0;
            BackgroundBox.SelectedIndex = 0;
        }
    }
}
