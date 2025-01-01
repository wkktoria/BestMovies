using BestMovies.API.EndpointFilters;
using BestMovies.API.EndpointHandlers;

namespace BestMovies.API.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void RegisterMoviesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var moviesGroups = endpointRouteBuilder.MapGroup("/movies");
        var moviesGroupsWithId = moviesGroups.MapGroup("/{movieId:int}");
        var moviesGroupsWithIdFilters = endpointRouteBuilder.MapGroup("/movies/{movieId:int}")
            .AddEndpointFilter(new DefaultMoviesAreLockedFilter(1))
            .AddEndpointFilter(new DefaultMoviesAreLockedFilter(2))
            .AddEndpointFilter(new DefaultMoviesAreLockedFilter(3))
            .AddEndpointFilter(new DefaultMoviesAreLockedFilter(4))
            .AddEndpointFilter(new DefaultMoviesAreLockedFilter(5))
            .AddEndpointFilter(new DefaultMoviesAreLockedFilter(6))
            .AddEndpointFilter(new DefaultMoviesAreLockedFilter(7))
            .AddEndpointFilter(new DefaultMoviesAreLockedFilter(8))
            .AddEndpointFilter(new DefaultMoviesAreLockedFilter(9))
            .AddEndpointFilter(new DefaultMoviesAreLockedFilter(10));

        moviesGroups.MapGet("", MoviesHandlers.GetMoviesAsync);
        moviesGroups.MapPost("", MoviesHandlers.CreateMoviesAsync)
            .AddEndpointFilter<ValidateAnnotationFilter>();
        moviesGroupsWithId.MapGet("", MoviesHandlers.GetMoviesByIdAsync).WithName("GetMovies");
        moviesGroupsWithIdFilters.MapPut("", MoviesHandlers.UpdateMoviesAsync);
        moviesGroupsWithIdFilters.MapDelete("", MoviesHandlers.DeleteMoviesAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>();
    }

    public static void RegisterDirectorsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var directorsGroups = endpointRouteBuilder.MapGroup("/movies/{movieId:int}/directors");

        directorsGroups.MapGet("", DirectorsHandlers.GetDirectorsAsync);
    }
}