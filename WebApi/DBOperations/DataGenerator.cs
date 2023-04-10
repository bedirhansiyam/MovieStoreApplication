using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using(var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
        {            
            if(context.Directors.Any())
            {
                return;
            }
            else
            {
                context.Directors.AddRange(
                    new Director{Name = "Christopher", Surname = "Nolan"},
                    new Director{Name = "Mary", Surname = "Harron"},
                    new Director{Name = "Quentin", Surname = "Tarantino"},
                    new Director{Name = "David", Surname = "Fincher"});
            }
            context.SaveChanges();

            if(context.Genres.Any())
            {
                return;
            }
            else
            {
                context.Genres.AddRange(
                    new Genre{GenreName = "Drama"},
                    new Genre{GenreName = "Comedy"},
                    new Genre{GenreName = "Action"},
                    new Genre{GenreName = "Crime"});
            }
            context.SaveChanges();

            if(context.Actors.Any())
            {
                return;
            }
            else
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
            context.SaveChanges();

            if(context.Movies.Any())
            {
                return;
            }
            else
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
            context.SaveChanges();
            
            if(context.MovieActors.Any())
            {
                return;
            }
            else
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
            context.SaveChanges();   

            if(context.Customers.Any())
            {
                return;
            }         
            else
            {
                context.Customers.AddRange(
                    new Customer
                    {
                        Name = "Baltasar", 
                        Surname = "Robert", 
                        Email = "balt@mail.com",
                        Password = "123456789",
                    },
                    new Customer
                    {
                        Name = "Jennifer", 
                        Surname = "Brian", 
                        Email = "jenn@mail.com",
                        Password = "123456789",
                    },
                    new Customer
                    {
                        Name = "Isabeau", 
                        Surname = "Oluf", 
                        Email = "oluf@mail.com",
                        Password = "123456789",
                    });
            }
            context.SaveChanges();   
        }
    }
}