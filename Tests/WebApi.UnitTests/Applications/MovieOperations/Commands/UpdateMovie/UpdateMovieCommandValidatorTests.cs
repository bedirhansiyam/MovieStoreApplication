using FluentAssertions;
using TestSetup;
using WebApi.Application.MovieOperations.Commands.UpdateMovie;
using static WebApi.Application.MovieOperations.Commands.UpdateMovie.UpdateMovieCommand;

namespace Applications.MovieOperations.Commands.UpdateMovie;

public class UpdateMovieCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0,"Name",0,0,0)]
    [InlineData(0,"Name",1,1,100)]
    [InlineData(1,"Name",0,-1,10)]
    [InlineData(1,"N",1,0,10)]
    [InlineData(1,"Name",-1,1,0)]
    [InlineData(1,"",1,0,-10)]
    public void WhenInvalidInputsAreGiven_UpdateMovieValidator_ShouldBeReturnErrors(int movieId, string movieName, int genreId, int directorId, decimal price)
    {
        UpdateMovieCommand command = new UpdateMovieCommand(null, null);
        command.MovieId = movieId;  
        command.Model = new UpdateMovieModel(){
            MovieName = movieName,
            GenreId = genreId,
            DirectorId = directorId,
            Price = price
        };

        UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_UpdateBookValidator_ShouldBeReturnError()
    {
        UpdateMovieCommand command = new UpdateMovieCommand(null, null);
        command.Model = new UpdateMovieModel(){
            MovieName = "TestMovieName9",
            GenreId = 1,
            DirectorId = 1,
            Price = 120,
            ActorIds = new List<int>(){1,3},
            ReleaseDate = DateTime.Now.Date
        };

        UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenActorIdsAreGivenEmpty_UpdateMovieValidator_ShouldBeReturnError()
    {
       UpdateMovieCommand command = new UpdateMovieCommand(null, null);
       command.MovieId = 1;
       UpdateMovieModel model = new UpdateMovieModel(){
            MovieName = "TestMovieName10",
            GenreId = 1,
            DirectorId = 1,
            Price = 120,
            ReleaseDate = DateTime.Now.Date.AddYears(-5)
        }; 
        model.ActorIds = new List<int>(){};
        command.Model = model;

        UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_UpdateMovieValidator_ShouldNotBeReturnError()
    {
        UpdateMovieCommand command = new UpdateMovieCommand(null, null);
        command.MovieId = 1;
        UpdateMovieModel model = new UpdateMovieModel(){
             MovieName = "TestMovieName11",
             GenreId = 2,
             DirectorId = 1,
             Price = 342,
             ReleaseDate = DateTime.Now.Date.AddYears(-15)
         }; 
         model.ActorIds = new List<int>(){1,3};
         command.Model = model;

         UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
         var result = validator.Validate(command);

         result.Errors.Count.Should().Be(0);
    }
}