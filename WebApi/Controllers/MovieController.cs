using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.Application.MovieOperations.Queries.GetMovies;
using WebApi.DBOperations;

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
}