using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LacaApp.Model
{
    /// <summary>
    /// Model for a Recipe
    /// </summary>
    public class RecipeModel
    {

        public string Name { get; set; }

        public string Amount { get; set; }

        public string Price { get; set; }

        public ObservableCollection<IngredientModel> ingredients { get; set; } = new ObservableCollection<IngredientModel>();

        public RecipeModel()
        {
        }

        public RecipeModel(int index, string name)
        {
            Name = name;
        }

    }
}
