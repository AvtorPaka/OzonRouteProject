using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Bll.Models;
using OzonRoute.Api.Bll.Services.Interfaces;
using OzonRoute.Api.Responses.V1;
using OzonRoute.Api.Responses.V1.Extensions;
using OzonRoute.Api.Views.Models;

namespace OzonRoute.Api.Controllers.V1;

public class V1GoodsViewController: Controller
{
    private readonly IGoodsService _goodsService;
    private readonly ILogger<V1GoodsViewController> _logger;

    public V1GoodsViewController([FromServices] IGoodsService goodsService, ILogger<V1GoodsViewController> logger)
    {
        _goodsService = goodsService;
        _logger = logger;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        IReadOnlyList<GoodStoreModel> goodsModels = await _goodsService.GetGoodsFromData(cancellationToken);
        IReadOnlyList<GetGoodsResponse> response = await goodsModels.MapModelsToResponse();
        GoodsPageViewModel viewModel = new GoodsPageViewModel(response);

        return View("/Views/Pages/GoodsPageView.cshtml", viewModel);
    }
}