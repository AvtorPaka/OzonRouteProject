using System.Reflection;
using Microsoft.Extensions.Hosting;
using OzonRoute.Infrastructure.Dal.Configuration;
using Serilog.Sinks.Elasticsearch;

namespace OzonRoute.Infrastructure.Dal.Infrastructure;

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
            NumberOfReplicas = 1
        };
    }
}