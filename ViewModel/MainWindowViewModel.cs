using GalaSoft.MvvmLight.Command;
using LacaApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Windows;

namespace LacaApp.ViewModel
{
    /// <summary>
    /// View Model for Project. Here is where the functions of Buttons, Textbox happens. 
    /// This implements INotifyPropertyChanged to get notified when UI changes or vice versa.
    /// Property recipes will contain the ListView.Items.
    /// </summary>
    [Serializable]
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Implementation for Main window UI. 
        /// </summary>
        #region Main Window View Model
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

        #region Buttons
        // Add Recipe Button
        public RelayCommand OpenAddRecipeWindowCommand { get; set; }

        private void ExecuteOpenAddRecipeWindowCommand()
        {
            RecipeWindow newWindow = new RecipeWindow();
            newWindow.ShowDialog();
        }

        // Add Ingredient Button 
        public RelayCommand OpenAddIngredientWindowCommand { get; set; }

        private void ExecuteOpenAddIngredientWindowCommand()
        {
            RecipeWindow newWindow = new RecipeWindow();
            newWindow.ShowDialog();
        }

        // Product Calculation Button 
        public RelayCommand OpenProductCalculationWindowCommand { get; set; }

        private void ExecuteOpenProductCalculationWindowCommand()
        {
            RecipeWindow newWindow = new RecipeWindow();
            newWindow.ShowDialog();
        }

        // Product Calculation Button 
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
            recipeModels.Remove((RecipeModel)recipe);
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

        #region Buttons
        // Cancel command
        public RelayCommand CancelCommand { get; set; }

        public Action CloseWindow { get; set; }

        public void ExecuteCancelCommand()
        {
            CloseWindow();
        }

        // Add ingredient command
        public RelayCommand AddIngredientCommand { get; set; }

        public void ExecuteAddIngredientCommand()
        {
            ingredients.Add(new IngredientModel());
        }

        // Delete ingredient command
        public RelayCommand<object> DeleteIngredientCommand { get; set; }

        public void ExecuteDeleteIngredientCommand(object ingredient)
        {
            ingredients.Remove((IngredientModel)ingredient);
        }

        // Save command
        public RelayCommand SaveRecipeCommand { get; set; }

        public void ExecuteSaveRecipeCommand()
        {
            newRecipe.ingredients = ingredients;
            string res = "";

            recipeModels.Add(newRecipe);

            foreach(var item in recipeModels)
            {
                res += item.Name + " ";
            }
            MessageBox.Show(res);
            CloseWindow();
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------------------------------------------------*/
        /// <summary>
        /// Mutual Section
        /// </summary>
        #region Constructor
        public MainWindowViewModel(Action action)
        {
            // Load data from file
            Load();

            #region Main Window buttons
            OpenAddRecipeWindowCommand = new RelayCommand(ExecuteOpenAddRecipeWindowCommand);

            OpenAddIngredientWindowCommand = new RelayCommand(ExecuteOpenAddIngredientWindowCommand);

            OpenProductCalculationWindowCommand = new RelayCommand(ExecuteOpenProductCalculationWindowCommand);

            OpenIngredientCalculationWindowCommand = new RelayCommand(ExecuteOpenIngredientCalculationWindowCommand);

            DeleteRecipeCommand = new RelayCommand<object>(ExecuteDeleteRecipeCommand);
            #endregion

            for (int i = 0; i < 3; i++)
            {
                recipeModels.Add(new RecipeModel("name " + i));
            }
            CloseWindow = action;

            #region Add Ingredient Window buttons
            CancelCommand = new RelayCommand(ExecuteCancelCommand);

            SaveRecipeCommand = new RelayCommand(ExecuteSaveRecipeCommand);

            AddIngredientCommand = new RelayCommand(ExecuteAddIngredientCommand);

            DeleteIngredientCommand = new RelayCommand<object>(ExecuteDeleteIngredientCommand);
            #endregion
        }

        //public MainWindowViewModel(Action action)
        //{
        //    //// Load data from file
        //    //Load();
        //    for (int i = 0; i < 3; i++)
        //    {
        //        recipeModels.Add(new RecipeModel("name " + i));
        //    }
        //    CloseWindow = action;

        //    #region Add Ingredient Window buttons
        //    CancelCommand = new RelayCommand(ExecuteCancelCommand);

        //    SaveRecipeCommand = new RelayCommand(ExecuteSaveRecipeCommand);

        //    AddIngredientCommand = new RelayCommand(ExecuteAddIngredientCommand);

        //    DeleteIngredientCommand = new RelayCommand<object>(ExecuteDeleteIngredientCommand);
        //    #endregion
        //}
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
