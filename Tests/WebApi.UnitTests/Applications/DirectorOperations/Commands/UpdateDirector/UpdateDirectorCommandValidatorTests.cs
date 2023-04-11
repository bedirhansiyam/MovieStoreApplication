using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using static WebApi.Application.DirectorOperations.Commands.UpdateDirector.UpdateDirectorCommand;

namespace Applications.DirectorOperations.Commands.UpdateDirector;

public class UpdateDirectorCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("N","",1)]
    [InlineData("","S",1)]
    [InlineData("N","S",0)]
    [InlineData("N","",0)]
    [InlineData("","S",0)]
    [InlineData("N","S",1)]
    public void WhenInvalidInputsAreGiven_UpdateDirectorValidator_ShouldBeReturnErrors(string name, string surname, int directorId)
    {
        UpdateDirectorCommand command = new UpdateDirectorCommand(null, null);
        command.directorId = directorId;
        command.Model = new UpdateDirectorModel(){Name = name, Surname = surname};

        UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_UpdateDirectorValidator_ShouldNotBeReturnError()
    {
        UpdateDirectorCommand command = new UpdateDirectorCommand(null, null);
        command.directorId = 1;
        command.Model = new UpdateDirectorModel(){Name = "TestName7", Surname = "TestSurname7"};

        UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count().Should().Be(0);
    }
}