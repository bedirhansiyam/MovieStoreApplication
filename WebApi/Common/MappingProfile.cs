using AutoMapper;
using WebApi.Entities;
using static WebApi.Application.DirectorOperations.Commands.UpdateDirector.UpdateDirectorCommand;
using static WebApi.Application.DirectorOperations.Queries.GetDirectorDetail.GetDirectorDetailQuery;
using static WebApi.Application.DirectorOperations.Queries.GetDirectors.GetDirectorsQuery;
using static WebApi.Application.MovieOperations.Commands.CreateMovie.CreateMovieCommand;
using static WebApi.Application.MovieOperations.Queries.GetMovieDetail.GetMovieDetailQuery;
using static WebApi.Application.MovieOperations.Queries.GetMovies.GetMoviesQuery;
using static WebApi.DirectorOperations.Commands.CreateDirector.CreateDirectorCommand;

namespace WebApi.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Movie, MoviesViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.GenreName)).ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.Name + " " + src.Director.Surname)).ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.MovieActors.Select(x => x.Actor.Name + " " + x.Actor.Surname)));
        
        CreateMap<Movie, MovieDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.GenreName)).ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.Name + " " + src.Director.Surname)).ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.MovieActors.Select(x => x.Actor.Name + " " + x.Actor.Surname)));

        CreateMap<Director, DirectorsViewModel>().ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.MoviesDirected.Select(x => x.MovieName))); 
        CreateMap<Director, DirectorDetailViewModel>().ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.MoviesDirected.Select(x => x.MovieName))); 

        CreateMap<CreateDirectorModel, Director>();
        CreateMap<UpdateDirectorModel, Director>();
        
        CreateMap<CreateMovieModel, Movie>().ReverseMap();

    }
}
