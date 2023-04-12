using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Queries.GetOrderDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.OrderOperations.Queries.GetOrderDetail;

public class GetOrderDetailQueryTests: IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetOrderDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenOrderIdIsNotMatchAnyOrder_InvalidOperationException_ShouldBeReturn()
    {
        var order = new Order(){MovieId = 1, CustomerId = 1};
        _context.Orders.Add(order);
        _context.SaveChanges();

        GetOrderDetailQuery query = new GetOrderDetailQuery(_context, _mapper);
        query.OrderId = 120;

        FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The order doesn't exist.");
    }

    [Fact]
    public void WhenGivenOrderIdIsMatchAnyOrder_InvalidOperationException_ShouldNotBeReturn()
    {
        var order = new Order(){MovieId = 1, CustomerId = 1};
        _context.Orders.Add(order);
        _context.SaveChanges();

        GetOrderDetailQuery query = new GetOrderDetailQuery(_context, _mapper);
        query.OrderId = order.Id;

        FluentActions.Invoking(() => query.Handle()).Invoke();

        var result = _context.Orders.SingleOrDefault(x => x.Id == order.Id);
        result.Should().NotBeNull();
    }
}