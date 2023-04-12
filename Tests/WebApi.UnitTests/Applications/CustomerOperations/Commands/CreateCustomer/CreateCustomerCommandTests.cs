using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.CustomerOperations.Commands.CreateCustomer.CreateCustomerCommand;

namespace Applications.CustomerOperations.Commands.CreateCustomer;

public class CreateCustomerCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateCustomerCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyGivenCustomerEmailIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var customer = new Customer(){Name = "TestCustomerName", Surname = "TestCustomerSurname", Email = "Test@mail.com", Password = "123456789"};
        _context.Customers.Add(customer);
        _context.SaveChanges();

        CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
        command.Model = new CreateCustomerModel(){Name = "TestCustomerName", Surname = "TestCustomerSurname", Email = customer.Email, Password = "123456789"};

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Email already exist.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_InvalidOperationException_ShouldNotBeReturn()
    {
        CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
        CreateCustomerModel model = new CreateCustomerModel(){Name = "TestCustomerName2", Surname = "TestCustomerSurname2", Email = "Test2@mail.com", Password = "123456789"};
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _context.Customers.SingleOrDefault(x => x.Email == model.Email);
        result.Should().NotBeNull();
    }
}