using Swashbuckle.AspNetCore.SwaggerGen;
using OzonRoute.Api.Bll.Services.Interfaces;
using OzonRoute.Api.Bll.Services;
using OzonRoute.Api.Dal.Context;
using OzonRoute.Api.Dal.Repositories.Interfaces;
using OzonRoute.Api.Dal.Repositories;
using OzonRoute.Api.Configuration.Models;

namespace OzonRoute.Api;

public sealed class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<DeliveryPriceContext>();
        services.AddScoped<IGoodPriceRepository, GoodPriceRepository>();
        services.AddScoped<IReportsRepository, ReportsRepository>();


        services.Configure<PriceCalculatorOptions>(_configuration.GetSection("PriceCalculatorOptions"));
        services.AddScoped<IPriceCalculatorService, PriceCalculatorService>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen((SwaggerGenOptions o) =>
        {
            o.CustomSchemaIds(x => x.FullName);
        });

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app)
    {
        if (_env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}