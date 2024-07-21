var builder = WebApplication.CreateBuilder(args);
CorsSettings corsSettings = builder.Configuration.GetSection("CorsSettings").Get<CorsSettings>() ??
                            throw new ConfigurationException("Unable to parse CORS Settings from appsettings.json");

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(optionsBuilder =>
  {
    optionsBuilder.WithOrigins(corsSettings.AllowedOrigins)
      .WithMethods(corsSettings.AllowedMethods)
      .WithHeaders(corsSettings.AllowedHeaders);
  });
});

var app = builder.Build();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
