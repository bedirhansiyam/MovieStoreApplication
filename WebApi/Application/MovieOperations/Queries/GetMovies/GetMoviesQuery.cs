using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Queries.GetMovies;

public class GetMoviesQuery
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public GetMoviesQuery(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public  List<MoviesViewModel> Handle()
    {
        var movieList = _dbContext.Movies.Include(x => x.Genre).Include(x => x.Director).Include(x => x.MovieActors).ThenInclude(x => x.Actor).OrderBy(x => x.Id).ToList<Movie>();
        var actors = _dbContext.MovieActors.Where(x => x.MovieId == x.Id).ToList();
        List<MoviesViewModel> vm = _mapper.Map<List<MoviesViewModel>>(movieList);   
        
        return vm;
    }

    public class MoviesViewModel
    {
        public string MovieName { get; set; }
        public string ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public string Director { get; set; }
        public List<string> Actors { get; set; }
               
    }
}