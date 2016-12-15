using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Recipe.Models;
using Recipe.Models.DbRecipe;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Recipe.Controllers.RecipeController
{
    public class RecipeController : Controller
    {
        // GET: AllRecipe
        //[HttpGet]
        //public ActionResult AllRecipe()
        //{
        //    RecipeContext db = new RecipeContext();

        //    return View(db.Recipes);
        //}

        // GET: AllRecipe

        static double? from;
        static double? to;

        [HttpGet]
        public ActionResult AllRecipe(IEnumerable<string> ingredients, string searchString, string TimeFrom, string TimeTo)
        {
            RecipeContext db = new RecipeContext();

           

            var Ingredient = from d in db.Ingredients
                             orderby d.Name
                             select d.Name;

            List<Models.DbRecipe.Recipe> recipes = db.Recipes.ToList();
            List<Models.DbRecipe.Recipe> recipesIngredients = new List<Models.DbRecipe.Recipe>();

            var ingredientsBox = new List<string>();
            ingredientsBox.AddRange(Ingredient.Distinct());
            ViewBag.Ingredients = new SelectList(ingredientsBox);


            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(s => s.Name.Contains(searchString)).ToList();
            }

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.currentUserId = User.Identity.GetUserId();
            }


            TimeSpan from = TimeSpan.MinValue;
            TimeSpan to = TimeSpan.MaxValue;
            from = TimeSpan.MinValue;
            to = TimeSpan.MaxValue;
            if (TimeFrom != null && TimeFrom != "") from = TimeSpan.Parse(TimeFrom);
            if (TimeTo != null && TimeTo != "") to = TimeSpan.Parse(TimeTo);
            initRatingRange();

            if (ingredients != null&& (ingredients.Count() > 0 && !ingredients.First().Equals("")) && ingredients.Count() != 0)
            {
                //recipes = (List<Models.DbRecipe.Recipe>)(from r in recipes
                //                                         from i in r.Ingredients
                //                                         from si in ingredients
                //                                         where i.Ingredient.Name == si
                //                                         select r).Distinct().ToList();

                foreach (var r in recipes)
                {
                    int count = 0;
                    foreach (var si in r.Ingredients)
                    {
                        if (ingredients.Contains(si.Ingredient.Name))
                        {
                            count++;
                        }
                    }
                    if(count== ingredients.Count())
                    {
                        recipesIngredients.Add(r);
                    }
                }
                recipesIngredients = recipesIngredients
                    .Where(r => r.Time >= from && r.Time <= to && r.Rating >= double.Parse(Session["ratingFrom"].ToString()) 
                        && r.Rating <= double.Parse(Session["ratingTo"].ToString()))
                    .ToList();
                return View(recipesIngredients);

            }

            recipes = recipes.Where(r => r.Time >= from && r.Time <= to && 
                 r.Rating >= double.Parse(Session["ratingFrom"].ToString()) && r.Rating <= double.Parse(Session["ratingTo"].ToString()))
                .ToList();
            return View(recipes);
        }

        private void initRatingRange()
        {
            if (Session["ratingFrom"] == null) Session["ratingFrom"] = 0;
            if (Session["ratingTo"] == null) Session["ratingTo"] = 5;
        }
        [HttpPost]
        public void SetRating(double? from, double? to)
        {
            Session["ratingFrom"] = from;
            Session["ratingTo"] = to;
            initRatingRange();
        }
        [HttpPost]
        public void RateRecipe(long RecipeId, double value)
        {
            RecipeContext db = new RecipeContext();
            var recipe = db.Recipes.Where(r => r.RecipeId == RecipeId).Single();
            ShortUserInfo user = null;
            for(int i = 0; i < recipe.RatedUsers.Count; i++)
            {
                if(recipe.RatedUsers[i].UserId.Equals(User.Identity.GetUserId()))
                {
                    user = recipe.RatedUsers[i];
                    break;
                }
            }
            var difference = value;
            if (User.Identity.GetUserId() != null)
            {
                if (user == null && User.Identity.GetUserId() != null)
                {
                    user = new ShortUserInfo();
                    user.UserId = User.Identity.GetUserId();
                    user.Mark = value;
                    db.Users.Add(user);
                    db.SaveChanges();
                    recipe.RatedUsers.Add(user);
                }
                else
                {
                    difference = value - user.Mark;
                    user.Mark = value;
                }
                double s = 0;
                for (int i = 0; i < recipe.RatedUsers.Count; i++)
                    s += recipe.RatedUsers[i].Mark;
                recipe.Rating = s / recipe.RatedUsers.Count;
                db.SaveChanges();
            }
            AllRecipe(null, null, null, null);
        }
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult AddRecipe()
        {
            RecipeContext db = new RecipeContext();

            List<SelectListItem> ingNames = db.Ingredients.Select(n => new SelectListItem { Value = n.Name, Text = n.Name }).Distinct().ToList();
            ViewBag.IngNames = ingNames;

            RecipeAddEditViewModel model = new RecipeAddEditViewModel();

            return View(model);
        }

        // POST: /Account/Register
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddRecipe(RecipeAddEditViewModel model, HttpPostedFileBase file, string submit)
        {
            RecipeContext db = new RecipeContext();

            switch (submit)
            {
                case "Add":
                    Ingredient ing = db.Ingredients.Where(n => n.Name == model.NewIngredientName).First();
                    model.Recipe.Ingredients.Add(new IngredientAmount
                    {
                        Ingredient = ing,
                        Amount =  model.NewIngredientAmount,
                        UnitOfMeasurement = model.NewIngredientUnits
                    });

                    model.NewIngredientName = default(string);
                    break;

                default:
                    if (file != null)
                    {
                        string pic = System.IO.Path.GetFileName(file.FileName);
                        string path = System.IO.Path.Combine(
                                               Server.MapPath("~/Images/Recipes"), pic);
                        model.Recipe.ImageUrl = "/Images/Recipes/" + pic;

                        // file is uploaded
                        file.SaveAs(path);

                        // save the image path path to the database or you can send image
                        // directly to database
                        // in-case if you want to store byte[] ie. for DB
                        using (MemoryStream ms = new MemoryStream())
                        {
                            file.InputStream.CopyTo(ms);
                            byte[] array = ms.GetBuffer();
                        }

                    }

                    //db.Recipes.Add(model.Recipe);
                    db.Entry<Models.DbRecipe.Recipe>(model.Recipe).State = EntityState.Added;
                    db.SaveChanges();
                    break;
            }

            List<SelectListItem> ingNames = db.Ingredients.Select(n => new SelectListItem { Value = n.Name, Text = n.Name }).Distinct().ToList();
            ViewBag.IngNames = ingNames;
            return View(model);
        }

        // GET: /Recipe/Edit
        [Authorize]
        public ActionResult Edit(int id)
        {

            RecipeContext db = new RecipeContext();
            //int IntId = Convert.ToInt32(id);

            RecipeAddEditViewModel model = new RecipeAddEditViewModel
            {
                Recipe = db.Recipes.Where(x => x.RecipeId == id).FirstOrDefault()
            };

            List<SelectListItem> ingNames = db.Ingredients.Select(n => new SelectListItem { Value = n.Name, Text = n.Name }).Distinct().ToList();
            ViewBag.IngNames = ingNames;

            return View(model);
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<ActionResult> Edit(Models.RecipeAddEditViewModel model, string submit)
        {
            RecipeContext db = new RecipeContext();

            switch (submit)
            {
                case "Add":
                    Ingredient ing = db.Ingredients.Where(n => n.Name == model.NewIngredientName).First();
                    model.Recipe.Ingredients.Add(new IngredientAmount
                    {
                        Ingredient = ing,
                        Amount = model.NewIngredientAmount,
                        UnitOfMeasurement = model.NewIngredientUnits
                    });

                    model.NewIngredientName = default(string);
                    break;

                default:
                    Models.DbRecipe.Recipe current = db.Recipes.Where(x => x.RecipeId == model.Recipe.RecipeId).Single();

                    current.Name = model.Recipe.Name;
                    current.Description = model.Recipe.Description;
                    current.Time = model.Recipe.Time;
                    current.ImageUrl = model.Recipe.ImageUrl;

                    current.Ingredients.Clear();
                    
                    foreach(var item in model.Recipe.Ingredients)
                    {
                        current.Ingredients.Add(item);
                    }

                    db.Entry<Models.DbRecipe.Recipe>(current).State = EntityState.Modified;

                    db.SaveChanges();
                    break;
            }


            List<SelectListItem> ingNames = db.Ingredients.Select(n => new SelectListItem { Value = n.Name, Text = n.Name }).Distinct().ToList();
            ViewBag.IngNames = ingNames;

            return View(model);
        }

        [HttpGet]
        public ViewResult Preview(int id)
        {
            RecipeContext db = new RecipeContext();
            Models.DbRecipe.Recipe current = db.Recipes.Where(x => x.RecipeId == id).Single();

            return View(current);
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/images/profile"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            // after successfully uploading redirect the user
            return RedirectToAction("actionname", "controller name");
        }

        [HttpPost]
        public ViewResult DeleteIngredient(RecipeAddEditViewModel model, string name, int amount)
        {
            RecipeContext db = new RecipeContext();

            for(int i = model.Recipe.Ingredients.Count - 1; i >= 0 ; i--)
            {
                if (model.Recipe.Ingredients[i].Ingredient.Name == name && model.Recipe.Ingredients[i].Amount == amount)
                {
                    model.Recipe.Ingredients.RemoveAt(i);
                    break;
                }
            }
            List<SelectListItem> ingNames = db.Ingredients.Select(n => new SelectListItem { Value = n.Name, Text = n.Name }).Distinct().ToList();
            ViewBag.IngNames = ingNames;

            if (model.Recipe.RecipeId == 0)
            {
                return View("AddRecipe", model);
            }
            else
            {
                return View("Edit", model);
            }
        }
    }
}