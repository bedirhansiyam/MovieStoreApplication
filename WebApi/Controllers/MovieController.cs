using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.Application.MovieOperations.Queries.GetMovies;
using WebApi.DBOperations;
using static WebApi.Application.MovieOperations.Commands.CreateMovie.CreateMovieCommand;
using static WebApi.Application.MovieOperations.Commands.UpdateMovie.UpdateMovieCommand;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class MovieController : ControllerBase
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public MovieController(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetMovies()
    {
        GetMoviesQuery query = new GetMoviesQuery(_dbContext,_mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GetMovieDetailById(int id)
    {
        GetMovieDetailQuery query = new GetMovieDetailQuery(_dbContext,_mapper);
        query.MovieId = id;

        GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddMovie([FromBody] CreateMovieModel newMovie)
    {
        CreateMovieCommand command = new CreateMovieCommand(_mapper,_dbContext);
        command.Model = newMovie;

        CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
        validator.ValidateAndThrow(command);        
        
        command.Handle();

        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieModel updatedMovie)
    {
        UpdateMovieCommand command = new UpdateMovieCommand(_dbContext,_mapper);
        command.MovieId = id;
        command.Model = updatedMovie;

        UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteMovie(int id)
    {
        DeleteMovieCommand command = new DeleteMovieCommand(_dbContext);
        command.MovieId = id;

        DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }
}