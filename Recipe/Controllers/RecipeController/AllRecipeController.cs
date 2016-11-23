using Recipe.Models.DbRecipe;
using System.Web.Mvc;

namespace Recipe.Controllers.RecipeController
{
    public class AllRecipeController : Controller
    {
        // GET: AllRecipe
        public ActionResult AllRecipe()
        {
            RecipeContext db = new RecipeContext();
            
            return View(db.Recipes);
        }
    }
}