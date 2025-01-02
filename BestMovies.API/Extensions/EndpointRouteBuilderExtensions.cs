using BestMovies.API.EndpointFilters;
using BestMovies.API.EndpointHandlers;
using Microsoft.AspNetCore.Identity;

namespace BestMovies.API.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void RegisterMoviesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("GetMovie/{movieId:int}", (int movieId) => $"The movie with id {movieId}")
            .WithOpenApi(operation =>
            {
                operation.Deprecated = true;
                return operation;
            }).WithSummary("This endpoint is deprecated and will be removed in a future version.")
            .WithDescription("Please use the endpoint /movies/{movieId} to avoid future problems.");

        var moviesGroups = endpointRouteBuilder.MapGroup("/movies")
            .RequireAuthorization();
        var moviesGroupsWithId = moviesGroups.MapGroup("/{movieId:int}")
            .RequireAuthorization();
        var moviesGroupsWithIdFilters = endpointRouteBuilder.MapGroup("/movies/{movieId:int}")
            .RequireAuthorization()
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
        moviesGroupsWithId.MapGet("", MoviesHandlers.GetMoviesByIdAsync).WithName("GetMovies")
            .WithOpenApi()
            .WithSummary("This endpoint will return movies by id.");
        moviesGroupsWithIdFilters.MapPut("", MoviesHandlers.UpdateMoviesAsync);
        moviesGroupsWithIdFilters.MapDelete("", MoviesHandlers.DeleteMoviesAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>();
    }

    public static void RegisterDirectorsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var directorsGroups = endpointRouteBuilder.MapGroup("/movies/{movieId:int}/directors")
            .RequireAuthorization("RequireAdminWithAppropriateName");

        directorsGroups.MapGet("", DirectorsHandlers.GetDirectorsAsync);
    }

    public static void RegisterIdentityEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGroup("/identity/").MapIdentityApi<IdentityUser>();
    }
}