using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recipe.Models.Recipe
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }

        public virtual List<Recipe> Recipes { get; set; }

    }
}