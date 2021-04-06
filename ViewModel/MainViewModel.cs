using GalaSoft.MvvmLight.Command;
using LacaApp.Model;
using LacaApp.View;
using System;
using System.Collections.Generic;
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
    [Serializable()]
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

        // To control when adding new recipe and when editing existed one
        // < 0 : add new
        // >= 0 : edit
        public int EditRecipeIndex { get; set; } = -1;

        #endregion

        #region Buttons

        // Add Recipe Window Button
        public RelayCommand<int> OpenAddRecipeWindowCommand { get; set; }

        public void ExecuteOpenAddRecipeWindowCommand(int recipeIndex)
        {
            RecipeWindow newWindow = new RecipeWindow(this)
            {
                DataContext = this
            };

            // If >=0 then editing existed recipe
            if (recipeIndex >= 0)
            {
                NewRecipe = RecipeModels[recipeIndex];
                EditRecipeIndex = recipeIndex;
            }

            newWindow.ShowDialog();
        }

        // Add Ingredient Window Button 
        public RelayCommand OpenAddIngredientWindowCommand { get; set; }

        private void ExecuteOpenAddIngredientWindowCommand()
        {
            IngredientWindow newWindow = new IngredientWindow
            {
                DataContext = this
            };

            newWindow.ShowDialog();
        }

        // Product Calculation Window Button 
        public RelayCommand OpenProductCalculationWindowCommand { get; set; }

        private void ExecuteOpenProductCalculationWindowCommand()
        {
            ProductCalculationWindow newWindow = new ProductCalculationWindow
            {
                DataContext = this
            };
            newWindow.ShowDialog();
        }

        // Product Calculation Window Button 
        public RelayCommand OpenIngredientCalculationWindowCommand { get; set; }

        private void ExecuteOpenIngredientCalculationWindowCommand()
        {
            // Reset state when open new window
            foreach (var item in RecipeModels)
            {
                item.IsSelected = false;
                item.CalculateAmount = 0;
            }

            IngredientCalculationWindow newWindow = new IngredientCalculationWindow
            {
                DataContext = this
            };

            newWindow.ShowDialog();
        }

        // Delete Recipe Command
        public RelayCommand<object> DeleteRecipeCommand { get; set; }

        private void ExecuteDeleteRecipeCommand(object recipe)
        {
            RecipeModels.Remove((RecipeModel)recipe);
            EditRecipeIndex = -1;
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
            NewRecipe.Ingredients.Add(new IngredientModel());
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
            // If new recipe (meaning EditRecipeIndex < 0) then add to list
            if (EditRecipeIndex < 0)
            {
                RecipeModels.Add(NewRecipe);
            } else // replacing old with new recipe
            {
                RecipeModels[EditRecipeIndex] = NewRecipe;
            }

            foreach (IngredientModel item in NewRecipe.Ingredients)
            {
                AddToIngredientList(item);
            }

            // Reset view model to reset UI
            NewRecipe = new RecipeModel();

            EditRecipeIndex = -1;

            Save();

            window.Close();
        }

        #endregion

        #endregion

        /*------------------------------------------------------------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------------------------------------------------*/
        /// <summary>
        /// Implementation for Ingredient window UI
        /// </summary>
        #region Ingredient Window View Model
        private ObservableCollection<IngredientModel> ingredientModels = new ObservableCollection<IngredientModel>();
        public ObservableCollection<IngredientModel>  IngredientModels
        {
            get
            {
                return ingredientModels;
            }

            set
            {
                if (ingredientModels != value)
                {
                    ingredientModels = value;
                    RaisePropertyChanged(nameof(ingredientModels));
                }
            }
        }
        #endregion

        /*------------------------------------------------------------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------------------------------------------------------*/
        #region Product Calculation Window View Model

        #region Variable
        private ObservableCollection<IngredientModel> result { get; set; } = new ObservableCollection<IngredientModel>();
        public ObservableCollection<IngredientModel> Result
        {
            get
            {
                return result;
            }

            set
            {
                if (result != value)
                {
                    result = value;
                }
                RaisePropertyChanged(nameof(result));
            }
        }
        #endregion

        #region Button
        public RelayCommand CalculateIngredientCommand { get; set; }
        private void ExecuteCalculateIngredientCommand()
        {
            Result = new ObservableCollection<IngredientModel>();

            foreach (var recipe in RecipeModels)
            {
                if (recipe.IsSelected)
                {
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        int index = Contains(Result, ingredient.Name);

                        if (index >= 0) {
                            Result[index].Amount += ingredient.Amount * recipe.CalculateAmount;
                        } else
                        {
                            Result.Add(new IngredientModel(ingredient.Name, ingredient.Amount * recipe.CalculateAmount));
                        }
                    }
                }
            }
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
            PATH = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/lacadata.txt";
            // Load data from file
            Load();

            #region Main Window buttons
            OpenAddRecipeWindowCommand = new RelayCommand<int>(ExecuteOpenAddRecipeWindowCommand);

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

            #region Ingredient Calculation Window Button
            CalculateIngredientCommand = new RelayCommand(ExecuteCalculateIngredientCommand);
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

        string PATH;

        static DataSerializer dataSerializer = new DataSerializer();

        public void Save()
        {
            Object[] Data = new Object[2] { RecipeModels, IngredientModels };

            dataSerializer.BinarySerialize(Data, PATH);
        }

        public void Load()
        {
            // Deserialize data from file
            Object[] Data = (Object[])dataSerializer.BinaryDeserialize(PATH);

            // Get the lists
            if (Data != null)
            {
                RecipeModels = (ObservableCollection<RecipeModel>)Data[0];
                IngredientModels = (ObservableCollection<IngredientModel>)Data[1];
            }

            // If file empty then initiate lists
            if (RecipeModels == null)
            {
                RecipeModels = new ObservableCollection<RecipeModel>();
            }

            if (IngredientModels == null)
            {
                IngredientModels = new ObservableCollection<IngredientModel>();
            }
        }
        #endregion

        #region Helper 
        private void AddToIngredientList(IngredientModel ingredientModel)
        {
            bool existed = false;
            foreach (var item in IngredientModels)
            {
                if (item.Name.Equals(ingredientModel.Name, StringComparison.OrdinalIgnoreCase))
                {
                    existed = true;
                    break;
                }
            }

            if (!existed)
            {
                IngredientModels.Add(ingredientModel);
            }
        }

        private int Contains(ObservableCollection<IngredientModel> result, string name)
        {
            int index = -1;

            foreach (var item in result)
            {
                if (name.Equals(item.Name, StringComparison.OrdinalIgnoreCase))
                {
                    index = result.IndexOf(item);
                    break;
                }
            }

            return index;
        }
        #endregion
    }
}
