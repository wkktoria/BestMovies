using BestMovies.API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.API.DBContexts;

public class BestMoviesContext(DbContextOptions<BestMoviesContext> options) : IdentityDbContext(options)
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Director> Directors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Director>().HasData(
            new Director { Id = 1, Name = "Steven Spielberg" },
            new Director { Id = 2, Name = "Martin Scorsese" },
            new Director { Id = 3, Name = "Alfred Hitchcock" },
            new Director { Id = 4, Name = "Stanley Kubrick" },
            new Director { Id = 5, Name = "Francis Ford Coppola" },
            new Director { Id = 6, Name = "Woody Allen" },
            new Director { Id = 7, Name = "Billy Wilder" },
            new Director { Id = 8, Name = "John Huston" },
            new Director { Id = 9, Name = "Peter Jackson" },
            new Director { Id = 10, Name = "Milos Forman" },
            new Director { Id = 11, Name = "Clint Eastwood" },
            new Director { Id = 12, Name = "David Lean" },
            new Director { Id = 13, Name = "Ingmar Bergman" },
            new Director { Id = 14, Name = "Joel Coen" },
            new Director { Id = 15, Name = "John Ford" },
            new Director { Id = 16, Name = "James Cameron" },
            new Director { Id = 17, Name = "Sidney Lumet" },
            new Director { Id = 18, Name = "Charles Chaplin" },
            new Director { Id = 19, Name = "Tim Burton" },
            new Director { Id = 20, Name = "Roman Polanski" },
            new Director { Id = 21, Name = "Quentin Tarantino" },
            new Director { Id = 22, Name = "Danny Boyle" },
            new Director { Id = 23, Name = "Ridley Scott" },
            new Director { Id = 24, Name = "David Fincher" },
            new Director { Id = 25, Name = "Christopher Nolan" }
        );

        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = 1, Title = "Schindler's List", Year = 1993, Rating = 9.0 },
            new Movie { Id = 2, Title = "Killers of the Flower Moon", Year = 2023, Rating = 7.6 },
            new Movie { Id = 3, Title = "Psycho", Year = 1960, Rating = 8.5 },
            new Movie { Id = 4, Title = "2001: A Space Odyssey", Year = 1968, Rating = 8.3 },
            new Movie { Id = 5, Title = "Apocalypse Now", Year = 1979, Rating = 8.4 },
            new Movie { Id = 6, Title = "Annie Hall", Year = 1977, Rating = 8.0 },
            new Movie { Id = 7, Title = "The Apartment", Year = 1960, Rating = 8.3 },
            new Movie { Id = 8, Title = "The Treasure of the Sierra Madre", Year = 1948, Rating = 8.2 },
            new Movie
            {
                Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring", Year = 2001, Rating = 8.9
            },
            new Movie { Id = 10, Title = "One Flew Over the Cuckoo's Nest", Year = 1975, Rating = 8.7 }
        );

        modelBuilder.Entity<Movie>()
            .HasMany(m => m.Directors)
            .WithMany(d => d.Movies)
            .UsingEntity(b => b.HasData(
                new { MoviesId = 1, DirectorsId = 1 },
                new { MoviesId = 2, DirectorsId = 2 },
                new { MoviesId = 3, DirectorsId = 3 },
                new { MoviesId = 4, DirectorsId = 4 },
                new { MoviesId = 5, DirectorsId = 5 },
                new { MoviesId = 6, DirectorsId = 6 },
                new { MoviesId = 7, DirectorsId = 7 },
                new { MoviesId = 8, DirectorsId = 8 },
                new { MoviesId = 9, DirectorsId = 9 },
                new { MoviesId = 10, DirectorsId = 10 }
            ));
    }
}