using BestMovies.API.DTOs;
using MiniValidation;

namespace BestMovies.API.EndpointFilters;

public class ValidateAnnotationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var movieForCreatingDTO = context.GetArgument<MovieForCreatingDTO>(2);

        if (!MiniValidator.TryValidate(movieForCreatingDTO, out var validationError))
        {
            return TypedResults.ValidationProblem(validationError);
        }

        return await next(context);
    }
}