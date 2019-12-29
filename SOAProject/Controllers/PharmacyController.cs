using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOAProject.Models;

namespace SOAProject.Controllers
{
    [Authorize(Roles = "pharmacy")]
    public class PharmacyController : Controller
    {
        private static List<Recipe> recipeList;
        private static List<Patient> patientList;

        public ActionResult Recipes(bool isDelivered = false)
        {
            int pharmacyId = GetPharmacyId();

            int delivered = isDelivered ? 1 : 0;

            ApiOperation recipeOp = ApiOperation.GetInstance();
            recipeList = recipeOp.GetRecipes("/getmedicinesforpharmacy", new Dictionary<string, string>
            {
                { "PharmacyID", pharmacyId.ToString()},
                { "IsDelivered", delivered.ToString()}
            });

            return View(recipeList);
        }

        public ActionResult RecipeDetail(int id)
        {
            Recipe foundedRecipe = null;
            foreach (var recipe in recipeList)
            {
                if (recipe.RecipeID == id)
                {
                    foundedRecipe = recipe;
                    break;
                }
            }
            return PartialView("RecipeDetail", foundedRecipe);
        }

        public ActionResult Patients()
        {
            int pharmacyId = GetPharmacyId();
            ApiOperation recipeOp = ApiOperation.GetInstance();
            patientList = recipeOp.GetPatients("/getpatientsforpharmacy", new Dictionary<string, string>
            {
                { "PharmacyId", pharmacyId.ToString()}
            });

            return View(patientList);
        }

        public ActionResult PatientDetail(int id)
        {
            Patient foundedPatient = null;
            foreach (var patient in patientList)
            {
                if (patient.UserID == id)
                {
                    foundedPatient = patient;
                    break;
                }
            }
            return PartialView("PatientDetail", foundedPatient);
        }

        private int GetPharmacyId()
        {
            return Convert.ToInt32(Session["PharmacyId"]);
        }
    }
}