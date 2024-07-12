using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Requests.V1;
using OzonRoute.Api.Responses.V1;

namespace OzonRoute.Api.Controllers.V1;

[ApiController]
[Route("v1/delivery-price")]
public class DeliveryPriceController: ControllerBase
{   
    [HttpPost(Name = "calculate")]
    public CalculateResponse Calculate(CalculateRequest request)
    {
        throw new NotImplementedException{};
    }
}