using OzonRoute.Api.Bll.Services;
using OzonRoute.Api.Bll.Services.Interfaces;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    EnvironmentName = Environments.Development,
    ContentRootPath = Directory.GetCurrentDirectory()
});

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddJsonFile("appsettings.Production.json", reloadOnChange: true, optional: true);
}

builder.Services.AddScoped<IPriceCalculator, PriceCalculatorService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

using WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

await app.RunAsync();