using WebApi.DBOperations;

namespace WebApi.Application.ActorOperations.Commands.DeleteActor;

public class DeleteActorCommand
{
    public int ActorId;
    private readonly IMovieStoreDbContext _dbContext;

    public DeleteActorCommand(IMovieStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var actor = _dbContext.Actors.SingleOrDefault(x => x.Id == ActorId);
        if(actor is null)
            throw new Exception("The actor doesn't exist.");

        _dbContext.Actors.Remove(actor);
        _dbContext.SaveChanges();
    }
}