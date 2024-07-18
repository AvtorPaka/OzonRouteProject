namespace OzonRoute.Api.Bll.Models;

public record GoodModel (
    //In cm
    double Lenght = 0,
    double Width = 0,
    double Height = 0,
    //In kg
    double Weight = 0.0
) {}
