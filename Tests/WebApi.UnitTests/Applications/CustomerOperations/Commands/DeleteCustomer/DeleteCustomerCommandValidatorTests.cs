using FluentAssertions;
using TestSetup;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using static WebApi.Application.CustomerOperations.Commands.DeleteCustomer.DeleteCustomerCommand;

namespace Applications.CustomerOperations.Commands.DeleteCustomer;

public class DeleteCustomerCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(1,"")]
    [InlineData(-1,"123456789")]
    [InlineData(-1,"")]
    public void WhenInvalidInputsAreGiven_DeleteCustomerValidator_ShouldBeReturnErrors(int customerId, string password)
    {
        DeleteCustomerCommand command = new DeleteCustomerCommand(null);
        command.CustomerId = customerId;
        command.Model = new DeleteCustomerModel(){Password = password};

        DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_DeleteCustomerValidator_ShouldNotBeReturnErrors()
    {
        DeleteCustomerCommand command = new DeleteCustomerCommand(null);
        command.CustomerId = 1;
        command.Model = new DeleteCustomerModel(){Password = "123456789"};

        DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}