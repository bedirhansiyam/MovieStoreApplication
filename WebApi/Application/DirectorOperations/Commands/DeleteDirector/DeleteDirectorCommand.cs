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
            throw new Exception("The director doesn't exist.");

        _dbContext.Directors.Remove(director);
        _dbContext.SaveChanges();    
    }
}