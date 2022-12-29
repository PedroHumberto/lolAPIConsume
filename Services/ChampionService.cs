using lol_consumingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace lol_consumingAPI.Services
{
    public class ChampionService
    {
        private HttpClient _client;
        public ChampionService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("LoLApi");
        }
        public async Task<List<Champion>> getChampion(string summonerId)
        {
            List<Champion> champion = new List<Champion>();
            try
            {

                string URL_PATH = $"/lol/champion-mastery/v4/champion-masteries/by-summoner/{summonerId}?api_key={Constants.API_KEY}";

                using HttpResponseMessage response = await _client.GetAsync(URL_PATH);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();

                champion = JsonConvert.DeserializeObject<List<Champion>>(jsonResponse);

                return champion;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message Champion:{0} ", e.Message);
                return champion;
            }
        }

    }
}