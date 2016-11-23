using System.Collections.Generic;

namespace Recipe.Models.DbRecipe
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public virtual List<IngredientAmount> Ingredients { get; set; }

    }
}