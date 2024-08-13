using FluentAssertions;
using OzonRoute.Domain.IntegrationTests.Fixtures;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Domain.Shared.Data.Models;
using OzonRoute.Tests.Infrastructure.Creators;
using OzonRoute.Tests.Infrastructure.Fakers;

namespace OzonRoute.Domain.IntegrationTests.RepositoryTests;

[Collection(nameof(DalFixture))]
public sealed class CalculationsRepositoryTests
{   
    private const double _requiredDoublePrecision = 0.00001d;
    private const decimal _requiredDecimalPrecision = 0.00001m;
    private static readonly TimeSpan _requiredDateTimePrecision = TimeSpan.FromMilliseconds(100);
    private readonly ICalculationsRepository _calculationsRepository;

    public CalculationsRepositoryTests(DalFixture dalFixture)
    {
        _calculationsRepository = dalFixture.CalculationsRepository;
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public async Task Add_Calculations_Success(int count)
    {
        //Arrange
        var cts = new CancellationTokenSource();
        var userId = Create.RandomId();
        var now = DateTime.UtcNow;

        CalculationEntityV1[] calculations = CalculationEntityV1Faker.GenerateEntities(count)
                                            .Select(x => x.WithUserId(userId).WithAt(now)).ToArray();
        //Act
        long[] ids = await _calculationsRepository.Add(calculations, cts.Token);

        //Assert
        ids.Should().HaveCount(count);
        ids.Should().AllSatisfy(x => x.Should().BeGreaterThan(0));
    }

    [Fact]
    public async Task Query_SingleCalculation_AndMapping_Success()
    {
        //Arrange
        var cts = new CancellationTokenSource();
        var userId = Create.RandomId();
        var now = DateTime.UtcNow;

        CalculationEntityV1[] calculations = CalculationEntityV1Faker.GenerateEntities()
                                            .Select(x => x.WithUserId(userId).WithAt(now)).ToArray();

        CalculationEntityV1 expected = calculations.Single();

        long id = (await _calculationsRepository.Add(calculations, cts.Token)).Single();
        //Act

        var queriedCalculations = await _calculationsRepository.Query(
            new CalculationHistoryQueryModel(userId, 1, 0),
            cts.Token
        );

        //Assert
        queriedCalculations.Should().ContainSingle();
        CalculationEntityV1 claimedCalculation = queriedCalculations.Single();

        claimedCalculation.Id.Should().Be(id);
        claimedCalculation.UserId.Should().Be(userId);
        claimedCalculation.GoodIds.Should().BeEquivalentTo(expected.GoodIds);
        claimedCalculation.TotalWeight.Should().BeApproximately(expected.TotalWeight, _requiredDoublePrecision);
        claimedCalculation.TotalVolume.Should().BeApproximately(expected.TotalVolume, _requiredDoublePrecision);
        claimedCalculation.Price.Should().BeApproximately(expected.Price, _requiredDecimalPrecision);
        claimedCalculation.Distance.Should().BeApproximately(expected.Distance, _requiredDoublePrecision);
        claimedCalculation.At.Should().BeCloseTo(expected.At, _requiredDateTimePrecision);
    }

    [Theory]
    [InlineData(1, 1, 0, 1)]
    [InlineData(10, 6, 5, 5)]
    [InlineData(5, 6, 0, 5)]
    [InlineData(1, 5, 0, 1)]
    public async Task Query_MultipleCalculations_WithOffset_Succes(int globalAmmount, int take, int skip, int expectedTakenAmmount)
    {
        //Arrange
        var cts = new CancellationTokenSource();
        var userId = Create.RandomId();
        var now = DateTime.UtcNow;

        CalculationEntityV1[] calculations = CalculationEntityV1Faker.GenerateEntities(globalAmmount)
                                            .Select(x => x.WithUserId(userId).WithAt(now)).ToArray();

        var ids = (await _calculationsRepository.Add(calculations, cts.Token)).ToHashSet();
        //Act

        var queriedCalculations = await _calculationsRepository.Query(
            new CalculationHistoryQueryModel(userId, take, skip),
            cts.Token
        );

        //Assert
        queriedCalculations.Should().HaveCount(expectedTakenAmmount);
        queriedCalculations.Should().OnlyContain(x => x.UserId == userId);
        queriedCalculations.Should().OnlyContain(x => ids.Contains(x.Id));
    }
}