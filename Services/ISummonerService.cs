using lol_consumingAPI.Models;

namespace lol_consumingAPI.Services
{
    public interface ISummonerService{
        Task<Summoner> getSummoner(string summonerName);
    }
}