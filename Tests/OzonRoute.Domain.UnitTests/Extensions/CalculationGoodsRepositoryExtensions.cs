using Moq;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;

namespace OzonRoute.Domain.UnitTests.Extensions;

public static class CalculationGoodsRepositoryExtensions
{
    public static Mock<ICalculationGoodsRepository> SetupAddCalculationGoods(this Mock<ICalculationGoodsRepository> mock)
    {   
        mock.Setup(x =>
        x.Add(
            It.IsAny<CalculationGoodEntityV1[]>(),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(() => []);

        return mock;
    }

    public static Mock<ICalculationGoodsRepository> VerifyAddCalculationGoodsWasCalledOnce(this Mock<ICalculationGoodsRepository> mock)
    {
        mock.Verify(x => 
            x.Add(
                It.IsAny<CalculationGoodEntityV1[]>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        return mock;
    }
}