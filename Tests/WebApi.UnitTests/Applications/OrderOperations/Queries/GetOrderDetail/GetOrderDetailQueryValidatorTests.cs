using FluentAssertions;
using TestSetup;
using WebApi.Application.OrderOperations.Queries.GetOrderDetail;

namespace Applications.OrderOperations.Queries.GetOrderDetail;

public class GetOrderDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidOrderIdIsGiven_Validator_ShouldBeReturnErrors()
    {
        GetOrderDetailQuery query = new GetOrderDetailQuery(null, null);
        query.OrderId = -1;

        GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidOrderIdIsGiven_Validator_ShouldNotBeReturnErrors()
    {
        GetOrderDetailQuery query = new GetOrderDetailQuery(null, null);
        query.OrderId = 1;

        GetOrderDetailQueryValidator validator = new GetOrderDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}