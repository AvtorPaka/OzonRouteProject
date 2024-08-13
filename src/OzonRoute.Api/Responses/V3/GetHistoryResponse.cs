namespace OzonRoute.Api.Responses.V3;

public record GetHistoryResponse (
    long Id,
    long UserId,
    CargoResponse Cargo,
    double Distance,
    decimal Price,
    DateTimeOffset At
) {}