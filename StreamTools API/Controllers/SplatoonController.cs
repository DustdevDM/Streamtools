using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StreamTools_API.Classes;
using StreamTools_API.Classes.API;

namespace StreamTools_API.Controllers
{
    /// <summary>
    /// Controller used for querying splatoon data relevant for streaming
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SplatoonController : ControllerBase
    {
        /// <summary>
        /// Calculates the win/loss rate of all Splatoon 3 matches of the last 24 hours based on the data set of my stat.ink profile
        /// </summary>
        /// <returns><see cref="WLRateResponse"/></returns>
        [HttpGet]
        [Route("WLStats")]
        public async Task<ActionResult<WLRateResponse>> WLStats()
        {
            Random rnd = new Random();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer klHDuehqI71psY3giO8-m3L_QLcE0zTYyvPOGAZdKso");
            var response = await httpClient.GetAsync("https://stat.ink/@Dustin_DM/spl3/index.json");
            string result = await response.Content.ReadAsStringAsync();

            List<StatInkMatchRecords>? statincMatchRecords = JsonConvert.DeserializeObject<List<StatInkMatchRecords>>(result);

            if (statincMatchRecords is null)
            {
                return this.StatusCode(500);
            }

            statincMatchRecords = statincMatchRecords.Where(m => m.Result != MatchResult.Draw && m.Result != MatchResult.Exempted_lose).Where(m => m.StartDate?.Iso8601 > DateTime.Now.AddHours(-24)).ToList();
            double all = statincMatchRecords.Count();
            double wins = statincMatchRecords.Where(m => m.Result == MatchResult.Win).ToList().Count;
            double percentage = Math.Round((wins / all) * 100, 2);

            if (all == 0)
            {
                return this.NoContent();
            }

            return new WLRateResponse($"{percentage}%", $"{wins}/{statincMatchRecords.Count}");
        }
    }
}
