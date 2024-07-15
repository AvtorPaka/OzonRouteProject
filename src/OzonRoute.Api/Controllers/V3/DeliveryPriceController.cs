using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Bll.Services.Interfaces;
using OzonRoute.Api.Bll.Models;
using OzonRoute.Api.Requests.V3;
using OzonRoute.Api.Requests.V3.Extensions;
using OzonRoute.Api.Responses.V3;
using OzonRoute.Api.Responses.V3.Extensions;

namespace OzonRoute.Api.Controllers.V3;

[ApiController]
[Route("v3/delivery-price")]
public class DeliveryPriceController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public DeliveryPriceController([FromServices] IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [HttpPost]
    [Route("calculate")]
    public async Task<CalculateResponse> Calculate(CalculateRequest request)
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        IPriceCalculatorService priceCalculatorService = scope.ServiceProvider.GetRequiredService<IPriceCalculatorService>();

        var requestModel = await request.MapRequestToModel();
        double result = await priceCalculatorService.CalculatePrice(goods:requestModel, distance: request.Distance);

        return new CalculateResponse(result);
    }


    [HttpPost]
    [Route("get-history")]
    public async Task<List<GetHistoryResponse>> GetHistory(GetHistoryRequest request, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        IPriceCalculatorService priceCalculatorService = scope.ServiceProvider.GetRequiredService<IPriceCalculatorService>();

        List<CalculateLogModel> log = await priceCalculatorService.QueryLog(request.Take, cancellationToken);
        List<GetHistoryResponse> response = await log.MapModelsToResponses();

        return response;
    }
}