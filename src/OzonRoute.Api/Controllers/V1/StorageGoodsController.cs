using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Responses.V1;
using OzonRoute.Api.Responses.V1.Extensions;
using OzonRoute.Domain.Models;
using OzonRoute.Domain.Services.Interfaces;

namespace OzonRoute.Api.Controllers.V1;

[ApiController]
[Route("v1/storage/goods")]
public class V1StorageGoodsController : ControllerBase
{
    private readonly IStorageGoodsService _storageGoodsService;

    public V1StorageGoodsController([FromServices] IStorageGoodsService storageGoodsService)
    {
        _storageGoodsService = storageGoodsService;
    }

    [HttpGet]
    [Route("get")]
    [ProducesResponseType(typeof(IEnumerable<GetGoodsResponse>), 200)]
    public async Task<IActionResult> GetGoods(CancellationToken cancellationToken)
    {
        IReadOnlyList<StorageGoodModel> goodsModels = await _storageGoodsService.QueryGoods(cancellationToken);
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