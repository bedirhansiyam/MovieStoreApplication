using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;

namespace Applications.OrderOperations.Commands.DeleteOrder;

public class DeleteOrderCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidOrderIdIsGiven_DeleteOrderValidator_ShouldBeReturnErrors()
    {
        DeleteOrderCommand command = new DeleteOrderCommand(null);
        command.OrderId = -1;

        DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidOrderIdIsGiven_DeleteOrderValidator_ShouldNotBeReturnErrors()
    {
        DeleteOrderCommand command = new DeleteOrderCommand(null);
        command.OrderId = 1;

        DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}