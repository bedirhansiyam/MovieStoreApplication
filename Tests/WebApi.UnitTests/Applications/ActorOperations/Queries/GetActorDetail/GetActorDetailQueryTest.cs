using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.ActorOperations.Queries.GetActorDetail;

public class GetActorDetailQueryTest: IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetActorDetailQueryTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenActorIdIsNotMatchAnyActor_InvalidOperationException_ShouldBeReturn()
    {
        var actor = new Actor(){Name = "TestName6", Surname = "TestSurname6"};
        _context.Actors.Add(actor);
        _context.SaveChanges();

        GetActorDetailQuery query = new GetActorDetailQuery(_context,_mapper);
        query.actorId = 100;

        FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The actor doesn't exist");
    }

    [Fact]
    public void WhenGivenActorIdIsMatchAnyActor_InvalidOperationException_ShouldNotBeReturn()
    {
        var actor = new Actor(){Name = "TestName7", Surname = "TestSurname7"};
        _context.Actors.Add(actor);
        _context.SaveChanges();

        GetActorDetailQuery query = new GetActorDetailQuery(_context,_mapper);
        query.actorId = actor.Id;

        FluentActions.Invoking(() => query.Handle()).Invoke();
        var result = _context.Actors.SingleOrDefault(x => x.Id == actor.Id);
        result.Should().NotBeNull();
    }
}