using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;

namespace Applications.ActorOperations.Queries.GetActorDetail;

public class GetActorDetailQueryValidatorTest: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidActorIdIsGiven_Validator_ShouldBeReturnErrors()
    {
        GetActorDetailQuery query = new GetActorDetailQuery(null, null);
        query.actorId = -1;

        GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count().Should().BeGreaterThan(0);    
    }

    [Fact]
    public void WhenValidActorIdIsGiven_Validator_ShouldNotBeReturnError()
    {
        GetActorDetailQuery query = new GetActorDetailQuery(null, null);
        query.actorId = 1;

        GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count().Should().Be(0);
    }
}