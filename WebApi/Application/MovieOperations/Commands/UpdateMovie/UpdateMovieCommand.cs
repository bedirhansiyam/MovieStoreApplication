using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Commands.UpdateMovie;

public class UpdateMovieCommand
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int MovieId;
    public UpdateMovieModel Model;

    public UpdateMovieCommand(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var movie = _dbContext.Movies.SingleOrDefault(x => x.Id == MovieId);
        if(movie is null)
            throw new InvalidOperationException("The movie doesn't exist.");

        _mapper.Map(Model,movie);
        _dbContext.SaveChanges();

        var movieActor = _dbContext.MovieActors.Where(x => x.MovieId == MovieId).ToList();
        foreach (var movAct in movieActor)
        {
            _dbContext.MovieActors.Remove(movAct);
            _dbContext.SaveChanges();
        }
        for(int i = 0; i<Model.ActorIds.Count; i++)
        {
            MovieActor newActor = new MovieActor();
            newActor.ActorId = Model.ActorIds[i];
            newActor.MovieId = movie.Id;
            _dbContext.MovieActors.Add(newActor);
            _dbContext.SaveChanges();
        }
    }

    public class UpdateMovieModel
    {
        public string MovieName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public decimal Price { get; set; }
        public List<int> ActorIds { get; set; }
    }
}