using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Recipe.Models.DbRecipe;
namespace Recipe.Models
{
    public class RecipeDetailsViewModel
    {
        public Recipe.Models.DbRecipe.Recipe Recipe { get; set; }

        public Recipe.Models.DbRecipe.IngredientAmount NewIngredient { get; set; }
    }
}