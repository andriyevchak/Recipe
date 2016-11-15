using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recipe.Models.BusinessObjects
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public virtual List<Recipe> Recipes { get; set; }

    }
}