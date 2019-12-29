using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SOAProject.Models
{
    public class ApiOperation
    {
        private static ApiOperation recipeOp;
        private  ApiOperation() { }

        public static ApiOperation GetInstance()
        {
            return recipeOp ?? (recipeOp = new ApiOperation());
        }

        public List<Recipe> GetRecipes(string url, Dictionary<string, string> dictionary)
        {
            var result = ApiConnect.Post(url, dictionary);

            List<Recipe> recipes = JsonConvert.DeserializeObject<List<Recipe>>(result.Result.ToString());

            if (recipes.Count > 0)
            {
                return DeserializeRecipe(recipes);
            }

            return null;
        }
        public List<Recipe> DeserializeRecipe(List<Recipe> recipes)
        {
            List<Recipe> recipes2 = new List<Recipe>();

            foreach (var recipe in recipes)
            {
                bool isSame = false;
                foreach (var r in recipes2)
                {
                    if (recipe.RecipeID == r.RecipeID)
                    {
                        r.MedicineName += "-" + recipe.MedicineName;
                        r.Type += "-" + recipe.Type;
                        r.Usage += "-" + recipe.Usage;

                        isSame = true;
                        break;
                    }
                }

                if (isSame)
                    continue;

                recipes2.Add(recipe);
            }

            return recipes2;
        }

        public List<Patient> GetPatients(string url, Dictionary<string, string> dictionary)
        {
            var result = ApiConnect.Post(url, dictionary);

            List<Patient> patientList = JsonConvert.DeserializeObject<List<Patient>>(result.Result.ToString());

            if (patientList.Count > 0)
            {
                return patientList;
            }
            return null;
        }
    }
}