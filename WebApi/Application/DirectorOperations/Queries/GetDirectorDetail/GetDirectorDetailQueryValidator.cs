using FluentValidation;
using WebApi.DBOperations;

namespace WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;

public class GetDirectorDetailQueryValidator : AbstractValidator<GetDirectorDetailQuery>
{
    public IMovieStoreDbContext _dbContext { get; set; }
    public GetDirectorDetailQueryValidator()
    {
        RuleFor(command => command.directorId).GreaterThan(0);
    }
}