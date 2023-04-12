using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.MovieOperations.Commands.CreateMovie.CreateMovieCommand;

namespace Applications.MovieOperations.Commands.CreateMovie;

public class CreateMovieCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateMovieCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistMovieNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var movie = new Movie(){MovieName = "TestMovieName", ReleaseDate = new DateTime(2008,07,14), GenreId = 1, DirectorId = 1, Price = 10};
        _context.Movies.Add(movie);
        _context.SaveChanges();

        CreateMovieCommand command = new CreateMovieCommand(_mapper,_context);
        command.Model = new CreateMovieModel(){MovieName = movie.MovieName, ReleaseDate =movie.ReleaseDate, GenreId = 2, DirectorId = 2, Price = 15};

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The movie already exist.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Movie_ShouldBeCreated()
    {
        CreateMovieCommand command = new CreateMovieCommand(_mapper,_context);
        CreateMovieModel model = new CreateMovieModel(){MovieName = "TestMovieName3", ReleaseDate = new DateTime(2008,07,14), GenreId = 2, DirectorId = 2, Price = 15};
        model.ActorIds = new List<int>(){1,2};
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _context.Movies.SingleOrDefault(x => x.MovieName == model.MovieName);
        result.Should().NotBeNull();
    }
}