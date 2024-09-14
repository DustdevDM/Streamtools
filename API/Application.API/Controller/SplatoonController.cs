using System.Globalization;
using Application.API.Models.Response;
using Core.BusinessLogic.Builder;
using Core.BusinessLogic.DTOs.Settings;
using Core.BusinessLogic.Enums;
using Core.BusinessLogic.Exceptions;
using Core.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controller
{
  /// <summary>
  /// Controller used for querying splatoon data relevant for streaming
  /// </summary>
  [ApiController]
  [Route("[controller]")]
  public class SplatoonController : ControllerBase
  {
    private readonly IStatInkService statInkService;
    private readonly StatInkSettingsDto statInkSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="SplatoonController"/> class.
    /// </summary>
    /// <param name="statInkService">Instance of <see cref="IStatInkService"/></param>
    /// <param name="statInkSettings">Instance of <see cref="StatInkSettingsDto"/></param>
    public SplatoonController(IStatInkService statInkService, StatInkSettingsDto statInkSettings)
    {
      this.statInkService = statInkService;
      this.statInkSettings = statInkSettings;
    }

    /// <summary>
    /// Calculates the win/loss rate of all Splatoon 3 matches of the last 24 hours based on the data set of
    /// my statink profile
    /// </summary>
    /// <param name="statInkQueryBuilder">Instance of <see cref="IStatInkQueryBuilder"/></param>
    /// <returns><see cref="SplatoonStatsResponse"/></returns>
    [HttpGet]
    [Route("Stats")]
    public async Task<ActionResult<SplatoonStatsResponse>> WlStats(
      [FromServices] IStatInkQueryBuilder statInkQueryBuilder)
    {
      statInkQueryBuilder
        .WithLobbyFilter(StatInkQueryLobby.ExceptPrivate)
        .FromDate(new DateTimeOffset(DateTime.Now).AddDays(-1))
        .TillDate(new DateTimeOffset(DateTime.Now))
        .FullQuery();

      try
      {
        var matchRecords =
          await this.statInkService.GetStatInkMatchRecords(this.statInkSettings.StatisticsUsername, statInkQueryBuilder, true);

        int totalMatches = matchRecords.Count;
        int wonMatches = matchRecords.Count(match => match.Result == StatInkQueryResult.Victory);

        if (matchRecords.Count == 0)
          return this.NoContent();

        double winPercentage = Math.Round((float)wonMatches / totalMatches * 100, 2);

        return new SplatoonStatsResponse(winPercentage.ToString(CultureInfo.InvariantCulture), $"{wonMatches}/{totalMatches}");
      }
      catch (StatInkUserNotFoundException ex)
      {
        return this.NotFound(ex.Message);
      }
      catch (StatInkException ex)
      {
        Console.WriteLine(ex);
        return this.StatusCode(500, ex.Message);
      }
    }

    /// <summary>
    /// Returns Splatfestdata of the last recorded match
    /// </summary>
    /// <param name="statInkQueryBuilder">Instance of <see cref="IStatInkQueryBuilder"/></param>
    /// <returns><see cref="SplatfestStatsResponse"/></returns>
    [HttpGet]
    [Route("Splatfest")]
    public async Task<ActionResult<SplatfestStatsResponse>> Splatfest(
      [FromServices] IStatInkQueryBuilder statInkQueryBuilder)
    {
      statInkQueryBuilder
        .WithLobbyFilter(StatInkQueryLobby.SplatfestAny)
        .FromDate(new DateTimeOffset(DateTime.Now).AddDays(-4))
        .TillDate(new DateTimeOffset(DateTime.Now))
        .FullQuery();

      try
      {
        var matchRecords =
          await this.statInkService.GetStatInkMatchRecords(this.statInkSettings.StatisticsUsername, statInkQueryBuilder, false);

        if (matchRecords.Count == 0)
          return this.NoContent();

        var matchesWithCloud = matchRecords.Where(match => match.CloutChange is not null).ToList();

        int cloudOpen = matchesWithCloud
          .Where(match => match.LobbyData.Key == "splatfest_open")
          .Sum(match => match.CloutChange) ?? 0;

        int cloudPro = matchesWithCloud
          .Where(match => match.LobbyData.Key == "splatfest_challenge")
          .Sum(match => match.CloutChange) ?? 0;

        return this.Ok(new SplatfestStatsResponse(
          matchRecords[0].GoodGuysSplatfestThemeName!,
          matchRecords[0].GoodGuysTeamColor,
          cloudOpen,
          cloudPro));
      }
      catch (StatInkUserNotFoundException ex)
      {
        return this.NotFound(ex.Message);
      }
      catch (StatInkException ex)
      {
        Console.WriteLine(ex);
        return this.StatusCode(500, ex.Message);
      }
    }
  }
}
