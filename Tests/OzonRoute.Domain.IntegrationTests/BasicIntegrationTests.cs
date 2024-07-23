using AutoFixture;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OzonRoute.Api.Controllers.V1;
using OzonRoute.Api.Responses.V1;
using OzonRoute.Domain.Services.Interfaces;
using Xunit.Abstractions;

namespace OzonRoute.Domain.IntegrationTests;

public sealed class BasicIntegrationTests
{   
    private readonly ITestOutputHelper _testOutputHelper;

    public BasicIntegrationTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task App_SwaggerCheck()
    {   
        //Arrange
        var app = new AppFixture();
        var httpClient = app.CreateClient();

        //Act
        var response = await httpClient.GetAsync("/swagger/index.html");

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task App_V1DeliveryPrice_ShouldLogData()
    {
        //Arrange
        var cts = new CancellationTokenSource();
        var app = new AppFixture();
        var request = new Fixture().Build<Api.Requests.V1.CalculateRequest>().Create();
        var contorller = app.Services.GetRequiredService<V1DeliveryPriceController>();
        var repository = app.Services.GetRequiredService<IReportsService>();

        //Act
        var response = await contorller.Calculate(repository, request, cts.Token);

        //Assert
        var actionResult = await contorller.GetHistory(new Api.Requests.V1.GetHistoryRequest(Take: int.MaxValue), cts.Token);
        var objectResult = actionResult as ObjectResult;

        Assert.NotNull(objectResult);
        Assert.NotNull(objectResult.Value);
        Assert.IsAssignableFrom<IEnumerable<GetHistoryResponse>>(objectResult.Value);
        Assert.Single((IEnumerable<GetHistoryResponse>)objectResult.Value);
    }

    [Fact]
    public async Task App_V1DeliveryPrice_ShouldUpdateReports()
    {
        //Arrange
        var cts = new CancellationTokenSource();
        var app = new AppFixture();
        var request = new Fixture().Build<Api.Requests.V1.CalculateRequest>().Create();
        var contorller = app.Services.GetRequiredService<V1DeliveryPriceController>();
        var repository = app.Services.GetRequiredService<IReportsService>();
        var reportsService = app.Services.GetRequiredService<IReportsService>();

        //Act
        var response = await contorller.Calculate(repository, request, cts.Token);

        //Assert
        var actionResult = await contorller.Reports(reportsService, cts.Token);
        var objectResult = actionResult as ObjectResult;

        Assert.NotNull(objectResult);
        Assert.NotNull(objectResult.Value);
        Assert.IsType<ReportsResponse>(objectResult.Value);
        
        ReportsResponse reportsResponse = (ReportsResponse)objectResult.Value;
        Assert.NotEqual(0, reportsResponse.WavgPrice);
    }
}