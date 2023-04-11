using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using static WebApi.Application.ActorOperations.Commands.CreateActor.CreateActorCommand;

namespace Applications.ActorOperations.Commands.CreateActor;

public class CreateActorCommandValidatorTest: IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("","")]
    [InlineData("N","")]
    [InlineData("","S")]
    [InlineData("N","S")]
    [InlineData("Name","S")]
    [InlineData("N","Surname")]
    public void WhenInvalidInputsAreGiven_CreateActorValidator_ShouldBeReturnErrors(string name, string surname)
    {
        CreateActorCommand command = new CreateActorCommand(null, null);
        command.Model = new CreateActorModel(){Name = name, Surname = surname};

        CreateActorCommandValidator validator = new CreateActorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_CreateActorValidator_ShouldNotBeReturnErrors()
    {
        CreateActorCommand command = new CreateActorCommand(null, null);
        command.Model = new CreateActorModel(){Name = "TestName", Surname = "TestSurname"};

        CreateActorCommandValidator validator = new CreateActorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count().Should().Be(0);
    }
}