using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace OzonRoute.Domain.IntegrationTests;

public sealed class AppFixture: WebApplicationFactory<Api.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {   
        builder.ConfigureServices(Services => {
            Services.AddControllers().AddControllersAsServices();
        });
        base.ConfigureWebHost(builder);
    }
}