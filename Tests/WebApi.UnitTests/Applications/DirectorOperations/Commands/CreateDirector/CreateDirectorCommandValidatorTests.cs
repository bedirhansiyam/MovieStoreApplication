using FluentAssertions;
using TestSetup;
using WebApi.DirectorOperations.Commands.CreateDirector;
using static WebApi.DirectorOperations.Commands.CreateDirector.CreateDirectorCommand;

namespace Applications.DirectorOperations.Commands.CreateDirector;

public class CreateDirectorCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("","")]
    [InlineData("N","")]
    [InlineData("","S")]
    [InlineData("N","S")]
    [InlineData("Name","S")]
    [InlineData("N","Surname")]
    public void WhenInvalidInputsAreGiven_CreateDirectorValidator_ShouldBeReturnErrors(string name, string surname)
    {
        CreateDirectorCommand command = new CreateDirectorCommand(null, null);
        command.Model = new CreateDirectorModel(){Name = name, Surname = surname};

        CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_CreateDirectorValidator_ShouldNotBeReturnError()
    {
        CreateDirectorCommand command = new CreateDirectorCommand(null, null);
        command.Model = new CreateDirectorModel(){Name = "TestName3", Surname = "TestSurname3"};

        CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count().Should().Be(0);
    }
}