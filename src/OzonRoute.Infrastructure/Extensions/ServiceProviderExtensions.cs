using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace OzonRoute.Infrastructure.Extensions;
public static class ConfigurationExtensions
{
    public static T GetConfigurationSnapshot<T>(this IServiceProvider serviceProvider) where T : class
    {
        var configurationOptions = serviceProvider.GetService<IOptionsSnapshot<T>>() ?? throw new ArgumentNullException($"{nameof(T)} configuration is missing.");
        return configurationOptions.Value;
    }

    public static T GetConfiguration<T>(this IServiceProvider serviceProvider) where T : class
    {
        var configurationOptions = serviceProvider.GetService<IOptions<T>>() ?? throw new ArgumentNullException($"{nameof(T)} configuration is missing.");
        return configurationOptions.Value;
    }
}