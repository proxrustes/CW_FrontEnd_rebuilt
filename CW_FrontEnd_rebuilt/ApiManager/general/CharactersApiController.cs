using CW_FrontEnd_rebuilt.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace CW_FrontEnd_rebuilt.ApiManager.general
{
   
    public class CharactersApiController : IApiController<Character>
    {
        private string GetAllURL() => @$"https://animedndwaifuapi.azurewebsites.net/api/characters";
        private string GetURL(int id) => @$"https://animedndwaifuapi.azurewebsites.net/api/characters/{id}";
        private string GeneralURL() => @$"https://animedndwaifuapi.azurewebsites.net/api/characters";

        private readonly HttpClient client;

        public CharactersApiController(HttpClient _client)
        {
            client = _client;
        }
#nullable enable
        public List<Character>? GetAll()
        {
            string url = GetAllURL();
            string response = client.GetStringAsync(url).Result;
            return ParseCharacterModels(response);
        }
        public Character Get(int id)
        {
            string url = GetURL(id);
            string response = client.GetStringAsync(url).Result;
            return ParseCharacterModel(response);
        }
        public async void Add([FromForm] Character value)
        {
            var json = JsonConvert.SerializeObject(value);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PostAsync(GeneralURL(), data);

            var result = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(result);
        }
        public async void Update([FromForm] Character value)
        {
            var json = JsonConvert.SerializeObject(value);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PostAsync(GetURL(value.characterId), data);

            var result = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(result);
        }
        public async void Delete(int id)
        {
            using var client = new HttpClient();

            var response = await client.DeleteAsync(GetURL(id));

            var result = await response.Content.ReadAsStringAsync();
        }
#nullable enable
        private Character? ParseCharacterModel(string json)
        {
            Character userModel = JsonConvert.DeserializeObject<Character>(json);
            return userModel;
        }
#nullable enable
        private List<Character>? ParseCharacterModels(string json)
        {
            List<Character> userModel = new List<Character>();
            Character[] array = JsonConvert.DeserializeObject<Character[]>(json);

            for (int i = 0; i < array.Length; i++)
            {
                userModel.Add(array[i]);
            }
            return userModel;
        }
    }
}
