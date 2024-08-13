using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using OzonRoute.Api.Responses.Errors;
using OzonRoute.Domain.Exceptions.Domain;
using OzonRoute.Domain.Exceptions.Infrastructure;

namespace OzonRoute.Api.Controllers.ActionFilters;
public sealed class ExceptionFilterAttribute : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {   
            case WrongCalculationIdsException exception:
                HandleWrongCalculationIdsForbiddenException(context, exception);
                break;
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
        JsonResult jsonResult = new(
            new ErrorResponse(
            StatusCode: HttpStatusCode.BadRequest,
            Exceptions: QueryExceptionsMessages(exception)
        ))
        {
            StatusCode = (int)HttpStatusCode.BadRequest
        };

        context.Result = jsonResult;
    }

    private static void HandleWrongCalculationIdsForbiddenException(ExceptionContext context, WrongCalculationIdsException exception)
    {
        JsonResult jsonResult = new JsonResult(
            new WrongCalculationIdsResponse(
                StatusCode: HttpStatusCode.Forbidden,
                WrongCalculationIds: (exception.InnerException as OneOrManyCalculationsBelongToAnotherUserException)!.WrongCalculationsIds,
                Exceptions: QueryExceptionsMessages(exception)
            )
        )
        {
            StatusCode = (int)HttpStatusCode.Forbidden
        };

        context.Result = jsonResult;
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

    private static IEnumerable<string> QueryExceptionsMessages(Exception exception)
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