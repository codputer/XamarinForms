using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpWrappers
{
    public class HttpWrapperBase
    {
        public async Task<string> GetAsync(string url)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return await responseMessage.Content.ReadAsStringAsync();
                }
                throw new Exception(responseMessage.ReasonPhrase);
            }
        }

        public async Task<object> PostAsync(string url, string data)
        {
            using (var client =new HttpClient())
            {
                HttpContent content=new StringContent(data);
                HttpResponseMessage responseMessage = await client.PostAsync(url, content);
                if (responseMessage.IsSuccessStatusCode)
                {
                   return await responseMessage.Content.ReadAsStringAsync();                 
                }
                throw new Exception(responseMessage.ReasonPhrase);
            }
        }
    }
}
