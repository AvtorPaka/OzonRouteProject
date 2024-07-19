using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Bll.Services.Interfaces;

namespace OzonRoute.Api.HostedServices;

public class GoodsSyncHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<GoodsSyncHostedService> _logger;

    public GoodsSyncHostedService(
        [FromServices] IServiceProvider serviceProvider,
        ILogger<GoodsSyncHostedService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation(">>GoodsSync Host Service start executting. | Date : {curDate}", DateTime.Now);

        using PeriodicTimer periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(10));
        try
        {
            while (await periodicTimer.WaitForNextTickAsync(cancellationToken))
            {
                await ProccesGoods(cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation(">>GoodsSync Host Service is stopped | Date : {curDate}", DateTime.Now);
        }
    }

    private async Task ProccesGoods(CancellationToken cancellationToken)
    {
        _logger.LogInformation(">>GoodsSync Host Service start processing goods | Date : {curDate}", DateTime.Now);
        using var scope = _serviceProvider.CreateAsyncScope();
        IGoodsService goodsService = scope.ServiceProvider.GetRequiredService<IGoodsService>();

        var goods = await goodsService.GetGoodsFromDetached(cancellationToken);
        await goodsService.UpdateGoods(goods, cancellationToken);

        _logger.LogInformation(">>GoodsSync Host Service stopped processing goods | Date : {curDate}", DateTime.Now);
    }
}