using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.DirectorOperations.Commands.DeleteDirector;

public class DeleteDirectorCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    public DeleteDirectorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenInvalidDirectorIdIsGivenInDelete_InvalidOperationException_ShouldBeReturn()
    {
        var director = new Director(){Name = "TestName8", Surname = "TestSurname8"};
        _context.Directors.Add(director);
        _context.SaveChanges();

        DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
        command.directorId = 10;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The director doesn't exist.");
    }

    [Fact]
    public void WhenDirectorHasMovieIsGivenInDelete_InvalidOperationException_ShouldBeReturn()
    {
        var director = new Director(){Name = "TestName9", Surname = "TestSurname9"};
        _context.Directors.Add(director);
        _context.SaveChanges();

        var movie = new Movie(){MovieName = "TestMovieName", GenreId = 1, DirectorId = director.Id, Price = 35, ReleaseDate = new DateTime(1994,05,21)};
        _context.Movies.Add(movie);
        _context.SaveChanges();

        director.MoviesDirected = _context.Movies.Where(x => x.DirectorId == director.Id).ToList();

        DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
        command.directorId = director.Id;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The director has movie/s. To delete the director you must first delete the director's movie/s.");
    }

    [Fact]
    public void WhenValidInputsAreGivenInDelete_InvalidOperationException_ShouldNotBeReturn()
    {
        var director = new Director(){Name = "TestName10", Surname = "TestSurname10"};
        _context.Directors.Add(director);
        _context.SaveChanges();

        var movie = new Movie(){MovieName = "TestMovieName1", GenreId = 1, DirectorId = director.Id + 1, Price = 35, ReleaseDate = new DateTime(1994,05,21)};
        _context.Movies.Add(movie);
        _context.SaveChanges();

        director.MoviesDirected = _context.Movies.Where(x => x.DirectorId == director.Id).ToList();

        DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
        command.directorId = director.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _context.Directors.SingleOrDefault(x => x.Id == director.Id);
        result.Should().BeNull();
    }
}