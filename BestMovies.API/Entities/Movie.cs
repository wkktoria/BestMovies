using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BestMovies.API.Entities;

public class Movie
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(150)]
    public required string Title { get; set; }
    public int Year { get; set; }
    public double Rating { get; set; }
    public ICollection<Director> Directors { get; set; } = new List<Director>();

    public Movie()
    {
    }

    [SetsRequiredMembers]
    public Movie(int id, string title, int year, double rating)
    {
        Id = id;
        Title = title;
        Year = year;
        Rating = rating;
    }
}