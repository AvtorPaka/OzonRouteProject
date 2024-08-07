using System.Transactions;
using AutoFixture;
using OzonRoute.Domain.Exceptions.Domain;
using OzonRoute.Domain.Models;
using OzonRoute.Domain.UnitTests.Builders;
using OzonRoute.Domain.UnitTests.Extensions;

namespace OzonRoute.Domain.UnitTests;

public class PriceCalculatorServiceTests
{
    [Fact]
    public async void CalculatePrice_PassingEmptyContainerOfGoods_ThrowValidationException()
    {
        //Arrange
        var cts = new CancellationTokenSource();
        var builder = new PriceCalculatorServiceBuilder();

        var cut = builder.Build();

        //Act, Assert
        await Assert.ThrowsAsync<DomainException>(async () => await cut.CalculatePrice(
            new DeliveryGoodsContainer(
            UserId: 1,
            Goods: Array.Empty<DeliveryGoodModel>().ToList(),
            Distance: 1000),
            cancellationToken: cts.Token));
    }

    [Theory]
    [MemberData(nameof(CalculatePriceByVolumeMemberData))]
    public async void CalculatePrice_WhenCalculatePriceByVolume_Succes(DeliveryGoodsContainer modelsContainer, double expected)
    {
        //Arrange
        var cts = new CancellationTokenSource();
        var builder = new PriceCalculatorServiceBuilder();

        builder.CalculationsRepository
            .SetupAddCalculations()
            .SetupCreateTransactionScope();
            
        builder.CalculationGoodsRepository
            .SetupAddCalculationGoods();

        var cut = builder.Build();

        //Act
        double result = await cut.CalculatePrice(
            deliveryGoodsContainer: modelsContainer,
            cancellationToken: cts.Token);

        //Assert
        Assert.Equal(expected, result);

        cut.CalculationsRepository
            .VerifyAddCalculationsWasCalledOnce()
            .VerifyCreateTransactionScopeWasCalledOnce(IsolationLevel.ReadCommitted);
        cut.CalculationGoodsRepository.
            VerifyAddCalculationGoodsWasCalledOnce();

        cut.VerifyNoOtherCalls();
    }

    [Theory]
    [InlineData(2000, 2000)]
    [InlineData(1200, 1200)]
    public async void CalculatePrice_WhenCalculatePriceByWeight_Succes(double weight, double expected)
    {
        //Arrange
        var cts = new CancellationTokenSource();
        var builder = new PriceCalculatorServiceBuilder();

        builder.CalculationsRepository
            .SetupAddCalculations()
            .SetupCreateTransactionScope();
            
        builder.CalculationGoodsRepository
            .SetupAddCalculationGoods();

        var cut = builder.Build();

        //Act
        double result = await cut.CalculatePrice(
            new DeliveryGoodsContainer(
            UserId: 1,
            Goods: new List<DeliveryGoodModel>() {new DeliveryGoodModel(
                Lenght: 10,
                Width: 10,
                Height: 10,
                Weight: weight
            )},
            Distance: 1000),
            cancellationToken: cts.Token);

        //Assert
        Assert.Equal(expected, result);
        
        cut.CalculationsRepository
            .VerifyAddCalculationsWasCalledOnce()
            .VerifyCreateTransactionScopeWasCalledOnce(IsolationLevel.ReadCommitted);
        cut.CalculationGoodsRepository.
            VerifyAddCalculationGoodsWasCalledOnce();

        cut.VerifyNoOtherCalls();
    }

    [Fact]
    public async void CalculatePrice_WhenAnyAndDistanceIs1000_Succes()
    {
        //Arrange
        var cts = new CancellationTokenSource();
        DeliveryGoodsContainer modelsContainer = new Fixture().Build<DeliveryGoodsContainer>().With(x => x.Distance, 1000).Create();

        var builder = new PriceCalculatorServiceBuilder();

        builder.CalculationsRepository
            .SetupAddCalculations()
            .SetupCreateTransactionScope();
            
        builder.CalculationGoodsRepository
            .SetupAddCalculationGoods();

        var cut = builder.Build();

        //Act
        double result = await cut.CalculatePrice(
            deliveryGoodsContainer: modelsContainer,
            cancellationToken: cts.Token);

        //Assert
        cut.CalculationsRepository
            .VerifyAddCalculationsWasCalledOnce()
            .VerifyCreateTransactionScopeWasCalledOnce(IsolationLevel.ReadCommitted);
        cut.CalculationGoodsRepository.
            VerifyAddCalculationGoodsWasCalledOnce();

        cut.VerifyNoOtherCalls();
    }

    public static IEnumerable<object[]> CalculatePriceByVolumeMemberData
    {
        get
        {
            yield return new object[] {
            new DeliveryGoodsContainer(
                UserId: 1,
                Goods: new List<DeliveryGoodModel>() {new DeliveryGoodModel(
                    Lenght: 10,
                    Width: 10,
                    Height: 10,
                    Weight: 2
                )},
                Distance: 1000), 1000};

            yield return new object[] {
            new DeliveryGoodsContainer(
                UserId: 1,
                Goods: new List<DeliveryGoodModel>() {new DeliveryGoodModel(
                    Lenght: 20,
                    Width: 20,
                    Height: 20,
                    Weight: 2
                )},
                Distance: 1000), 8000};
        }
    }
}