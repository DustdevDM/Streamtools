using Application.API.Models.Response;
using Core.BusinessLogic.Builder;
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

    /// <summary>
    /// Initializes a new instance of the <see cref="SplatoonController"/> class.
    /// </summary>
    /// <param name="statInkService">Instance of <see cref="IStatInkService"/></param>
    public SplatoonController(IStatInkService statInkService)
    {
      this.statInkService = statInkService;
    }

    /// <summary>
    /// Calculates the win/loss rate of all Splatoon 3 matches of the last 24 hours based on the data set of
    /// my statink profile
    /// </summary>
    /// <param name="statInkQueryBuilder">Instance of <see cref="IStatInkQueryBuilder"/></param>
    /// <returns><see cref="WlRateResponse"/>Instance of <see cref="WlRateResponse"/></returns>
    [HttpGet]
    [Route("Stats")]
    public async Task<ActionResult<WlRateResponse>> WlStats(
      [FromServices] IStatInkQueryBuilder statInkQueryBuilder)
    {
      statInkQueryBuilder
        .WithLobbyFilter(StatInkQueryLobby.ExceptPrivate)
        .FromDate(new DateTimeOffset(DateTime.Now).AddDays(-1))
        .TillDate(new DateTimeOffset(DateTime.Now))
        .FullQuery();

      try
      {
        (double totalMatches, double wonMatches, double winPercentage) stats =
          await this.statInkService.CalculateWinLooseRate(statInkQueryBuilder, true);

        if (stats.totalMatches == 0)
          return this.NoContent();

        return new WlRateResponse($"{stats.winPercentage}%", $"{stats.wonMatches}/{stats.totalMatches}", "#ffffff");
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
