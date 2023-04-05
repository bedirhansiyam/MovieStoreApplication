using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;

public class GetDirectorDetailQuery
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int directorId { get; set; }

    public GetDirectorDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public DirectorDetailViewModel Handle()
    {
        var director = _dbContext.Directors.Include(x => x.MoviesDirected).FirstOrDefault(x => x.Id == directorId);
        if(director is null)
            throw new Exception("The director doesn't exist.");

        var result = _mapper.Map<DirectorDetailViewModel>(director);

        return result;
    }

    public class DirectorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Movies { get; set; }
    }
}