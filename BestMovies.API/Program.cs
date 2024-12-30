using BestMovies.API.DBContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BestMoviesContext>(
    o => o.UseSqlite(builder.Configuration.GetConnectionString("BestMoviesStr"))
);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();