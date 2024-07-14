using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Bll.Services.Interfaces;
using OzonRoute.Api.Bll.Models;
using OzonRoute.Api.Requests.V1;
using OzonRoute.Api.Requests.V1.Extensions;
using OzonRoute.Api.Responses.V1;
using OzonRoute.Api.Responses.V1.Extensions;

namespace OzonRoute.Api.Controllers.V1;

[ApiController]
[Route("v1/delivery-price")]
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
        double result = priceCalculatorService.CalculatePrice(goods:requestModel, distance: 1000);

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

    [HttpPost]
    [Route("delete-history")]
    public async Task<IActionResult> DeleteHistory(CancellationToken cancellationToken)
    {   
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken); //Fiction
        using var scope = _serviceProvider.CreateAsyncScope();
        IPriceCalculatorService priceCalculatorService = scope.ServiceProvider.GetRequiredService<IPriceCalculatorService>();
        
        priceCalculatorService.ClearLog();
        return Ok();
    }
}