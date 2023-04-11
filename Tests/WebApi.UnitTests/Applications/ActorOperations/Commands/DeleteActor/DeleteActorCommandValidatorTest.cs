using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Commands.DeleteActor;

namespace Applications.ActorOperations.Commands.DeleteActor;

public class DeleteActorCommandValidatorTest: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidActorIdIsGivenDelete_InvalidOperationException_ShouldReturnErrors()
    {
        DeleteActorCommand command = new DeleteActorCommand(null);
        command.ActorId = -1;

        DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidActorIdIsGivenUpdate_InvalidOperationException_ShouldNotBeReturnError()
    {
        DeleteActorCommand command = new DeleteActorCommand(null);
        command.ActorId = 1;

        DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count().Should().Be(0);
    }
}