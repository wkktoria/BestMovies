using AutoMapper;
using BestMovies.API.DBContexts;
using BestMovies.API.DTOs;
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

app.MapGet("/movies/{id:int}",
    async Task<Results<NoContent, Ok<MovieDTO>>> (BestMoviesContext context, IMapper mapper, int id) =>
    {
        var movie = mapper.Map<MovieDTO>(await context.Movies.FirstOrDefaultAsync(m => m.Id == id));

        return movie == null ? TypedResults.NoContent() : TypedResults.Ok(movie);
    });

app.MapGet("/movies/{movieId:int}/directors",
    async Task<Results<NoContent, Ok<IEnumerable<DirectorDTO>>>> (BestMoviesContext context, IMapper mapper,
        int movieId) =>
    {
        var movie = await context.Movies.Include(m => m.Directors)
            .FirstOrDefaultAsync(movie => movie.Id == movieId);

        return movie == null
            ? TypedResults.NoContent()
            : TypedResults.Ok(mapper.Map<IEnumerable<DirectorDTO>>(movie.Directors));
    });

app.MapGet("/movies",
    async Task<Results<NoContent, Ok<IEnumerable<MovieDTO>>>> (BestMoviesContext context, IMapper mapper,
        [FromQuery(Name = "movieName")] string? title) =>
    {
        var movies = await context.Movies.Where(m => title == null || m.Title.ToLower().Contains(title.ToLower()))
            .ToListAsync();

        return movies.Count <= 0
            ? TypedResults.NoContent()
            : TypedResults.Ok(mapper.Map<IEnumerable<MovieDTO>>(movies));
    });

app.Run();