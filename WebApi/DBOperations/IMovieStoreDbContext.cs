using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations;

public interface IMovieStoreDbContext
{
    DbSet<Actor> Actors { get; set; }
    DbSet<Director> Directors { get; set; }
    DbSet<Movie> Movies { get; set; }
    DbSet<Genre> Genres { get; set; }

    int SaveChanges();
}