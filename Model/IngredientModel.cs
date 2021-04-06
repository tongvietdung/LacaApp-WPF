using System;

namespace LacaApp.Model
{
    /// <summary>
    /// Model for an Ingredient
    /// </summary>
    [Serializable()]
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

        public IngredientModel(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }
    }
}
