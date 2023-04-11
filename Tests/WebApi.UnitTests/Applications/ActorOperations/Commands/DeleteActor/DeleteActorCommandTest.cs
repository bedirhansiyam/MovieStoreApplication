
using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Commands.DeleteActor;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.ActorOperations.Commands.DeleteActor;

public class DeleteActorCommandTest: IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    public DeleteActorCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenDoesNotExistActordIsGivenInDelete_InvalidOperationException_ShouldBeReturn()
    {
        var actor = new Actor(){Name = "TestName4", Surname = "TestSurname4"};
        _context.Actors.Add(actor);
        _context.SaveChanges();

        DeleteActorCommand command = new DeleteActorCommand(_context);
        command.ActorId = 10;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The actor doesn't exist.");
    }

    [Fact]
    public void WhenActorHasMovieIsGivenInDelete_InvalidOperationException_ShouldBeReturn()
    {
        var actor = new Actor(){Name = "TestName5", Surname = "TestSurname5"};
        _context.Actors.Add(actor);
        _context.SaveChanges();

        var movieActor = new MovieActor(){ActorId = actor.Id, MovieId = 1};
        _context.MovieActors.Add(movieActor);
        _context.SaveChanges();

        DeleteActorCommand command = new DeleteActorCommand(_context);
        command.ActorId = actor.Id;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The actor has movie/s. To delete the actor you must first delete the actor's movie/s.");
    }

    [Fact]
    public void WhenValidInputsAreGivenInDelete_InvalidOperationException_ShouldNotBeReturn()
    {
        var actor = new Actor(){Name = "TestName10", Surname = "TestSurname10"};
        _context.Actors.Add(actor);
        _context.SaveChanges();

        DeleteActorCommand command = new DeleteActorCommand(_context);
        command.ActorId = actor.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _context.Directors.SingleOrDefault(x => x.Id == actor.Id);
        result.Should().BeNull();
    }
}