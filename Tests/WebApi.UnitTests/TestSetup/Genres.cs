using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup;

public static class Genres
{
    public static void AddGenres(this MovieStoreDbContext context)
    {
        context.Genres.AddRange(
            new Genre{GenreName = "Drama"},
            new Genre{GenreName = "Comedy"},
            new Genre{GenreName = "Action"},
            new Genre{GenreName = "Crime"});
    }
}