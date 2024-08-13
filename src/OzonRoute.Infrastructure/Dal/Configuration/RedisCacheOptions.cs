namespace OzonRoute.Infrastructure.Dal.Configuration;

public class RedisCacheOptions
{
    public string ConnectionString {get; init;} = string.Empty;
    public string User {get; init;} = string.Empty;
    public string Password {get; init;} = string.Empty;
}