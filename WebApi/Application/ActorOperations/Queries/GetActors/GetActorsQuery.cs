using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.ActorOperations.Queries.GetActors;

public class GetActorsQuery
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public GetActorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<ActorsViewModel> Handle()
    {
        var actorList = _dbContext.Actors.Include(x => x.MovieActors).ThenInclude(x => x.Movie).OrderBy(x => x.Id).ToList();
        var movies = _dbContext.MovieActors.Where(x => x.ActorId == x.Id).ToList();

        List<ActorsViewModel> vm = _mapper.Map<List<ActorsViewModel>>(actorList);

        return vm;
    }

    public class ActorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Movies { get; set; }
    }
}