using System.Threading.Tasks;
using System.Web.Mvc;
using Recipe.Models;
using Recipe.Models.DbRecipe;

namespace Recipe.Controllers.RecipeController
{
    public class AddRecipeController : Controller
    {
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult AddRecipe()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddRecipe(Recipe.Models.DbRecipe.Recipe model)
        {
            RecipeContext db = new RecipeContext();

            db.Recipes.Add(model);
            db.SaveChanges();

            return View(model);
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult AddIngredient()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddIngredient(Ingredient model)
        {
            RecipeContext db = new RecipeContext();

            db.Ingredients.Add(model);
            db.SaveChanges();
            
            return View(model);
        }
    }
}