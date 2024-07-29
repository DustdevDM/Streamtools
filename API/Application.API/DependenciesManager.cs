using Core.BusinessLogic.Builder;
using Core.BusinessLogic.Services;

namespace Application.API;

/// <summary>
/// Extracts dependency management into its own space
/// </summary>
public static class DependenciesManager
{
  /// <summary>
  /// Adds dependencies into a Service collection´
  /// </summary>
  /// <param name="serviceCollection">Instance of <see cref="IServiceCollection"/></param>
  public static void AddDependenciesToService(IServiceCollection serviceCollection)
  {
    serviceCollection.AddScoped<IStatInkService, StatInkService>();
    serviceCollection.AddScoped<IStatInkQueryBuilder, StatInkQueryBuilder>();
    serviceCollection.AddScoped<HttpClient>();
  }
}
