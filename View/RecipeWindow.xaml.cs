using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using LacaApp.Model;

namespace LacaApp
{
    /// <summary>
    /// Interaction logic for RecipeWindow.xaml
    /// </summary>
    public partial class RecipeWindow : Window
    {
        public ObservableCollection<IngredientModel> Ingredients { get; set; } = new ObservableCollection<IngredientModel>();

        public RecipeWindow()
        {
            InitializeComponent();

            for (int i = 1; i < 3; i++)
            {
                Ingredients.Add(new IngredientModel());
            }

            IngredientList.ItemsSource = Ingredients;
            IngredientList.Items.Refresh();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddIngredientbutton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
