using FluentAssertions;
using TestSetup;
using WebApi.Application.MovieOperations.Commands.DeleteMovie;

namespace Applications.MovieOperations.Commands.DeleteMovie;

public class DeleteMovieCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidMovieIdIsGiven_DeleteMovieValidator_ShouldBeReturnErrors()
    {
        DeleteMovieCommand command = new DeleteMovieCommand(null);
        command.MovieId = -1;

        DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidMovieIdIsGiven_DeleteMovieValidator_ShouldNotBeReturnErrors()
    {
        DeleteMovieCommand command = new DeleteMovieCommand(null);
        command.MovieId = 1;

        DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}