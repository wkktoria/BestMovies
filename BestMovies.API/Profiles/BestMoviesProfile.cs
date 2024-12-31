using AutoMapper;
using BestMovies.API.DTOs;
using BestMovies.API.Entities;

namespace BestMovies.API.Profiles;

public class BestMoviesProfile : Profile
{
    public BestMoviesProfile()
    {
        CreateMap<Movie, MovieDTO>().ReverseMap();
        CreateMap<Movie, MovieForCreatingDTO>().ReverseMap();
        CreateMap<Movie, MovieForUpdatingDTO>().ReverseMap();
        
        CreateMap<Director, DirectorDTO>()
            .ForMember(d => d.MovieId,
                o => o.MapFrom(d => d.Movies.First().Id));
    }
}