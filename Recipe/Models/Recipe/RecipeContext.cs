using System.Data.Entity;

namespace Recipe.Models.Recipe
{
    public class RecipeContext : DbContext
    {
        public RecipeContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientAmount> IngredientAmounts { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
    }
}