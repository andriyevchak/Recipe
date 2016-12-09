using Microsoft.AspNet.Identity;
using Recipe.Models.DbRecipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recipe.Controllers.RecipeController
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MyRatedRecipes()
        {
            RecipeContext db = new RecipeContext();

            var ingredientsBox = new List<string>();

            var Ingredient = from d in db.Ingredients
                             orderby d.Name
                             select d.Name;

            List<Models.DbRecipe.Recipe> recipes = db.Recipes.ToList();
            List<Models.DbRecipe.Recipe> userRecipes = new List<Models.DbRecipe.Recipe>();
            
            ingredientsBox.AddRange(Ingredient.Distinct());
            ViewBag.Ingredients = new SelectList(ingredientsBox);

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.currentUserId = User.Identity.GetUserId();
            }
            for (int i = 0; i < recipes.Count; i++)
            {
                if (recipes[i].RatedUsers != null)
                for (int j = 0; j < recipes[i].RatedUsers.Count; j++)
                {
                    if (recipes[i].RatedUsers[j].UserId.Equals(User.Identity.GetUserId()))
                    {
                        userRecipes.Add(recipes[i]);
                    }
                }
            }
            userRecipes = userRecipes.OrderByDescending(x => {
                double mark = 0;
                if (x.RatedUsers != null)
                {
                    for (int i = 0; i < x.RatedUsers.Count; i++)
                    {
                        if (x.RatedUsers[i].UserId.Equals(ViewBag.currentUserId))
                        {
                            mark = x.RatedUsers[i].Mark;
                        }
                    }
                }
                return mark;
            }).ToList();
            return View(userRecipes);
        }
    }
}