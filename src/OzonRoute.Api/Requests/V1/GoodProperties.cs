using System.ComponentModel.DataAnnotations;

namespace OzonRoute.Api.Requests.V1;

public record GoodProperties(
    // In mm
    int Lenght = 0,
    int Width = 0,
    int Height = 0) {
        
    }