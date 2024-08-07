using AutoBogus;
using Bogus;
using OzonRoute.Domain.Shared.Data.Entities;

namespace OzonRoute.Tests.Infrastructure.Fakers;

//AutoBogus is not threadsafe :O
public static class CalculationGoodEntityV1Faker
{
    private static readonly object _lock = new object();

    private static readonly Faker<CalculationGoodEntityV1> Faker = new AutoFaker<CalculationGoodEntityV1>()
        .RuleFor(x => x.Id, s => s.Random.Long(0L));

    public static CalculationGoodEntityV1[] GenerateEntites(int count = 1)
    {
        lock (_lock)
        {
            return Enumerable.Repeat(Faker.Generate(), count).ToArray();
        }
    }

    public static CalculationGoodEntityV1 WithUserId(this CalculationGoodEntityV1 entityV1, long userId)
    {
        return entityV1 with {UserId = userId};
    }

    public static CalculationGoodEntityV1 WithId(this CalculationGoodEntityV1 entityV1, long id)
    {
        return entityV1 with {Id = id};
    }

    public static CalculationGoodEntityV1 WithWidth(this CalculationGoodEntityV1 entityV1, double witdh)
    {
        return entityV1 with {Width = witdh};
    }
    
    public static CalculationGoodEntityV1 WithHeight(this CalculationGoodEntityV1 entityV1, double height)
    {
        return entityV1 with {Height = height};
    }

    public static CalculationGoodEntityV1 WithLength(this CalculationGoodEntityV1 entityV1, double length)
    {
        return entityV1 with {Length = length};
    }

    public static CalculationGoodEntityV1 WithWeight(this CalculationGoodEntityV1 entityV1, double weight)
    {
        return entityV1 with {Weight = weight};
    }
}