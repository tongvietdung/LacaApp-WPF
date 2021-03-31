using GalaSoft.MvvmLight.Command;
using LacaApp.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace LacaApp.ViewModel
{
    /// <summary>
    /// View Model for Main Window. Here is where the functions of Buttons, Textbox happens. 
    /// This implements INotifyPropertyChanged to get notified when UI changes or vice versa.
    /// Property recipes will contain the ListView.Items.
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // List of recipes which is the ItemsSource for ListViewItem
        #region ObservableCollection<RecipeModel> recipes

        private ObservableCollection<RecipeModel> recipes = new ObservableCollection<RecipeModel>();

        public ObservableCollection<RecipeModel> Recipes
        {
            get
            {
                return recipes;
            }

            set
            {
                if (recipes != value)
                {
                    recipes = value;
                    RaisePropertyChanged(nameof(recipes));
                }
            }
        }
        #endregion

        #region Button
        // Add Recipe Button
        public RelayCommand AddRecipeCommand { get; set; }

        private void ExecuteAddRecipeCommand()
        {
            RecipeWindow newWindow = new RecipeWindow();
            newWindow.ShowDialog();
        }

        // Add Ingredient Button 
        public RelayCommand AddIngredientCommand { get; set; }

        private void ExecuteAddIngredientCommand()
        {
            RecipeWindow newWindow = new RecipeWindow();
            newWindow.ShowDialog();
        }

        // Product Calculation Button 
        public RelayCommand ProductCalculationCommand { get; set; }

        private void ExecuteProductCalculationCommand()
        {
            RecipeWindow newWindow = new RecipeWindow();
            newWindow.ShowDialog();
        }

        // Product Calculation Button 
        public RelayCommand IngredientCalculationCommand { get; set; }

        private void ExecuteIngredientCalculationCommand()
        {
            RecipeWindow newWindow = new RecipeWindow();
            newWindow.ShowDialog();
        }

        // Delete Recipe Command
        public RelayCommand<object> DeleteRecipeCommand { get; set; }

        private void ExecuteDeleteRecipeCommand(object recipe)
        {
            recipes.Remove((RecipeModel)recipe);
        }

        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            // Add dummy recipe list
            for (int i = 0; i < 3; i++)
            {
                recipes.Add(new RecipeModel(recipes.Count, "Name " + i.ToString()));
            }

            // Bind Execution to add button
            AddRecipeCommand = new RelayCommand(ExecuteAddRecipeCommand);

            AddIngredientCommand = new RelayCommand(ExecuteAddIngredientCommand);

            ProductCalculationCommand = new RelayCommand(ExecuteProductCalculationCommand);

            IngredientCalculationCommand = new RelayCommand(ExecuteIngredientCalculationCommand);

            DeleteRecipeCommand = new RelayCommand<object>(ExecuteDeleteRecipeCommand);
        }
        #endregion

        #region Property Change Notification
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
