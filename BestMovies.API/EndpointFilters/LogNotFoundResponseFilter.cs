using System.Net;

namespace BestMovies.API.EndpointFilters;

public class LogNotFoundResponseFilter(ILogger<LogNotFoundResponseFilter> logger) : IEndpointFilter
{
    private readonly ILogger<LogNotFoundResponseFilter> _logger = logger;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);
        var actualResult = (result is INestedHttpResult nestedResult) ? nestedResult.Result : (IResult)result!;

        if (actualResult is IStatusCodeHttpResult { StatusCode: (int)HttpStatusCode.NotFound })
        {
            _logger.LogInformation("Resource {} was not found", context.HttpContext.Request.Path);
        }

        return result;
    }
}