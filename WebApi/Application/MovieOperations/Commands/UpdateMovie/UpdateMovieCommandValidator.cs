using FluentValidation;

namespace WebApi.Application.MovieOperations.Commands.UpdateMovie;

public class UpdateMovieCommandValidator: AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(command => command.Model.MovieName).MinimumLength(2).When(x => x.Model.MovieName != string.Empty);
        RuleFor(command => command.Model.GenreId).GreaterThan(0).When(x => x.Model.GenreId != default);
        RuleFor(command => command.Model.DirectorId).GreaterThan(0).When(x => x.Model.DirectorId != default);
        RuleFor(command => command.Model.ReleaseDate).LessThan(DateTime.Now).When(x => x.Model.ReleaseDate != DateTime.Now);
        RuleFor(command => command.Model.ActorIds).NotEmpty().When(x => x.Model.ActorIds != default);
        RuleFor(command => command.Model.Price).GreaterThan(0).When(x => x.Model.Price != default);
        RuleFor(command => command.MovieId).GreaterThan(0);
    }
}