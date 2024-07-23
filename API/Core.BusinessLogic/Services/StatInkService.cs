using System.Net;
using Core.BusinessLogic.Builder;
using Core.BusinessLogic.DTOs;
using Core.BusinessLogic.DTOs.Settings;
using Core.BusinessLogic.Enums;
using Core.BusinessLogic.Exceptions;
using Newtonsoft.Json;

namespace Core.BusinessLogic.Services;

public class StatInkService(HttpClient httpClient, StatInkSettingsDTO statInkSettings) : IStatInkService
{
  public async Task<(double, double, double)> CalculateWinLooseRate(
    IStatInkQueryBuilder statInkQueryBuilder,
    bool ignoreDisconnects)
  {
    string baseUrl = $"https://stat.ink/@{statInkSettings.StatisticsUsername}/spl3/index.json";
    Dictionary<string, string?> filters = statInkQueryBuilder.Build();
    string uri = $"{baseUrl}?{string.Join("&", filters.Where(filter => filter.Value != null).Select(filter => $"{filter.Key}={filter.Value}"))}";

    HttpResponseMessage httpResponse = await httpClient.GetAsync(uri);

    if (httpResponse.StatusCode == HttpStatusCode.NotFound)
    {
      throw new StatInkUserNotFoundException($"{statInkSettings.StatisticsUsername} cant be found on stat.ink");
    }

    if (!httpResponse.IsSuccessStatusCode)
    {
      throw new StatInkException(
        $"Unable to fetch data from stat.ink. Statink returned status code {httpResponse.StatusCode.ToString()}");
    }

    try
    {
      string response = await httpResponse.Content.ReadAsStringAsync();
      List<StatInkMatchRecordsDTO>?
        matchRecords = JsonConvert.DeserializeObject<List<StatInkMatchRecordsDTO>>(response);

    if (matchRecords is null)
      throw new JsonSerializationException("Unable to parse match records");

    if (ignoreDisconnects)
    {
      matchRecords = matchRecords
        .Where(match => !match.GoodGuys.Any(guy => guy.IsDisconnected))
        .Where(match => !match.BadGuys.Any(guy => guy.IsDisconnected))
        .ToList();
    }

    double totalMatches = matchRecords.Count;
    double wonMatches = matchRecords.Count(match => match.Result == StatInkQueryResult.Victory);
    double winPercentage = Math.Round((wonMatches / totalMatches) * 100, 2);

    return (totalMatches, wonMatches, winPercentage);
    }
    catch (Exception ex)
    {
      throw new StatInkException("Unable to parse stat.ink output", ex);
    }
  }
}
