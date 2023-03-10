using lol_consumingAPI.Models;
using lol_consumingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace lol_consumingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoLApiController : Controller
    {

        private ISummonerService _summonerService;
        private ChampionService _championService;

        public LoLApiController(SummonerService summonerService, ChampionService championService)
        {
            _summonerService = summonerService;
            _championService = championService;
        }


        [HttpGet("{summonerName}")]
        public async Task<IActionResult> GetSummoner(string summonerName = "thomasbatard")
        {
            Summoner summoner = await _summonerService.getSummoner(summonerName);

            return Ok(summoner);
        }


        //NEED TO DO AS A POST, AND THEN GET FROM DB THE BEST CHOICES, FOR NOW I WILL DO THAT FOR TESTS. LATTER I WILL POST THE BEST CHOICE AND THEN GET THE BESTCHOICE FROM _CONTEXT
        [HttpGet("{summonerName}/championsBestChoices")]
        public async Task<IActionResult> GetBestChoices(string summonerId = "WZ4STAyOsQcF862Vw60M0UiPTfOvTIKrV7xDtzuUexeoX6Q", summonerName = "thomasbatard")
        {
            List<Champion> champions = await _championService.getChampion(summonerId);

            Summoner summoner = await _summonerService.getSummoner(summonerName);

            List<SummonerBestChoices> bestChoices = new List<SummonerBestChoices>();

            foreach (Champion champion in champions)
            {
                if (champion.championLevel > 4)
                {
                    //Try to use a map lib to parse that.
                    bestChoices.Add(new SummonerBestChoices()
                    {
                        SummonarName = summonerName,
                        championId = champion.championId,
                        championLevel = champion.championLevel,
                        championPoints = champion.championPoints,
                        summonerLevel = summoner.summonerLevel
                    });
                }
            }
            return Ok(bestChoices);
        }

    }
}
