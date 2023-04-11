using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;

namespace Applications.DirectorOperations.Commands.DeleteDirector;

public class DeleteDirectorCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidDirectorIdIsGiven_DeleteDirectorValidator_ShouldReturnErrors()
    {
        DeleteDirectorCommand command = new DeleteDirectorCommand(null);
        command.directorId = -1;

        DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidDirectorIdIsGiven_DeleteDirectorValidator_ShouldNotReturnError()
    {
        DeleteDirectorCommand command = new DeleteDirectorCommand(null);
        command.directorId = 1;

        DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count().Should().Be(0);
    }
}