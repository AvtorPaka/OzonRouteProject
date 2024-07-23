using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Requests.V2;
using OzonRoute.Api.Requests.V2.Extensions;
using OzonRoute.Api.Responses.V2;
using OzonRoute.Api.Responses.V2.Extensions;
using OzonRoute.Api.Validators.V2;
using FluentValidation;
using OzonRoute.Domain.Services.Interfaces;
using OzonRoute.Domain.Models;

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
    public async Task<IActionResult> Calculate(
        [FromServices] IReportsService reportsService,
        [FromBody] CalculateRequest request,
        CancellationToken cancellationToken)
    {   
        var validator = new CalculateRequestValidator();
        await validator.ValidateAndThrowAsync(request);

        var requestModel = await request.MapRequestToModel();
        double resultPrice = await _priceCalculatorService.CalculatePrice(goods:requestModel, distance: 1000);

        await reportsService.CalculateNewReportData(
            goods: requestModel,
            distance: 1000,
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
        var validator = new GetHistoryRequestValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        IReadOnlyList<CalculateLogModel> log = await _priceCalculatorService.QueryLog(request.Take, cancellationToken);
        IReadOnlyList<GetHistoryResponse> response = await log.MapModelsToResponses();

        return Ok(response);
    }
}