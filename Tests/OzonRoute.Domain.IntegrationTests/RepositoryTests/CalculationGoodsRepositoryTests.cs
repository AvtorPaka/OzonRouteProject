using FluentAssertions;
using OzonRoute.Domain.IntegrationTests.Fixtures;
using OzonRoute.Domain.Shared.Data.Entities;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Tests.Infrastructure.Creators;
using OzonRoute.Tests.Infrastructure.Fakers;

namespace OzonRoute.Domain.IntegrationTests.RepositoryTests;


[Collection(nameof(DalFixture))]
public sealed class CalculationGoodsRepositoryTests
{
    private const double _requiredDoublePrecision = 0.00001d;

    private readonly ICalculationGoodsRepository _calculationGoodsRepository;

    public CalculationGoodsRepositoryTests(DalFixture dalFixture)
    {
        _calculationGoodsRepository = dalFixture.CalculationGoodsRepository;
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public async Task Add_CalculationGoods_Success(int count)
    {
        //Arrange
        var cts = new CancellationTokenSource();
        var userId = Create.RandomId();

        CalculationGoodEntityV1[] calculationGoods = CalculationGoodEntityV1Faker.GenerateEntites(count)
                                                    .Select(x => x.WithUserId(userId)).ToArray();
        //Act
        long[] ids = await _calculationGoodsRepository.Add(calculationGoods, cts.Token);

        //Assert
        ids.Should().HaveCount(count);
        ids.Should().AllSatisfy(x => x.Should().BeGreaterThan(0));
    }

    [Fact]
    public async Task Query_CalculationGoods_Succes()
    {
        //Arrange
        var cts = new CancellationTokenSource();
        var userId = Create.RandomId();

        CalculationGoodEntityV1[] calculationGoods = CalculationGoodEntityV1Faker.GenerateEntites(10)
                                                     .Select(x => x.WithUserId(userId)).ToArray();

        var ids = (await _calculationGoodsRepository.Add(calculationGoods, cts.Token)).ToHashSet();

        //Act
        var queriedGoods = await _calculationGoodsRepository.Query(userId, cts.Token);

        //Assert
        queriedGoods.Should().NotBeEmpty();
        queriedGoods.Should().HaveCount(10);
        queriedGoods.Should().OnlyContain(x => x.UserId == userId);
        queriedGoods.Should().OnlyContain(x => ids.Contains(x.Id));
    }

    [Fact]
    public async Task QuerySingleGood_AndMapping_Succes()
    {
        //Arrange
        var cts = new CancellationTokenSource();
        long userId = Create.RandomId();

        CalculationGoodEntityV1[] calculationGood = CalculationGoodEntityV1Faker.GenerateEntites(1)
                                                    .Select(x => x.WithUserId(userId)).ToArray();
        
        CalculationGoodEntityV1 expected = calculationGood.Single();

        var generatedId = (await _calculationGoodsRepository.Add(calculationGood, cts.Token)).Single();

        //Act
        var queriedGoods = await _calculationGoodsRepository.Query(userId, cts.Token);

        //Assert
        queriedGoods.Should().HaveCount(1);
        var claimedGood = queriedGoods.Single();

        claimedGood.Id.Should().Be(generatedId);
        claimedGood.UserId.Should().Be(userId);
        claimedGood.Width.Should().BeApproximately(expected.Width, _requiredDoublePrecision);
        claimedGood.Height.Should().BeApproximately(expected.Height, _requiredDoublePrecision);
        claimedGood.Length.Should().BeApproximately(expected.Length, _requiredDoublePrecision);
        claimedGood.Width.Should().BeApproximately(expected.Width, _requiredDoublePrecision);
    }

    [Fact]
    public async Task Query_CalculationGoods_WithWrongUserId_ShouldBeEmpty()
    {
        //Arrange
        var cts = new CancellationTokenSource();
        var expectedUserId = Create.RandomId();
        var wrongUserId = Create.RandomId();

        CalculationGoodEntityV1[] calculationGoods = CalculationGoodEntityV1Faker.GenerateEntites(1)
                                                        .Select(x => x.WithUserId(expectedUserId)).ToArray();
        
        await _calculationGoodsRepository.Add(calculationGoods, cts.Token);
        //Act

        var queriedGoods = await _calculationGoodsRepository.Query(wrongUserId, cts.Token);
        //Assert

        queriedGoods.Should().BeEmpty();
    }
}