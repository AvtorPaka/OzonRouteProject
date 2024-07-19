using Swashbuckle.AspNetCore.SwaggerGen;
using OzonRoute.Api.Bll.Services.Interfaces;
using OzonRoute.Api.Bll.Services;
using OzonRoute.Api.Dal.Context;
using OzonRoute.Api.Dal.Repositories.Interfaces;
using OzonRoute.Api.Dal.Repositories;
using OzonRoute.Api.Configuration.Models;
using OzonRoute.Api.HostedServices;
using System.Text.Json;

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
        services.AddSingleton<GoodsContext>();
        services.AddScoped<IGoodPriceRepository, GoodPriceRepository>();
        services.AddScoped<IReportsRepository, ReportsRepository>();
        services.AddScoped<IGoodsRepository, GoodsRepository>();

        services.Configure<PriceCalculatorOptions>(_configuration.GetSection("PriceCalculatorOptions"));
        services.AddScoped<IPriceCalculatorService, PriceCalculatorService>();

        services.AddScoped<IGoodsService, GoodsService>();
        services.AddHostedService<GoodsSyncHostedService>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen((SwaggerGenOptions o) =>
        {
            o.CustomSchemaIds(x => x.FullName);
        });

        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddMvc().AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower);
    }

    public void Configure(IApplicationBuilder app)
    {
        if (_env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //Buffering for logging requests data
        app.Use(async (context, next) => 
        {   
            context.Request.EnableBuffering();
            await next.Invoke();
        });

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}