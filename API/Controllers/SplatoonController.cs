using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StreamTools_API.Classes;
using StreamTools_API.Classes.API;
using StreamTools_API.Classes.Configuration;

namespace StreamTools_API.Controllers
{
    /// <summary>
    /// Controller used for querying splatoon data relevant for streaming
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SplatoonController : ControllerBase
    {
        private readonly StatInkSettings statInkSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SplatoonController"/> class.
        /// </summary>
        /// <param name="statInkSettings">Instance of <see cref="statInkSettings"/></param>
        public SplatoonController(StatInkSettings statInkSettings)
        {
          this.statInkSettings = statInkSettings;
        }

        /// <summary>
        /// Calculates the win/loss rate of all Splatoon 3 matches of the last 24 hours based on the data set of my statink profile
        /// </summary>
        /// <returns><see cref="WlRateResponse"/></returns>
        [HttpGet]
        [Route("WLStats")]
        public async Task<ActionResult<WlRateResponse>> WlStats()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {this.statInkSettings.ApiToken}");
            HttpResponseMessage response =
              await httpClient.GetAsync(
                $"https://stat.ink/@{this.statInkSettings.StatisticsUsername}/spl3/index.json");
            string result = await response.Content.ReadAsStringAsync();

            List<StatInkMatchRecords>? statincMatchRecords = JsonConvert.DeserializeObject<List<StatInkMatchRecords>>(result);

            if (statincMatchRecords is null)
            {
                return this.NoContent();
            }

            statincMatchRecords = statincMatchRecords.Where(m => m.StartDate.Iso8601 > DateTime.Now.AddHours(-24) && m.GoodGuys.Any(p => p.IsDisconnected) == false && m.BadGuys.Any(p => p.IsDisconnected) == false).ToList();
            double all = statincMatchRecords.Count;
            double wins = statincMatchRecords.Where(m => m.Result == MatchResult.Win).ToList().Count;
            double percentage = Math.Round((wins / all) * 100, 2);

            string? splatfestColor = null;
            if (statincMatchRecords.First().LobbyData.Key.Contains("splatfest", StringComparison.CurrentCultureIgnoreCase))
            {
                splatfestColor = $"#{statincMatchRecords.First().GoodGuysTeamColor}";
            }

            if (all == 0)
            {
                return this.NoContent();
            }

            return new WlRateResponse($"{percentage}%", $"{wins}/{statincMatchRecords.Count}", splatfestColor);
        }
    }
}
