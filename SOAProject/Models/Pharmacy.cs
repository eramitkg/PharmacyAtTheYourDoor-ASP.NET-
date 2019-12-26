using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOAProject.Models
{
    public class Pharmacy
    {
        public int PharmacyID { get; set; }
        public string PharmacyName { get; set; }
        public string Phone { get; set; }
        public string Neigborhood { get; set; }
        public string Street { get; set; }
        public string Substreet { get; set; }
        public string County { get; set; }
        public string Province { get; set; }

        public override string ToString()
        {
            return PharmacyName;
        }

    }
}