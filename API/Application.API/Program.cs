using System.Reflection;
using Application.API;
using Core.BusinessLogic.DTOs.Settings;
using Core.BusinessLogic.Exceptions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

CorsSettingsDTO corsSettingsDto = builder.Configuration.GetSection("CorsSettings").Get<CorsSettingsDTO>() ??
                            throw new ConfigurationException("Unable to parse CORS Settings from appsettings.json");

StatInkSettingsDto statInkSettingsDto = builder.Configuration.GetSection("StatInkSettings").Get<StatInkSettingsDto>() ??
                                  throw new ConfigurationException(
                                    "Unable to parse StatInk Settings from appsettings.json");

DependenciesManager.AddDependenciesToService(builder.Services);

builder.Services.AddSingleton(statInkSettingsDto);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
  c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(optionsBuilder =>
  {
    optionsBuilder.WithOrigins(corsSettingsDto.AllowedOrigins)
      .WithMethods(corsSettingsDto.AllowedMethods)
      .WithHeaders(corsSettingsDto.AllowedHeaders);
  });
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();

  app.UseSwaggerUI();
}

app.UseCors();

app.MapControllers();

app.Run();
