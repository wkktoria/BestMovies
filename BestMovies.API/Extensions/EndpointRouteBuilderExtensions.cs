using BestMovies.API.EndpointHandlers;

namespace BestMovies.API.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void RegisterMoviesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var moviesEndpoints = endpointRouteBuilder.MapGroup("/movies");
        var moviesEndpointsWithId = moviesEndpoints.MapGroup("/{movieId:int}");

        moviesEndpoints.MapGet("", MoviesHandlers.GetMoviesAsync);
        moviesEndpoints.MapPost("", MoviesHandlers.CreateMoviesAsync);
        moviesEndpointsWithId.MapGet("", MoviesHandlers.GetMoviesByIdAsync).WithName("GetMovies");
        moviesEndpointsWithId.MapPut("", MoviesHandlers.UpdateMoviesAsync);
        moviesEndpointsWithId.MapDelete("", MoviesHandlers.DeleteMoviesAsync);
    }

    public static void RegisterDirectorsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var directorsEndpoints = endpointRouteBuilder.MapGroup("/movies/{movieId:int}/directors");

        directorsEndpoints.MapGet("", DirectorsHandlers.GetDirectorsAsync);
    }
}