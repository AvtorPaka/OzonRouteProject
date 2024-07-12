using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Bll.Services.Interfaces;
using OzonRoute.Api.Requests.V1;
using OzonRoute.Api.Responses.V1;
using OzonRoute.Api.Bll.Models;

namespace OzonRoute.Api.Controllers.V1;

[ApiController]
[Route("v1/delivery-price")]
public class DeliveryPriceController : ControllerBase
{
    [HttpPost]
    [Route("/calculate")]
    public async Task<CalculateResponse> Calculate(CalculateRequest request)
    {
        await Task.Delay(TimeSpan.FromSeconds(10));
        throw new NotImplementedException();
    }


    [HttpPost]
    [Route("/get-history")]
    public async Task<GetHistoryResponse> GetHistory(GetHistoryRequest request)
    {
        await Task.Delay(TimeSpan.FromSeconds(10));
        throw new NotImplementedException();
    }
}