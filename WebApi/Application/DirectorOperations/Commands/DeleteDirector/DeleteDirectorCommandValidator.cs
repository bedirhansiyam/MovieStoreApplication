namespace WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using FluentValidation;

public class DeleteDirectorCommandValidator: AbstractValidator<DeleteDirectorCommand>
{
    public DeleteDirectorCommandValidator()
    {
        RuleFor(x => x.directorId).GreaterThan(0);
    }
}