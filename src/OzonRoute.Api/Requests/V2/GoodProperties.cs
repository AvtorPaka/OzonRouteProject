namespace OzonRoute.Api.Requests.V2;

public record GoodProperties(
    //In mm
    int Lenght = 0,
    int Width = 0,
    int Height = 0,
    //In kg
    double Weight = 0.0)
{ }