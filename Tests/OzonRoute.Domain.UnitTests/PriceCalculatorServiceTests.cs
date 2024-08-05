using AutoFixture;
using Moq;
using OzonRoute.Domain.Configuration.Models;
using OzonRoute.Domain.Exceptions.Domain;
using OzonRoute.Domain.Models;
using OzonRoute.Domain.Services;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;

namespace OzonRoute.Domain.UnitTests;

public class PriceCalculatorServiceTests
{
    [Fact]
    public async void CalculatePrice_PassingEmptyContainerOfGoods_ThrowValidationException()
    {
        //Arrange
        var cts = new CancellationTokenSource();
        var options = new PriceCalculatorOptions() { VolumeToPriceRatio = 1, WeightToPriceRatio = 1 };
        var calculationsRepositoryMock = new Mock<ICalculationsRepository>();

        var cut = new PriceCalculatorService(
            options: options,
            calculationsRepository: calculationsRepositoryMock.Object
        );

        //Act, Assert
        await Assert.ThrowsAsync<DomainException>(async () => await cut.CalculatePrice(
            new DeliveryGoodsContainer(
            UserId: 1,
            Goods: Array.Empty<DeliveryGoodModel>().ToList(),
            Distance: 1000),
            cancellationToken: cts.Token));
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

    [Theory]
    [MemberData(nameof(CalculatePriceByVolumeMemberData))]
    public async void CalculatePrice_WhenCalculatePriceByVolume_Succes(DeliveryGoodsContainer modelsContainer, double expected)
    {
        //Arrange
        var cts = new CancellationTokenSource();
        var options = new PriceCalculatorOptions() { VolumeToPriceRatio = 1, WeightToPriceRatio = 1 };
        var calculationsRepositoryMock = new Mock<ICalculationsRepository>(MockBehavior.Strict);
        
        calculationsRepositoryMock.Setup(x => x.Save(It.IsAny<CalculationEntityV1>()));

        var cut = new PriceCalculatorService(
            options: options,
            calculationsRepository: calculationsRepositoryMock.Object
        );

        //Act
        double result = await cut.CalculatePrice(
            deliveryGoodsContainer: modelsContainer,
            cancellationToken: cts.Token);

        //Assert
        Assert.Equal(expected, result);
        calculationsRepositoryMock.Verify(x => x.Save(It.IsAny<CalculationEntityV1>()));
    }

    [Theory]
    [InlineData(2000, 2000)]
    [InlineData(1200, 1200)]
    public async void CalculatePrice_WhenCalculatePriceByWeight_Succes(double weight, double expected)
    {
        //Arrange
        var cts = new CancellationTokenSource();
        var options = new PriceCalculatorOptions() { VolumeToPriceRatio = 1, WeightToPriceRatio = 1 };
        var calculationsRepositoryMock = new Mock<ICalculationsRepository>(MockBehavior.Strict);

        calculationsRepositoryMock.Setup(x => x.Save(It.IsAny<CalculationEntityV1>()));

        var cut = new PriceCalculatorService(
            options: options,
            calculationsRepository: calculationsRepositoryMock.Object
        );

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
        calculationsRepositoryMock.Verify(x => x.Save(It.IsAny<CalculationEntityV1>()));
    }

    [Fact]
    public async void CalculatePrice_WhenAnyAndDistanceIs1000_Succes()
    {
        //Arrange
        var cts = new CancellationTokenSource();
        var options = new PriceCalculatorOptions() { VolumeToPriceRatio = 1, WeightToPriceRatio = 1 };
        var calculationsRepositoryMock = new Mock<ICalculationsRepository>(MockBehavior.Strict);

        calculationsRepositoryMock.Setup(x => x.Save(It.IsAny<CalculationEntityV1>()));

        DeliveryGoodsContainer modelsContainer = new Fixture().Build<DeliveryGoodsContainer>().With(x => x.Distance, 1000).Create();

        var cut = new PriceCalculatorService(
            options: options,
            calculationsRepository: calculationsRepositoryMock.Object
        );

        //Act and Assert
        double result = await cut.CalculatePrice(
            deliveryGoodsContainer: modelsContainer,
            cancellationToken: cts.Token);
        calculationsRepositoryMock.Verify(x => x.Save(It.IsAny<CalculationEntityV1>()));
    }
}