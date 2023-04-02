using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Queries.GetMovieDetail;

public class GetMovieDetailQuery
{
    public int MovieId { get; set; }
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetMovieDetailQuery(IMovieStoreDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    public MovieDetailViewModel Handle()
    {
        var movie = _dbContext.Movies.Include(x => x.Genre).Include(x => x.Director).Include(x => x.MovieActors).ThenInclude(x => x.Actor).Where(x => x.Id == MovieId).SingleOrDefault();
        
        if(movie is null)
            throw new InvalidOperationException("The movie doesn't exist");

        var actors = _dbContext.MovieActors.Where(x => x.MovieId == MovieId).ToList();

        MovieDetailViewModel vm = _mapper.Map<MovieDetailViewModel>(movie);

        return vm;
    }

    public class MovieDetailViewModel
    {
        public string MovieName { get; set; }
        public string ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public string Director { get; set; }
        public List<string> Actors { get; set; }
    }

}