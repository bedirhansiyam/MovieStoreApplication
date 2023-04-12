using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.MovieOperations.Queries.GetMovieDetail;

public class GetMovieDetailQueryTests: IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetMovieDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenMovieIdIsNotMatchAnyMovie_InvalidOperationException_ShouldBeReturn()
    {
        var movie = new Movie(){MovieName = "TestMovieName14", ReleaseDate = new DateTime(2008,07,14), GenreId = 1, DirectorId = 1, Price = 10};
        _context.Movies.Add(movie);
        _context.SaveChanges();

        GetMovieDetailQuery query = new GetMovieDetailQuery(_context,_mapper);
        query.MovieId = 100;

        FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The movie doesn't exist") ;
    }

    [Fact]
    public void WhenGivenMovieIdIsMatchAnyMovie_InvalidOperationException_ShouldNotBeReturn()
    {
        var movie = new Movie(){MovieName = "TestMovieName15", ReleaseDate = new DateTime(2008,07,14), GenreId = 1, DirectorId = 1, Price = 10};
        _context.Movies.Add(movie);
        _context.SaveChanges();

        GetMovieDetailQuery query = new GetMovieDetailQuery(_context,_mapper);
        query.MovieId = movie.Id;

        FluentActions.Invoking(() => query.Handle()).Invoke();
        var result = _context.Movies.SingleOrDefault(x => x.Id == movie.Id);
        result.Should().NotBeNull();
    }
}