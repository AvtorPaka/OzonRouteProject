using OzonRoute.Api.HostedServices;
using System.Text.Json;
using OzonRoute.Api.Controllers.ActionFilters;
using System.Net;
using OzonRoute.Infrastructure.Extensions;
using OzonRoute.Domain.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc;

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
        services
            .AddDomain(_configuration)
            .AddDalInfrastucture(_configuration)
            .AddInfrastructureServices()
            .AddDalRepositories()
            .AddControllers()
            .AddJsonOptions(o => o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower)
            .AddMvcOptions(ConfigureMvc)
            .Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(o => o.CustomSchemaIds(x => x.FullName))
            .AddHostedService<GoodsSyncHostedService>()
            .AddHttpContextAccessor();
    }

    private static void ConfigureMvc(MvcOptions o)
    {
        o.Filters.Add(new ExceptionFilterAttribute());
        o.Filters.Add(new ErrorResponseTypesAttribute((int)HttpStatusCode.InternalServerError));
        o.Filters.Add(new ErrorResponseTypesAttribute((int)HttpStatusCode.BadRequest));

    }

    public void Configure(IApplicationBuilder app)
    {
        // if (_env.IsDevelopment())
        // {
        // }
        app.UseSwagger();
        app.UseSwaggerUI();

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
                defaults: new
                {
                    Controller = "V1GoodsView",
                    Action = "Index"
                }
            );
        });
    }
}