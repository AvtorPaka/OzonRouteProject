using OzonRoute.Api.Bll.Services;
using OzonRoute.Api.Bll.Services.Interfaces;
using OzonRoute.Api.Dal.Context;
using OzonRoute.Api.Dal.Repositories;
using OzonRoute.Api.Dal.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    EnvironmentName = Environments.Development,
    ContentRootPath = Directory.GetCurrentDirectory()
});

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddJsonFile("appsettings.Production.json", reloadOnChange: true, optional: true);
}

builder.Services.AddSingleton<DeliveryPriceContext>();
builder.Services.AddScoped<IGoodPriceRepository, GoodPriceRepository>();
builder.Services.AddScoped<IPriceCalculatorService, PriceCalculatorService>();
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