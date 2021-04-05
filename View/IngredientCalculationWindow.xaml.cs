using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace LacaApp.View
{
    /// <summary>
    /// Interaction logic for IngredientCalculationWindow.xaml
    /// </summary>
    public partial class IngredientCalculationWindow : Window
    {
        public IngredientCalculationWindow()
        {
            InitializeComponent();
        }

        // Only number in textbox
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
