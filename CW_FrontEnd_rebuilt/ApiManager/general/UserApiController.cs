using CW_FrontEnd_rebuilt.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace CW_FrontEnd_rebuilt.ApiManager.general
{
    public class UserApiController : IApiController<User>
    {
        private string GetAllURL() => @$"https://localhost:44342/api/users";
        private string GetURL(int id) => @$"https://localhost:44342/api/users/{id}";
        private string GeneralURL() => @$"https://localhost:44342/api/users";


        private readonly HttpClient client;

        public UserApiController()
        {
            client = new HttpClient();
        }

#nullable enable
        public List<User>? GetAll()
        {
            string url = GetAllURL();
            string response = client.GetStringAsync(url).Result;
            return ParseUserModels(response);
        }
#nullable enable
        public User? Get(int id)
        {
            string url = GetURL(id);
            string response = client.GetStringAsync(url).Result;
            return ParseUserModel(response);
        }

        public async void Add([FromForm] User value)
        {
            var json = JsonConvert.SerializeObject(value);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PostAsync(GeneralURL(), data);

            var result = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(result);
        }

        public async void Update([FromForm] User value)
        {
            var json = JsonConvert.SerializeObject(value);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PutAsync(GeneralURL(), data);

            var result = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(result);
        }

        public async void Delete(int id)
        {
            using var client = new HttpClient();

            var response = await client.DeleteAsync(GetURL(id));

            var result = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(result);
        }
#nullable enable
        private User? ParseUserModel(string json)
        {
            User userModel = JsonConvert.DeserializeObject<User>(json); ;
            return userModel;
        }
#nullable enable
        private List<User>? ParseUserModels(string json)
        {
            List<User> userModel = new List<User>();
            User[] array = JsonConvert.DeserializeObject<User[]>(json);

            for (int i = 0; i < array.Length; i++)
            {
                userModel.Add(array[i]);
            }
            return userModel;
        }
    }
}
