using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.ActorOperations.Commands.CreateActor.CreateActorCommand;

namespace Applications.ActorOperations.Commands.CreateActor;

public class CreateActorCommandTest: IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateActorCommandTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyGivenActorFullnameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var actor = new Actor(){Name = "TestName", Surname = "TestSurname"};
        _context.Actors.Add(actor);
        _context.SaveChanges();

        CreateActorCommand command = new CreateActorCommand(_context,_mapper);
        command.Model = new CreateActorModel(){Name = actor.Name, Surname = actor.Surname};

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The actor already exist.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Actor_ShouldBeCreated()
    {
        CreateActorCommand command = new CreateActorCommand(_context,_mapper);
        CreateActorModel model = new CreateActorModel(){Name = "TestName", Surname = "TestSurname"};
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var actor = _context.Actors.SingleOrDefault(x => x.Name == model.Name && x.Surname == model.Surname);
        actor.Should().NotBeNull();
    }
}