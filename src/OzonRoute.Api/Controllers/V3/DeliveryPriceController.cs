using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Requests.V3;
using OzonRoute.Api.Requests.V3.Extensions;
using OzonRoute.Api.Responses.V3;
using OzonRoute.Api.Responses.V3.Extensions;
using OzonRoute.Api.Validators.V3;
using FluentValidation;
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
    public async Task<IActionResult> Calculate([FromBody] CalculateRequest request)
    {   
        var validator = new CalculateRequestValidator();
        await validator.ValidateAndThrowAsync(request);

        var requestModel = await request.MapRequestToModel();
        double resultPrice = await _priceCalculatorService.CalculatePrice(goods:requestModel, distance: request.Distance);

        await _priceCalculatorService.CalculateNewReportData(
            goods: requestModel,
            distance: request.Distance,
            finalPrice: resultPrice
        );

        return Ok(new CalculateResponse(resultPrice));
    }

    [HttpGet]
    [Route("get-history")]
    [ProducesResponseType(typeof(IEnumerable<GetHistoryResponse>), 200)]
    public async Task<IActionResult> GetHistory([FromQuery] GetHistoryRequest request, CancellationToken cancellationToken)
    {   
        var validator = new GetHistoryRequestValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        IReadOnlyList<CalculateLogModel> log = await _priceCalculatorService.QueryLog(request.Take, cancellationToken);
        IReadOnlyList<GetHistoryResponse> response = await log.MapModelsToResponses();

        return Ok(response);
    }
}