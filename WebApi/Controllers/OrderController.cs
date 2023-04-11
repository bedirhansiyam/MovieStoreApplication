using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.Application.OrderOperations.Commands.DeleteOrder;
using WebApi.Application.OrderOperations.Queries.GetOrderDetail;
using WebApi.Application.OrderOperations.Queries.GetOrderDetailByCustomer;
using WebApi.Application.OrderOperations.Queries.GetOrders;
using WebApi.DBOperations;
using static WebApi.Application.OrderOperations.Commands.CreateOrder.CreateOrderCommand;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]s")]
public class OrderController : ControllerBase
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public OrderController(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddOrder([FromBody] CreateOrderModel newOrder)
    {
        CreateOrderCommand command = new CreateOrderCommand(_dbContext,_mapper);
        command.Model = newOrder;

        CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpGet]
    public IActionResult GetOrders()
    {
        GetOrderQuery query = new GetOrderQuery(_dbContext,_mapper);
        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GetOrderDetailByCustomerId(int id)
    {
        GetOrderDetailByCustomerQuery query = new GetOrderDetailByCustomerQuery(_dbContext,_mapper);
        query.OrderedCustomerId = id;

        GetOrderDetailByCustomerQueryValidator validator = new GetOrderDetailByCustomerQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("orderId")]
    public IActionResult GetOrderByOrderId(int orderId)
    {
        GetOrderDetailQuery query = new GetOrderDetailQuery(_dbContext,_mapper);
        query.OrderId = orderId;

        GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpDelete("id")]
    public IActionResult DeleteOrder(int id)
    {
        DeleteOrderCommand command = new DeleteOrderCommand(_dbContext);
        command.OrderId = id;

        DeleteOrderCommandValidator validator = new DeleteOrderCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        
        return Ok();
    }
}