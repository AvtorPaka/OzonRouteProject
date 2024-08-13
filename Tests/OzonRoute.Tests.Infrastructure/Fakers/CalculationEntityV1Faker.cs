using AutoBogus;
using Bogus;
using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Tests.Infrastructure.Fakers;

public static class CalculationEntityV1Faker
{
    private static readonly object _lock = new object();

    private static Faker<CalculationEntityV1> Faker = new AutoFaker<CalculationEntityV1>()
        .RuleFor(x => x.Id, s => s.Random.Long(0L))
        .RuleFor(x => x.Price, s => s.Random.Decimal())
        .RuleFor(x => x.TotalVolume, s => s.Random.Double())
        .RuleFor(x => x.Distance, s => s.Random.Double())
        .RuleFor(x => x.TotalWeight, s => s.Random.Double());

    public static CalculationEntityV1[] GenerateEntities(int count = 1)
    {
        lock (_lock)
        {
            return Enumerable.Repeat(Faker.Generate(), count).ToArray();
        }
    }

    public static CalculationEntityV1 WithUserId(this CalculationEntityV1 calculationEntity, long userId)
    {
        return calculationEntity with {UserId = userId};
    }

    public static CalculationEntityV1 WithGoodIds(this CalculationEntityV1 calculationEntity, long[] goodsIds)
    {
        return calculationEntity with {GoodIds = goodsIds};
    }

    public static CalculationEntityV1 WithTotalVolume(this CalculationEntityV1 calculationEntity, double totalVolume)
    {
        return calculationEntity with {TotalVolume = totalVolume};
    }

    public static CalculationEntityV1 WithTotalWeight(this CalculationEntityV1 calculationEntity, double totalWeight)
    {
        return calculationEntity with {TotalWeight = totalWeight};
    }

    public static CalculationEntityV1 WithPrice(this CalculationEntityV1 calculationEntity, decimal price)
    {
        return calculationEntity with {Price = price};
    }

    public static CalculationEntityV1 WithDistance(this CalculationEntityV1 calculationEntity, double distance)
    {
        return calculationEntity with {Distance = distance};
    }

    public static CalculationEntityV1 WithAt(this CalculationEntityV1 calculationEntity, DateTimeOffset dateTimeOffset)
    {
        return calculationEntity with {At = dateTimeOffset};
    }
}