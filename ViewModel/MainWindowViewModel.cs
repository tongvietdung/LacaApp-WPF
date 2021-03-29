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
        public RelayCommand AddRecipeCommand { get; set; }

        private void ExecuteAddRecipeCommand()
        {
            // Open Recipe Window
            MessageBox.Show("New window opened");
        }

        public RelayCommand<object> DeleteRecipeCommand { get; set; }

        private void ExecuteDeleteRecipeCommand(object item)
        {
            recipes.Remove((RecipeModel)item);
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
