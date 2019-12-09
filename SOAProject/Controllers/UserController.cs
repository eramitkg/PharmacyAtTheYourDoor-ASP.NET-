using System;
using System.Collections.Generic;
using System.Linq;
using SOAProject.Models;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace SOAProject.Controllers
{
    public class UserController : Controller
    {
        public static User user;
        public static Doctor doctor;
        public static Pharmacy pharmacy;

        private static int patientId;
        private static int pharmacyId;
        // GET: User
        [HttpGet]
        public ActionResult Login()
        {
            for (int i = 0; i < 10; i++)
            {
                BaseObject.Session += Guid.NewGuid().ToString().Replace("-", "");
            }
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
                if(role == "doctor")
                {
                    //AddCookie("access_token",res.Token);
                    //AddCookie("User_ID", res.User_ID);
                    ToastrService.AddToUserQueue(new Toastr("Başarılı Bir Şekilde Gerçekleşti", "Giriş Yapıldı", ToastrType.Success));
                    return RedirectToAction("Index", "Doctor");
                }
                else if(role == "user")
                {
                    ToastrService.AddToUserQueue(new Toastr("Başarılı Bir Şekilde Gerçekleşti", "Giriş Yapıldı", ToastrType.Success));
                    return RedirectToAction("Recipes", "Patient", new { @id = patientId });
                }
                else if(role == "pharmacy")
                {
                    ToastrService.AddToUserQueue(new Toastr("Başarılı Bir Şekilde Gerçekleşti", "Giriş Yapıldı", ToastrType.Success));
                    return RedirectToAction("Recipes", "Pharmacy", new { @id = pharmacyId });
                }
                else
                {
                    ToastrService.AddToUserQueue(new Toastr("Lütfen giriş bilgilerinizi kontrol ediniz ", "Giriş Yapılamadı", ToastrType.Error));
                }
                
            }
            else
            {
                ToastrService.AddToUserQueue(new Toastr("Lütfen giriş bilgilerinizi kontrol ediniz ", "Giriş Yapılamadı", ToastrType.Error));
            }

            return View();
        }

        
        public ActionResult Register()
        {
            return View();
        }

        
        public bool Login(string loginNo,string Password,string Role)
        {
            if (Role =="user")
            {
                var result = ApiConnect.Post("/login", new Dictionary<string, string>
                {
                    { "TCNo",loginNo},
                    {"Password",Password},
                    {"Role",Role}
                });

                List<User> users = JsonConvert.DeserializeObject<List<User>>(result.Result.ToString());
                if (users.Count > 0)
                {
                    user = users[0];
                    patientId = user.PATIENTID;
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
                if (doctors.Count > -1)
                {
                    doctor = doctors[0];
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
                    pharmacyId = pharmacy.PHARMACYID;
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
           
        }

        public void AddCookie(string cookieName,string cookieValue)
        {
            HttpCookie cookie = new HttpCookie(cookieName, cookieValue);
            cookie.Expires = DateTime.Now.AddYears(1);
            HttpContext.Response.Cookies.Add(cookie);
        }    
    }
}