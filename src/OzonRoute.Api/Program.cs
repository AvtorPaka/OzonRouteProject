namespace OzonRoute.Api;

internal sealed class Program
{
    public static async Task Main()
    {
        var builder = Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(webBuidler =>
        {
            webBuidler.UseStartup<Startup>();
            webBuidler.ConfigureAppConfiguration((context, config) =>
            {
                //In fact no need for that cause WebHost do it automatically
                if (context.HostingEnvironment.IsProduction())
                {
                    config.AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true);
                }
            });
        })
        .ConfigureHostOptions(x =>
        {
            x.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
        });

        await builder.Build().RunAsync();
    }
}