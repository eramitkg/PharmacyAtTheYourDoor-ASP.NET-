using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOAProject.Models
{
    public class Doctor
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string TC { get; set; }
        public string Phone { get; set; }
        public string NEIGBORHOOD { get; set; }
        public string STREET { get; set; }
        public string SUBSTREET { get; set; }
        public string COUNTY { get; set; }
        public string PROVINCE { get; set; }
        public string DEPARTMENT { get; set; }
        public string HEALTHINSTITUTION { get; set; }
    }
}