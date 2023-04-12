using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.OrderOperations.Commands.DeleteOrder;

public class DeleteOrderCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;

    public DeleteOrderCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenDoesNotExistOrderIdIsGivenInDelete_InvalidOperationException_ShouldBeReturn()
    {
        var order = new Order(){MovieId = 1, CustomerId =1};
        _context.Orders.Add(order);
        _context.SaveChanges();

        DeleteOrderCommand command = new DeleteOrderCommand(_context);
        command.OrderId = 20;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The order doesn't exist.");
    }

    [Fact]
    public void WhenValidOrderIdIsGiven_InvalidOperationException_ShouldNotBeReturn()
    {
        var order = new Order(){MovieId = 1, CustomerId =1};
        _context.Orders.Add(order);
        _context.SaveChanges();

        DeleteOrderCommand command = new DeleteOrderCommand(_context);
        command.OrderId = order.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        
        var result = _context.Orders.SingleOrDefault(x => x.Id == order.Id);
        result.Should().BeNull();

    }
}