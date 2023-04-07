using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.Application.ActorOperations.Commands.DeleteActor;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.Application.ActorOperations.Queries.GetActors;
using WebApi.DBOperations;
using static WebApi.Application.ActorOperations.Commands.CreateActor.CreateActorCommand;
using static WebApi.Application.ActorOperations.Commands.UpdateActor.UpdateActorCommand;

namespace WebApi.Controllers;
[ApiController]
[Route("[Controller]s")]

public class ActorController : ControllerBase
{   
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public ActorController(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetActors()
    {
        GetActorsQuery query = new GetActorsQuery(_dbContext,_mapper);
        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GetActorDetailById(int id)
    {
        GetActorDetailQuery query = new GetActorDetailQuery(_dbContext,_mapper);
        query.actorId = id;

        GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddActor([FromBody] CreateActorModel newActor)
    {
        CreateActorCommand command = new CreateActorCommand(_dbContext,_mapper);
        command.Model = newActor;

        CreateActorCommandValidator validator = new CreateActorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateActor([FromBody] UpdateActorModel updatedActor, int id)
    {
        UpdateActorCommand command = new UpdateActorCommand(_mapper,_dbContext);
        command.Model = updatedActor;
        command.ActorId = id;

        UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteActor(int id)
    {
        DeleteActorCommand command = new DeleteActorCommand(_dbContext);
        command.ActorId = id;

        DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

}