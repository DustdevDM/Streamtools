using System.Reflection;
using StreamTools_API.Classes.Configuration;
using StreamTools_API.Classes.Exceptions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

CorsSettings corsSettings = builder.Configuration.GetSection("CorsSettings").Get<CorsSettings>() ??
                            throw new ConfigurationException("Unable to parse CORS Settings from appsettings.json");

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
    optionsBuilder.WithOrigins(corsSettings.AllowedOrigins)
      .WithMethods(corsSettings.AllowedMethods)
      .WithHeaders(corsSettings.AllowedHeaders);
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
