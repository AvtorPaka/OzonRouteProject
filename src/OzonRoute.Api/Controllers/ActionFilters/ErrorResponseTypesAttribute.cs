using Microsoft.AspNetCore.Mvc;
using OzonRoute.Api.Responses.Errors;

namespace OzonRoute.Api.Controllers.ActionFilters;

public sealed class ErrorResponseTypesAttribute: ProducesResponseTypeAttribute
{
    public ErrorResponseTypesAttribute(int statusCode): base(typeof(ErrorResponse), statusCode)
    {

    }
}
