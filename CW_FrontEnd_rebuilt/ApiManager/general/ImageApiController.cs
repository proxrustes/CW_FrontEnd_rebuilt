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

        private readonly HttpConfig httpWorker;

        public ImageApiController()
        {
            httpWorker = new HttpConfig();
        }

        public string getImageByCategory(string type, string category)
        {

            try
            {
                string url = SearchRandomImage(type, category);
                string response = httpWorker.GetJsonResponse(url).Result;
                string result = ParseSearchingModel(response);

                return result;

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


        //get single image
#nullable enable
        private string? ParseSearchingModel(string json)
        {
            string searchingModel = null;
            JObject images = JObject.Parse(json);

            var imagesArray =
                from c in images["url"]
                select c;

            foreach (var item in imagesArray)
            {
                searchingModel = (string)item["url"];
            }

            if (searchingModel == null)
                return null;

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

