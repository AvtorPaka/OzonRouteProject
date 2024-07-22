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
    private readonly IGoodsService _goodsService;
    private readonly ILogger<V1GoodsController> _logger;

    public V1GoodsController([FromServices] IGoodsService goodsService, ILogger<V1GoodsController> logger)
    {
        _goodsService = goodsService;
        _logger = logger;
    }

    [HttpGet]
    [Route("get")]
    [ProducesResponseType(typeof(IEnumerable<GetGoodsResponse>), 200)]
    public async Task<IActionResult> GetGoods(CancellationToken cancellationToken)
    {
        IReadOnlyList<GoodStoreModel> goodsModels = await _goodsService.GetGoodsFromData(cancellationToken);
        IReadOnlyList<GetGoodsResponse> response = await goodsModels.MapModelsToResponse();

        return Ok(response);
    }

    [HttpGet]
    [Route("price")]
    [ProducesResponseType(typeof(CalculateResponse), 200)]
    public async Task<IActionResult> Calculate(
        [FromServices] IPriceCalculatorService priceCalculatorService,
        [FromQuery(Name = "Id")] int id)
    {   
        GoodEntity entity = await _goodsService.GetGoodFromData(id);
        GoodModel goodModel = new GoodModel(
            Lenght: entity.Lenght,
            Width: entity.Width,
            Height: entity.Height,
            Weight: entity.Weight
        );

        double shipPrice = await priceCalculatorService.CalculatePrice(goods: [goodModel], distance: 1000);
        double finalPrice = shipPrice += entity.Price;

        return Ok(new CalculateResponse(finalPrice));
    }
}