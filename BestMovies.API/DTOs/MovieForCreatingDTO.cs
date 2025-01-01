using System.ComponentModel.DataAnnotations;

namespace BestMovies.API.DTOs;

public class MovieForCreatingDTO
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Title { get; set; }
    public int Year { get; set; }
    public double Rating { get; set; }
}