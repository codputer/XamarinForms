using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public class HttpClientService<T, TRm> where T : Entity where TRm : RequestModel<T>
    {
        HttpClient client;
        private static string siteName;

        public HttpClientService()
        {
            this.client = new HttpClient();
            string a = ApplicationDataFactory.Url;
            string t = ApplicationDataFactory.UserData.AccessToken;
            this.SetHttpProperties(a, t);
        }

        private bool SetHttpProperties(string address, string token)
        {
            this.client.BaseAddress = new Uri(address);
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }

        public async Task<string> AddAsync(T data)
        {
            string path = siteName + "/api/" + typeof(T).Name + "/Add";
            HttpResponseMessage response = await this.client.PostAsync(path, this.PrepareStringContent(data));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SyncAsync(List<T> data)
        {
            var content = this.PrepareStringContent(data);
            string path = siteName + "/api/" + typeof(T).Name + "/Sync";
            HttpResponseMessage response = await this.client.PostAsync(path, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private StringContent PrepareStringContent(object data)
        {
            string serializeObject = JsonConvert.SerializeObject(data);
            var content = new StringContent(serializeObject, Encoding.UTF8, "application/json");
            return content;
        }

        public async Task<string> EditAsync(T data)
        {
            string path = siteName + "/api/" + typeof(T).Name + "/Edit";
            HttpResponseMessage response = await this.client.PutAsync(path, this.PrepareStringContent(data));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteAsync(string id)
        {
            string path = siteName + "/api/" + typeof(T).Name + "/" + id;
            HttpResponseMessage response = await this.client.DeleteAsync(path);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<Tuple<List<T>, int>> SearchAsync(TRm data)
        {
            try
            {
                string requestUri = siteName + "/api/" + typeof(T).Name + "Query/Search";
                StringContent value = this.PrepareStringContent(data);
                HttpResponseMessage response;
                try
                {
                    response = await this.client.PostAsync(requestUri, value);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                string content = await response.Content.ReadAsStringAsync();
                int count = 0;
                IEnumerable<string> values;
                bool valueFound = response.Headers.TryGetValues("Count", out values);
                Tuple<List<T>, int> searchAsync = null;
                if (valueFound)
                {
                    count = Convert.ToInt32(values.First());
                    searchAsync = new Tuple<List<T>, int>(new List<T>(), count);
                    if (count > 0)
                    {
                        searchAsync = JsonConvert.DeserializeObject<Tuple<List<T>, int>>(content);
                    }
                }

                return searchAsync;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
        
        public virtual async Task<Tuple<List<T>, int>> PullFromServer(TRm request)
        {
            return await this.SearchAsync(request);
        }

        public virtual async Task<string> PushToServer(List<T> list)
        {
            return await this.SyncAsync(list);
        }
    }
}