using System;
using System.Collections.Generic;

namespace Recipe.Models.DbRecipe
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public TimeSpan Time { get; set; }

        public double Rating { get; set; }
        public virtual List<IngredientAmount> Ingredients { get; set; }

        //public virtual List<ApplicationUser> RatedUsers { get; set; }
        //public string RatedUsers { get; set; }

        public virtual List<ShortUserInfo> RatedUsers { get; set; }
    }
}