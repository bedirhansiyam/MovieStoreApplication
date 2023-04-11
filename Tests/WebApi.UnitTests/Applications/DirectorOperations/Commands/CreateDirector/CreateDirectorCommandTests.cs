using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.DBOperations;
using WebApi.DirectorOperations.Commands.CreateDirector;
using WebApi.Entities;
using static WebApi.DirectorOperations.Commands.CreateDirector.CreateDirectorCommand;

namespace Applications.DirectorOperations.Commands.CreateDirector;

public class CreateDirectorCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateDirectorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistDirectorFullnameIsGive_InvalidOperationException_ShouldBeReturn()
    {
        var director = new Director(){Name = "TestName1" , Surname = "TestSurname1"};
        _context.Directors.Add(director);
        _context.SaveChanges();

        CreateDirectorCommand command = new CreateDirectorCommand(_context,_mapper);
        command.Model = new CreateDirectorModel(){Name = director.Name, Surname = director.Surname};

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The director already exist.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Director_ShouldBeCreated()
    {
        CreateDirectorCommand command = new CreateDirectorCommand(_context,_mapper);
        CreateDirectorModel model = new CreateDirectorModel(){Name = "TestName2", Surname = "TestSurname2"};
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _context.Directors.SingleOrDefault(x => x.Name == model.Name && x.Surname == model.Surname);
        result.Should().NotBeNull();
    }
}