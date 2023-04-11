using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.DirectorOperations.Commands.UpdateDirector.UpdateDirectorCommand;

namespace Applications.DirectorOperations.Commands.UpdateDirector;

public class UpdateDirectorCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public UpdateDirectorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenDoesNotExistDirectorIdIsGivenUpdate_InvalidOperationException_ShouldBeReturn()
    {
        var director = new Director(){Name = "TestName4" , Surname = "TestSurname4"};
        _context.Directors.Add(director);
        _context.SaveChanges();

        UpdateDirectorCommand command = new UpdateDirectorCommand(_mapper,_context);
        command.directorId = 10;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The director doesn't exist.");
    }

    [Fact]
    public void WhenValidDirectorIdIsGiven_InvalidOperationException_ShouldNotBeReturn()
    {
        var director = new Director(){Name = "TestName5" , Surname = "TestSurname5"};
        _context.Directors.Add(director);
        _context.SaveChanges();

        UpdateDirectorCommand command = new UpdateDirectorCommand(_mapper,_context);
        UpdateDirectorModel model = new UpdateDirectorModel(){Name = "TestName6" , Surname = "TestSurname6"};
        command.directorId = director.Id;
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _context.Directors.SingleOrDefault(x => x.Id == director.Id);
        result.Should().NotBeNull();
    }
}
