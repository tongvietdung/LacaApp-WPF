using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using LacaApp.Model;
using LacaApp.ViewModel;
using System;

namespace LacaApp
{
    /// <summary>
    /// Interaction logic for RecipeWindow.xaml
    /// </summary>
    public partial class RecipeWindow : Window
    {
        MainViewModel vm;

        public RecipeWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            vm = viewModel;
        }

        // Only number in textbox
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Reset state when open new recipe window
        private void This_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            vm.EditRecipeIndex = -1;
            vm.NewRecipe = new RecipeModel();
        }
    }
}
