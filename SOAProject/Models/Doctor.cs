using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOAProject.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Tc{ get; set; }
        public string Phone { get; set; }
        public string Neigborhood { get; set; }
        public string Street { get; set; }
        public string Substreet { get; set; }
        public string County { get; set; }
        public string Province { get; set; }
        public string Departmant { get; set; }
        public string Healthinstitution { get; set; }
    }
}