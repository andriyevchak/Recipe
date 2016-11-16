﻿using System.Data.Entity;

namespace Recipe.Models.BusinessObjects
{
    public class RecipeContext : DbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientAmount> IngredientAmounts { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
    }
}