using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Commands.CreateToken;
using WebApi.Application.CustomerOperations.Commands.DeleteCustomer;
using WebApi.Application.CustomerOperations.Commands.RefreshToken;
using WebApi.DBOperations;
using WebApi.TokenOperations.Models;
using static WebApi.Application.CustomerOperations.Commands.CreateCustomer.CreateCustomerCommand;
using static WebApi.Application.CustomerOperations.Commands.CreateToken.CreateTokenCommand;
using static WebApi.Application.CustomerOperations.Commands.DeleteCustomer.DeleteCustomerCommand;

namespace WebApi.Controllers;


[ApiController]
[Route("[Controller]s")]
public class CustomerController: ControllerBase
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public CustomerController(IMovieStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _configuration = configuration;
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

    [HttpPost("connect/token")]
    public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login) 
    {
        CreateTokenCommand command = new CreateTokenCommand(_dbContext,_mapper,_configuration);
        command.Model = login;

        var token = command.TokenHandle();

        return token; 
    }

    [HttpGet("refreshToken")]
    public ActionResult<Token> RefreshToken([FromQuery] string token)
    {
        RefreshTokenCommand command = new RefreshTokenCommand(_dbContext,_configuration);
        command.RefreshToken = token;

        var newToken = command.Handle();

        return newToken;
    }
}