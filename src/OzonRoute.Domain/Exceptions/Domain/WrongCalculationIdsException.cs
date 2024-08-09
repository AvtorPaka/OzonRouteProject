using OzonRoute.Domain.Exceptions.Infrastructure;

namespace OzonRoute.Domain.Exceptions.Domain;

public class WrongCalculationIdsException : Exception
{
    public WrongCalculationIdsException(string? message, OneOrManyCalculationsBelongToAnotherUserException? innerException) : base(message, innerException)
    {
    }
}