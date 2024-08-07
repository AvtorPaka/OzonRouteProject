namespace OzonRoute.Api.Responses.V1;

public record GetHistoryResponse (
    long Id,
    long UserId,
    CargoResponse Cargo,
    double Price,
    DateTimeOffset At
) {}