namespace OzonRoute.Api.Requests.V3;

public record GoodProperties(
    //In m
    int Lenght = 0,
    int Width = 0,
    int Height = 0,
    //In kg
    double Weight = 0.0)
{ }