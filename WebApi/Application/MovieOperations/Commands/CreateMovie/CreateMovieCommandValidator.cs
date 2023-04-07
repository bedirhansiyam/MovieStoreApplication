using FluentValidation;

namespace WebApi.Application.MovieOperations.Commands.CreateMovie;

public class CreateMovieCommandValidator: AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(command => command.Model.MovieName).MinimumLength(2).NotEmpty();
        RuleFor(command => command.Model.GenreId).NotEmpty().GreaterThan(0);
        RuleFor(command => command.Model.DirectorId).NotEmpty().GreaterThan(0);
        RuleFor(command => command.Model.ReleaseDate).NotEmpty().LessThan(DateTime.Now.Date);
        RuleFor(command => command.Model.ActorIds).NotEmpty();
        RuleFor(command => command.Model.Price).NotEmpty().GreaterThan(0);
    }
}