using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Movie
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string MovieName { get; set; }
    public DateTime ReleaseDate { get; set; }
    public Genre Genre { get; set; }    
    public int GenreId { get; set; }
    public Director Director { get; set; }
    public int DirectorId { get; set; }
    public int ActorId { get; set; }
    public virtual ICollection<MovieActor> MovieActors { get; set; }
    public decimal Price { get; set; }

}