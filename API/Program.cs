var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin();
        });
});

var app = builder.Build();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
