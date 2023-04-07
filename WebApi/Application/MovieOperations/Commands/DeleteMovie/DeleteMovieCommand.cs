using WebApi.DBOperations;

namespace WebApi.Application.MovieOperations.Commands.DeleteMovie;

public class DeleteMovieCommand
{
    public int MovieId;
    private readonly IMovieStoreDbContext _dbContext;

    public DeleteMovieCommand(IMovieStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var movie = _dbContext.Movies.SingleOrDefault(x => x.Id == MovieId);
        if(movie is null)
            throw new InvalidOperationException("The movie doesn't exist.");

        _dbContext.Movies.Remove(movie);
        _dbContext.SaveChanges();
    }
}