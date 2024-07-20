namespace OzonRoute.Api.Bll.Models;

public record GoodModel (
    //In cm
    double Lenght = 0.0d,
    double Width = 0.0d,
    double Height = 0.0d,
    //In kg
    double Weight = 0.0d
) {}
