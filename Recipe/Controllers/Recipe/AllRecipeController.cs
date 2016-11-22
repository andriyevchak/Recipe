using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recipe.Controllers.Recipe
{
    public class AllRecipeController : Controller
    {
        // GET: AllRecipe
        public ActionResult AllRecipe()
        {
            return View();
        }
    }
}