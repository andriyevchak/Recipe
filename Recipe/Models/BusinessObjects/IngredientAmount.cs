using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Recipe.Enums;

namespace Recipe.Models.BusinessObjects
{
    public class IngredientAmount
    {
        public int IngredientAmountId { get; set; }
        public int Amount { get; set; }
        public UnitsOfMeasurement UnitOfMeasurement { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Recipe Recipe { get; set; }

    }
}