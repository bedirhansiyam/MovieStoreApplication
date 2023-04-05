using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.DirectorOperations.Commands.CreateDirector;

public class CreateDirectorCommand
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateDirectorModel Model;
    public CreateDirectorCommand(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public void Handle()
    {
        var director = _dbContext.Directors.SingleOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname);
        if(director is not null)
            throw new Exception("The director already exist.");

        director = _mapper.Map<Director>(Model);

        _dbContext.Directors.Add(director);
        _dbContext.SaveChanges();
    }

    public class CreateDirectorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}