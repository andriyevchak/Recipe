using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recipe.Controllers.Recipe
{
    public class AddRecipeController : Controller
    {
        // GET: AddRecipe
        public ActionResult Index()
        {
            return View();
        }
    }
}