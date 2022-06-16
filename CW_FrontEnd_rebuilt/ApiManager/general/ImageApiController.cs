using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CW_FrontEnd_rebuilt.ApiManager.general
{
    public class ImageApiController
    {
        private string SearchRandomImage(string type, string category) => @$"https://waifu.pics/api/{type}/{category}";
        private string GetQuote() => @$"https://animechan.vercel.app/api/random";

        private static readonly HttpClient client = new HttpClient();

        public ImageApiController()
        {
        }

        public string[] getImageByCategory(string type, string category)
        {
            try
            {
                string[] array = new string[2];
                string url = SearchRandomImage(type, category);
                string response = client.GetStringAsync(url).Result;
                string image_url = ParseSearchingModel(response);
                array[0] = image_url;

                url = GetQuote();
                response = client.GetStringAsync(url).Result;
                string quote = ParseQuoteModel(response);
                array[1] = quote;

                return array;

            }
            catch
            {
                throw new Exception($"Search is not valid: {category}");
            }
        }
        //get single quote
#nullable enable
        private string? ParseQuoteModel(string json)
        {
            string searchingModel = null;
            JObject images = JObject.Parse(json);

            searchingModel = images["quote"].ToString();


            return searchingModel;
        }

        //get single image
#nullable enable
        private string? ParseSearchingModel(string json)
        {
            string searchingModel = null;
            JObject images = JObject.Parse(json);

            string parsed = images["url"].ToString();
            searchingModel = parsed;

            return searchingModel;
        }
    }
}

