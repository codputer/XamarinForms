using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace App2
{
    public class SigninService
    {
        public LoggedInUser Signin(string email,string password)
        {
            HttpClient client = new HttpClient();
             string defaultEmail = "admin@demo1.com";
             string defaultPassword = "Pass@123";
            var dictionary = new Dictionary<string, string>
                                 {
                                     {
                                         "username",
                                         string.IsNullOrWhiteSpace(email) ? defaultEmail : email
                                     },
                                     {
                                         "password",
                                         string.IsNullOrWhiteSpace(password)
                                             ? defaultPassword
                                             : password
                                     },
                                     { "grant_type", "password" }
                                 };

            string url = ApplicationDataFactory.Url + "/token";
            var content = new FormUrlEncodedContent(dictionary);
            HttpResponseMessage responseMessage = client.PostAsync(url, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                LoggedInUser userInfo = JsonConvert.DeserializeObject<LoggedInUser>(result);
                return userInfo;
            }

            return null;
        }
    }
}
