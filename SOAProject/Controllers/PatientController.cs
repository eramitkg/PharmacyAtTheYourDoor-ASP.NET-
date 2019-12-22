using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOAProject.Models;

namespace SOAProject.Controllers
{
    [Authorize(Roles = "patient")]
    public class PatientController : Controller
    {
        private static List<Recipe> recipesList;

        public ActionResult Recipes()
        {
            int patientId = GetPatientId();

            RecipeOperation recipeOp = RecipeOperation.getInstance();
            recipesList = recipeOp.GetRecipes("/getmedicinesforpatient", new Dictionary<string, string>
            {
                { "PatientId", patientId.ToString()},
                { "IsDelivered", false.ToString()}
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
        private int GetPatientId()
        {
            return Convert.ToInt32(Session["PatientID"]);
        }
    }
}