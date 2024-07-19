using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Bll.Services.Interfaces;
using OzonRoute.Api.Bll.Models;
using OzonRoute.Api.Requests.V2;
using OzonRoute.Api.Requests.V2.Extensions;
using OzonRoute.Api.Responses.V2;
using OzonRoute.Api.Responses.V2.Extensions;

namespace OzonRoute.Api.Controllers.V2;

[ApiController]
[Route("v2/delivery-price")]
public class V2DeliveryPriceController : ControllerBase
{   
    private readonly IPriceCalculatorService _priceCalculatorService;

    public V2DeliveryPriceController([FromServices] IPriceCalculatorService priceCalculatorService)
    {
        _priceCalculatorService = priceCalculatorService;
    }

    [HttpPost]
    [Route("calculate")]
    [ProducesResponseType(typeof(CalculateResponse), 200)]
    public async Task<IActionResult> Calculate([FromBody] CalculateRequest request)
    {
        var requestModel = await request.MapRequestToModel();
        double resultPrice = await _priceCalculatorService.CalculatePrice(goods:requestModel, distance: 1000);

        await _priceCalculatorService.CalculateNewReportData(
            goods: requestModel,
            distance: 1000,
            finalPrice: resultPrice
        );

        return Ok(new CalculateResponse(resultPrice));
    }

    [HttpPost]
    [Route("get-history")]
    [ProducesResponseType(typeof(IEnumerable<GetHistoryResponse>), 200)]
    public async Task<IActionResult> GetHistory([FromBody] GetHistoryRequest request, CancellationToken cancellationToken)
    {
        IReadOnlyList<CalculateLogModel> log = await _priceCalculatorService.QueryLog(request.Take, cancellationToken);
        IReadOnlyList<GetHistoryResponse> response = await log.MapModelsToResponses();

        return Ok(response);
    }
}