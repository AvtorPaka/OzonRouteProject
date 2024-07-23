using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Requests.V1;
using OzonRoute.Api.Requests.V1.Extensions;
using OzonRoute.Api.Responses.V1;
using OzonRoute.Api.Responses.V1.Extensions;
using OzonRoute.Domain.Services.Interfaces;
using OzonRoute.Domain.Models;

namespace OzonRoute.Api.Controllers.V1;

[ApiController]
[Route("v1/delivery-price")]
public class V1DeliveryPriceController : ControllerBase
{
    private readonly IPriceCalculatorService _priceCalculatorService;

    public V1DeliveryPriceController([FromServices] IPriceCalculatorService priceCalculatorService)
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
        IReadOnlyList<CalculateLogModel> log = await _priceCalculatorService.QueryLog(
            model: new GetHistoryModel(request.Take),
            cancellationToken);
        IReadOnlyList<GetHistoryResponse> response = await log.MapModelsToResponses();

        return Ok(response);
    }

    [HttpDelete]
    [Route("delete-history")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> DeleteHistory(CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken); //Fiction
        _priceCalculatorService.ClearLog();
        return Ok();
    }

    [HttpGet]
    [Route("reports/01")]
    [ProducesResponseType(typeof(ReportsResponse), 200)]
    public async Task<IActionResult> Reports(
        [FromServices] IReportsService reportsService,
        CancellationToken cancellationToken)
    {
        ReportModel reportModel = await  reportsService.GetReport(cancellationToken);
        ReportsResponse reportsResponse = await reportModel.MapModelToResponse();

        return Ok(reportsResponse);
    }
}