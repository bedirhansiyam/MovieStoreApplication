using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Commands.CreateOrder;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.OrderOperations.Commands.CreateOrder.CreateOrderCommand;

namespace Applications.OrderOperations.Commands.CreateOrder;

public class CreateOrderCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateOrderCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenMovieIdDoesNotExist_InvalidOperationException_ShouldBeReturn()
    {
        var movie = new Movie(){MovieName = "TestMovieName", ReleaseDate = new DateTime(2008,07,14), GenreId = 1, DirectorId = 1, Price = 10};
        _context.Movies.Add(movie);
        _context.SaveChanges();

        CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
        command.Model = new CreateOrderModel(){MovieId = movie.Id + 1};

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Movie not found!");
    }
}