using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.DirectorOperations.Queries.GetDirectorDetail;

public class GetDirectorDetailQueryTest: IClassFixture<CommonTestFixture>
{
    private readonly MovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetDirectorDetailQueryTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenDirectorIdIsNotMatchAnyDirector_InvalidOperationException_ShouldBeReturn()
    {
        var director = new Director(){Name = "TestName11", Surname = "TestSurname11"};
        _context.Directors.Add(director);
        _context.SaveChanges();

        GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context,_mapper);
        query.directorId = 100;

        FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The director doesn't exist.");
    }

    [Fact]   
    public void WhenGivenDirectorIdIsMatchAnyDirector_InvalidOperationException_ShouldNotBeReturn()
    {
        var director = new Director(){Name = "TestName12", Surname = "TestSurname12"};
        _context.Directors.Add(director);
        _context.SaveChanges();

        GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context,_mapper);
        query.directorId = director.Id;

        FluentActions.Invoking(() => query.Handle()).Invoke();

        var result = _context.Directors.SingleOrDefault(x => x.Id == director.Id);
        result.Should().NotBeNull();
    }
}