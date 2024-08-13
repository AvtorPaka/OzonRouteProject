using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Responses.V1;
using OzonRoute.Api.Responses.V1.Extensions;
using OzonRoute.Domain.Models;
using OzonRoute.Domain.Services.Interfaces;

namespace OzonRoute.Api.Controllers.V1;


[ApiController]
[Route("/v1/delivery-reports")]
public class V1DeliveryReportsController: ControllerBase
{
    private readonly IReportsService _reportsService;

    public V1DeliveryReportsController([FromServices] IReportsService reportsService)
    {
        _reportsService = reportsService;
    }

    [HttpGet]
    [Route("get-personal")]
    [ProducesResponseType(typeof(ReportsResponse), 200)]
    public async Task<IActionResult> GetReportsPersonal(
        [FromQuery(Name = "Id")] long userId,
        CancellationToken cancellationToken)
    {
        ReportModel reportModel = await _reportsService.GetReport(userId, cancellationToken);
        ReportsResponse reportsResponse = reportModel.MapModelToResponse();

        return Ok(reportsResponse);
    }

    [HttpGet]
    [Route("get-global")]
    [ProducesResponseType(typeof(ReportsResponse), 200)]
    public async Task<IActionResult> GetReportsGlobal(CancellationToken cancellationToken)
    {
        ReportModel reportModel = await _reportsService.GetReport(
            userId: -1,
            cancellationToken: cancellationToken);

        ReportsResponse response = reportModel.MapModelToResponse();

        return Ok(response);
    }

    [HttpDelete]
    [Route("clear-personal")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> ClearReportsPersonal(
        [FromQuery(Name = "Id")] long userId,
        CancellationToken cancellationToken)
    {
        await _reportsService.ClearReportData(userId, cancellationToken);

        return Ok();
    }

    [HttpDelete]
    [Route("clear-global")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> ClearReportsPersonal(CancellationToken cancellationToken)
    {
        await _reportsService.ClearReportData(
            userId: -1,
            cancellationToken: cancellationToken);

        return Ok();
    }
}