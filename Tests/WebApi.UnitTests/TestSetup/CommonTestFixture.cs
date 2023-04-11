using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace TestSetup;

public class CommonTestFixture
{
    public MovieStoreDbContext Context { get; set; }
    public IMapper Mapper { get; set; }
    public CommonTestFixture()
    {
        var options = new DbContextOptionsBuilder<MovieStoreDbContext>().UseInMemoryDatabase(databaseName: "MovieStoreTestDB").Options;
        Context = new MovieStoreDbContext(options);

        Context.Database.EnsureCreated();
        Context.AddActors();
        Context.AddCustomers();
        Context.AddDirectors();
        Context.AddGenres();
        Context.AddMovieActors();
        Context.AddMovies();
        Context.AddOrders();

        Mapper = new MapperConfiguration(cfg => {cfg.AddProfile<MappingProfile>();}).CreateMapper();
    }
}