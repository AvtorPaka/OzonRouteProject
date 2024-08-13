using System.Transactions;
using Moq;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;

namespace OzonRoute.Domain.UnitTests.Extensions;

public static class CalculationsRepositoryExtensions
{
    public static Mock<ICalculationsRepository> SetupAddCalculations(this Mock<ICalculationsRepository> mock)
    {
        mock.Setup(x => 
        x.Add(
            It.IsAny<CalculationEntityV1[]>(),
            It.IsAny<CancellationToken>()
            )).ReturnsAsync(() => []);
        
        return mock;
    }

    public static Mock<ICalculationsRepository> SetupCreateTransactionScope(this Mock<ICalculationsRepository> mock)
    {
        mock.Setup<TransactionScope>(x =>
        x.CreateTransactionScope(
            It.IsAny<IsolationLevel>())
        ).Returns(() => new TransactionScope());

        return mock;
    }

    public static Mock<ICalculationsRepository> VerifyCreateTransactionScopeWasCalledOnce(this Mock<ICalculationsRepository> mock, IsolationLevel isolationLevel)
    {
        mock.Verify(x =>
            x.CreateTransactionScope(
                It.Is<IsolationLevel>(x => x == isolationLevel)
            ),
            Times.Once);

        return mock;
    }

    public static Mock<ICalculationsRepository> VerifyAddCalculationsWasCalledOnce(this Mock<ICalculationsRepository> mock)
    {   
        mock.Verify(x => 
            x.Add(
                It.IsAny<CalculationEntityV1[]>(),
                It.IsAny<CancellationToken>()
                ),
                Times.Once);

        return mock;
    }
}