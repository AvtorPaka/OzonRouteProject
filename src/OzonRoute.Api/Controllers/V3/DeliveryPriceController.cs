using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Requests.V3;
using OzonRoute.Api.Requests.V3.Extensions;
using OzonRoute.Api.Responses.V3;
using OzonRoute.Api.Responses.V3.Extensions;
using OzonRoute.Domain.Services.Interfaces;
using OzonRoute.Domain.Models;

namespace OzonRoute.Api.Controllers.V3;

[ApiController]
[Route("v3/delivery-price")]
public class V3DeliveryPriceController : ControllerBase
{
    private readonly IPriceCalculatorService _priceCalculatorService;

    public V3DeliveryPriceController([FromServices] IPriceCalculatorService priceCalculatorService)
    {
        _priceCalculatorService = priceCalculatorService;
    }

    [HttpPost]
    [Route("calculate")]
    [ProducesResponseType(typeof(CalculateResponse), 200)]
    public async Task<IActionResult> Calculate(
        [FromServices] IReportsService reportsService,
        [FromBody] CalculateRequest request,
        CancellationToken cancellationToken)
    {   
        var requestModel = await request.MapRequestToModelsContainer();
        double resultPrice = await _priceCalculatorService.CalculatePrice(requestModel, cancellationToken);

        await reportsService.CalculateNewReportData(
            goods: requestModel.Goods,
            distance: requestModel.Distance,
            finalPrice: resultPrice,
            cancellationToken: cancellationToken
        );

        return Ok(new CalculateResponse(resultPrice));
    }

    [HttpGet]
    [Route("get-history")]
    [ProducesResponseType(typeof(IEnumerable<GetHistoryResponse>), 200)]
    public async Task<IActionResult> GetHistory([FromQuery] GetHistoryRequest request, CancellationToken cancellationToken)
    {   
        IReadOnlyList<CalculationLogModel> log = await _priceCalculatorService.QueryLog(
            model: new GetHistoryModel(
                UserId: request.UserId,
                Take: request.Take,
                Skip: request.Skip
            ),
            cancellationToken);
        IReadOnlyList<GetHistoryResponse> response = await log.MapModelsToResponses();

        return Ok(response);
    }
}