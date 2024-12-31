using AutoMapper;
using BestMovies.API.DBContexts;
using BestMovies.API.DTOs;
using BestMovies.API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BestMoviesContext>(
    o => o.UseSqlite(builder.Configuration.GetConnectionString("BestMoviesStr"))
);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.MapGet("/", () => "Today is: " + DateTime.Now);

var moviesEndpoints = app.MapGroup("/movies");
var moviesEndpointsWithId = moviesEndpoints.MapGroup("/{movieId:int}");
var directorsEndpoints = moviesEndpointsWithId.MapGroup("/directors");

moviesEndpoints.MapGet("",
    async Task<Results<NoContent, Ok<IEnumerable<MovieDTO>>>> (BestMoviesContext context, IMapper mapper,
        [FromQuery(Name = "movieName")] string? title) =>
    {
        var movies = await context.Movies.Where(m => title == null || m.Title.ToLower().Contains(title.ToLower()))
            .ToListAsync();

        return movies.Count <= 0
            ? TypedResults.NoContent()
            : TypedResults.Ok(mapper.Map<IEnumerable<MovieDTO>>(movies));
    });

moviesEndpoints.MapPost("",
    async (BestMoviesContext context, IMapper mapper,
        // LinkGenerator linkGenerator, HttpContext httpContext,
        [FromBody] MovieForCreatingDTO movieForCreatingDTO) =>
    {
        var movie = mapper.Map<Movie>(movieForCreatingDTO);
        context.Add(movie);

        await context.SaveChangesAsync();

        var movieToReturn = mapper.Map<MovieDTO>(movie);
        // var linkToReturn = linkGenerator.GetUriByName(httpContext, "GetMovies", new { id = movieToReturn.Id });

        // return TypedResults.Created(linkToReturn, movieToReturn);
        return TypedResults.CreatedAtRoute(movieToReturn, "GetMovies", new { movieId = movieToReturn.Id });
    });

moviesEndpointsWithId.MapGet("",
    async Task<Results<NoContent, Ok<MovieDTO>>> (BestMoviesContext context, IMapper mapper, int movieId) =>
    {
        var movie = mapper.Map<MovieDTO>(await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId));

        return movie == null ? TypedResults.NoContent() : TypedResults.Ok(movie);
    }).WithName("GetMovies");

moviesEndpointsWithId.MapPut("", async Task<Results<NotFound, Ok>> (BestMoviesContext context, IMapper mapper,
    int movieId,
    [FromBody] MovieForCreatingDTO movieForUpdatingDTO) =>
{
    var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

    if (movie == null)
    {
        return TypedResults.NotFound();
    }

    mapper.Map(movieForUpdatingDTO, movie);

    await context.SaveChangesAsync();

    return TypedResults.Ok();
});

moviesEndpointsWithId.MapDelete("",
    async Task<Results<NotFound, NoContent>> (BestMoviesContext context, int movieId) =>
    {
        var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie == null)
        {
            return TypedResults.NotFound();
        }

        context.Movies.Remove(movie);

        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    });

directorsEndpoints.MapGet("",
    async Task<Results<NoContent, Ok<IEnumerable<DirectorDTO>>>> (BestMoviesContext context, IMapper mapper,
        int movieId) =>
    {
        var movie = await context.Movies.Include(m => m.Directors)
            .FirstOrDefaultAsync(movie => movie.Id == movieId);

        return movie == null
            ? TypedResults.NoContent()
            : TypedResults.Ok(mapper.Map<IEnumerable<DirectorDTO>>(movie.Directors));
    });

app.Run();