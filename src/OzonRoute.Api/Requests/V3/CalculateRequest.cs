namespace OzonRoute.Api.Requests.V3;

public record CalculateRequest(
    long UserId,
    List<GoodProperties> Goods,
    int Distance = 0) {}