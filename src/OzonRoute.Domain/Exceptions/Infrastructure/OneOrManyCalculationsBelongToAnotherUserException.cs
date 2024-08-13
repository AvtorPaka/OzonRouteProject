namespace OzonRoute.Domain.Exceptions.Infrastructure;

public class OneOrManyCalculationsBelongToAnotherUserException : Exception
{   
    public long[] WrongCalculationsIds {get; private init;}

    public OneOrManyCalculationsBelongToAnotherUserException(long[] wrongCalculationIds)
    {
        WrongCalculationsIds = wrongCalculationIds;
    }

    public OneOrManyCalculationsBelongToAnotherUserException(long[] wrongCalculationIds, string? message) : base(message)
    {
        WrongCalculationsIds = wrongCalculationIds;
    }

    public OneOrManyCalculationsBelongToAnotherUserException(long[] wrongCalculationIds, string? message, Exception? innerException) : base(message, innerException)
    {
        WrongCalculationsIds = wrongCalculationIds;
    }
}