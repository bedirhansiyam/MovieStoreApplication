using AutoMapper;
using WebApi.Entities;
using static WebApi.Application.MovieOperations.Queries.GetMovieDetail.GetMovieDetailQuery;
using static WebApi.Application.MovieOperations.Queries.GetMovies.GetMoviesQuery;

namespace WebApi.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Movie, MoviesViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.GenreName)).ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.Name + " " + src.Director.Surname)).ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.MovieActors.Select(x => x.Actor.Name + " " + x.Actor.Surname)));
        CreateMap<Movie, MovieDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.GenreName)).ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.Name + " " + src.Director.Surname)).ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.MovieActors.Select(x => x.Actor.Name + " " + x.Actor.Surname)));
        
    }
}
