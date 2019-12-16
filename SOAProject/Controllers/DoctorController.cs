using System;
using SOAProject.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

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
                { "doctorId", id.ToString()}
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


            var result =ApiConnect.Post("/createrecipefordoctor", new Dictionary<string, string>
            {
                { "doctorId", doctorId.ToString()},
                { "tcNo", form["txtTcNo"] }
            });

            if (Convert.ToInt16(result.Result) == -1)
            {
                ToastrService.AddToUserQueue(new Toastr("Kullanıcı Bulunamadı", "Reçete Yazılamadı.", ToastrType.Error));
                return RedirectToAction("Recipes", "Doctor", new { @id = doctorId });
            }
                


            int i = Convert.ToInt32(form["counter"]);

            for (int j = 1; j <= i; j++)
            {
                string medName, medType, medUsage;

                medName = form["txtMedName" + j];
                medType = form["txtMedType" + j];
                medUsage = form["txtMedUsage" + j];

                var result2 = ApiConnect.Post("/addmedicinetorecipefordoctor", new Dictionary<string, string>
                {
                    { "medName", medName},
                    { "medType", medType },
                    { "medUsage", medUsage }
                });
            }

            if (Convert.ToInt32(result.Result) == 0)
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