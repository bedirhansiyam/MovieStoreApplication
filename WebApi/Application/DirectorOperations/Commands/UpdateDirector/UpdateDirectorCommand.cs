using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector;

public class UpdateDirectorCommand
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public UpdateDirectorModel Model;
    public int directorId;

    public UpdateDirectorCommand(IMapper mapper, IMovieStoreDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var director = _dbContext.Directors.SingleOrDefault(x => x.Id == directorId);
        if(director is null)
            throw new InvalidOperationException("The director doesn't exist.");

        _mapper.Map(Model, director);    
        _dbContext.SaveChanges();
    }

    public class UpdateDirectorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}