using FluentValidation;

namespace WebApi.Application.ActorOperations.Commands.CreateActor;

public class CreateActorCommandValidator: AbstractValidator<CreateActorCommand>
{
    public CreateActorCommandValidator()
    {
        RuleFor(command => command.Model.Name).MinimumLength(2);
        RuleFor(command => command.Model.Surname).MinimumLength(2);
    }
}