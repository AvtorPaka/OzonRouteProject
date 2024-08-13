namespace OzonRoute.Api.Requests.V1;

public record CalculateRequest(
    long UserId,
    List<GoodProperties> Goods) {}