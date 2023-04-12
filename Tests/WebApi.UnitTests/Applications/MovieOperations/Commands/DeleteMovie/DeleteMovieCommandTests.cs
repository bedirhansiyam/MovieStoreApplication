using FluentAssertions;
using TestSetup;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.MovieOperations.Commands.DeleteMovie;

public class DeleteMovieCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;

    public DeleteMovieCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenInvalidMovieIdIsGivenDelete_InvalidOperationException_ShouldBeReturn()
    {
        var movie = new Movie(){MovieName = "TestMovieName12", ReleaseDate = new DateTime(2008,07,14), GenreId = 1, DirectorId = 1, Price = 10};
        _context.Movies.Add(movie);
        _context.SaveChanges();

        DeleteMovieCommand command = new DeleteMovieCommand(_context);
        command.MovieId = 100;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The movie doesn't exist.");
    }

    [Fact]
    public void WhenValidMovieIdIsGivenDelete_InvalidOperationException_ShouldNotBeReturn()
    {
        var movie = new Movie(){MovieName = "TestMovieName13", ReleaseDate = new DateTime(2008,07,14), GenreId = 1, DirectorId = 1, Price = 10};
        _context.Movies.Add(movie);
        _context.SaveChanges();

        DeleteMovieCommand command = new DeleteMovieCommand(_context);
        command.MovieId = movie.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _context.Movies.SingleOrDefault(x => x.Id == movie.Id);
        result.Should().BeNull();
    }
}