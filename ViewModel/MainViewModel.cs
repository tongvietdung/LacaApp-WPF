using GalaSoft.MvvmLight.Command;
using LacaApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace LacaApp.ViewModel
{
    /// <summary>
    /// View Model for Project. Here is where the functions of Buttons, Textbox happens. 
    /// This implements INotifyPropertyChanged to get notified when UI changes or vice versa.
    /// Property recipes will contain the ListView.Items.
    /// </summary>
    [Serializable]
    public class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Implementation for Main window UI. 
        /// </summary>
        #region Main Window View Model

        #region Variables
        // List of recipes which is the ItemsSource for ListViewItem
        #region ObservableCollection<RecipeModel> recipeModels

        private ObservableCollection<RecipeModel> recipeModels;

        public ObservableCollection<RecipeModel> RecipeModels
        {
            get
            {
                return recipeModels;
            }

            set
            {
                if (recipeModels != value)
                {
                    recipeModels = value;
                    RaisePropertyChanged(nameof(recipeModels));
                }
            }
        }
        #endregion

        #endregion

        #region Buttons

        // Add Recipe Window Button
        public RelayCommand<RecipeModel> OpenAddRecipeWindowCommand { get; set; }

        public void ExecuteOpenAddRecipeWindowCommand(RecipeModel newRecipe)
        {
            RecipeWindow newWindow = new RecipeWindow
            {
                DataContext = this
            };

            if (newRecipe != null)
            {
                NewRecipe = newRecipe;
            }

            newWindow.ShowDialog();
        }

        // Add Ingredient Window Button 
        public RelayCommand OpenAddIngredientWindowCommand { get; set; }

        private void ExecuteOpenAddIngredientWindowCommand()
        {
            RecipeWindow newWindow = new RecipeWindow();
            newWindow.ShowDialog();
        }

        // Product Calculation Window Button 
        public RelayCommand OpenProductCalculationWindowCommand { get; set; }

        private void ExecuteOpenProductCalculationWindowCommand()
        {
            RecipeWindow newWindow = new RecipeWindow();
            newWindow.ShowDialog();
        }

        // Product Calculation Window Button 
        public RelayCommand OpenIngredientCalculationWindowCommand { get; set; }

        private void ExecuteOpenIngredientCalculationWindowCommand()
        {
            RecipeWindow newWindow = new RecipeWindow();
            newWindow.ShowDialog();
        }

        // Delete Recipe Command
        public RelayCommand<object> DeleteRecipeCommand { get; set; }

        private void ExecuteDeleteRecipeCommand(object recipe)
        {
            RecipeModels.Remove((RecipeModel)recipe);
            Save();
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------------------------------------------------*/
        /// <summary>
        /// Implementation for Recipe window UI
        /// </summary>
        #region Recipe Window View Model

        #region Variables

        // List of ingredients which is ItemsSource for ListView
        #region ObservableCollection<RecipeModel> ingredients

        private ObservableCollection<IngredientModel> ingredients = new ObservableCollection<IngredientModel>();

        public ObservableCollection<IngredientModel> Ingredients
        {
            get
            {
                return ingredients;
            }

            set
            {
                if (ingredients != value)
                {
                    ingredients = value;
                    RaisePropertyChanged(nameof(ingredients));
                }
            }
        }
        #endregion

        // The recipe to be newly created, or edited.
        #region RecipeModel newRecipe
        private RecipeModel newRecipe = new RecipeModel();
        public RecipeModel NewRecipe
        {
            get
            {
                return newRecipe;
            }

            set
            {
                if (newRecipe != value)
                {
                    newRecipe = value;
                }
                RaisePropertyChanged(nameof(newRecipe));
            }
        }

        #endregion

        #region IngredientModel newIngredient
        private IngredientModel newIngredient = new IngredientModel();
        public IngredientModel NewIngredient
        {
            get
            {
                return newIngredient;
            }

            set
            {
                if (newIngredient != value)
                {
                    newIngredient = value;
                    RaisePropertyChanged(nameof(newIngredient));
                }
            }
        }
        #endregion

        IngredientModel tempInredient;

        #endregion

        #region Buttons
        // Cancel command
        public RelayCommand<Window> CancelCommand { get; set; }

        private void ExecuteCancelCommand(Window window)
        {
            NewRecipe = new RecipeModel();
            window.Close();
        }

        // Add ingredient command
        public RelayCommand AddIngredientCommand { get; set; }

        private void ExecuteAddIngredientCommand()
        {
            NewIngredient = new IngredientModel();
            NewRecipe.Ingredients.Add(NewIngredient);
        }

        // Delete ingredient command
        public RelayCommand<object> DeleteIngredientCommand { get; set; }

        private void ExecuteDeleteIngredientCommand(object ingredient)
        {
            NewRecipe.Ingredients.Remove((IngredientModel)ingredient);
        }

        // Save command
        public RelayCommand<Window> SaveRecipeCommand { get; set; }

        private void ExecuteSaveRecipeCommand(Window window)
        {
            RecipeModels.Add(NewRecipe);

            Save();

            NewRecipe = new RecipeModel();

            window.Close();
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------------------------------------------------*/
        /// <summary>
        /// Mutual Section
        /// </summary>
        #region Constructor
        public MainViewModel()
        {
            // Load data from file
            Load();

            #region Main Window buttons
            OpenAddRecipeWindowCommand = new RelayCommand<RecipeModel>(ExecuteOpenAddRecipeWindowCommand);

            OpenAddIngredientWindowCommand = new RelayCommand(ExecuteOpenAddIngredientWindowCommand);

            OpenProductCalculationWindowCommand = new RelayCommand(ExecuteOpenProductCalculationWindowCommand);

            OpenIngredientCalculationWindowCommand = new RelayCommand(ExecuteOpenIngredientCalculationWindowCommand);

            DeleteRecipeCommand = new RelayCommand<object>(ExecuteDeleteRecipeCommand);
            #endregion

            #region Add Ingredient Window buttons
            CancelCommand = new RelayCommand<Window>(ExecuteCancelCommand);

            SaveRecipeCommand = new RelayCommand<Window>(ExecuteSaveRecipeCommand);

            AddIngredientCommand = new RelayCommand(ExecuteAddIngredientCommand);

            DeleteIngredientCommand = new RelayCommand<object>(ExecuteDeleteIngredientCommand);
            #endregion

        }

        #endregion

        #region Property Change Notification
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion

        #region Save System
        const string PATH = "C:/Users/vietd/Desktop/lacadata.txt";

        static DataSerializer dataSerializer = new DataSerializer();

        public void Save()
        {
            dataSerializer.BinarySerialize(recipeModels, PATH);
        }

        public void Load()
        {
            recipeModels = (ObservableCollection<RecipeModel>)dataSerializer.BinaryDeserialize(PATH);

            if (recipeModels == null)
            {
                recipeModels = new ObservableCollection<RecipeModel>();
            }
        }
        #endregion

    }
}
