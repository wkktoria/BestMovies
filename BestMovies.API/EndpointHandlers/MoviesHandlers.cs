using AutoMapper;
using BestMovies.API.DBContexts;
using BestMovies.API.DTOs;
using BestMovies.API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.API.EndpointHandlers;

public static class MoviesHandlers
{
    public static async Task<Results<NoContent, Ok<IEnumerable<MovieDTO>>>> GetMoviesAsync(BestMoviesContext context,
        IMapper mapper,
        [FromQuery(Name = "movieName")] string? title)
    {
        var movies = await context.Movies
            .Where(m => title == null || m.Title.ToLower().Contains(title.ToLower()))
            .ToListAsync();

        return movies.Count <= 0
            ? TypedResults.NoContent()
            : TypedResults.Ok(mapper.Map<IEnumerable<MovieDTO>>(movies));
    }

    public static async Task<Results<NotFound, Ok<MovieDTO>>> GetMoviesByIdAsync(BestMoviesContext context, IMapper mapper,
        int movieId)
    {
        var movieResult = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

        if (movieResult == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(mapper.Map<MovieDTO>(movieResult));
    }

    public static async Task<CreatedAtRoute<MovieDTO>> CreateMoviesAsync(BestMoviesContext context,
        IMapper mapper,
        // LinkGenerator linkGenerator, HttpContext httpContext,
        [FromBody] MovieForCreatingDTO movieForCreatingDTO)
    {
        var movie = mapper.Map<Movie>(movieForCreatingDTO);
        context.Add(movie);

        await context.SaveChangesAsync();

        var movieToReturn = mapper.Map<MovieDTO>(movie);
        // var linkToReturn = linkGenerator.GetUriByName(httpContext, "GetMovies", new { id = movieToReturn.Id });

        // return TypedResults.Created(linkToReturn, movieToReturn);
        return TypedResults.CreatedAtRoute(movieToReturn, "GetMovies", new { movieId = movieToReturn.Id });
    }

    public static async Task<Results<NotFound, Ok>> UpdateMoviesAsync(BestMoviesContext context, IMapper mapper,
        int movieId, [FromBody] MovieForCreatingDTO movieForUpdatingDTO)
    {
        var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie == null)
        {
            return TypedResults.NotFound();
        }

        mapper.Map(movieForUpdatingDTO, movie);

        await context.SaveChangesAsync();

        return TypedResults.Ok();
    }

    public static async Task<Results<NotFound, NoContent>> DeleteMoviesAsync(BestMoviesContext context, int movieId)
    {
        var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie == null)
        {
            return TypedResults.NotFound();
        }

        context.Movies.Remove(movie);

        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}