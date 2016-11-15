using System.Data.Entity;

namespace Recipe.Models.BusinessObjects
{
    public class RecipeContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
    }
}