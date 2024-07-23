using Core.BusinessLogic.Builder;
using Core.BusinessLogic.Services;

namespace Application.API;

public static class DependenciesManager
{
  public static void addDependeciesToSerice(IServiceCollection serviceCollection)
  {
    serviceCollection.AddScoped<IStatInkService, StatInkService>();
    serviceCollection.AddScoped<IStatInkQueryBuilder, StatInkQueryBuilder>();
    serviceCollection.AddScoped<HttpClient>();
  }
}
