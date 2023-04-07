using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.ActorOperations.Queries.GetActorDetail;

public class GetActorDetailQuery
{
    public int actorId;
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetActorDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public ActorDetailViewModel Handle()
    {
        var actor = _dbContext.Actors.Include(x => x.MovieActors).ThenInclude(x => x.Movie).Where(x => x.Id == actorId).SingleOrDefault();
        if(actor is null)
            throw new Exception("The actor doesn't exist");

        var movies = _dbContext.MovieActors.Where(x => x.ActorId == actorId);

        ActorDetailViewModel vm = _mapper.Map<ActorDetailViewModel>(actor);

        return vm;
    }

    public class ActorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Movies { get; set; }
    }
}