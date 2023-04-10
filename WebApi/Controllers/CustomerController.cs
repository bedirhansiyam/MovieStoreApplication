using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.DBOperations;
using static WebApi.Application.CustomerOperations.Commands.CreateCustomer.CreateCustomerCommand;
using static WebApi.Application.CustomerOperations.Commands.DeleteCustomer.DeleteCustomerCommand;

namespace WebApi.Controllers;


[ApiController]
[Route("[Controller]s")]
public class CustomerController: ControllerBase
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public CustomerController(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddCustomer([FromBody] CreateCustomerModel newCustomer)
    {
        CreateCustomerCommand command = new CreateCustomerCommand(_dbContext,_mapper);
        command.Model = newCustomer;

        CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteCustomer(int id, [FromBody] DeleteCustomerModel model)
    {
        DeleteCustomerCommand command = new DeleteCustomerCommand(_dbContext);
        command.CustomerId = id;
        command.Model = model;

        DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }
}