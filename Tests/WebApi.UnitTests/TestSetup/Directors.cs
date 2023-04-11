using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup;

public static class Directors
{
    public static void AddDirectors(this MovieStoreDbContext context)
    {
        context.Directors.AddRange(
            new Director{Name = "Christopher", Surname = "Nolan"},
            new Director{Name = "Mary", Surname = "Harron"},
            new Director{Name = "Quentin", Surname = "Tarantino"},
            new Director{Name = "David", Surname = "Fincher"});
    }
}