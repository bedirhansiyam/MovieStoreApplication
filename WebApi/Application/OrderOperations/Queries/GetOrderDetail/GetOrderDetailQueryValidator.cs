using FluentValidation;

namespace WebApi.Application.OrderOperations.Queries.GetOrderDetail;


public class GetOrderDetailQueryValidator: AbstractValidator<GetOrderDetailQuery>
{
    public GetOrderDetailQueryValidator()
    {
        RuleFor(command => command.OrderId).NotEmpty().GreaterThan(0);
    }
}