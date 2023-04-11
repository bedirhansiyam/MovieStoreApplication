using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup;

public static class Movies
{
    public static void AddMovies(this MovieStoreDbContext context)
    {
        context.Movies.AddRange(
                new Movie
            {
                MovieName = "The Dark Knight",
                ReleaseDate = new DateTime(2008,07,14),
                GenreId = 3,
                DirectorId = 1,
                Price = 34.99m
            },
                new Movie
            {
                MovieName = "American Psycho",
                ReleaseDate = new DateTime(2000,01,21),
                GenreId = 4,
                DirectorId = 2,
                Price = 25.50m
            },
                new Movie
            {
                MovieName = "Se7en",
                ReleaseDate = new DateTime(1995,09,15),
                GenreId = 1,
                DirectorId = 4,
                Price = 49.99m
            },
                new Movie
            {
                MovieName = "Pulp Fiction",
                ReleaseDate = new DateTime(1994,05,21),
                GenreId = 1,
                DirectorId = 3,
                Price = 34.99m
            });         
    }
}