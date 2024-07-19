using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Bll.Models;
using OzonRoute.Api.Bll.Services;
using OzonRoute.Api.Bll.Services.Interfaces;
using OzonRoute.Api.Responses.V1;
using OzonRoute.Api.Responses.V1.Extensions;

namespace OzonRoute.Api.Controllers.V1;

[ApiController]
[Route("v1/goods")]
public class V1GoodsController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public V1GoodsController([FromServices] IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [HttpGet]
    [Route("get")]
    [ProducesResponseType(typeof(IEnumerable<GetGoodsResponse>), 200)]
    public async Task<IActionResult> GetGoods(CancellationToken cancellationToken)
    {
        var scope = _serviceProvider.CreateAsyncScope();
        IGoodsService goodsService = scope.ServiceProvider.GetRequiredService<IGoodsService>();

        IReadOnlyList<GoodStoreModel> goodsModels = await goodsService.GetGoodsFromData(cancellationToken);
        IReadOnlyList<GetGoodsResponse> response = await goodsModels.MapModelsToResponse();

        return Ok(response);
    }
}