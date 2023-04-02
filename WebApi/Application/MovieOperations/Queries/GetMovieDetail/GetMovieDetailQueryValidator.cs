using FluentValidation;
using WebApi.DBOperations;

namespace WebApi.Application.MovieOperations.Queries.GetMovieDetail;

public class GetMovieDetailQueryValidator : AbstractValidator<GetMovieDetailQuery>
{
    public IMovieStoreDbContext _context { get; set; }
    public GetMovieDetailQueryValidator()
    {
        RuleFor(command => command.MovieId).GreaterThan(0);
        //RuleFor(command => command.MovieId).LessThan(_context.Movies.Count());
    }
}