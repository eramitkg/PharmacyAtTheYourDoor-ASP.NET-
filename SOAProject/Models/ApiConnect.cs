using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace SOAProject.Models
{
    public class ApiConnect
    {
        public static string ConnString = ConfigurationManager.ConnectionStrings["DefaultApiUrl"].ConnectionString;

        public static object Request(string ApiUrl,Dictionary<string,string> keyValuePairs = null)
        {
            var data = Api(ApiUrl,keyValuePairs);
            return data.Result;
        }

        public async static Task<object> Api(string ApiUrl,Dictionary<string,string> keyValuePairs = null)
        {
            using(var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                client.DefaultRequestHeaders.Add("Session", BaseObject.Session);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BaseObject.Token);
                var content = new FormUrlEncodedContent(keyValuePairs);

                var response = client.PostAsync(ConnString + ApiUrl,content);

                var responseString = response.Result.Content.ReadAsStringAsync();
                var err = responseString.Result;
                try
                {
                    var res = JsonConvert.DeserializeObject(responseString.Result);
                    return res;
                }
                catch (Exception)
                {

                    return err;
                }
            }
        }
    }
}