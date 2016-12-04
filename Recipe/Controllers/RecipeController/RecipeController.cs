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
        [HttpGet]
        public ActionResult AllRecipe(string Name , string searchString)
        {
            RecipeContext db = new RecipeContext();

            var GenreLst = new List<string>();

            var GenreQry = from d in db.Recipes
                           orderby d.Name
                           select d.Name;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.Name = new SelectList(GenreLst);


            if (!String.IsNullOrEmpty(searchString))
            {
                return View(db.Recipes.Where(s => s.Name.Contains(searchString)));
            }


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
        public async Task<ActionResult> AddRecipe(Models.DbRecipe.Recipe model, HttpPostedFileBase file)
        {
            RecipeContext db = new RecipeContext();

            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Images/Recipes"), pic);
                model.ImageUrl = "/Images/Recipes/"+pic;

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
    }
}