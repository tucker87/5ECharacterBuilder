using System;
using System.Windows;
using _5ECharacterBuilder;
using _5EDatabase;

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
            ClassBox.ItemsSource = Enum.GetNames(typeof(Class));
            RaceBox.ItemsSource = Enum.GetNames(typeof(Race));
            BackgroundBox.ItemsSource = Enum.GetNames(typeof(Background));
            ClassBox.SelectedIndex = 0;
            RaceBox.SelectedIndex = 0;
            BackgroundBox.SelectedIndex = 0;
        }
    }
}
