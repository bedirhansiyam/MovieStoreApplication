using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup;

public static class MovieActors
{
    public static void AddMovieActors(this MovieStoreDbContext context)
    {
        context.MovieActors.AddRange(
            new MovieActor{ActorId = 1, MovieId = 1},
            new MovieActor{ActorId = 2, MovieId = 1},
            new MovieActor{ActorId = 2, MovieId = 2},
            new MovieActor{ActorId = 3, MovieId = 3},
            new MovieActor{ActorId = 4, MovieId = 3},
            new MovieActor{ActorId = 5, MovieId = 4},
            new MovieActor{ActorId = 6, MovieId = 4},
            new MovieActor{ActorId = 7, MovieId = 4});
    }
}