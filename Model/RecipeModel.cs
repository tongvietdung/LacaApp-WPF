using System;
using System.Collections.ObjectModel;

namespace LacaApp.Model
{
    /// <summary>
    /// Model for a Recipe
    /// </summary>
    [Serializable]
    public class RecipeModel
    {

        public string Name { get; set; }

        public string Amount { get; set; }

        public string Price { get; set; }

        public bool AsIngredient { get; set; } = false;

        public ObservableCollection<IngredientModel> Ingredients { get; set; } = new ObservableCollection<IngredientModel>();

        public RecipeModel()
        {
        }

        public RecipeModel(string name)
        {
            Name = name;
        }
    }
}
