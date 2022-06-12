using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;

namespace CW_FrontEnd_rebuilt.ApiManager.general
{
    public class ImageApiController
    {
        private string SearchRandomImage(string type, string category) => @$"https://api.waifu.pics/{type}/{category}";
        private string SearchInBulk(string type, string category) => @$"https://api.waifu.pics/many/{type}/{category}";
        private string GetQuote() => @$"https://animechan.vercel.app/api/random";

        private readonly HttpConfig httpWorker;

        public ImageApiController()
        {
            httpWorker = new HttpConfig();
        }

        public string[] getImageByCategory(string type, string category)
        {
            try
            {
                string[] array = new string[2];
                string url = SearchRandomImage(type, category);
                string response = httpWorker.GetJsonResponse(url).Result;
                string image_url = ParseSearchingModel(response);
                array[0] = image_url;

                url = GetQuote();
                response = httpWorker.GetJsonResponse(url).Result;
                string quote = ParseQuoteModel(response);
                array[1] = quote;

                return array;

            }
            catch
            {
                throw new Exception($"Search is not valid: {category}");
            }
        }
        public List<string> getImageByCategoryMany(string type, string category)
        {

            string url = SearchInBulk(type, category);
            string response = httpWorker.GetJsonResponse(url).Result;
            return ParseSearchingModels(response);
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
        //get 30 image links
#nullable enable
        private List<string>? ParseSearchingModels(string json)
        {
            JsonObject obj = JsonNode.Parse(json).AsObject();
            JsonArray jsonArray = (JsonArray)obj["files"];
            string jsonString = jsonArray.ToString();
            List<string> listS = JsonConvert.DeserializeObject<List<string>>(jsonString);


            return listS;
        }
    }
}

