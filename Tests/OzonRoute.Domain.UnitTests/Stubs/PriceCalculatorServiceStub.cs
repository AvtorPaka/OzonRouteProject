using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OzonRoute.Domain.Configuration.Models;
using OzonRoute.Domain.Services;
using OzonRoute.Domain.Shared.Data.Interfaces;

namespace OzonRoute.Domain.UnitTests.Stubs;

internal class PriceCalculatorServiceStub : PriceCalculatorService
{   
    public Mock<ICalculationsRepository> CalculationsRepository {get; init;}
    public Mock<ICalculationGoodsRepository> CalculationGoodsRepository {get; init;}

    public PriceCalculatorServiceStub(
        Mock<IOptionsSnapshot<PriceCalculatorOptions>> options
       ,Mock<ICalculationsRepository> calculationsRepository
       ,Mock<ICalculationGoodsRepository> calculationGoodsRepository
       ,Mock<ILogger<PriceCalculatorService>> logger)
     : base(options.Object, calculationsRepository.Object, calculationGoodsRepository.Object, logger.Object)
    {
        CalculationGoodsRepository = calculationGoodsRepository;
        CalculationsRepository = calculationsRepository;
    }

    public void VerifyNoOtherCalls()
    {
        CalculationsRepository.VerifyNoOtherCalls();
        CalculationGoodsRepository.VerifyNoOtherCalls();
    }
}