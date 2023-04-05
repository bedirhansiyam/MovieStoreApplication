namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector;

using FluentValidation;

public class UpdateDirectorCommandValidator: AbstractValidator<UpdateDirectorCommand>
{
    public UpdateDirectorCommandValidator()
    {
        RuleFor(command => command.Model.Name).MinimumLength(2).When(x => x.Model.Name != string.Empty);
        RuleFor(command => command.Model.Surname).MinimumLength(2).When(x => x.Model.Surname != string.Empty);
    }
}