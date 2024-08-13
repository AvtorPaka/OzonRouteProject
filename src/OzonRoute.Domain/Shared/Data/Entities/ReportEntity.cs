using System.Text.Json.Serialization;

namespace OzonRoute.Domain.Shared.Data.Entities;

public class ReportEntity : ICachedEntity
{   
    [JsonIgnore]
    public long UserId {get; init;} = -1;

    [JsonPropertyName("mw")]
    public double MaxWeight { get; set; } = 0.0d;

    [JsonPropertyName("mv")]
    public double MaxVolume { get; set; } = 0.0d;

    [JsonPropertyName("mdfhg")]
    public int MaxDistanceForHeaviestGood { get; set; } = 0;

    [JsonPropertyName("mdflg")]
    public int MaxDistanceForLargestGood { get; set; } = 0;

    [JsonPropertyName("tnum")]
    public int TotalNumberOfGoods { get; set; } = 0;

    [JsonPropertyName("sp")]
    public double SummaryPrice { get; set; } = 0.0d;

    [JsonIgnore]
    public string CacheKey { get => $"rep_{(UserId == -1 ? "g" : UserId)}";}
}