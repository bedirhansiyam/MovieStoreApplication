using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.Application.DirectorOperations.Queries.GetDirectors;
using WebApi.DBOperations;

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
}