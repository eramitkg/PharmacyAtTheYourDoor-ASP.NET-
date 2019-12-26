using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOAProject.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public int UserID { get; set; }
        public string Tc { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Neigborhood { get; set; }
        public string Substreet { get; set; }
        public string Street { get; set; }
        public string Province { get; set; }
        public string County { get; set; }
        public string PharmacyName { get; set; }
    }
}