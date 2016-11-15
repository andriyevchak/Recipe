using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recipe.Models.BusinessObjects
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }

        public virtual List<Product> Products { get; set; }

    }
}