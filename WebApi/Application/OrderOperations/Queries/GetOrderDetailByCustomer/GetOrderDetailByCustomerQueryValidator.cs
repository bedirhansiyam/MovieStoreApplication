using FluentValidation;


namespace WebApi.Application.OrderOperations.Queries.GetOrderDetailByCustomer;

public class GetOrderDetailByCustomerQueryValidator: AbstractValidator<GetOrderDetailByCustomerQuery>
{
    public GetOrderDetailByCustomerQueryValidator()
    {
        RuleFor(command => command.OrderedCustomerId).NotEmpty().GreaterThan(0);
    }
}