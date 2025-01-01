using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace BestMovies.API.EndpointFilters;

public class DefaultMoviesAreLockedFilter : IEndpointFilter
{
    private readonly int _lockedDefaultMovieId;

    public DefaultMoviesAreLockedFilter(int lockedDefaultMovieId)
    {
        _lockedDefaultMovieId = lockedDefaultMovieId;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var movieId = context.HttpContext.Request.Method switch
        {
            "PUT" => context.GetArgument<int>(2),
            "DELETE" => context.GetArgument<int>(1),
            _ => throw new NotSupportedException("This filter is not supported for this scenario.")
        };

        if (movieId == _lockedDefaultMovieId)
        {
            return TypedResults.Problem(new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Default movie cannot be changed or deleted",
                Detail = "You cannot modify or delete the default movie."
            });
        }

        var result = await next.Invoke(context);
        return result;
    }
}