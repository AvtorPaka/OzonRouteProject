using Swashbuckle.AspNetCore.SwaggerGen;
using OzonRoute.Api.HostedServices;
using System.Text.Json;
using OzonRoute.Api.Controllers.ActionFilters;
using System.Net;
using OzonRoute.Infrastructure.Dal.Repositories;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Domain.Configuration.Models;
using OzonRoute.Domain.Services;
using OzonRoute.Domain.Services.Interfaces;
using Microsoft.Extensions.Options;
using OzonRoute.Api.Configuration.Extensions;
using OzonRoute.Infrastructure.Dal.Contexts;

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
        
        services.AddScoped<IPriceCalculatorService, PriceCalculatorService>(x => new PriceCalculatorService(
            options: x.GetConfigurationSnapshot<PriceCalculatorOptions>(),
            goodPriceRepository: x.GetRequiredService<IGoodPriceRepository>(),
            reportsRepository: x.GetRequiredService<IReportsRepository>()
        ));

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

        services.AddMvc().AddMvcOptions(o =>
        {
            o.Filters.Add(new ExceptionFilterAttribute());
            o.Filters.Add(new ErrorResponseTypesAttribute((int)HttpStatusCode.InternalServerError));
            o.Filters.Add(new ErrorResponseTypesAttribute((int)HttpStatusCode.BadRequest));
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        if (_env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        //Buffering for logging requests data
        app.Use(async (context, next) =>
        {
            context.Request.EnableBuffering();
            await next.Invoke();
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapControllerRoute(
                name: "v1/goods/veiew",
                pattern: "v1/goods/view",
                defaults: new {
                    Controller = "V1GoodsView",
                    Action = "Index"
                }
            );
        });
    }
}