using System.Reflection;
using Microsoft.Extensions.Hosting;
using Serilog.Sinks.Elasticsearch;

namespace OzonRoute.Infrastructure.Configuration;

public static class ElasticSearchConf
{
    public static ElasticsearchSinkOptions ConfigureSinkOptions(HostBuilderContext context, ElasticSearchOptions options)
    {
        return new ElasticsearchSinkOptions(
            node: new Uri(options.NodeHost)
        )
        {
            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name?.ToLower().Replace(".", "-")}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
            AutoRegisterTemplate = true,
            NumberOfShards = 2,
            NumberOfReplicas = 1, //Causes yellow health on elastic indexex, ignore (cause n.o nodes meant to be > n.o replicas)
            EmitEventFailure = EmitEventFailureHandling.RaiseCallback,
            FailureCallback = (mes, exc) => Console.WriteLine($"Unable to submit event: {mes} | {exc}")
        };
    }
}