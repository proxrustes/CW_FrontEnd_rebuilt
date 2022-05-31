using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CW_FrontEnd_rebuilt.ApiManager
{
    public class HttpConfig
    {
        public async Task<string> GetJsonResponse(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string response = await httpClient.GetStringAsync(httpClient.BaseAddress);

                if (string.IsNullOrWhiteSpace(response))
                {
                    return "";
                }
                return response;
            }
        }
    }
}
