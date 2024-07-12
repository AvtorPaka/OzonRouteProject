namespace OzonRoute.Api.Responses.V1;

public record GetHistoryProperties (
    CargoResponse Cargo,
    double Price = 0.0
) {}