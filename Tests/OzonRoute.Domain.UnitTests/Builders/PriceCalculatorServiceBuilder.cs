using Moq;
using OzonRoute.Domain.Configuration.Models;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Domain.UnitTests.Stubs;

namespace OzonRoute.Domain.UnitTests.Builders;

public class PriceCalculatorServiceBuilder
{
    public Mock<ICalculationsRepository> CalculationsRepository {get; init;}
    public Mock<ICalculationGoodsRepository> CalculationGoodsRepository {get; init;}

    public PriceCalculatorOptions Options {get; init;} = new PriceCalculatorOptions() { VolumeToPriceRatio = 1, WeightToPriceRatio = 1 };

    public PriceCalculatorServiceBuilder(PriceCalculatorOptions? options = null)
    {   
        if (options != null) {Options = options;}
        CalculationGoodsRepository = new Mock<ICalculationGoodsRepository>(MockBehavior.Strict);
        CalculationsRepository = new Mock<ICalculationsRepository>(MockBehavior.Strict);
    }

    internal PriceCalculatorServiceStub Build()
    {
        return new PriceCalculatorServiceStub(
            options: Options,
            calculationsRepository: CalculationsRepository,
            calculationGoodsRepository: CalculationGoodsRepository
        );
    }
}