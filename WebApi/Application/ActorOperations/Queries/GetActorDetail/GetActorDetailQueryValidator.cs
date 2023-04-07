namespace WebApi.Application.ActorOperations.Queries.GetActorDetail;

using FluentValidation;

public class GetActorDetailQueryValidator: AbstractValidator<GetActorDetailQuery>
{
    public GetActorDetailQueryValidator()
    {
        RuleFor(x => x.actorId).GreaterThan(0);
    }
}