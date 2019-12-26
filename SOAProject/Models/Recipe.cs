using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOAProject.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }
        public int Tc { get; set; }
        public string MedicineName { get; set; }
        public string Type { get; set; }
        public string Usage { get; set; }
        public DateTime IssueDate { get; set; }
        public string Departmant { get; set; }
        public string Username { get; set; }
        public string Surname { get; set; }
    }
}