using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SOAProject.Models;

namespace SOAProject.Controllers
{
    [Authorize(Roles = "patient")]
    public class PatientController : Controller
    {
        private static List<Recipe> recipesList;

        public ActionResult Recipes(bool isDelivered = false)
        {

            int delivered = isDelivered ? 1: 0;
            ViewData["delivered"] = delivered.ToString();
            int patientId = GetPatientId();

            RecipeOperation recipeOp = RecipeOperation.getInstance();
            recipesList = recipeOp.GetRecipes("/getmedicinesforpatient", new Dictionary<string, string>
            {
                { "PatientId", patientId.ToString()},
                { "IsDelivered", delivered.ToString()}
            });

            return View(recipesList);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Recipes(FormCollection form)
        {
            string recipeID = form["recipeID"].ToString();

            var result = ApiConnect.Post("/recipeDeliver", new Dictionary<string, string>
            {
                { "RecipeID", recipeID}
            });

            if (result.Result.ToString() == "1")
            {
                ToastrService.AddToUserQueue(new Toastr("Başarılı Bir Şekilde Gerçekleşti", "Teslim Bildirildi", ToastrType.Info));
                return RedirectToAction("Recipes","Patient");
            }
            return RedirectToAction("Recipes", "Patient");

        }
       
        public ActionResult Settings()
        {
            string patientID = GetPatientId().ToString();

            var result = ApiConnect.Post("/getPatientInformation", new Dictionary<string, string>
            {
                { "PatientID", patientID}
            });

            List<Patient> patients = JsonConvert.DeserializeObject<List<Patient>>(result.Result.ToString());

            var getPharmacies = ApiConnect.Get("/getPharmacies");

            List<Pharmacy> pharmacies = JsonConvert.DeserializeObject<List<Pharmacy>>(getPharmacies.Result.ToString());

            List<SelectListItem> pharmaciesSelectList = new List<SelectListItem>();
            foreach (var item in pharmacies)
            {
                if (patients[0].PharmacyName != item.PharmacyName)
                    pharmaciesSelectList.Add(new SelectListItem { Text = item.PharmacyName, Value = item.PharmacyID.ToString() });
                else
                    pharmaciesSelectList.Add(new SelectListItem { Text = item.PharmacyName, Value = item.PharmacyID.ToString(), Selected = true });
            }

            ViewData["PharmacyList"] = pharmaciesSelectList;

            return View(patients[0]);
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Settings(FormCollection form)
        {
            string pharmacyId = form["PharmacyList"].ToString();
            string patientId = GetPatientId().ToString();

            var result = ApiConnect.Post("/changePharmacy", new Dictionary<string, string>
            {
                {"PatientID", patientId},
                {"PharmacyID",pharmacyId}
            });

            if (result.Result.ToString() == "1")
            {
                ToastrService.AddToUserQueue(new Toastr("Başarılı Bir Şekilde Gerçekleşti", "Eczane Değiştirildi", ToastrType.Info));
                return RedirectToAction("Recipes", "Patient");
            }
            return RedirectToAction("Recipes", "Patient");
        }
        public ActionResult RecipeDetail(int id)
        {
            Recipe foundedRecipe = null;
            foreach (var recipe in recipesList)
            {
                if (recipe.RecipeID == id)
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