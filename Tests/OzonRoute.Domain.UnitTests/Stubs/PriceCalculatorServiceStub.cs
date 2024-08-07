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
        PriceCalculatorOptions options
       ,Mock<ICalculationsRepository> calculationsRepository
       ,Mock<ICalculationGoodsRepository> calculationGoodsRepository)
     : base(options, calculationsRepository.Object, calculationGoodsRepository.Object)
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