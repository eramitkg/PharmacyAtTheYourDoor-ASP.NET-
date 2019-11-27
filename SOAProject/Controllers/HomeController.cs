using Newtonsoft.Json;
using SOAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOAProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        
        public ActionResult Index()
        {    
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public void PostUsers()
        {
            var request = ApiConnect.Post("/api/users", new Dictionary<string, string>
                {
                    { "id","12321312313" },
                    { "token", "sdasdsaf213123" },
                    {"geo","oguzhankaymak" }
                }
            );


        }
    }
}