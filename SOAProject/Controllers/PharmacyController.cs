using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOAProject.Models;

namespace SOAProject.Controllers
{
    public class PharmacyController : Controller
    {
        private static int? pharmacyId = 0; 
        private static List<Recipe> recipeList;
        private static List<User> userList;

        public void Index()
        {
            Response.Redirect("~/Pharmacy/Recipes/" + pharmacyId);
        }
        public ActionResult Recipes(int? id, bool isDelivered = false)
        {
            int delivered = isDelivered ? 1 : 0;
            
            if(id != null)
                pharmacyId = id;

            RecipeOperation recipeOp = RecipeOperation.getInstance();
            recipeList = recipeOp.GetRecipes("/getmedicinesforpharmacy", new Dictionary<string, string>
            {
                { "PharmacyId", pharmacyId.ToString()},
                { "IsDelivered", delivered.ToString()}
            });

            return View(recipeList);
        }

        public ActionResult RecipeDetail(int id)
        {
            Recipe foundedRecipe = null;
            foreach (var recipe in recipeList)
            {
                if (recipe.RECIPEID == id)
                {
                    foundedRecipe = recipe;
                    break;
                }
            }
            return PartialView("RecipeDetail", foundedRecipe);
        }

        public ActionResult Users()
        {
            RecipeOperation recipeOp = RecipeOperation.getInstance();
            userList = recipeOp.GetUsers("/getusersforpharmacy", new Dictionary<string, string>
            {
                { "PharmacyId", pharmacyId.ToString()}
            });

            return View(userList);
        }

        public ActionResult UserDetail(int id)
        {
            User foundedUser = null;
            foreach (var user in userList)
            {
                if (user.USERID == id)
                {
                    foundedUser = user;
                    break;
                }
            }
            return PartialView("UserDetail", foundedUser);
        }
    }
}