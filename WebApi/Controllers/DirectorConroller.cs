using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.Application.DirectorOperations.Queries.GetDirectors;
using WebApi.DBOperations;
using WebApi.DirectorOperations.Commands.CreateDirector;
using static WebApi.Application.DirectorOperations.Commands.UpdateDirector.UpdateDirectorCommand;
using static WebApi.DirectorOperations.Commands.CreateDirector.CreateDirectorCommand;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]

public class DirectorController : ControllerBase
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public DirectorController(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetDirectors()
    {
        GetDirectorsQuery query = new GetDirectorsQuery(_dbContext,_mapper);
        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GetDirectorById(int id)
    {
        GetDirectorDetailQuery query = new GetDirectorDetailQuery(_dbContext,_mapper);
        query.directorId = id;

        GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddDirector([FromBody] CreateDirectorModel newDirector)
    {
        CreateDirectorCommand command = new CreateDirectorCommand(_dbContext,_mapper);
        command.Model = newDirector;

        CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateDirector([FromBody] UpdateDirectorModel updatedDirector, int id)
    {
        UpdateDirectorCommand command = new UpdateDirectorCommand(_mapper,_dbContext);
        command.directorId = id;
        command.Model = updatedDirector;

        UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteDirector(int id)
    {
        DeleteDirectorCommand command = new DeleteDirectorCommand(_dbContext);
        command.directorId = id;

        DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }
}