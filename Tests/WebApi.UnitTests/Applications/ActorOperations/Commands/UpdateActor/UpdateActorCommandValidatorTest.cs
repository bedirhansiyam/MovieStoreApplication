using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using static WebApi.Application.ActorOperations.Commands.UpdateActor.UpdateActorCommand;

namespace Applications.ActorOperations.Commands.UpdateActor;

public class UpdateActorCommandValidatorTest: IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("N","",1)]
    [InlineData("","S",1)]
    [InlineData("N","S",0)]
    [InlineData("N","",0)]
    [InlineData("","S",0)]
    [InlineData("N","S",1)]
    public void WhenInvalidInputsAreGiven_UpdateActorValidator_ShouldBeReturnErrors(string name, string surname, int actorId)
    {
        UpdateActorCommand command = new UpdateActorCommand(null, null);
        command.ActorId = actorId;
        command.Model = new UpdateActorModel(){Name = name, Surname = surname};

        UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_UpdateActorValidator_ShouldNotBeReturnError()
    {
        UpdateActorCommand command = new UpdateActorCommand(null, null);
        command.ActorId = 1;
        command.Model = new UpdateActorModel(){Name = "TestName", Surname = "TestSurname"};

        UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count().Should().Be(0);
    }
}