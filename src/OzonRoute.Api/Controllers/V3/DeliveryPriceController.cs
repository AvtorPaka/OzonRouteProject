using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Requests.V3;
using OzonRoute.Api.Requests.V3.Extensions;
using OzonRoute.Api.Responses.V3;
using OzonRoute.Api.Responses.V3.Extensions;
using OzonRoute.Domain.Services.Interfaces;
using OzonRoute.Domain.Models;
using OzonRoute.Api.Responses.Errors;

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
        var saveCalculationsModel = await _priceCalculatorService.CalculatePrice(requestModel, cancellationToken);
        double resultPrice = (double)saveCalculationsModel.Price;

        await _priceCalculatorService.SaveCalculationsData(
            saveModel: saveCalculationsModel,
            token: cancellationToken
        );

        await reportsService.UpdateReportData(
            saveModel: saveCalculationsModel,
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

    [HttpGet]
    [Route("get-history/by-ids")]
    [ProducesResponseType(typeof(IEnumerable<GetHistoryResponse>), 200)]
    [ProducesResponseType(typeof(WrongCalculationIdsResponse), 403)]
    public async Task<IActionResult> GetHistoryByIds([FromQuery] GetHistoryByIdsRequest request, CancellationToken cancellationToken)
    {
        IReadOnlyList<CalculationLogModel> calculationHistory = await _priceCalculatorService.QueryLogByIds(
            new GetHistoryByIdsModel(
                UserId: request.UserId,
                CalculationIds: request.CalculationIds ?? []
            ),
            cancellationToken: cancellationToken
        );

        IReadOnlyList<GetHistoryResponse> result = await calculationHistory.MapModelsToResponses();

        return Ok(result);
    }
}