using System.Net;
using FluentValidation;

namespace OzonRoute.Api.Middleware;


//Not best practice
public sealed class ErrorMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next.Invoke(httpContext);
        }
        catch (ValidationException ex)
        {   
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(new 
            {
                ex.Message
            }.ToString()!);            
        }
        catch (Exception)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}