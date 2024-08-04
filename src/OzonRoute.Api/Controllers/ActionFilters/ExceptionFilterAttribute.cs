using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using OzonRoute.Api.Responses.Errors;
using OzonRoute.Domain.Exceptions.Domain;

namespace OzonRoute.Api.Controllers.ActionFilters;
public sealed class ExceptionFilterAttribute : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case DomainException exception:
                HandleBadRequest(context, exception);
                break;
            default:
                HandleInternalServerError(context);
                break;
        }
    }

    private static void HandleBadRequest(ExceptionContext context, Exception exception)
    {
        JsonResult jsonValidationResult = new(
            new ErrorResponse(
            StatusCode: HttpStatusCode.BadRequest,
            Exceptions: QueryExceptions(exception)
        ))
        {
            StatusCode = (int)HttpStatusCode.BadRequest
        };

        context.Result = jsonValidationResult;
    }

    private static void HandleInternalServerError(ExceptionContext context)
    {
        JsonResult jsonErrorResult = new(
            new ErrorResponse(
            StatusCode: HttpStatusCode.InternalServerError,
            Exceptions: new List<string> {"Working on this."}
        ))
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        context.Result = jsonErrorResult;
    }

    private static IEnumerable<string> QueryExceptions(Exception exception)
    {   
        yield return exception.Message;

        Exception? innerException = exception.InnerException;
        while (innerException != null)
        {
            yield return innerException.Message;
            innerException = innerException.InnerException;
        }
    }
}