using FluentAssertions;
using TestSetup;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.CustomerOperations.Commands.DeleteCustomer.DeleteCustomerCommand;

namespace Applications.CustomerOperations.Commands.DeleteCustomer;

public class DeleteCustomerCommandTests:IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;

    public DeleteCustomerCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenDoesNotExistCustomerIdIsGivenInDelete_InvalidOperationException_ShouldBeReturn()
    {
        var customer = new Customer(){Name = "TestCustomerName3", Surname = "TestCustomerSurname3", Email = "Test3@mail.com", Password = "123456789"};
        _context.Customers.Add(customer);
        _context.SaveChanges();

        DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
        command.CustomerId = 120;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The customer doesn't exist.");
    }

    [Fact]
    public void WhenInvalidPasswordIsGivenInDelete_InvalidOperationException_ShouldBeReturn()
    {
        var customer = new Customer(){Name = "TestCustomerName4", Surname = "TestCustomerSurname4", Email = "Test4@mail.com", Password = "123456789"};
        _context.Customers.Add(customer);
        _context.SaveChanges();

        DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
        command.CustomerId = customer.Id;
        command.Model = new DeleteCustomerModel(){Password = "1"};

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Wrong password!");
    }

    [Fact]
    public void WhenValidInputsAreGivenInDelete_InvalidOperationException_ShouldNotBeReturn()
    {
        var customer = new Customer(){Name = "TestCustomerName4", Surname = "TestCustomerSurname4", Email = "Test4@mail.com", Password = "123456789"};
        _context.Customers.Add(customer);
        _context.SaveChanges();

        DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
        command.CustomerId = customer.Id;
        DeleteCustomerModel model = new DeleteCustomerModel(){Password = "123456789"};
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _context.Customers.SingleOrDefault(x => x.Id == customer.Id && x.Password == model.Password);
        result.Should().BeNull();
    }
}