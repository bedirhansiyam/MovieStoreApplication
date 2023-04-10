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
            throw new InvalidOperationException("The actor doesn't exist.");

        var movies = _dbContext.MovieActors.Where(x => x.ActorId == ActorId).Any();
        if(movies)
            throw new InvalidOperationException("The actor has movie/s. To delete the actor you must first delete the actor's movie/s.");

        _dbContext.Actors.Remove(actor);
        _dbContext.SaveChanges();
    }
}