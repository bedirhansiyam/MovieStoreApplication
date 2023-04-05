using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.DirectorOperations.Queries.GetDirectors;

public class GetDirectorsQuery
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetDirectorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<DirectorsViewModel> Handle()
    {
        var directors = _dbContext.Directors.Include(x => x.MoviesDirected).OrderBy(x => x.Id).ToList();
        var movies =_dbContext.Movies.Where(x => x.Id == x.DirectorId).ToList();

        List<DirectorsViewModel> vm = _mapper.Map<List<DirectorsViewModel>>(directors);

        return vm;
    }

    public class DirectorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Movies { get; set; }
    }
}