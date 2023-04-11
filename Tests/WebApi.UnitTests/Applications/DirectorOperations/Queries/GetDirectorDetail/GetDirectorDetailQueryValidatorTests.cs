using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;

namespace Applications.DirectorOperations.Queries.GetDirectorDetail;

public class GetDirectorDetailQueryValidatorTest: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidDirectorIdIsGiven_Validator_ShouldReturnErrors()
    {
        GetDirectorDetailQuery query = new GetDirectorDetailQuery(null, null);
        query.directorId = -1;

        GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count().Should().BeGreaterThan(0); 
    }

    [Fact]
    public void WhenValidDirectorIdIsGiven_Validator_ShouldNotReturnError()
    {        
        GetDirectorDetailQuery query = new GetDirectorDetailQuery(null, null);
        query.directorId = 1;

        GetDirectorDetailQueryValidator validator = new GetDirectorDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count().Should().Be(0);
    }
}