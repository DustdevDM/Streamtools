namespace Application.API.Models.Response
{
    /// <summary>
    /// Response that represents the Winning Match Percentage of a Stat.inc query
    /// </summary>
    public class SplatoonStatsResponse
    {
        /// <summary>
        /// Initiates a new Instance of <see cref="SplatoonStatsResponse"/>
        /// </summary>
        /// <param name="winPercentage">The percentage of matches that were won</param>
        /// <param name="wonMatches">The number of winning matches in relation to all matches</param>
        public SplatoonStatsResponse(string winPercentage, string wonMatches)
        {
            this.WinPercentage = winPercentage;
            this.WonMatches = wonMatches;
        }

        /// <summary>
        /// The percentage of matches that were won
        /// </summary>
        public string WinPercentage { get; set; }

        /// <summary>
        /// The number of winning matches in relation to all matches
        /// </summary>
        public string WonMatches { get; set; }
    }
}
