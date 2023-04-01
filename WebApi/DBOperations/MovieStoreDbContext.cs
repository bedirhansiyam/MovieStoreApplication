using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations;

public class MovieStoreDbContext : DbContext, IMovieStoreDbContext
{
    public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options)
    {}
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public int SaveChanges()
    {
        return base.SaveChanges();
    }
}

