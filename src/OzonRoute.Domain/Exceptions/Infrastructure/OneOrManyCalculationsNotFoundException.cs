namespace OzonRoute.Domain.Exceptions.Infrastructure;

public class OneOrManyCalculationsNotFoundException : Exception
{
    public OneOrManyCalculationsNotFoundException()
    {
    }

    public OneOrManyCalculationsNotFoundException(string? message) : base(message)
    {
    }

    public OneOrManyCalculationsNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}