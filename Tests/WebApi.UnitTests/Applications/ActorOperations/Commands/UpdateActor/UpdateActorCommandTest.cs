using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.ActorOperations.Commands.UpdateActor.UpdateActorCommand;

namespace Applications.ActorOperations.Commands.UpdateActor;

public class UpdateActorCommandTest: IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;
    public UpdateActorCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenDoesNotExistActorIdIsGivenUpdate_InvalidOperationException_ShouldBeReturn()
    {
        var actor = new Actor(){Name = "TestName", Surname = "TestSurname"};
        _context.Actors.Add(actor);
        _context.SaveChanges();

        UpdateActorCommand command = new UpdateActorCommand(_mapper,_context);
        command.ActorId = 10;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The actor doesn't exist.");
    }

    [Fact]
    public void WhenValidActorIdIsGivenUpdate_InvalidOperationException_ShouldNotReturn()
    {
        var actor = new Actor(){Name = "TestName1", Surname = "TestSurname1"};
        _context.Actors.Add(actor);
        _context.SaveChanges();

        UpdateActorCommand command = new UpdateActorCommand(_mapper,_context);
        UpdateActorModel model = new UpdateActorModel(){Name = "TestName2", Surname = "TestSurname2"};
        command.Model = model;
        command.ActorId = actor.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var  result = _context.Actors.SingleOrDefault(x => x.Id == actor.Id);
        result.Should().NotBeNull();
    }
}