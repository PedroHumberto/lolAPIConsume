using lol_consumingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace lol_consumingAPI.Services
{
    public class SummonerService : ISummonerService
    {
        private HttpClient _client;
        public SummonerService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("LoLApi");
        }

        public async Task<Summoner> getSummoner(string summonerName)
        {
            Summoner? summoner = new Summoner();
            try
            {
                string URL_PATH = $"lol/summoner/v4/summoners/by-name/{summonerName}?api_key={Constants.API_KEY}";

                using HttpResponseMessage response = await _client.GetAsync(URL_PATH);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();

                summoner = JsonConvert.DeserializeObject<Summoner>(jsonResponse);

                return summoner;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message Summoner :{0} ", e.Message);
                return summoner;
            }
        }
    }
}