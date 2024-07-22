using Microsoft.Extensions.Options;

namespace OzonRoute.Api.Configuration.Extensions;
public static class ConfigurationExtensions
{
    public static T GetConfigurationSnapshot<T>(this IServiceProvider serviceProvider) where T : class
    {
        var configurationOptions = serviceProvider.GetService<IOptionsSnapshot<T>>() ?? throw new ArgumentNullException($"{nameof(T)} configuration is missing.");
        return configurationOptions.Value;
    }
}