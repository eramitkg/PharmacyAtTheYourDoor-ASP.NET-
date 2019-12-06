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
            string TC = form["TC"];
            string password = form["Password"];
            string role = form["role"];
            if (Login(TC, password, role))
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
                    return RedirectToAction("Contact", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }
            return View();
            
        }

        
        public ActionResult Register()
        {
            return View();
        }

        
        public bool Login(string TC,string Password,string Role)
        {
            var result = ApiConnect.Post("/login", new Dictionary<string, string>
                {
                    { "TCNo",TC},
                    {"Password",Password},
                    {"Role",Role}

                }
            );


            if (Role == "doctor")
            {
                List<Doctor> doctors = JsonConvert.DeserializeObject<List<Doctor>>(result.Result.ToString());
                if (doctors.Count > -1)
                {
                    doctor = doctors[0];
                    return true;
                }
                return false;
            }
            else if (Role == "user")
            {
                List<User> users = JsonConvert.DeserializeObject<List<User>>(result.Result.ToString());
                if (users.Count > 0)
                {
                    user = users[0];
                    return true;
                }
                return false;
            }
            
            else
            {
                //Ayarlanacak
                List<User> resultList = JsonConvert.DeserializeObject<List<User>>(result.Result.ToString());
                return true;
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