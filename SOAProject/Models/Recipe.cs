using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOAProject.Models
{
    public class Recipe
    {
        public int RECIPEID { get; set; }
        public string MEDICINENAME { get; set; }
        public string TYPE { get; set; }
        public string USAGE { get; set; }
        public DateTime ISSUEDATE { get; set; }
        public string DEPARTMENT { get; set; }
        public string TC { get; set; }
        public string USERNAME { get; set; }
        public string SURNAME { get; set; }
    }
}