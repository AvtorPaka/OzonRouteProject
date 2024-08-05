using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Responses.V1;
using OzonRoute.Api.Responses.V1.Extensions;
using OzonRoute.Domain.Models;
using OzonRoute.Domain.Services.Interfaces;
using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Api.Controllers.V1;

[ApiController]
[Route("v1/goods")]
public class V1GoodsController : ControllerBase
{
    private readonly IStorageGoodsService _storageGoodsService;
    private readonly ILogger<V1GoodsController> _logger;

    public V1GoodsController([FromServices] IStorageGoodsService storageGoodsService, ILogger<V1GoodsController> logger)
    {
        _storageGoodsService = storageGoodsService;
        _logger = logger;
    }

    [HttpGet]
    [Route("get")]
    [ProducesResponseType(typeof(IEnumerable<GetGoodsResponse>), 200)]
    public async Task<IActionResult> GetGoods(CancellationToken cancellationToken)
    {
        IReadOnlyList<StorageGoodModel> goodsModels = await _storageGoodsService.GetGoodsFromStorage(cancellationToken);
        IReadOnlyList<GetGoodsResponse> response = await goodsModels.MapModelsToResponse();

        return Ok(response);
    }

    [HttpGet]
    [Route("price")]
    [ProducesResponseType(typeof(CalculateResponse), 200)]
    public async Task<IActionResult> CalculateFullPrice(
        [FromServices] IPriceCalculatorService priceCalculatorService,
        [FromQuery(Name = "Id")] int id,
        CancellationToken cancellationToken)
    {
        double finalPrice = await _storageGoodsService.CalculateFullPrice(priceCalculatorService, id, cancellationToken);

        return Ok(new CalculateResponse(finalPrice));
    }
}