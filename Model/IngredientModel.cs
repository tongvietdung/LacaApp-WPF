using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LacaApp.Model
{
    /// <summary>
    /// Model for an Ingredient
    /// </summary>
    [Serializable]
    public class IngredientModel
    {
        public int Index { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }

        public string Unit { get; set; }

        public int Price { get; set; }

        public IngredientModel()
        {

        }
    }
}
