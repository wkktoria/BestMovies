using BestMovies.API.DBContexts;
using BestMovies.API.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BestMoviesContext>(
    o => o.UseSqlite(builder.Configuration.GetConnectionString("BestMoviesStr"))
);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.MapGet("/", () => "Today is: " + DateTime.Now);

app.RegisterMoviesEndpoints();
app.RegisterDirectorsEndpoints();

app.Run();