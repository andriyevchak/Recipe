using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Recipe.Models.DbRecipe;
using Recipe.Enums;

namespace Recipe.Models
{
    public class RecipeAddEditViewModel
    {
        public DbRecipe.Recipe Recipe { get; set;}

        public string NewIngredientName { get; set; }

        public int NewIngredientAmount { get; set; }

        public UnitsOfMeasurement NewIngredientUnits { get; set; }

        public RecipeAddEditViewModel()
        {
            Recipe = new DbRecipe.Recipe();
            Recipe.Ingredients = new List<IngredientAmount>();
        }
    }
}