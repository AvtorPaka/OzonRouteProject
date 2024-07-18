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
public class V1DeliveryPriceController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public V1DeliveryPriceController([FromServices] IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [HttpPost]
    [Route("calculate")]
    [ProducesResponseType(typeof(CalculateResponse), 200)]
    public async Task<IActionResult> Calculate([FromBody] CalculateRequest request)
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        IPriceCalculatorService priceCalculatorService = scope.ServiceProvider.GetRequiredService<IPriceCalculatorService>();

        var requestModel = await request.MapRequestToModel();
        double resultPrice = await priceCalculatorService.CalculatePrice(goods: requestModel, distance: 1000);
        
        await priceCalculatorService.CalculateNewReportData(
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
        using var scope = _serviceProvider.CreateAsyncScope();
        IPriceCalculatorService priceCalculatorService = scope.ServiceProvider.GetRequiredService<IPriceCalculatorService>();

        IReadOnlyList<CalculateLogModel> log = await priceCalculatorService.QueryLog(request.Take, cancellationToken);
        IReadOnlyList<GetHistoryResponse> response = await log.MapModelsToResponses();

        return Ok(response);
    }

    [HttpPost]
    [Route("delete-history")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> DeleteHistory(CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken); //Fiction
        using var scope = _serviceProvider.CreateAsyncScope();
        IPriceCalculatorService priceCalculatorService = scope.ServiceProvider.GetRequiredService<IPriceCalculatorService>();

        priceCalculatorService.ClearLog();
        return Ok();
    }


    [HttpPost]
    [Route("reports/01")]
    [ProducesResponseType(typeof(ReportsResponse), 200)]
    public async Task<IActionResult> Reports(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        IPriceCalculatorService priceCalculatorService = scope.ServiceProvider.GetRequiredService<IPriceCalculatorService>();

        ReportModel reportModel = await priceCalculatorService.GetReport(cancellationToken);
        ReportsResponse reportsResponse = await reportModel.MapModelToResponse();

        return Ok(reportsResponse);
    }
}