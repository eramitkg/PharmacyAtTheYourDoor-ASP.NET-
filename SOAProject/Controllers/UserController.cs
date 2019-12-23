using System;
using System.Collections.Generic;
using System.Linq;
using SOAProject.Models;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Security;

namespace SOAProject.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        public static Patient patient;
        public static Doctor doctor;
        public static Pharmacy pharmacy;

        // GET: Patient
        [HttpGet]
        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection form)
        {
            string loginNo = form["loginNo"];
            string password = form["Password"];
            string role = form["role"];
            if (Login(loginNo, password, role))
            {
                ToastrService.AddToUserQueue(new Toastr("Sisteme başarılı bir şekilde giriş yapıldı", "Hoşgeldiniz", ToastrType.Success));
                TempData["login"] = "success";
                if (role == "doctor")
                    return RedirectToAction("Recipes", "Doctor");


                else if (role == "pharmacy")
                    return RedirectToAction("Recipes", "Pharmacy");

                else
                    return RedirectToAction("Recipes", "Patient"); 
            }
            else
                ToastrService.AddToUserQueue(new Toastr("Lütfen giriş bilgilerinizi kontrol ediniz ", "Giriş Yapılamadı", ToastrType.Error));
            

            return View();
        }

        
        public ActionResult Register()
        {
            return View();
        }

        
        public bool Login(string loginNo,string Password,string Role)
        {
            if (Role =="patient")
            {
                var result = ApiConnect.Post("/login", new Dictionary<string, string>
                {
                    { "TCNo",loginNo},
                    {"Password",Password},
                    {"Role",Role}
                });

                List<Patient> patients = JsonConvert.DeserializeObject<List<Patient>>(result.Result.ToString());
                if (patients.Count > 0)
                {
                    patient = patients[0];
                    FormsAuthentication.SetAuthCookie(Role.ToString(), false);
                    Session["PatientID"] = patient.PATIENTID;
                    return true;
                }
                return false;
            }

            else if (Role == "doctor")
            {
                var result = ApiConnect.Post("/login", new Dictionary<string, string>
                {
                    { "TCNo",loginNo},
                    {"Password",Password},
                    {"Role",Role}

                });

                List<Doctor> doctors = JsonConvert.DeserializeObject<List<Doctor>>(result.Result.ToString());
                if (doctors.Count > 0)
                {
                    doctor = doctors[0];
                    FormsAuthentication.SetAuthCookie(Role.ToString(), false);
                    Session["DoctorID"] = doctor.DOCTORID;
                    return true;
                }
                return false;
            }
            
            else if (Role == "pharmacy")
            {
                var result = ApiConnect.Post("/login", new Dictionary<string, string>
                {
                    { "RecordNo",loginNo},
                    {"Password",Password},
                    {"Role",Role}

                });

                List<Pharmacy> pharmacies = JsonConvert.DeserializeObject<List<Pharmacy>>(result.Result.ToString());
                if (pharmacies.Count > 0)
                {
                    pharmacy = pharmacies[0];
                    FormsAuthentication.SetAuthCookie(Role.ToString(), false);
                    Session["PharmacyId"] = pharmacy.PHARMACYID;
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
           
        }


        public ActionResult Logout()
        {
            ToastrService.AddToUserQueue(new Toastr("Başarılı Bir Şekilde Gerçekleşti", "Çıkış Yapıldı", ToastrType.Info));
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}