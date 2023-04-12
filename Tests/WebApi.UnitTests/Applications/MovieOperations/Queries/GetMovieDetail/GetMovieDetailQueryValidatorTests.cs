using FluentAssertions;
using TestSetup;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;

namespace Applications.MovieOperations.Queries.GetMovieDetail;

public class GetMovieDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidMovieIdIsGiven_Validator_ShouldBeReturnErrors()
    {
        GetMovieDetailQuery query = new GetMovieDetailQuery(null, null);
        query.MovieId = -1;

        GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidMovieIdIsGiven_Validator_ShouldNotBeReturnErrors()
    {
        GetMovieDetailQuery query = new GetMovieDetailQuery(null, null);
        query.MovieId = 1;

        GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}