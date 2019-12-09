using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOAProject.Models
{
    public class User
    {
        public int USERID { get; set; }
        public string TC { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string SURNAME { get; set; }
        public string PHONE { get; set; }
        public string NEIGBORHOOD { get; set; }
        public string SUBSTREET { get; set; }
        public string STREET { get; set; }
        public string PROVINCE { get; set; }
        public string COUNTY { get; set; }
        public int PATIENTID { get; set; }

    }
}