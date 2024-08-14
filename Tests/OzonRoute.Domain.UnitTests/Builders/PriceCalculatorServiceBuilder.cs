using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OzonRoute.Domain.Configuration.Models;
using OzonRoute.Domain.Services;
using OzonRoute.Domain.Shared.Data.Interfaces;
using OzonRoute.Domain.UnitTests.Stubs;

namespace OzonRoute.Domain.UnitTests.Builders;

public class PriceCalculatorServiceBuilder
{
    public Mock<ICalculationsRepository> CalculationsRepository {get; init;}
    public Mock<ICalculationGoodsRepository> CalculationGoodsRepository {get; init;}
    public Mock<ILogger<PriceCalculatorService>> Logger {get; init;}

    public PriceCalculatorOptions Options {get; init;} = new PriceCalculatorOptions() { VolumeToPriceRatio = 1, WeightToPriceRatio = 1 };

    public Mock<IOptionsSnapshot<PriceCalculatorOptions>> OptionsSnapshot {get; init;}

    public PriceCalculatorServiceBuilder(PriceCalculatorOptions? options = null)
    {   
        if (options != null) {Options = options;}
        CalculationGoodsRepository = new Mock<ICalculationGoodsRepository>(MockBehavior.Strict);
        CalculationsRepository = new Mock<ICalculationsRepository>(MockBehavior.Strict);
        Logger = new Mock<ILogger<PriceCalculatorService>>(MockBehavior.Strict);
        OptionsSnapshot = new Mock<IOptionsSnapshot<PriceCalculatorOptions>>();
    }

    internal PriceCalculatorServiceStub Build()
    {
        return new PriceCalculatorServiceStub(
            options: OptionsSnapshot,
            calculationsRepository: CalculationsRepository,
            calculationGoodsRepository: CalculationGoodsRepository,
            logger: Logger
        );
    }
}