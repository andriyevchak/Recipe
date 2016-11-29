using Recipe.Models.DbRecipe;
using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Recipe.Controllers.RecipeController
{
    public class RecipeController : Controller
    {
        // GET: AllRecipe
        public ActionResult AllRecipe()
        {
            RecipeContext db = new RecipeContext();



            return View(db.Recipes);
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult AddRecipe()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddRecipe(Models.DbRecipe.Recipe model)
        {
            RecipeContext db = new RecipeContext();

            db.Recipes.Add(model);
            db.SaveChanges();

            return View(model);
        }

        // GET: /Recipe/Edit
        public ActionResult Edit(int id)
        {

            RecipeContext db = new RecipeContext();
            //int IntId = Convert.ToInt32(id);
            Models.DbRecipe.Recipe recipe = db.Recipes.Where(x => x.RecipeId == id).FirstOrDefault();

            return View(recipe);
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<ActionResult> Edit(Models.DbRecipe.Recipe model)
        {
            RecipeContext db = new RecipeContext();

            Models.DbRecipe.Recipe current = db.Recipes.Where(x => x.RecipeId == model.RecipeId).Single();

            current = model;

            db.SaveChanges();

            return View(model);
        }
    }
}