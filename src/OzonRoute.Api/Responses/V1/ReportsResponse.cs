using System.Text.Json.Serialization;

namespace OzonRoute.Api.Responses.V1;

public record ReportsResponse(
    [property:JsonPropertyName("max_weight")]
    double MaxWeight = 0.0d,
    [property:JsonPropertyName("max_volume")]
    double MaxVolume = 0.0d,
    [property:JsonPropertyName("max_distance_for_heaviest_good")]
    int MaxDistanceForHeaviestGood = 0,
    [property:JsonPropertyName("max_distance_for_largest_good")]
    int MaxDistanceForLargestGood = 0,
    [property:JsonPropertyName("wavg_price")]
    double WavgPrice = 0.0d
) {}
