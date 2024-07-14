namespace OzonRoute.Api.Bll.Models;

public record GoodModel (
    //In mm
    int Lenght = 0,
    int Width = 0,
    int Height = 0,
    //In kg
    double Weight = 0.0
) {}
