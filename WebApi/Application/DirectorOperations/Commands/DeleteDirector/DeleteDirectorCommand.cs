using WebApi.DBOperations;

namespace WebApi.Application.DirectorOperations.Commands.DeleteDirector;

public class DeleteDirectorCommand
{
    private readonly IMovieStoreDbContext _dbContext;
    public int directorId;

    public DeleteDirectorCommand(IMovieStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var director = _dbContext.Directors.SingleOrDefault(x => x.Id == directorId);
        if(director is null)
            throw new InvalidOperationException("The director doesn't exist.");

        var movies = _dbContext.Movies.Where(x => x.DirectorId == directorId).Any();
        if(movies)
            throw new InvalidOperationException("The director has movie/s. To delete the director you must first delete the director's movie/s.");

        _dbContext.Directors.Remove(director);
        _dbContext.SaveChanges();    
    }
}