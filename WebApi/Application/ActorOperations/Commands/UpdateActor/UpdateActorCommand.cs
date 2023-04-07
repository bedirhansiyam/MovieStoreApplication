using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.ActorOperations.Commands.UpdateActor;

public class UpdateActorCommand
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int ActorId;
    public UpdateActorModel Model;
    public UpdateActorCommand(IMapper mapper, IMovieStoreDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var actor = _dbContext.Actors.SingleOrDefault(x => x.Id == ActorId);
        if(actor is null)
            throw new Exception("The actor doesn't exist.");
        
        _mapper.Map(Model, actor);
        _dbContext.SaveChanges();
    }

    public class UpdateActorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}