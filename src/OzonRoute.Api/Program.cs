using OzonRoute.Infrastructure.Extensions;

namespace OzonRoute.Api;

public sealed class Program
{
    public static async Task Main()
    {
        var builder = Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(webBuidler => webBuidler.UseStartup<Startup>())
        .ConfigureHostOptions(x => x.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore);


        await builder
            .AddSerilog()
            .Build()
            .MigrateUp()
            .RunAsync();
    }
}