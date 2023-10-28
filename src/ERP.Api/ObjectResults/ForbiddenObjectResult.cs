using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ERP.Api.ObjectResults;

[DefaultStatusCode(StatusCodes.Status403Forbidden)]
public sealed class ForbiddenObjectResult : ObjectResult
{
    public ForbiddenObjectResult([ActionResultObjectValue]object value) : base(value)
    {
        StatusCode = StatusCodes.Status403Forbidden;
    }
}
