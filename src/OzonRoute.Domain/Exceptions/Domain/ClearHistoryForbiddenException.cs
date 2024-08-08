using OzonRoute.Domain.Exceptions.Infrastructure;

namespace OzonRoute.Domain.Exceptions.Domain;

public class ClearHistoryForbiddenException : Exception
{
    public ClearHistoryForbiddenException(string? message, OneOrManyCalculationsBelongToAnotherUserException? innerException) : base(message, innerException)
    {
    }
}