using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup;

public static class Actors
{
    public static void AddActors(this MovieStoreDbContext context)
    {
        context.Actors.AddRange(
            new Actor{Name = "Heath", Surname = "Ledger"},
            new Actor{Name = "Christian", Surname = "Bale"}, 
            new Actor{Name = "Brad", Surname = "Pitt"}, 
            new Actor{Name = "Morgan", Surname = "Freeman"},
            new Actor{Name = "Uma", Surname = "Thurman"},
            new Actor{Name = "John", Surname = "Travolta"},
            new Actor{Name = "Quentin", Surname = "Tarantino"});
    }
}