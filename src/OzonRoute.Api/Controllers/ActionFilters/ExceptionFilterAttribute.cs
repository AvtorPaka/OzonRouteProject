using Microsoft.AspNetCore.Mvc.Filters;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using OzonRoute.Api.Responses.Errors;

namespace OzonRoute.Api.Controllers.ActionFilters;
public sealed class ExceptionFilterAttribute : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ValidationException exception:
                HandleValidationError(context, exception);
                break;
            default:
                HandleInternalServerError(context);
                break;
        }
    }

    private static void HandleValidationError(ExceptionContext context, ValidationException validationException)
    {
        JsonResult jsonValidationResult = new(
            new ErrorResponse(
            StatusCode: HttpStatusCode.BadRequest,
            Message: validationException.Message
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
            Message: "Working on this."
        ))
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        context.Result = jsonErrorResult;
    }
}