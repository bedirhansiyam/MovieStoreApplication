using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.MovieOperations.Commands.UpdateMovie.UpdateMovieCommand;

namespace Applications.MovieOperations.Commands.UpdateMovie;

public class UpdateMovieCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public UpdateMovieCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenDoesNotExistMovieIdIsGivenUpdate_InvalidOpereationException_ShouldBeReturn()
    {
        var movie = new Movie(){MovieName = "TestMovieName6", ReleaseDate = new DateTime(2008,07,14), GenreId = 1, DirectorId = 1, Price = 10};
        _context.Movies.Add(movie);
        _context.SaveChanges();

        UpdateMovieCommand command = new UpdateMovieCommand(_context,_mapper);
        command.MovieId = 100;
        
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The movie doesn't exist.");
    }

    [Fact]
    public void WhenValidMovieIdIsGivenUpdate_InvalidOperationException_ShouldNotReturn()
    {
        var movie = new Movie(){MovieName = "TestMovieName6", ReleaseDate = new DateTime(2008,07,14), GenreId = 1, DirectorId = 1, Price = 10};
        _context.Movies.Add(movie);
        _context.SaveChanges();

        UpdateMovieCommand command = new UpdateMovieCommand(_context,_mapper);
        UpdateMovieModel model = new UpdateMovieModel(){MovieName = "TestMovieName7", ReleaseDate = new DateTime(2007,07,14), GenreId = 2, DirectorId = 2, Price = 20}; 
        model.ActorIds = new List<int>(){1,2};
        
        command.MovieId = movie.Id;
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _context.Movies.SingleOrDefault(x => x.Id == movie.Id);
        result.Should().NotBeNull();
    }
}