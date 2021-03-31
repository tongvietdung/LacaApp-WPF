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

        public RecipeWindow()
        {
            InitializeComponent();
            this.DataContext = new RecipeWindowViewModel(this.Close);
        }

        // Only number in textbox
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
