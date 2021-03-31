using GalaSoft.MvvmLight.Command;
using LacaApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LacaApp.ViewModel
{
    /// <summary>
    /// View Model for Recipe Window. 
    /// 
    /// </summary>
    public class RecipeWindowViewModel : INotifyPropertyChanged
    {
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
        #region RecipeModel recipe
        private RecipeModel recipe = new RecipeModel();

        public RecipeModel Recipe
        {
            get
            {
                return recipe;
            }

            set
            {
                if (recipe != value)
                {
                    recipe = value;
                }
                RaisePropertyChanged(nameof(recipe));
            }
        }
        #endregion

        #region Buttons
        public RelayCommand CancelCommand { get; set; }

        public Action CloseWindow { get; set; }

        public void ExecuteCancelCommand()
        {
            CloseWindow();
        }

        public RelayCommand AddIngredientCommand { get; set; }

        public void ExecuteAddIngredientCommand()
        {
            ingredients.Add(new IngredientModel());
        }

        public RelayCommand<object> DeleteIngredientCommand { get; set; }

        public void ExecuteDeleteIngredientCommand(object ingredient)
        {
            ingredients.Remove((IngredientModel)ingredient);
        }
        #endregion

        public RecipeWindowViewModel(Action action)
        {
            CloseWindow = action;
            CancelCommand = new RelayCommand(ExecuteCancelCommand);
            AddIngredientCommand = new RelayCommand(ExecuteAddIngredientCommand);
            DeleteIngredientCommand = new RelayCommand<object>(ExecuteDeleteIngredientCommand);
        }

        #region Property changed notification
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
