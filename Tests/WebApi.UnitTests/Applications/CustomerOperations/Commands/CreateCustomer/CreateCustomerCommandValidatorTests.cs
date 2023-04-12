using FluentAssertions;
using TestSetup;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using static WebApi.Application.CustomerOperations.Commands.CreateCustomer.CreateCustomerCommand;

namespace Applications.CustomerOperations.Commands.CreateCustomer;

public class CreateCustomerCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("","Surname","Email","123456789")]
    [InlineData("Name","","Email","123456789")]
    [InlineData("Name","Surname","","123456789")]
    [InlineData("Name","Surname","Email","")]
    [InlineData("Name","Surname","Email","1234")]
    [InlineData("N","Surname","Email","123456789")]
    [InlineData("Name","S","Email","123456789")]
    [InlineData("Name","Surname","Em","123456789")]
    public void WhenInvalidInputsAreGiven_CreateCustomerValidator_ShouldBeReturnErrors(string name, string surname, string email, string password)
    {
        CreateCustomerCommand command = new CreateCustomerCommand(null, null);
        command.Model = new CreateCustomerModel(){Name = name, Surname = surname, Email = email, Password = password};

        CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidIputsAreGiven_CreateCustomerValidator_ShouldNotBeReturnErrors()
    {
        CreateCustomerCommand command = new CreateCustomerCommand(null, null);
        command.Model = new CreateCustomerModel(){Name = "name", Surname = "surname", Email = "email", Password = "123456789"};

        CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}