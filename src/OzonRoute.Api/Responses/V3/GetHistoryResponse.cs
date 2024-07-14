namespace OzonRoute.Api.Responses.V3;

public record GetHistoryResponse (
    DateTime At,
    CargoResponse Cargo,
    double Price = 0.0,
    double Distance = 0.0
) {}