using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SOAProject.Models
{
    public class RecipeOperation
    {
        private static RecipeOperation recipeOp;
        private  RecipeOperation() { }

        public static RecipeOperation getInstance()
        {
            return recipeOp ?? (recipeOp = new RecipeOperation());
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
                    if (recipe.RECIPEID == r.RECIPEID)
                    {
                        r.MEDICINENAME += "-" + recipe.MEDICINENAME;
                        r.TYPE += "-" + recipe.TYPE;
                        r.USAGE += "-" + recipe.USAGE;

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

        public List<User> GetUsers(string url, Dictionary<string, string> dictionary)
        {
            var result = ApiConnect.Post(url, dictionary);

            List<User> userList = JsonConvert.DeserializeObject<List<User>>(result.Result.ToString());

            if (userList.Count > 0)
            {
                return userList;
            }
            return null;
        }
    }
}