using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Responses.V1;
using OzonRoute.Api.Responses.V1.Extensions;
using OzonRoute.Api.Views.Models;
using OzonRoute.Domain.Models;
using OzonRoute.Domain.Services.Interfaces;

namespace OzonRoute.Api.Controllers.V1;

public class V1GoodsViewController: Controller
{
    private readonly IStorageGoodsService _storageGoodsService;
    private readonly ILogger<V1GoodsViewController> _logger;

    public V1GoodsViewController([FromServices] IStorageGoodsService storageGoodsService, ILogger<V1GoodsViewController> logger)
    {
        _storageGoodsService = storageGoodsService;
        _logger = logger;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        IReadOnlyList<StorageGoodModel> goodsModels = await _storageGoodsService.QueryGoods(cancellationToken);
        
        IReadOnlyList<GetGoodsResponse> response = await goodsModels.MapModelsToResponse();
        GoodsPageViewModel viewModel = new GoodsPageViewModel(response);

        return View("/Views/Pages/GoodsPageView.cshtml", viewModel);
    }
}