using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OzonRoute.Infrastructure.Dal.Configuration;
using OzonRoute.Infrastructure.Dal.Infrastructure;
using Serilog;
using Serilog.Exceptions;

namespace OzonRoute.Infrastructure.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder AddSerilog(this IHostBuilder builder)
    {   
        builder.UseSerilog((context, configuration) => 
        {   
            var options = context.Configuration.GetSection($"{nameof(ElasticSearchOptions)}").Get<ElasticSearchOptions>()
                    ?? throw new ArgumentNullException("ElasticSearchOptions is missing.");

            var elasticOptions = ElasticSearchConf.ConfigureSinkOptions(context, options);

            configuration
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .WriteTo.Elasticsearch(elasticOptions)
                .ReadFrom.Configuration(context.Configuration);
            
            if (context.HostingEnvironment.IsDevelopment())
            {   
                string template = "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}]  {Message:lj}{NewLine}{Exception}";

                configuration
                    .WriteTo.Console(outputTemplate: template)
                    .WriteTo.Debug(outputTemplate: template);
            }
        });

        return builder;
    }
}