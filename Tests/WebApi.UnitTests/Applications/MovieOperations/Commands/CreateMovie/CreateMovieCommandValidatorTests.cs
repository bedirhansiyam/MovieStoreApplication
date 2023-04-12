using FluentAssertions;
using TestSetup;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using static WebApi.Application.MovieOperations.Commands.CreateMovie.CreateMovieCommand;

namespace Applications.MovieOperations.Commands.CreateMovie;

public class CreateMovieCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("",0,0,0)]
    [InlineData("Name",0,0,0)]
    [InlineData("",1,0,0)]
    [InlineData("",0,1,0)]
    [InlineData("",0,0,1)]
    [InlineData("N",1,1,1)]
    [InlineData("Name",1,1,0)]
    [InlineData("Name",1,0,1)]
    [InlineData("Name",0,1,1)]
    [InlineData("",1,1,1)]
    public void WhenInvalidInputsAreGiven_CreateMovieValidator_ShouldBeReturnErrors(string movieName, int genreId, int directorId, decimal price)
    {
        CreateMovieCommand command = new CreateMovieCommand(null, null);
        command.Model = new CreateMovieModel(){MovieName = movieName, GenreId = genreId, DirectorId = directorId, Price = price};

        CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count().Should().BeGreaterThan(0);
    }
    
    [Fact]
    public void WhenDateTimeEqualNowIsGiven_CreateMovieValidator_ShouldBeReturnError()
    {
        CreateMovieCommand command = new CreateMovieCommand(null,null);
        CreateMovieModel model = new CreateMovieModel(){
            MovieName = "TestMovieName4",
            GenreId = 1,
            DirectorId = 1,
            Price = 15,
            ReleaseDate = DateTime.Now.Date
        };
        model.ActorIds = new List<int>(){1,2};
        command.Model = model;

        CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenActorIdsAreGivenEmpty_CreateMovieValidator_ShouldBeReturnError()
    {
        CreateMovieCommand command = new CreateMovieCommand(null,null);
        CreateMovieModel model = new CreateMovieModel(){
            MovieName = "TestMovieName5",
            GenreId = 1,
            DirectorId = 1,
            Price = 15,
            ReleaseDate = DateTime.Now.Date.AddYears(-2)
        };
        model.ActorIds = new List<int>(){};
        command.Model = model;

        CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_CreateMovieValidator_ShouldNotBeReturnError()
    {
        CreateMovieCommand command = new CreateMovieCommand(null,null);
        CreateMovieModel model = new CreateMovieModel(){
            MovieName = "TestMovieName6",
            GenreId = 1,
            DirectorId = 1,
            Price = 15,
            ReleaseDate = DateTime.Now.Date.AddYears(-2)
        };
        model.ActorIds = new List<int>(){1,2};
        command.Model = model;

        CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}