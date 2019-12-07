using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOAProject.Models;

namespace SOAProject.Controllers
{
    public class PatientController : Controller
    {
        private static int patientId;
        private static List<Recipe> recipesList;

        public void Index()
        { 
            Response.Redirect("~/Patient/Recipes/" + patientId);
        }

        public ActionResult Recipes(int id, bool isDelivered = false)
        {
            int delivered = isDelivered ? 1 : 0;
            patientId = id;

            RecipeOperation recipeOp = RecipeOperation.getInstance();
            recipesList = recipeOp.GetRecipes("/getmedicinesforpatient", new Dictionary<string, string>
            {
                { "PatientId", id.ToString()},
                { "IsDelivered", delivered.ToString()}
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
    }
}