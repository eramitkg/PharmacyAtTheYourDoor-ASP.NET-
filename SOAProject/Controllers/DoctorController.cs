using SOAProject.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SOAProject.Controllers
{
    public class DoctorController : Controller
    {
        private static int doctorId;
        private static List<Recipe> recipesList;

        public void Index()
        {
            Response.Redirect("~/Doctor/Recipes/" + doctorId);
        }


        public ActionResult Recipes(int id)
        {
            doctorId = id;

            RecipeOperation recipeOp = RecipeOperation.getInstance();
            recipesList = recipeOp.GetRecipes("/getmedicinesfordoctor", new Dictionary<string, string>
            {
                { "doctorId", doctorId.ToString()}
            });

            return View(recipesList);
        }

        public ActionResult RecipeDetail(int id)
        {
            Recipe foundedRecipe = null;
            foreach (var recipe in recipesList)
            {
                if (recipe.RECIPEID == id)
                {
                    foundedRecipe = recipe;
                    break;
                }
            }
            return PartialView("RecipeDetail", foundedRecipe);
        }

        public ActionResult WriteRecipe()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WriteRecipe(FormCollection form)
        {
            RecipeOperation reOP = RecipeOperation.getInstance();

            var result = reOP.CreateRecipe("/createrecipefordoctor", new Dictionary<string, string>
            {
                { "doctorId", doctorId.ToString()},
                { "tcNo", form["txtTcNo"] },
                { "medName", form["txtMedName"] },
                { "medType", form["txtMedType"] },
                { "medUsage", form["txtMedUsage"] }
            });

            if (result)
            {
                ToastrService.AddToUserQueue(new Toastr("Reçete Başarılı Bir Şekilde Eklendi", "Reçete Yazıldı.", ToastrType.Success));
                return RedirectToAction("Recipes", "Doctor", new { @id = doctorId });
            }
            else
            {
                ToastrService.AddToUserQueue(new Toastr("Reçete Eklenemedi", "Reçete Yazılamadı.", ToastrType.Error));
            }

            return null;

        }
    }
}