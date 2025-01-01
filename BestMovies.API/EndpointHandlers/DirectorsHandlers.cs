using AutoMapper;
using BestMovies.API.DBContexts;
using BestMovies.API.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.API.EndpointHandlers;

public static class DirectorsHandlers
{
    public static async Task<Results<NotFound, Ok<IEnumerable<DirectorDTO>>>> GetDirectorsAsync(
        BestMoviesContext context, IMapper mapper,
        int movieId)
    {
        var movieResult = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

        if (movieResult == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(mapper.Map<IEnumerable<DirectorDTO>>(
            (await context.Movies
                .Include(movie => movie.Directors).FirstOrDefaultAsync(movie => movie.Id == movieId))
            ?.Directors));
    }
}