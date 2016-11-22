using System.Threading.Tasks;
using System.Web.Mvc;
using Recipe.Models.Recipe;

namespace Recipe.Controllers.Recipe
{
    public class AddRecipeController : Controller
    {
        //
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
            
            //if (ModelState.IsValid)
            //{
            //    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            //    var result = await UserManager.CreateAsync(user, model.Password);
            //    if (result.Succeeded)
            //    {
            //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

            //        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            //        // Send an email with this link
            //        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            //        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            //        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

            //        return RedirectToAction("Index", "Home");
            //    }
            //    AddErrors(result);
            //}

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}