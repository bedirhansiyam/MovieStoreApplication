using FluentValidation;

namespace WebApi.Application.CustomerOperations.Commands.DeleteCustomer;

public class DeleteCustomerCommandValidator: AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(command => command.CustomerId).GreaterThan(0);
        RuleFor(command => command.Model.Password).NotEmpty();
    }
}