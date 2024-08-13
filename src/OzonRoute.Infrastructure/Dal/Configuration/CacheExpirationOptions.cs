namespace OzonRoute.Infrastructure.Dal.Configuration;

public class CacheExpirationOptions
{
    public ReportsRepositoryCacheExpirationOptions ReportsRepositoryCacheExpirationOptions {get; init;} = new ReportsRepositoryCacheExpirationOptions(0);
}

public record ReportsRepositoryCacheExpirationOptions (
    int AbsoluteExpirationTime
) {}