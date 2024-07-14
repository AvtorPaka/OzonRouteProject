namespace OzonRoute.Api.Responses.V3;
public record CargoResponse (
    double Volume = 0.0,
    double Weight = 0.0
) {}